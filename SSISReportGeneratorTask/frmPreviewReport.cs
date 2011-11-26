using System;
using System.Windows.Forms;

namespace SSISReportGeneratorTask100
{
    public partial class frmPreviewReport : Form
    {
        public frmPreviewReport()
        {
            InitializeComponent();
        }

        public string ServerPath { get; set; }
        public string ReportPath { get; set; }

        public void PreviewReport()
        {
            rptViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            rptViewer.ServerReport.ReportServerUrl = new Uri(ServerPath);
            rptViewer.ServerReport.ReportPath = ReportPath.Replace(@"\", @"/");
            rptViewer.ShowParameterPrompts = true;
            rptViewer.RefreshReport();
        }
    }
}
