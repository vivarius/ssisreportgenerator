using System.Linq;
using System.Net;
using Microsoft.SqlServer.Dts.Runtime;
using SSISReportGeneratorTask100.ReportExecution2005;

namespace SSISReportGeneratorTask100.ReportingHandlers
{
    public struct ReportParameter
    {
        public string Name;
        public string Value;
    }

    public class ReportTools
    {
        public static string PDF = "PDF";
        public static string EXCEL = "EXCEL";
        public static string WORD = "WORD";

        const string DeviceSettings = "<DeviceInfo><HumanReadablePDF>True</HumanReadablePDF></DeviceInfo>";

        public byte[] RenderReport(string url, string reportPath, ReportParameter[] parameters, string formatType, IDTSComponentEvents componentEvents)
        {
            return RenderReport(url, reportPath, parameters, formatType, false, componentEvents);
        }

        public byte[] RenderReport(string url, string reportPath, ReportParameter[] parameters, string formatType, bool humanReadablePdf, IDTSComponentEvents componentEvents)
        {
            bool refire = false;
            byte[] retVal;
            componentEvents.FireInformation(0, "SSISReportGeneratorTask",
                                            "Create the instance of the WS - " + string.Format("{0}ReportExecution2005.asmx", (url.Substring(url.Length - 1, 1) == "/")
                                                                                  ? url
                                                                                  : url + "/"),
                                            string.Empty, 0, ref refire);

            using (var rs = new ReportExecutionService
                                {
                                    Credentials = CredentialCache.DefaultCredentials,
                                    Url = string.Format("{0}ReportExecution2005.asmx", (url.Substring(url.Length - 1, 1) == "/") ? url : url + "/")
                                })
            {
                const string historyID = null;
                string encoding;
                string mimeType;
                string extension;
                Warning[] warnings;
                string[] streamIDs;

                var execHeader = new ExecutionHeader();


                componentEvents.FireInformation(0, "SSISReportGeneratorTask",
                                                "Load the report from " + reportPath,
                                                string.Empty, 0, ref refire);

                var execInfo = rs.LoadReport(reportPath, historyID);

                componentEvents.FireInformation(0, "SSISReportGeneratorTask",
                                                "Set parameters",
                                                string.Empty, 0, ref refire);

                rs.SetExecutionParameters(parameters.Select(p => new ParameterValue
                                                                     {
                                                                         Label = p.Name,
                                                                         Name = p.Name,
                                                                         Value = p.Value
                                                                     }).ToArray(),
                                          "en-EN");

                rs.ExecutionHeaderValue = execHeader;
                rs.ExecutionHeaderValue.ExecutionID = execInfo.ExecutionID;

                componentEvents.FireInformation(0, "SSISReportGeneratorTask",
                                                "Render the report",
                                                string.Empty, 0, ref refire);

                retVal = rs.Render(formatType,
                                 (humanReadablePdf)
                                     ? DeviceSettings
                                     : null,
                                 out extension,
                                 out encoding,
                                 out mimeType,
                                 out warnings,
                                 out streamIDs);
            }

            return retVal;
        }
    }
}