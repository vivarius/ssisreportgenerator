using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.DataTransformationServices.Controls;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Runtime.Wrapper;
using SSISReportGeneratorTask100.ReportExecution2005;
using SSISReportGeneratorTask100.ReportingHandlers;
using SSISReportGeneratorTask100.ReportService2005;
using ReportParameter = SSISReportGeneratorTask100.ReportService2005.ReportParameter;
using TaskHost = Microsoft.SqlServer.Dts.Runtime.TaskHost;
using Variable = Microsoft.SqlServer.Dts.Runtime.Variable;
using VariableDispenser = Microsoft.SqlServer.Dts.Runtime.VariableDispenser;

namespace SSISReportGeneratorTask100
{
    public partial class frmEditProperties : Form
    {
        #region Properties

        private readonly Connections _connections;
        private string _reportName;
        private readonly TaskHost _taskHost;

        private CatalogItem _catalogItem;
        private bool _isFirstLoad;
        private string _reportPath;
        private ReportingService2005 _reportServerProperties;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the variables.
        /// </summary>
        /// <value>The variables.</value>
        private Variables Variables
        {
            get { return _taskHost.Variables; }
        }

        /// <summary>
        /// Gets the connections.
        /// </summary>
        /// <value>The connections.</value>
        private Connections Connections
        {
            get { return _connections; }
        }

        /// <summary>
        /// Gets or sets the report server source.
        /// </summary>
        /// <value>The report server source.</value>
        public ReportServerProperties ReportServerSource { get; set; }

        /// <summary>
        /// Gets or sets the source node.
        /// </summary>
        /// <value>The source node.</value>
        public TreeNode SourceNode { get; set; }

        #endregion

        #region .ctor

        public frmEditProperties(TaskHost taskHost, Connections connections)
        {
            InitializeComponent();
            _isFirstLoad = true;

            Cursor.Current = Cursors.WaitCursor;

            _taskHost = taskHost;
            _connections = connections;

            if (taskHost == null)
            {
                Cursor.Current = Cursors.Default;
                throw new ArgumentNullException("taskHost");
            }

            cmbSourceVariables.Items.AddRange(LoadVariables("System.String").ToArray());

            LoadConfigFileConnections();
            LoadVariablesForReportName();

            if (_taskHost.Properties[NamedStringMembers.REPORTSERVER] == null)
            {
                Cursor.Current = Cursors.Default;
                return;
            }

            if (_taskHost.Properties[NamedStringMembers.REPORTSERVER].GetValue(_taskHost) != null)
            {
                cmbSourceVariables.Text = _taskHost.Properties[NamedStringMembers.REPORTSERVER].GetValue(_taskHost).ToString();

                string.Format("{0}/ReportService2005.asmx", _taskHost.Properties[NamedStringMembers.REPORTSERVER].GetValue(_taskHost));
                string.Format("{0}/ReportExecution2005.asmx", _taskHost.Properties[NamedStringMembers.REPORTSERVER].GetValue(_taskHost));

                try
                {
                    LoadTreeViewPanelsFromSrv();

                    if (_taskHost.Properties[NamedStringMembers.REPORTNAME].GetValue(_taskHost) != null)
                    {
                        _reportName = _taskHost.Properties[NamedStringMembers.REPORTNAME].GetValue(_taskHost).ToString();
                        _reportPath = _taskHost.Properties[NamedStringMembers.REPORTPATH].GetValue(_taskHost).ToString();

                        cmbFileType.Text = _taskHost.Properties[NamedStringMembers.OUTPUT_TYPE].GetValue(_taskHost).ToString();
                        cmbConfigurationFile.Text = _taskHost.Properties[NamedStringMembers.DESTINATION_FILE].GetValue(_taskHost).ToString();
                        cmbReportName.Text = _taskHost.Properties[NamedStringMembers.REPORTNAME_EXPRESSION].GetValue(_taskHost).ToString();

                        if (_taskHost.Properties[NamedStringMembers.CONFIGURATION_TYPE].GetValue(_taskHost).ToString() == ConfigurationType.TASK_VARIABLE)
                            optChooseVariable.Checked = true;
                        if (_taskHost.Properties[NamedStringMembers.CONFIGURATION_TYPE].GetValue(_taskHost).ToString() == ConfigurationType.FILE_CONNECTOR)
                            optChooseConfigFileConnector.Checked = true;

                        tvReportServerSource.Scrollable = true;

                        tvReportServerSource.SelectedNode = SourceNode = TreeViewHandling.FindRecursive(tvReportServerSource.Nodes[0], _reportName, _reportPath);

                        tvReportServerSource.SelectedNode.ForeColor = System.Drawing.Color.DarkRed;
                        tvReportServerSource.SelectedNode.EnsureVisible();

                        cmbFileType.SelectedText = _taskHost.Properties[NamedStringMembers.OUTPUT_FILE].GetValue(_taskHost).ToString();

                        _reportServerProperties = ((ReportServerProperties)(SourceNode.TreeView.Tag)).ReportsServerInstance;

                        tvReportServerSource.SelectedNode.EnsureVisible();

                        _reportPath = string.Format("{0}/{1}", _reportPath, _reportName);
                    }
                }
                catch
                { }
            }

            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the tree view panels from SQL Report Server.
        /// </summary>
        private void LoadTreeViewPanelsFromSrv()
        {
            try
            {
                string url = string.Format("{0}ReportService2005.asmx",
                                           EvaluateExpression(cmbSourceVariables.Text,
                                                              _taskHost.VariableDispenser));

                ReportServerSource = new ReportServerProperties(url);
                RefreshSourceTreeView(tvReportServerSource, ReportServerSource);
                ListrenderingExtensions(url);
                tvReportServerSource.ExpandAll();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads the name of the variables for report name.
        /// </summary>
        private void LoadVariablesForReportName()
        {
            cmbReportName.Items.AddRange(LoadVariables("System.String").ToArray());
        }

        /// <summary>
        /// Refreshes the source tree view.
        /// </summary>
        /// <param name="treeView">The tree view.</param>
        /// <param name="reportingService2005">The reporting service2005.</param>
        private static void RefreshSourceTreeView(TreeView treeView, ReportServerProperties reportingService2005)
        {
            treeView.BeginUpdate();
            treeView.Nodes.Clear();
            treeView.Nodes.Add(TreeViewHandling.GetFolderAsNodes(reportingService2005.ReportsServerInstance, false));
            treeView.EndUpdate();
            treeView.Tag = reportingService2005;
        }

        /// <summary>
        /// Loads the report properties.
        /// </summary>
        private void LoadReportProperties()
        {
            GetBasicInfo();
            FillParameter();
            FillDataSource();
            FillSecurity();
            FillDependentItems();

            lbSelectedReportName.Text = _reportName;
            btPreview.Enabled = true;
            lbRenderedReportName.Text = EvaluateExpression(cmbReportName.Text, _taskHost.VariableDispenser).ToString();

        }

        /// <summary>
        /// Gets the basic info.
        /// </summary>
        private void GetBasicInfo()
        {
            CatalogItem[] catalogItems = _reportServerProperties.ListChildren(SourceNode.Parent.FullPath.Replace(SourceNode.TreeView.Nodes[0].Text, string.Empty).Replace(@"\", "/"), false);
            foreach (CatalogItem catalogItem in catalogItems.Where(catalogItem => catalogItem.Name == SourceNode.Text))
            {
                _catalogItem = catalogItem;
                break;
            }

            if (_catalogItem == null)
                return;

            Text = string.Format(Text, IsNull(_catalogItem.Name));
            txtCreatedBy.Text = IsNull(_catalogItem.CreatedBy);
            txtCreatedOn.Text = IsNull(_catalogItem.CreationDate);
            txtDescription.Text = IsNull(_catalogItem.Description);
            txtID.Text = "{" + IsNull(_catalogItem.ID) + "}";
            txtModifiedBy.Text = IsNull(_catalogItem.ModifiedBy);
            txtModifyOn.Text = IsNull(_catalogItem.ModifiedDate);

            txtSize.Text = (_catalogItem.Size != null)
                               ? string.Format("{0:F2}  KB", _catalogItem.Size / 1024)
                               : "n/a";
        }

        /// <summary>
        /// get report data sources
        /// </summary>
        private void FillDataSource()
        {
            try
            {
                lvDataSource.Items.Clear();

                DataSource[] dataSources = _reportServerProperties.GetItemDataSources(_reportPath);
                foreach (DataSource dataSource in dataSources)
                {
                    var dsref = (DataSourceReference)dataSource.Item;
                    var li = new ListViewItem { Text = dataSource.Name, Tag = dataSource };
                    li.SubItems.Add(dsref.Reference);
                    li.ImageKey = "Database";
                    lvDataSource.Items.Add(li);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// get report parameters
        /// </summary>
        private void FillParameter()
        {
            Cursor = Cursors.WaitCursor;
            grdParameters.Rows.Clear();
            try
            {
                ReportParameter[] parameter = _reportServerProperties.GetReportParameters(_reportPath, null, false, null, null);
                foreach (ReportParameter reportParameter in parameter)
                {
                    int index = grdParameters.Rows.Add();

                    DataGridViewRow row = grdParameters.Rows[index];

                    row.Cells["grdColParams"] = new DataGridViewTextBoxCell { Value = reportParameter.Name };
                    row.Cells["grdColType"] = new DataGridViewTextBoxCell { Value = reportParameter.Type.ToString() };
                    row.Cells["grdColNullable"] = new DataGridViewTextBoxCell { Value = reportParameter.Nullable.ToString() };
                    row.Cells["grdColState"] = new DataGridViewTextBoxCell { Value = reportParameter.State.ToString() };
                    row.Cells["grdColDefault"] = new DataGridViewTextBoxCell
                                                     {
                                                         Value = (reportParameter.DefaultValues != null)
                                                                     ? string.Join(",", (reportParameter.DefaultValues.Where(p => p != null).Select(p => p.ToString()).ToArray()))
                                                                     : string.Empty
                                                     };

                    row.Cells["grdColVars"] = LoadComboCellVariables(reportParameter.Type.ToString());
                    row.Cells["grdColExpression"] = new DataGridViewButtonCell();
                }

                if (_isFirstLoad)
                {
                    var mappingParams = (MappingParams)_taskHost.Properties[NamedStringMembers.MAPPING_PARAMS].GetValue(_taskHost);

                    foreach (MappingParam mappingParam in mappingParams)
                    {
                        foreach (DataGridViewRow row in grdParameters.Rows.Cast<DataGridViewRow>().Where(row => row.Cells[0].Value.ToString() == mappingParam.Name))
                        {
                            row.Cells[5].Value = mappingParam.Value;
                        }
                    }
                }

                _isFirstLoad = false;
            }
            catch
            {
            }

            Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// get security information
        /// </summary>
        private void FillSecurity()
        {
            try
            {
                bool warning;
                lvSecurity.Items.Clear();

                Policy[] policies = _reportServerProperties.GetPolicies(_reportPath, out warning);

                foreach (Policy policie in policies)
                {
                    var li = new ListViewItem { Name = policie.GroupUserName, Text = policie.GroupUserName };
                    li.SubItems.Add(String.Join(",", (policie.Roles.Where(p => p != null).Select(p => p.Name).ToArray())));
                    li.ImageKey = @"User";
                    lvSecurity.Items.Add(li);
                }
            }
            catch
            {
            }
        }

        ///// <summary>
        ///// Fills the snapshots.
        ///// </summary>
        //private void FillSnapshots()
        //{
        //    lvSnapshots.Items.Clear();
        //    if (_reportServerProperties.ListReportHistory(_reportPath).Length > 0)
        //    {
        //        foreach (ReportHistorySnapshot reportHistorySnapshot in _reportServerProperties.ListReportHistory(_reportPath))
        //        {
        //            var li = new ListViewItem
        //                         {
        //                             Text = reportHistorySnapshot.CreationDate.ToLongDateString(),
        //                             Tag = reportHistorySnapshot
        //                         };

        //            li.SubItems.Add(Strings.FormatNumber((reportHistorySnapshot.Size / 1024), 2) + " KB");

        //            li.SubItems.Add(reportHistorySnapshot.HistoryID);
        //            li.ImageKey = @"Snapshot";
        //            lvSnapshots.Items.Add(li);
        //        }
        //    }
        //    else
        //    {
        //        lvSnapshots.Items.Add("No Snapshots available.");
        //    }
        //}

        /// <summary>
        /// Fills the dependent items.
        /// </summary>
        private void FillDependentItems()
        {
            lvDependentItems.Items.Clear();
            if (_reportServerProperties.ListDependentItems(_reportPath).Length > 0)
            {
                foreach (CatalogItem catalogItem in _reportServerProperties.ListDependentItems(_reportPath))
                {
                    var li = new ListViewItem { Text = catalogItem.Name, Tag = catalogItem };
                    li.SubItems.Add(catalogItem.Path);
                    li.SubItems.Add(catalogItem.Type.ToString());
                    lvDependentItems.Items.Add(li);
                }
            }
            else
            {
                lvDependentItems.Items.Add("No Dependent Items.");
            }
        }

        /// <summary>
        /// Determines whether the specified value is null.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static string IsNull(Object value)
        {
            string returnValue;
            try
            {
                if (!String.IsNullOrEmpty((string)value))
                {
                    returnValue = (string)value;
                }
                else
                {
                    returnValue = "n/a";
                }
            }
            catch
            {
                returnValue = "n/a";
            }

            return returnValue;
        }

        /// <summary>
        /// Loads the variables.
        /// </summary>
        /// <param name="parameterInfo">The parameter info.</param>
        /// <returns></returns>
        private List<string> LoadVariables(string parameterInfo)
        {
            return
                Variables.Cast<Variable>().Where(
                    variable => Type.GetTypeCode(Type.GetType(parameterInfo)) == variable.DataType).Select(
                        variable => string.Format("@[{0}::{1}]", variable.Namespace, variable.Name)).ToList();
        }

        /// <summary>
        /// Loads the combo cell variables.
        /// </summary>
        /// <param name="parameterInfo">The parameter info.</param>
        /// <returns></returns>
        private DataGridViewComboBoxCell LoadComboCellVariables(string parameterInfo)
        {
            var comboBoxCell = new DataGridViewComboBoxCell();

            foreach (
                Variable variable in
                    Variables.Cast<Variable>().Where(
                        variable =>
                        variable.DataType == TypeCode.Object ||
                        Type.GetTypeCode(Type.GetType(string.Format("System.{0}", parameterInfo))) == variable.DataType)
                )
            {
                comboBoxCell.Items.Add(string.Format("@[{0}::{1}]", variable.Namespace, variable.Name));
            }

            return comboBoxCell;
        }

        /// <summary>
        /// Loads the config file connections.
        /// </summary>
        private void LoadConfigFileConnections()
        {
            cmbConfigurationFile.Items.Clear();
            foreach (ConnectionManager connection in Connections)
            {
                cmbConfigurationFile.Items.Add(connection.Name);
            }
        }

        /// <summary>
        /// This method evaluate expressions like @([System::TaskName] + [System::TaskID]) or any other operation created using
        /// ExpressionBuilder
        /// </summary>
        /// <param name="mappedParam">The mapped param.</param>
        /// <param name="variableDispenser">The variable dispenser.</param>
        /// <returns></returns>
        private static object EvaluateExpression(string mappedParam, VariableDispenser variableDispenser)
        {
            object variableObject = null;

            try
            {
                if (mappedParam.Contains("@"))
                {
                    new ExpressionEvaluatorClass
                    {
                        Expression = mappedParam
                    }.Evaluate(DtsConvert.GetExtendedInterface(variableDispenser),
                               out variableObject,
                               false);
                }
                else
                {
                    variableObject = mappedParam;
                }
            }
            catch (Exception)
            {
                variableObject = string.Empty;
            }

            return variableObject;
        }

        private void ListrenderingExtensions(string URL)
        {
            var rs = new ReportExecutionService
                                            {
                                                Url = URL,
                                                Credentials = System.Net.CredentialCache.DefaultCredentials
                                            };

            try
            {
                ReportExecution2005.Extension[] extensions = rs.ListRenderingExtensions();

                if (extensions != null)
                {
                    cmbFileType.Items.Clear();
                    cmbFileType.Items.AddRange(extensions);
                }
            }
            catch { }
        }

        #endregion

        #region Events

        private void btLoad_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            LoadTreeViewPanelsFromSrv();
            Cursor.Current = Cursors.Default;
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if (cmbFileType.SelectedItem.ToString().Trim() == string.Empty)
            {
                MessageBox.Show("Please choose exported file type");
                cmbFileType.Focus();
                return;
            }

            if (cmbConfigurationFile.SelectedItem == null && string.IsNullOrEmpty(cmbConfigurationFile.Text))
            {
                MessageBox.Show("Please choose the destination of the exported file");
                cmbConfigurationFile.Focus();
                return;
            }
            
            //Save the values
            _taskHost.Properties[NamedStringMembers.REPORTSERVER].SetValue(_taskHost, cmbSourceVariables.Text);
            _taskHost.Properties[NamedStringMembers.REPORTNAME].SetValue(_taskHost, tvReportServerSource.SelectedNode.Text);
            _taskHost.Properties[NamedStringMembers.REPORTNAME_EXPRESSION].SetValue(_taskHost, cmbReportName.Text);
            _taskHost.Properties[NamedStringMembers.REPORTPATH].SetValue(_taskHost, tvReportServerSource.SelectedNode.Parent.FullPath.Replace(tvReportServerSource.Nodes[0].Text, string.Empty).Replace(@"\", "/"));
            _taskHost.Properties[NamedStringMembers.OUTPUT_TYPE].SetValue(_taskHost, cmbFileType.SelectedItem);
            _taskHost.Properties[NamedStringMembers.DESTINATION_FILE].SetValue(_taskHost, cmbConfigurationFile.Text);
            _taskHost.Properties[NamedStringMembers.CONFIGURATION_TYPE].SetValue(_taskHost, optChooseVariable.Checked
                                                                                                ? ConfigurationType.TASK_VARIABLE
                                                                                                : ConfigurationType.FILE_CONNECTOR);

            var mappingParams = new MappingParams();
            mappingParams.AddRange(from DataGridViewRow row in grdParameters.Rows
                                   select new MappingParam
                                              {
                                                  Name = row.Cells[0].Value.ToString(),
                                                  Type = row.Cells[1].Value.ToString(),
                                                  Value = row.Cells[5].Value.ToString()
                                              });

            _taskHost.Properties[NamedStringMembers.MAPPING_PARAMS].SetValue(_taskHost, mappingParams);
            Close();
        }

        private void optChooseConfigFileConnector_CheckedChanged(object sender, EventArgs e)
        {
            btConfigFileExpression.Enabled = optChooseVariable.Checked;
            cmbConfigurationFile.Items.Clear();
            LoadConfigFileConnections();
        }

        private void optChooseVariable_CheckedChanged(object sender, EventArgs e)
        {
            btConfigFileExpression.Enabled = optChooseVariable.Checked;
            cmbConfigurationFile.Items.Clear();
            cmbConfigurationFile.Items.AddRange(LoadVariables("System.String").ToArray());
        }

        private void btConfigFileExpression_Click(object sender, EventArgs e)
        {
            if (optChooseVariable.Checked)
                using (ExpressionBuilder expressionBuilder = ExpressionBuilder.Instantiate(_taskHost.Variables,
                                                                                        _taskHost.VariableDispenser,
                                                                                        Type.GetType("String"),
                                                                                        cmbConfigurationFile.Text))
                {
                    if (expressionBuilder.ShowDialog() == DialogResult.OK)
                    {
                        cmbConfigurationFile.Text = expressionBuilder.Expression;
                    }
                }
        }

        private void btPreview_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            using (var frmPreviewReport = new frmPreviewReport
            {
                ServerPath = (string)EvaluateExpression(cmbSourceVariables.Text.Trim(), _taskHost.VariableDispenser),
                ReportPath = tvReportServerSource.SelectedNode.Parent.FullPath.Replace(SourceNode.TreeView.Nodes[0].Text, string.Empty).Replace(@"\", "/") + "/" + tvReportServerSource.SelectedNode.Text
            })
            {
                frmPreviewReport.PreviewReport();
                Cursor.Current = Cursors.Default;
                frmPreviewReport.ShowDialog();
            }
        }

        private void tvReportServerSource_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvReportServerSource.SelectedNode == null)
                return;

            if ((ItemTypeEnum)tvReportServerSource.SelectedNode.Tag != ItemTypeEnum.Report)
                return;

            SourceNode = tvReportServerSource.SelectedNode;
            _reportServerProperties = ((ReportServerProperties)(SourceNode.TreeView.Tag)).ReportsServerInstance;
            _reportPath = SourceNode.FullPath.Replace(SourceNode.TreeView.Nodes[0].Text, string.Empty).Replace(@"\", "/");
            _reportName = tvReportServerSource.SelectedNode.Text;

            LoadReportProperties();
        }

        private void grdParameters_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 6:
                    {
                        using (ExpressionBuilder expressionBuilder = ExpressionBuilder.Instantiate(_taskHost.Variables,
                                                                                                _taskHost.VariableDispenser,
                                                                                                Type.GetType((grdParameters.Rows[e.RowIndex].Cells[1]).Value.ToString().Trim()),
                                                                                                string.Empty))
                        {
                            if (expressionBuilder.ShowDialog() == DialogResult.OK)
                            {
                                ((DataGridViewComboBoxCell)grdParameters.Rows[e.RowIndex].Cells[e.ColumnIndex - 1]).Items.Add(expressionBuilder.Expression);
                                grdParameters.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value = expressionBuilder.Expression;
                            }
                        }
                    }

                    break;
            }
        }

        private void btReportName_Click(object sender, EventArgs e)
        {
            using (ExpressionBuilder expressionBuilder = ExpressionBuilder.Instantiate(_taskHost.Variables,
                                                                                    _taskHost.VariableDispenser,
                                                                                    Type.GetType("String"),
                                                                                    cmbReportName.Text))
            {
                if (expressionBuilder.ShowDialog() == DialogResult.OK)
                {
                    cmbReportName.Text = expressionBuilder.Expression;
                    lbRenderedReportName.Text = EvaluateExpression(cmbReportName.Text, _taskHost.VariableDispenser).ToString();
                }
            }
        }

        #endregion
    }
}