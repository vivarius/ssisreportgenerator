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
        "Version=1.2.0.0," +
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
                    reportName = EvaluateExpression(ReportNameFromExpression, variableDispenser).ToString();
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
                                                               Value = EvaluateExpression(mappingParams.Value, variableDispenser).ToString()
                                                           };

                    componentEvents.FireInformation(0, "SSISReportGeneratorTask",
                                                        string.Format("Param. {0} - {1} with value: {2}",
                                                                      mappingParams.Name,
                                                                      mappingParams.Value,
                                                                      EvaluateExpression(mappingParams.Value, variableDispenser)),
                                                        string.Empty, 0, ref refire);
                }

                componentEvents.FireInformation(0, "SSISReportGeneratorTask",
                                                    string.Format("Start to render the report for {0}{1} OutPutType is {2}",
                                                                  EvaluateExpression(ReportServer, variableDispenser),
                                                                  ReportPath + "/" + reportName,
                                                                  OutPutType),
                                                    string.Empty, 0, ref refire);

                byte[] reportSource = reportTools.RenderReport(EvaluateExpression(ReportServer, variableDispenser).ToString(),
                                                               ReportPath + "/" + reportName,
                                                               reportParameters,
                                                               OutPutType,
                                                               componentEvents);

                var targetFile = GetTargetFile(variableDispenser, connections);

                componentEvents.FireInformation(0, "SSISReportGeneratorTask",
                                                string.Format("Copy the result to {0}",
                                                              targetFile),
                                                string.Empty, 0, ref refire);

                if (File.Exists(targetFile))
                    File.Delete(targetFile);

                File.WriteAllBytes(targetFile, reportSource);

                componentEvents.FireInformation(0, "SSISReportGeneratorTask",
                                                string.Format("The file was generated successfully to {0}",
                                                              targetFile),
                                                string.Empty, 0, ref refire);

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
                        : EvaluateExpression(DestinationFile, variableDispenser).ToString();
        }

        /// <summary>
        /// This method evaluate expressions like @([System::TaskName] + [System::TaskID]) or any other operation created using 
        /// ExpressionBuilder
        /// </summary>
        /// <param name="mappedParam"></param>
        /// <param name="variableDispenser"></param>
        /// <returns></returns>
        private static object EvaluateExpression(string mappedParam, VariableDispenser variableDispenser)
        {
            object variableObject = null;

            var expressionEvaluatorClass = new ExpressionEvaluatorClass
            {
                Expression = mappedParam
            };

            expressionEvaluatorClass.Evaluate(DtsConvert.GetExtendedInterface(variableDispenser), out variableObject, false);
            return variableObject;
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

            XmlAttribute reportServer = doc.CreateAttribute(string.Empty, NamedStringMembers.REPORTSERVER, string.Empty);
            reportServer.Value = ReportServer;

            XmlAttribute reportPath = doc.CreateAttribute(string.Empty, NamedStringMembers.REPORTPATH, string.Empty);
            reportPath.Value = ReportPath;

            XmlAttribute reportName = doc.CreateAttribute(string.Empty, NamedStringMembers.REPORTNAME, string.Empty);
            reportName.Value = ReportName;

            XmlAttribute reportNameFromExpression = doc.CreateAttribute(string.Empty, NamedStringMembers.REPORTNAME_EXPRESSION, string.Empty);
            reportNameFromExpression.Value = ReportNameFromExpression;

            XmlAttribute mappingParams = doc.CreateAttribute(string.Empty, NamedStringMembers.MAPPING_PARAMS, string.Empty);
            mappingParams.Value = Serializer.SerializeToXmlString(MappingParams);

            XmlAttribute outPutType = doc.CreateAttribute(string.Empty, NamedStringMembers.OUTPUT_TYPE, string.Empty);
            outPutType.Value = OutPutType;

            XmlAttribute fileSourceType = doc.CreateAttribute(string.Empty, NamedStringMembers.CONFIGURATION_TYPE, string.Empty);
            fileSourceType.Value = FileSourceType;

            XmlAttribute destinationFile = doc.CreateAttribute(string.Empty, NamedStringMembers.DESTINATION_FILE, string.Empty);
            destinationFile.Value = DestinationFile;

            taskElement.Attributes.Append(reportServer);
            taskElement.Attributes.Append(reportPath);
            taskElement.Attributes.Append(reportName);
            taskElement.Attributes.Append(reportNameFromExpression);
            taskElement.Attributes.Append(mappingParams);
            taskElement.Attributes.Append(outPutType);
            taskElement.Attributes.Append(fileSourceType);
            taskElement.Attributes.Append(destinationFile);

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
                ReportServer = node.Attributes.GetNamedItem(NamedStringMembers.REPORTSERVER).Value;
                ReportPath = node.Attributes.GetNamedItem(NamedStringMembers.REPORTPATH).Value;
                ReportName = node.Attributes.GetNamedItem(NamedStringMembers.REPORTNAME).Value;
                ReportNameFromExpression = node.Attributes.GetNamedItem(NamedStringMembers.REPORTNAME_EXPRESSION).Value;
                MappingParams = Serializer.DeSerializeFromXmlString(typeof(MappingParams), node.Attributes.GetNamedItem(NamedStringMembers.MAPPING_PARAMS).Value);
                OutPutType = node.Attributes.GetNamedItem(NamedStringMembers.OUTPUT_TYPE).Value;
                FileSourceType = node.Attributes.GetNamedItem(NamedStringMembers.CONFIGURATION_TYPE).Value;
                DestinationFile = node.Attributes.GetNamedItem(NamedStringMembers.DESTINATION_FILE).Value;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message + " " + exception.StackTrace);
            }
        }

        #endregion
    }
}
