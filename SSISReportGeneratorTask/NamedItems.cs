using System;
using System.Collections.Generic;

namespace SSISReportGeneratorTask100
{
    internal static class NamedStringMembers
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
}

