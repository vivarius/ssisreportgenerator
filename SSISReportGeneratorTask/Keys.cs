using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Runtime.Wrapper;
using VariableDispenser = Microsoft.SqlServer.Dts.Runtime.VariableDispenser;

namespace SSISReportGeneratorTask100
{
    internal static class Keys
    {
        public const string REPORTSERVER = "ReportServer";
        public const string REPORTPATH = "ReportPath";
        public const string REPORTNAME = "ReportName";
        public const string REPORTNAME_EXPRESSION = "ReportNameFromExpression";
        public const string MAPPING_PARAMS = "MappingParams";
        public const string OUTPUT_TYPE = "OutPutType";
        public const string OUTPUT_FILE = "OutputFile";
        public const string CONFIGURATION_TYPE = "FileSourceType";
        public const string DESTINATION_FILE = "DestinationFile";

        public const string SEND_FILE_BY_EMAIL = "SendFileByEmail";
        public const string SMTP_SERVER = "SmtpServer";
        public const string RECIPIENTS = "SmtpRecipients";
        public const string FROM = "SmtpFrom";
        public const string EMAIL_SUBJECT = "EmailSubject";
        public const string EMAIL_BODY = "EmailBody";

        public const string IsSharePointIntegratedMode = "IsSharePointIntegratedMode";
        public const string SITE_NAME = "SharePointSiteName";

        public const string TRUE = "True";
        public const string FALSE = "False";

        public const string REGEX_EMAIL = @"^[a-z0-9][a-z0-9_\.-]{0,}[a-z0-9]@[a-z0-9][a-z0-9_\.-]{0,}[a-z0-9][\.][a-z0-9]{2,4}$";
    }

    internal static class ConfigurationType
    {
        public const string NO_CONFIGURATION = "No Configuration File";
        public const string FILE_CONNECTOR = "File Connector";
        public const string TASK_VARIABLE = "Variable";
    }

    [Serializable]
    public class MappingParam
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }

    [Serializable]
    public class MappingParams : List<MappingParam>
    {

    }

    public static class Tools
    {
        /// <summary>
        /// Finds the string in combo box.
        /// </summary>
        /// <param name="comboBox">The combo box.</param>
        /// <param name="searchTextItem">The search text item.</param>
        /// <param name="startIndex">The start index.</param>
        /// <returns></returns>
        public static int FindStringInComboBox(ComboBox comboBox, string searchTextItem, int startIndex)
        {
            if (startIndex >= comboBox.Items.Count)
                return -1;

            int indexPosition = comboBox.FindString(searchTextItem, startIndex);

            if (indexPosition <= startIndex)
                return -1;

            return comboBox.Items[indexPosition].ToString() == searchTextItem
                                    ? indexPosition
                                    : FindStringInComboBox(comboBox, searchTextItem, indexPosition);
        }

        public static bool SendEmail(VariableDispenser variableDispenser,
                                     Connections connections,
                                     IDTSComponentEvents componentEvents,
                                     string filePath,
                                     string from,
                                     string to,
                                     string subject,
                                     string body,
                                     string smtp)
        {
            bool retVal = false;
            bool refire = false;
            try
            {

                componentEvents.FireInformation(0, "SSISReportGeneratorTask",
                                                "Build the e-mail...",
                                                string.Empty, 0, ref refire);

                using (MailMessage mailMessage = new MailMessage
                                                     {
                                                         From = new MailAddress(EvaluateExpression(from, variableDispenser).ToString()),
                                                         Subject = EvaluateExpression(subject, variableDispenser).ToString(),
                                                         Body = EvaluateExpression(body, variableDispenser).ToString(),
                                                     })
                {
                    var strTo = EvaluateExpression(to, variableDispenser).ToString().Split(';');

                    foreach (string item in strTo)
                    {
                        mailMessage.To.Add(new MailAddress(item));
                    }

                    mailMessage.Attachments.Add(new Attachment(filePath));
                    componentEvents.FireInformation(0, "SSISReportGeneratorTask",
                                                    "File attachment added",
                                                    string.Empty, 0, ref refire);
                    try
                    {
                        componentEvents.FireInformation(0, "SSISReportGeneratorTask",
                                                        string.Format("Send e-mail using {0}", GetConnectionParameter(connections[smtp].ConnectionString, "SmtpServer")),
                                                        string.Empty, 0, ref refire);

                        SmtpClient smtpClient = new SmtpClient(GetConnectionParameter(connections[smtp].ConnectionString, "SmtpServer"))
                                                    {
                                                        EnableSsl = Convert.ToBoolean(GetConnectionParameter(connections[smtp].ConnectionString, "EnableSsl")),
                                                        //Credentials = CredentialCache.DefaultNetworkCredentials,
                                                        UseDefaultCredentials = Convert.ToBoolean(GetConnectionParameter(connections[smtp].ConnectionString, "UseWindowsAuthentication"))
                                                    };

                        componentEvents.FireInformation(0, "SSISReportGeneratorTask", "Send the e-mail", string.Empty, 0, ref refire);

                        smtpClient.Send(mailMessage);

                        componentEvents.FireInformation(0, "SSISReportGeneratorTask", "E-mail sended", string.Empty, 0, ref refire);
                    }
                    catch (Exception exception)
                    {
                        componentEvents.FireError(0, "SSISReportGeneratorTask", string.Format("Problem: {0} {1}", exception.Message, exception.StackTrace), "", 0);
                        retVal = false;
                    }
                }

                retVal = true;
            }
            catch (Exception exception)
            {
                componentEvents.FireError(0, "SSISReportGeneratorTask", string.Format("Problem: {0} {1}", exception.Message, exception.StackTrace), "", 0);
                retVal = false;
            }

            return retVal;
        }

        /// <summary>
        /// This method evaluate expressions like @([System::TaskName] + [System::TaskID]) or any other operation created using
        /// ExpressionBuilder
        /// </summary>
        /// <param name="mappedParam">The mapped param.</param>
        /// <param name="variableDispenser">The variable dispenser.</param>
        /// <returns></returns>
        public static object EvaluateExpression(string mappedParam, VariableDispenser variableDispenser)
        {
            Regex regex = new Regex(Keys.REGEX_EMAIL, RegexOptions.IgnoreCase);

            if (regex.IsMatch(mappedParam))
                return mappedParam;

            object variableObject = null;

            try
            {
                var expressionEvaluatorClass = new ExpressionEvaluatorClass
                {
                    Expression = mappedParam
                };

                expressionEvaluatorClass.Evaluate(DtsConvert.GetExtendedInterface(variableDispenser), out variableObject, false);
            }
            catch (Exception) // for hardcoded values
            {
                variableObject = mappedParam;
            }

            return variableObject;
        }

        public static string GetConnectionParameter(string connectionString, string parameter)
        {
            string result = string.Empty;
            parameter += "=";

            int startOf = connectionString.IndexOf(parameter);

            if (startOf != -1)
            {
                startOf += parameter.Length;
                int endOf = connectionString.IndexOf(";", startOf);
                result = connectionString.Substring(startOf, endOf - startOf);
            }

            return result;
        }
    }
}

