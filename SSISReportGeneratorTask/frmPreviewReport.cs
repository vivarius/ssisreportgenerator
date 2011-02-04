using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            rptViewer.ServerReport.ReportServerUrl = new Uri(ServerPath); //new Uri(string.Format("http://{0}{1}{2}", completeURI.Host, completeURI.Segments[0], completeURI.Segments[1]));
            rptViewer.ServerReport.ReportPath = ReportPath.Replace(@"\", @"/");
            rptViewer.ShowParameterPrompts = true;
            rptViewer.RefreshReport();
        }
    }
}
