using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Runtime.Wrapper;
using SSISReportGeneratorTask100.ReportingHandlers;
using DTSExecResult = Microsoft.SqlServer.Dts.Runtime.DTSExecResult;
using DTSProductLevel = Microsoft.SqlServer.Dts.Runtime.DTSProductLevel;
using VariableDispenser = Microsoft.SqlServer.Dts.Runtime.VariableDispenser;

namespace SSISReportGeneratorTask100.SSIS
{
    [DtsTask(
        DisplayName = "SSRS Report Generator Task",
        UITypeName = "SSISReportGeneratorTask100.SSISReportGeneratorTaskUIInterface" +
        ",SSISReportGeneratorTask100," +
        "Version=1.3.0.14," +
        "Culture=Neutral," +
        "PublicKeyToken=baf53b3fe9523f48",
        TaskContact = "cosmin.vlasiu@gmail.com",
        RequiredProductLevel = DTSProductLevel.None
        )]
    public class SSISReportGeneratorTask : Task, IDTSComponentPersist
    {
        #region Constructor

        public SSISReportGeneratorTask()
        {
        }

        #endregion

        #region Public Properties
        [Category("Report Generator"), Description("ReportServer")]
        public string ReportServer { get; set; }
        [Category("Report Generator"), Description("ReportPath")]
        public string ReportPath { get; set; }
        [Category("Report Generator"), Description("ReportName")]
        public string ReportName { get; set; }
        [Category("Report Generator"), Description("Report Name From Expression")]
        public string ReportNameFromExpression { get; set; }
        [Category("Report Generator"), Description("MappingParams")]
        public object MappingParams { get; set; }
        [Category("Report Generator"), Description("OutPutType")]
        public string OutPutType { get; set; }
        [Category("Report Generator"), Description("FileSourceType")]
        public string FileSourceType { get; set; }
        [Category("Report Generator"), Description("DestinationFile")]
        public string DestinationFile { get; set; }
        [Category("Report Generator"), Description("SendFileByEmail")]
        public string SendFileByEmail { get; set; }
        [Category("Report Generator"), Description("SmtpServer")]
        public string SmtpServer { get; set; }
        [Category("Report Generator"), Description("SmtpRecipients")]
        public string SmtpRecipients { get; set; }
        [Category("Report Generator"), Description("From")]
        public string SmtpFrom { get; set; }
        [Category("Report Generator"), Description("EmailSubject")]
        public string EmailSubject { get; set; }
        [Category("Report Generator"), Description("EmailBody")]
        public string EmailBody { get; set; }
        #endregion

        #region Private Properties

        Variables _vars = null;

        #endregion

        #region Validate

        /// <summary>
        /// Validate local parameters
        /// </summary>
        public override DTSExecResult Validate(Connections connections, VariableDispenser variableDispenser,
                                               IDTSComponentEvents componentEvents, IDTSLogging log)
        {
            bool isBaseValid = true;


            if (base.Validate(connections, variableDispenser, componentEvents, log) != DTSExecResult.Success)
            {
                componentEvents.FireError(0, "SSISReportGeneratorTask", "Base validation failed", "", 0);
                isBaseValid = false;
            }

            if (string.IsNullOrEmpty(ReportServer))
            {
                componentEvents.FireError(0, "SSISReportGeneratorTask", "Please specify a Report Server URL.", "", 0);
                isBaseValid = false;
            }

            if (string.IsNullOrEmpty(ReportPath))
            {
                componentEvents.FireError(0, "SSISReportGeneratorTask", "Please specify a path to your report.", "", 0);
                isBaseValid = false;
            }

            if (string.IsNullOrEmpty(ReportName))
            {
                componentEvents.FireError(0, "SSISReportGeneratorTask", "Please specify the report name.", "", 0);
                isBaseValid = false;
            }

            if (string.IsNullOrEmpty(OutPutType))
            {
                componentEvents.FireError(0, "SSISReportGeneratorTask", "Please specify the output file type.", "", 0);
                isBaseValid = false;
            }

            if (string.IsNullOrEmpty(FileSourceType))
            {
                componentEvents.FireError(0, "SSISReportGeneratorTask", "You didn't specify the output type version : by File Connector or by Variable/Expression.", "", 0);
                isBaseValid = false;
            }

            if (string.IsNullOrEmpty(DestinationFile))
            {
                componentEvents.FireError(0, "SSISReportGeneratorTask", "Please specify a destination file path of the generated report.", "", 0);
                isBaseValid = false;
            }

            if (SendFileByEmail == Keys.TRUE && (string.IsNullOrEmpty(SmtpFrom) || string.IsNullOrEmpty(SmtpRecipients)))
            {
                componentEvents.FireError(0, "SSISReportGeneratorTask", "Please specify the minimal elements to send the email: the Sender and the Recipient(s) addresses.", "", 0);
                isBaseValid = false;
            }

            return isBaseValid ? DTSExecResult.Success : DTSExecResult.Failure;
        }

        #endregion

        #region Execute

        /// <summary>
        /// This method is a run-time method executed dtsexec.exe
        /// </summary>
        /// <param name="connections"></param>
        /// <param name="variableDispenser"></param>
        /// <param name="componentEvents"></param>
        /// <param name="log"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public override DTSExecResult Execute(Connections connections, VariableDispenser variableDispenser, IDTSComponentEvents componentEvents, IDTSLogging log, object transaction)
        {
            bool refire = false;
            componentEvents.FireInformation(0, "SSISReportGeneratorTask", "Get Needed Variables", string.Empty, 0, ref refire);

            GetNeededVariables(variableDispenser, componentEvents);

            try
            {

                string reportName = ReportName;

                if (!string.IsNullOrEmpty(ReportNameFromExpression))
                {
                    reportName = Tools.EvaluateExpression(ReportNameFromExpression, variableDispenser).ToString();
                }


                var reportTools = new ReportTools();

                componentEvents.FireInformation(0, "SSISReportGeneratorTask", "Get Report's parameters", string.Empty, 0, ref refire);
                var reportParameters = new ReportParameter[((MappingParams)MappingParams).Count];

                int paramCounter = 0;

                foreach (var mappingParams in (MappingParams)MappingParams)
                {
                    reportParameters[paramCounter++] = new ReportParameter
                                                           {
                                                               Name = mappingParams.Name,
                                                               Value = Tools.EvaluateExpression(mappingParams.Value, variableDispenser).ToString()
                                                           };

                    componentEvents.FireInformation(0, "SSISReportGeneratorTask",
                                                        string.Format("Param. {0} - {1} with value: {2}",
                                                                      mappingParams.Name,
                                                                      mappingParams.Value,
                                                                      Tools.EvaluateExpression(mappingParams.Value, variableDispenser)),
                                                        string.Empty, 0, ref refire);
                }

                componentEvents.FireInformation(0, "SSISReportGeneratorTask",
                                                    string.Format("Start to render the report for {0}{1} OutPutType is {2}",
                                                                  Tools.EvaluateExpression(ReportServer, variableDispenser),
                                                                  ReportPath + "/" + reportName,
                                                                  OutPutType),
                                                    string.Empty, 0, ref refire);

                byte[] reportSource = reportTools.RenderReport(Tools.EvaluateExpression(ReportServer, variableDispenser).ToString(),
                                                               ReportPath + "/" + reportName,
                                                               reportParameters,
                                                               OutPutType,
                                                               componentEvents);

                var targetFile = GetTargetFile(variableDispenser, connections);

                componentEvents.FireInformation(0, "SSISReportGeneratorTask",
                                                string.Format("Copy the result to {0}", targetFile),
                                                string.Empty, 0, ref refire);

                if (File.Exists(targetFile))
                    File.Delete(targetFile);

                File.WriteAllBytes(targetFile, reportSource);

                componentEvents.FireInformation(0, "SSISReportGeneratorTask",
                                string.Format("The file was generated successfully to {0}",
                                              targetFile),
                                string.Empty, 0, ref refire);

                if (SendFileByEmail == Keys.TRUE)
                {
                    componentEvents.FireInformation(0, "SSISReportGeneratorTask",
                                                    string.Format("Prepare to send the file by email from -{0}- to -{1}-",
                                                                   Tools.EvaluateExpression(SmtpFrom, variableDispenser),
                                                                   Tools.EvaluateExpression(SmtpRecipients, variableDispenser)),
                                                    string.Empty, 0, ref refire);

                    Tools.SendEmail(variableDispenser,
                                    connections,
                                    componentEvents,
                                    targetFile,
                                    SmtpFrom,
                                    SmtpRecipients,
                                    EmailSubject,
                                    EmailBody,
                                    SmtpServer);
                }

            }
            catch (Exception ex)
            {
                componentEvents.FireError(0, "SSISReportGeneratorTask", string.Format("Problem: {0} {1}", ex.Message, ex.StackTrace), "", 0);
            }
            finally
            {

                if (_vars.Locked)
                {
                    _vars.Unlock();
                }
            }

            return base.Execute(connections, variableDispenser, componentEvents, log, transaction);
        }

        #endregion

        #region Methods


        private string GetTargetFile(VariableDispenser variableDispenser, Connections connections)
        {
            return FileSourceType == ConfigurationType.FILE_CONNECTOR
                        ? connections[DestinationFile].ConnectionString
                        : Tools.EvaluateExpression(DestinationFile, variableDispenser).ToString();
        }



        /// <summary>
        /// Gets the needed variables.
        /// </summary>
        /// <param name="variableDispenser">The variable dispenser.</param>
        private void GetNeededVariables(VariableDispenser variableDispenser, IDTSComponentEvents componentEvents)
        {

            bool refire = false;
            string param = string.Empty;
            try
            {
                //Get variables for ReportServer
                param = ReportServer;

                componentEvents.FireInformation(0, "SSISReportGeneratorTask", "ReportServer = " + ReportServer, string.Empty, 0, ref refire);

                if (param.Contains("@"))
                {
                    var regexStr = param.Split('@');

                    foreach (var nexSplitedVal in regexStr.Where(val => val.Trim().Length != 0).Select(strVal => strVal.Split(new[] { "::" }, StringSplitOptions.RemoveEmptyEntries)))
                    {
                        try
                        {
                            componentEvents.FireInformation(0, "SSISReportGeneratorTask", nexSplitedVal[1].Remove(nexSplitedVal[1].IndexOf(']')), string.Empty, 0, ref refire);
                            variableDispenser.LockForRead(nexSplitedVal[1].Remove(nexSplitedVal[1].IndexOf(']')));
                        }
                        catch { }
                    }
                }
                //else
                //    variableDispenser.LockForRead(param);
            }
            catch (Exception ex)
            {
                componentEvents.FireError(0, "SSISReportGeneratorTask", string.Format("Problem ReportServer: {0} {1}", ex.Message, ex.StackTrace), "", 0);
            }

            try
            {
                //Get variables for ReportPath
                param = ReportPath;

                componentEvents.FireInformation(0, "SSISReportGeneratorTask", "ReportPath = " + ReportPath, string.Empty, 0, ref refire);

                if (param.Contains("@"))
                {
                    var regexStr = param.Split('@');

                    foreach (var nexSplitedVal in regexStr.Where(val => val.Trim().Length != 0).Select(strVal => strVal.Split(new[] { "::" }, StringSplitOptions.RemoveEmptyEntries)))
                    {
                        try
                        {
                            componentEvents.FireInformation(0, "SSISReportGeneratorTask", nexSplitedVal[1].Remove(nexSplitedVal[1].IndexOf(']')), string.Empty, 0, ref refire);
                            variableDispenser.LockForRead(nexSplitedVal[1].Remove(nexSplitedVal[1].IndexOf(']')));
                        }
                        catch { }
                    }
                }
                //else
                //    variableDispenser.LockForRead(param);
            }
            catch (Exception ex)
            {
                componentEvents.FireError(0, "SSISReportGeneratorTask", string.Format("Problem ReportPath: {0} {1}", ex.Message, ex.StackTrace), "", 0);
            }

            try
            {
                //Get variables for ReportName
                param = ReportNameFromExpression;

                componentEvents.FireInformation(0, "SSISReportGeneratorTask", "ReportName = " + ReportNameFromExpression, string.Empty, 0, ref refire);

                if (param.Contains("@"))
                {
                    var regexStr = param.Split('@');

                    foreach (var nexSplitedVal in regexStr.Where(val => val.Trim().Length != 0).Select(strVal => strVal.Split(new[] { "::" }, StringSplitOptions.RemoveEmptyEntries)))
                    {
                        try
                        {
                            componentEvents.FireInformation(0, "SSISReportGeneratorTask", nexSplitedVal[1].Remove(nexSplitedVal[1].IndexOf(']')), string.Empty, 0, ref refire);
                            variableDispenser.LockForRead(nexSplitedVal[1].Remove(nexSplitedVal[1].IndexOf(']')));
                        }
                        catch { }
                    }
                }
                //else
                //    variableDispenser.LockForRead(param);
            }
            catch (Exception ex)
            {
                componentEvents.FireError(0, "SSISReportGeneratorTask", string.Format("Problem ReportName: {0} {1}", ex.Message, ex.StackTrace), "", 0);
            }

            try
            {

                //Get variables for DestinationFile
                param = DestinationFile;

                componentEvents.FireInformation(0, "SSISReportGeneratorTask", "DestinationFile = " + DestinationFile, string.Empty, 0, ref refire);

                if (param.Contains("@"))
                {
                    var regexStr = param.Split('@');

                    foreach (var nexSplitedVal in regexStr.Where(val => val.Trim().Length != 0).Select(strVal => strVal.Split(new[] { "::" }, StringSplitOptions.RemoveEmptyEntries)))
                    {
                        try
                        {
                            componentEvents.FireInformation(0, "SSISReportGeneratorTask", nexSplitedVal[1].Remove(nexSplitedVal[1].IndexOf(']')), string.Empty, 0, ref refire);
                            variableDispenser.LockForRead(nexSplitedVal[1].Remove(nexSplitedVal[1].IndexOf(']')));
                        }
                        catch { }
                    }
                }
                //else
                //    variableDispenser.LockForRead(param);
            }
            catch (Exception ex)
            {
                componentEvents.FireError(0, "SSISReportGeneratorTask", string.Format("Problem DestinationFile: {0} {1}", ex.Message, ex.StackTrace), "", 0);
            }



            try
            {
                componentEvents.FireInformation(0, "SSISReportGeneratorTask", "MappingParams ", string.Empty, 0, ref refire);
                //Get variables for MappingParams
                foreach (var mappingParams in (MappingParams)MappingParams)
                {

                    try
                    {
                        if (mappingParams.Value.Contains("@"))
                        {
                            var regexStr = mappingParams.Value.Split('@');

                            foreach (var nexSplitedVal in
                                    regexStr.Where(val => val.Trim().Length != 0).Select(strVal => strVal.Split(new[] { "::" }, StringSplitOptions.RemoveEmptyEntries)))
                            {
                                try
                                {
                                    componentEvents.FireInformation(0, "SSISReportGeneratorTask", nexSplitedVal[1].Remove(nexSplitedVal[1].IndexOf(']')), string.Empty, 0, ref refire);
                                    variableDispenser.LockForRead(nexSplitedVal[1].Remove(nexSplitedVal[1].IndexOf(']')));
                                }
                                catch { }
                            }
                        }
                    }
                    catch
                    {
                        //oops, it's a fix value
                    }

                }
            }
            catch (Exception ex)
            {
                componentEvents.FireError(0, "SSISReportGeneratorTask", string.Format("Problem MappingParams: {0} {1}", ex.Message, ex.StackTrace), "", 0);
            }

            variableDispenser.GetVariables(ref _vars);
        }

        #endregion

        #region Implementation of IDTSComponentPersist

        /// <summary>
        /// Saves to XML.
        /// </summary>
        /// <param name="doc">The doc.</param>
        /// <param name="infoEvents">The info events.</param>
        void IDTSComponentPersist.SaveToXML(XmlDocument doc, IDTSInfoEvents infoEvents)
        {
            XmlElement taskElement = doc.CreateElement(string.Empty, "SSISReportGeneratorTask", string.Empty);

            XmlAttribute reportServer = doc.CreateAttribute(string.Empty, Keys.REPORTSERVER, string.Empty);
            reportServer.Value = ReportServer;

            XmlAttribute reportPath = doc.CreateAttribute(string.Empty, Keys.REPORTPATH, string.Empty);
            reportPath.Value = ReportPath;

            XmlAttribute reportName = doc.CreateAttribute(string.Empty, Keys.REPORTNAME, string.Empty);
            reportName.Value = ReportName;

            XmlAttribute reportNameFromExpression = doc.CreateAttribute(string.Empty, Keys.REPORTNAME_EXPRESSION, string.Empty);
            reportNameFromExpression.Value = ReportNameFromExpression;

            XmlAttribute mappingParams = doc.CreateAttribute(string.Empty, Keys.MAPPING_PARAMS, string.Empty);
            mappingParams.Value = Serializer.SerializeToXmlString(MappingParams);

            XmlAttribute outPutType = doc.CreateAttribute(string.Empty, Keys.OUTPUT_TYPE, string.Empty);
            outPutType.Value = OutPutType;

            XmlAttribute fileSourceType = doc.CreateAttribute(string.Empty, Keys.CONFIGURATION_TYPE, string.Empty);
            fileSourceType.Value = FileSourceType;

            XmlAttribute destinationFile = doc.CreateAttribute(string.Empty, Keys.DESTINATION_FILE, string.Empty);
            destinationFile.Value = DestinationFile;

            XmlAttribute sendFileByEmail = doc.CreateAttribute(string.Empty, Keys.SEND_FILE_BY_EMAIL, string.Empty);
            sendFileByEmail.Value = SendFileByEmail;

            XmlAttribute serverSMTP = doc.CreateAttribute(string.Empty, Keys.SMTP_SERVER, string.Empty);
            serverSMTP.Value = SmtpServer;

            XmlAttribute recipients = doc.CreateAttribute(string.Empty, Keys.RECIPIENTS, string.Empty);
            recipients.Value = SmtpRecipients;

            XmlAttribute smtpFrom = doc.CreateAttribute(string.Empty, Keys.FROM, string.Empty);
            smtpFrom.Value = SmtpFrom;

            XmlAttribute emailSubject = doc.CreateAttribute(string.Empty, Keys.EMAIL_SUBJECT, string.Empty);
            emailSubject.Value = EmailSubject;

            XmlAttribute emailBody = doc.CreateAttribute(string.Empty, Keys.EMAIL_BODY, string.Empty);
            emailBody.Value = EmailBody;

            taskElement.Attributes.Append(reportServer);
            taskElement.Attributes.Append(reportPath);
            taskElement.Attributes.Append(reportName);
            taskElement.Attributes.Append(reportNameFromExpression);
            taskElement.Attributes.Append(mappingParams);
            taskElement.Attributes.Append(outPutType);
            taskElement.Attributes.Append(fileSourceType);
            taskElement.Attributes.Append(destinationFile);

            taskElement.Attributes.Append(sendFileByEmail);
            taskElement.Attributes.Append(serverSMTP);
            taskElement.Attributes.Append(recipients);
            taskElement.Attributes.Append(smtpFrom);
            taskElement.Attributes.Append(emailSubject);
            taskElement.Attributes.Append(emailBody);

            doc.AppendChild(taskElement);
        }

        /// <summary>
        /// Loads from XML.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="infoEvents">The info events.</param>
        void IDTSComponentPersist.LoadFromXML(XmlElement node, IDTSInfoEvents infoEvents)
        {
            if (node.Name != "SSISReportGeneratorTask")
            {
                throw new Exception("Unexpected task element when loading task.");
            }

            try
            {
                ReportServer = node.Attributes.GetNamedItem(Keys.REPORTSERVER).Value;
                ReportPath = node.Attributes.GetNamedItem(Keys.REPORTPATH).Value;
                ReportName = node.Attributes.GetNamedItem(Keys.REPORTNAME).Value;
                ReportNameFromExpression = node.Attributes.GetNamedItem(Keys.REPORTNAME_EXPRESSION).Value;
                MappingParams = Serializer.DeSerializeFromXmlString(typeof(MappingParams), node.Attributes.GetNamedItem(Keys.MAPPING_PARAMS).Value);
                OutPutType = node.Attributes.GetNamedItem(Keys.OUTPUT_TYPE).Value;
                FileSourceType = node.Attributes.GetNamedItem(Keys.CONFIGURATION_TYPE).Value;
                DestinationFile = node.Attributes.GetNamedItem(Keys.DESTINATION_FILE).Value;

                SendFileByEmail = node.Attributes.GetNamedItem(Keys.SEND_FILE_BY_EMAIL).Value;
                SmtpServer = node.Attributes.GetNamedItem(Keys.SMTP_SERVER).Value;
                SmtpRecipients = node.Attributes.GetNamedItem(Keys.RECIPIENTS).Value;

                SmtpFrom = node.Attributes.GetNamedItem(Keys.FROM).Value;
                EmailSubject = node.Attributes.GetNamedItem(Keys.EMAIL_SUBJECT).Value;
                EmailBody = node.Attributes.GetNamedItem(Keys.EMAIL_BODY).Value;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message + " " + exception.StackTrace);
            }
        }

        #endregion
    }
}
