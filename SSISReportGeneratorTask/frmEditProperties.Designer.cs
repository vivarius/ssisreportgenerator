namespace SSISReportGeneratorTask100
{
    partial class frmEditProperties
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditProperties));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btOk = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.cmbSourceVariables = new System.Windows.Forms.ComboBox();
            this.btLoad = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tvReportServerSource = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbRenderedReportName = new System.Windows.Forms.Label();
            this.lbSelectedReportName = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbReportName = new System.Windows.Forms.ComboBox();
            this.btReportName = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbConfigurationFile = new System.Windows.Forms.ComboBox();
            this.optChooseVariable = new System.Windows.Forms.RadioButton();
            this.optChooseConfigFileConnector = new System.Windows.Forms.RadioButton();
            this.btConfigFileExpression = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbFileType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btPreview = new System.Windows.Forms.Button();
            this.txtID = new System.Windows.Forms.Label();
            this.label1212 = new System.Windows.Forms.Label();
            this.txtModifiedBy = new System.Windows.Forms.Label();
            this.txtModifyOn = new System.Windows.Forms.Label();
            this.txtCreatedBy = new System.Windows.Forms.Label();
            this.txtCreatedOn = new System.Windows.Forms.Label();
            this.txtSize = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.grdParameters = new System.Windows.Forms.DataGridView();
            this.grdColParams = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdColType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdColNullable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdColState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdColDefault = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdColVars = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.grdColExpression = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lvParameter = new System.Windows.Forms.ListView();
            this.cName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cNullable = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cDefaultValues = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imagesCollection = new System.Windows.Forms.ImageList(this.components);
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lvDataSource = new System.Windows.Forms.ListView();
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chReference = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.lvSecurity = new System.Windows.Forms.ListView();
            this.cUser = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cRole = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.lvDependentItems = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdParameters)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btOk
            // 
            this.btOk.Location = new System.Drawing.Point(689, 523);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 0;
            this.btOk.Text = "OK";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(608, 523);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(1, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(763, 515);
            this.splitContainer1.SplitterDistance = 254;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.cmbSourceVariables);
            this.splitContainer2.Panel1.Controls.Add(this.btLoad);
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tvReportServerSource);
            this.splitContainer2.Size = new System.Drawing.Size(254, 515);
            this.splitContainer2.SplitterDistance = 54;
            this.splitContainer2.TabIndex = 0;
            // 
            // cmbSourceVariables
            // 
            this.cmbSourceVariables.FormattingEnabled = true;
            this.cmbSourceVariables.Location = new System.Drawing.Point(3, 25);
            this.cmbSourceVariables.Name = "cmbSourceVariables";
            this.cmbSourceVariables.Size = new System.Drawing.Size(208, 21);
            this.cmbSourceVariables.TabIndex = 43;
            // 
            // btLoad
            // 
            this.btLoad.Location = new System.Drawing.Point(217, 23);
            this.btLoad.Name = "btLoad";
            this.btLoad.Size = new System.Drawing.Size(34, 23);
            this.btLoad.TabIndex = 4;
            this.btLoad.Text = "Go";
            this.btLoad.UseVisualStyleBackColor = true;
            this.btLoad.Click += new System.EventHandler(this.btLoad_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(218, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Server -> pattern: http://{server}/{instance}/";
            // 
            // tvReportServerSource
            // 
            this.tvReportServerSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvReportServerSource.ImageIndex = 0;
            this.tvReportServerSource.ImageList = this.imageList1;
            this.tvReportServerSource.Location = new System.Drawing.Point(0, 0);
            this.tvReportServerSource.Name = "tvReportServerSource";
            this.tvReportServerSource.SelectedImageIndex = 0;
            this.tvReportServerSource.Size = new System.Drawing.Size(254, 457);
            this.tvReportServerSource.TabIndex = 4;
            this.tvReportServerSource.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvReportServerSource_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder.gif");
            this.imageList1.Images.SetKeyName(1, "report.gif");
            this.imageList1.Images.SetKeyName(2, "Datasource.gif");
            this.imageList1.Images.SetKeyName(3, "database.gif");
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(505, 515);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.btPreview);
            this.tabPage1.Controls.Add(this.txtID);
            this.tabPage1.Controls.Add(this.label1212);
            this.tabPage1.Controls.Add(this.txtModifiedBy);
            this.tabPage1.Controls.Add(this.txtModifyOn);
            this.tabPage1.Controls.Add(this.txtCreatedBy);
            this.tabPage1.Controls.Add(this.txtCreatedOn);
            this.tabPage1.Controls.Add(this.txtSize);
            this.tabPage1.Controls.Add(this.txtDescription);
            this.tabPage1.Controls.Add(this.Label10);
            this.tabPage1.Controls.Add(this.Label9);
            this.tabPage1.Controls.Add(this.Label8);
            this.tabPage1.Controls.Add(this.Label7);
            this.tabPage1.Controls.Add(this.Label6);
            this.tabPage1.Controls.Add(this.Label5);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(497, 489);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Properties";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbRenderedReportName);
            this.groupBox1.Controls.Add(this.lbSelectedReportName);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.cmbReportName);
            this.groupBox1.Controls.Add(this.btReportName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbConfigurationFile);
            this.groupBox1.Controls.Add(this.optChooseVariable);
            this.groupBox1.Controls.Add(this.optChooseConfigFileConnector);
            this.groupBox1.Controls.Add(this.btConfigFileExpression);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbFileType);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(9, 257);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(479, 185);
            this.groupBox1.TabIndex = 79;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Actions";
            // 
            // lbRenderedReportName
            // 
            this.lbRenderedReportName.AutoSize = true;
            this.lbRenderedReportName.Location = new System.Drawing.Point(92, 111);
            this.lbRenderedReportName.Name = "lbRenderedReportName";
            this.lbRenderedReportName.Size = new System.Drawing.Size(0, 13);
            this.lbRenderedReportName.TabIndex = 51;
            // 
            // lbSelectedReportName
            // 
            this.lbSelectedReportName.AutoSize = true;
            this.lbSelectedReportName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSelectedReportName.ForeColor = System.Drawing.Color.Maroon;
            this.lbSelectedReportName.Location = new System.Drawing.Point(92, 155);
            this.lbSelectedReportName.Name = "lbSelectedReportName";
            this.lbSelectedReportName.Size = new System.Drawing.Size(0, 13);
            this.lbSelectedReportName.TabIndex = 50;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 155);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 13);
            this.label11.TabIndex = 49;
            this.label11.Text = "Selected report";
            // 
            // cmbReportName
            // 
            this.cmbReportName.FormattingEnabled = true;
            this.cmbReportName.Location = new System.Drawing.Point(92, 127);
            this.cmbReportName.Name = "cmbReportName";
            this.cmbReportName.Size = new System.Drawing.Size(315, 21);
            this.cmbReportName.TabIndex = 48;
            // 
            // btReportName
            // 
            this.btReportName.Location = new System.Drawing.Point(413, 126);
            this.btReportName.Name = "btReportName";
            this.btReportName.Size = new System.Drawing.Size(45, 23);
            this.btReportName.TabIndex = 47;
            this.btReportName.Text = "(fx)";
            this.btReportName.UseVisualStyleBackColor = true;
            this.btReportName.Click += new System.EventHandler(this.btReportName_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 45;
            this.label4.Text = "Report name:";
            // 
            // cmbConfigurationFile
            // 
            this.cmbConfigurationFile.FormattingEnabled = true;
            this.cmbConfigurationFile.Location = new System.Drawing.Point(92, 58);
            this.cmbConfigurationFile.Name = "cmbConfigurationFile";
            this.cmbConfigurationFile.Size = new System.Drawing.Size(315, 21);
            this.cmbConfigurationFile.TabIndex = 44;
            // 
            // optChooseVariable
            // 
            this.optChooseVariable.AutoSize = true;
            this.optChooseVariable.Location = new System.Drawing.Point(288, 84);
            this.optChooseVariable.Name = "optChooseVariable";
            this.optChooseVariable.Size = new System.Drawing.Size(173, 17);
            this.optChooseVariable.TabIndex = 43;
            this.optChooseVariable.Text = "Path from Variable / Expression";
            this.optChooseVariable.UseVisualStyleBackColor = true;
            this.optChooseVariable.CheckedChanged += new System.EventHandler(this.optChooseVariable_CheckedChanged);
            // 
            // optChooseConfigFileConnector
            // 
            this.optChooseConfigFileConnector.AutoSize = true;
            this.optChooseConfigFileConnector.Checked = true;
            this.optChooseConfigFileConnector.Location = new System.Drawing.Point(139, 85);
            this.optChooseConfigFileConnector.Name = "optChooseConfigFileConnector";
            this.optChooseConfigFileConnector.Size = new System.Drawing.Size(140, 17);
            this.optChooseConfigFileConnector.TabIndex = 42;
            this.optChooseConfigFileConnector.TabStop = true;
            this.optChooseConfigFileConnector.Text = "Path from File connector";
            this.optChooseConfigFileConnector.UseVisualStyleBackColor = true;
            this.optChooseConfigFileConnector.CheckedChanged += new System.EventHandler(this.optChooseConfigFileConnector_CheckedChanged);
            // 
            // btConfigFileExpression
            // 
            this.btConfigFileExpression.Enabled = false;
            this.btConfigFileExpression.Location = new System.Drawing.Point(413, 56);
            this.btConfigFileExpression.Name = "btConfigFileExpression";
            this.btConfigFileExpression.Size = new System.Drawing.Size(45, 23);
            this.btConfigFileExpression.TabIndex = 41;
            this.btConfigFileExpression.Text = "(fx)";
            this.btConfigFileExpression.UseVisualStyleBackColor = true;
            this.btConfigFileExpression.Click += new System.EventHandler(this.btConfigFileExpression_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "At this path:";
            // 
            // cmbFileType
            // 
            this.cmbFileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFileType.FormattingEnabled = true;
            this.cmbFileType.Items.AddRange(new object[] {
            "PDF",
            "Excel",
            "Word",
            "HTML 4.0",
            "CSV",
            "MHTML",
            "XML",
            "IMAGE"});
            this.cmbFileType.Location = new System.Drawing.Point(92, 29);
            this.cmbFileType.Name = "cmbFileType";
            this.cmbFileType.Size = new System.Drawing.Size(120, 21);
            this.cmbFileType.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Export as:";
            // 
            // btPreview
            // 
            this.btPreview.Enabled = false;
            this.btPreview.Location = new System.Drawing.Point(378, 448);
            this.btPreview.Name = "btPreview";
            this.btPreview.Size = new System.Drawing.Size(110, 26);
            this.btPreview.TabIndex = 78;
            this.btPreview.Text = "Preview";
            this.btPreview.UseVisualStyleBackColor = true;
            this.btPreview.Click += new System.EventHandler(this.btPreview_Click);
            // 
            // txtID
            // 
            this.txtID.AutoSize = true;
            this.txtID.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.Location = new System.Drawing.Point(98, 124);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(0, 13);
            this.txtID.TabIndex = 77;
            // 
            // label1212
            // 
            this.label1212.AutoSize = true;
            this.label1212.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1212.Location = new System.Drawing.Point(6, 124);
            this.label1212.Name = "label1212";
            this.label1212.Size = new System.Drawing.Size(54, 13);
            this.label1212.TabIndex = 76;
            this.label1212.Text = "Object ID:";
            // 
            // txtModifiedBy
            // 
            this.txtModifiedBy.AutoSize = true;
            this.txtModifiedBy.Location = new System.Drawing.Point(101, 102);
            this.txtModifiedBy.Name = "txtModifiedBy";
            this.txtModifiedBy.Size = new System.Drawing.Size(0, 13);
            this.txtModifiedBy.TabIndex = 75;
            // 
            // txtModifyOn
            // 
            this.txtModifyOn.AutoSize = true;
            this.txtModifyOn.Location = new System.Drawing.Point(101, 81);
            this.txtModifyOn.Name = "txtModifyOn";
            this.txtModifyOn.Size = new System.Drawing.Size(0, 13);
            this.txtModifyOn.TabIndex = 74;
            // 
            // txtCreatedBy
            // 
            this.txtCreatedBy.AutoSize = true;
            this.txtCreatedBy.Location = new System.Drawing.Point(101, 60);
            this.txtCreatedBy.Name = "txtCreatedBy";
            this.txtCreatedBy.Size = new System.Drawing.Size(0, 13);
            this.txtCreatedBy.TabIndex = 73;
            // 
            // txtCreatedOn
            // 
            this.txtCreatedOn.AutoSize = true;
            this.txtCreatedOn.Location = new System.Drawing.Point(101, 39);
            this.txtCreatedOn.Name = "txtCreatedOn";
            this.txtCreatedOn.Size = new System.Drawing.Size(0, 13);
            this.txtCreatedOn.TabIndex = 72;
            // 
            // txtSize
            // 
            this.txtSize.AutoSize = true;
            this.txtSize.Location = new System.Drawing.Point(101, 18);
            this.txtSize.Name = "txtSize";
            this.txtSize.Size = new System.Drawing.Size(0, 13);
            this.txtSize.TabIndex = 71;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(101, 146);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(387, 105);
            this.txtDescription.TabIndex = 70;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(6, 146);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(63, 13);
            this.Label10.TabIndex = 69;
            this.Label10.Text = "Description:";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(6, 102);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(64, 13);
            this.Label9.TabIndex = 68;
            this.Label9.Text = "Modified by:";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(6, 81);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(65, 13);
            this.Label8.TabIndex = 67;
            this.Label8.Text = "Modified on:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(6, 60);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(61, 13);
            this.Label7.TabIndex = 66;
            this.Label7.Text = "Created by:";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(6, 39);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(62, 13);
            this.Label6.TabIndex = 65;
            this.Label6.Text = "Created on:";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(6, 18);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(30, 13);
            this.Label5.TabIndex = 64;
            this.Label5.Text = "Size:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.grdParameters);
            this.tabPage2.Controls.Add(this.lvParameter);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(497, 489);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Parameters";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // grdParameters
            // 
            this.grdParameters.AllowUserToAddRows = false;
            this.grdParameters.AllowUserToDeleteRows = false;
            this.grdParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdParameters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.grdColParams,
            this.grdColType,
            this.grdColNullable,
            this.grdColState,
            this.grdColDefault,
            this.grdColVars,
            this.grdColExpression});
            this.grdParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdParameters.Location = new System.Drawing.Point(3, 3);
            this.grdParameters.Name = "grdParameters";
            this.grdParameters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.grdParameters.Size = new System.Drawing.Size(491, 483);
            this.grdParameters.TabIndex = 60;
            this.grdParameters.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdParameters_CellContentClick);
            // 
            // grdColParams
            // 
            this.grdColParams.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.grdColParams.Frozen = true;
            this.grdColParams.HeaderText = "Parameters";
            this.grdColParams.Name = "grdColParams";
            this.grdColParams.ReadOnly = true;
            this.grdColParams.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.grdColParams.Width = 66;
            // 
            // grdColType
            // 
            this.grdColType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.grdColType.FillWeight = 40F;
            this.grdColType.HeaderText = "Param Type";
            this.grdColType.Name = "grdColType";
            this.grdColType.ReadOnly = true;
            this.grdColType.Width = 89;
            // 
            // grdColNullable
            // 
            this.grdColNullable.HeaderText = "Nullable";
            this.grdColNullable.Name = "grdColNullable";
            this.grdColNullable.ReadOnly = true;
            // 
            // grdColState
            // 
            this.grdColState.HeaderText = "State";
            this.grdColState.Name = "grdColState";
            this.grdColState.ReadOnly = true;
            // 
            // grdColDefault
            // 
            this.grdColDefault.HeaderText = "Default";
            this.grdColDefault.Name = "grdColDefault";
            this.grdColDefault.ReadOnly = true;
            // 
            // grdColVars
            // 
            this.grdColVars.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.grdColVars.DropDownWidth = 300;
            this.grdColVars.HeaderText = "Variables";
            this.grdColVars.MaxDropDownItems = 10;
            this.grdColVars.Name = "grdColVars";
            this.grdColVars.Sorted = true;
            this.grdColVars.Width = 240;
            // 
            // grdColExpression
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2);
            this.grdColExpression.DefaultCellStyle = dataGridViewCellStyle1;
            this.grdColExpression.HeaderText = "f(x)";
            this.grdColExpression.Name = "grdColExpression";
            this.grdColExpression.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.grdColExpression.Text = "f(x)";
            this.grdColExpression.ToolTipText = "Expressions...";
            this.grdColExpression.Width = 30;
            // 
            // lvParameter
            // 
            this.lvParameter.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cName,
            this.chType,
            this.cNullable,
            this.cState,
            this.cDefaultValues});
            this.lvParameter.Location = new System.Drawing.Point(3, 3);
            this.lvParameter.Name = "lvParameter";
            this.lvParameter.Size = new System.Drawing.Size(491, 181);
            this.lvParameter.SmallImageList = this.imagesCollection;
            this.lvParameter.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvParameter.TabIndex = 59;
            this.lvParameter.UseCompatibleStateImageBehavior = false;
            this.lvParameter.View = System.Windows.Forms.View.Details;
            this.lvParameter.Visible = false;
            // 
            // cName
            // 
            this.cName.Text = "Name";
            this.cName.Width = 102;
            // 
            // chType
            // 
            this.chType.Text = "Type";
            this.chType.Width = 95;
            // 
            // cNullable
            // 
            this.cNullable.Text = "Nullable";
            this.cNullable.Width = 97;
            // 
            // cState
            // 
            this.cState.Text = "State";
            this.cState.Width = 61;
            // 
            // cDefaultValues
            // 
            this.cDefaultValues.Text = "Default Values";
            // 
            // imagesCollection
            // 
            this.imagesCollection.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagesCollection.ImageStream")));
            this.imagesCollection.TransparentColor = System.Drawing.Color.Transparent;
            this.imagesCollection.Images.SetKeyName(0, "User");
            this.imagesCollection.Images.SetKeyName(1, "DBRole");
            this.imagesCollection.Images.SetKeyName(2, "Snapshot");
            this.imagesCollection.Images.SetKeyName(3, "Database");
            this.imagesCollection.Images.SetKeyName(4, "Parameter");
            this.imagesCollection.Images.SetKeyName(5, "Property");
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lvDataSource);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(497, 489);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Data Sources";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lvDataSource
            // 
            this.lvDataSource.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chReference});
            this.lvDataSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDataSource.Location = new System.Drawing.Point(0, 0);
            this.lvDataSource.Name = "lvDataSource";
            this.lvDataSource.Size = new System.Drawing.Size(497, 489);
            this.lvDataSource.SmallImageList = this.imagesCollection;
            this.lvDataSource.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvDataSource.TabIndex = 61;
            this.lvDataSource.UseCompatibleStateImageBehavior = false;
            this.lvDataSource.View = System.Windows.Forms.View.Details;
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 169;
            // 
            // chReference
            // 
            this.chReference.Text = "Reference";
            this.chReference.Width = 259;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.lvSecurity);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(497, 489);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Security";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // lvSecurity
            // 
            this.lvSecurity.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cUser,
            this.cRole});
            this.lvSecurity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSecurity.Location = new System.Drawing.Point(0, 0);
            this.lvSecurity.Name = "lvSecurity";
            this.lvSecurity.Size = new System.Drawing.Size(497, 489);
            this.lvSecurity.SmallImageList = this.imagesCollection;
            this.lvSecurity.TabIndex = 53;
            this.lvSecurity.UseCompatibleStateImageBehavior = false;
            this.lvSecurity.View = System.Windows.Forms.View.Details;
            // 
            // cUser
            // 
            this.cUser.Text = "User";
            this.cUser.Width = 248;
            // 
            // cRole
            // 
            this.cRole.Text = "Role(s)";
            this.cRole.Width = 250;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.lvDependentItems);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(497, 489);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Dependent Items";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // lvDependentItems
            // 
            this.lvDependentItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvDependentItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDependentItems.Location = new System.Drawing.Point(0, 0);
            this.lvDependentItems.Name = "lvDependentItems";
            this.lvDependentItems.Size = new System.Drawing.Size(497, 489);
            this.lvDependentItems.SmallImageList = this.imagesCollection;
            this.lvDependentItems.TabIndex = 65;
            this.lvDependentItems.UseCompatibleStateImageBehavior = false;
            this.lvDependentItems.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Date";
            this.columnHeader1.Width = 134;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Size";
            this.columnHeader2.Width = 79;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Snapshot ID";
            this.columnHeader3.Width = 140;
            // 
            // frmEditProperties
            // 
            this.AcceptButton = this.btOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(763, 546);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOk);
            this.Name = "frmEditProperties";
            this.Text = "Report Generator";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdParameters)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        internal System.Windows.Forms.Label txtID;
        internal System.Windows.Forms.Label label1212;
        internal System.Windows.Forms.Label txtModifiedBy;
        internal System.Windows.Forms.Label txtModifyOn;
        internal System.Windows.Forms.Label txtCreatedBy;
        internal System.Windows.Forms.Label txtCreatedOn;
        internal System.Windows.Forms.Label txtSize;
        internal System.Windows.Forms.TextBox txtDescription;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.ListView lvParameter;
        internal System.Windows.Forms.ColumnHeader cName;
        internal System.Windows.Forms.ColumnHeader chType;
        internal System.Windows.Forms.ColumnHeader cNullable;
        internal System.Windows.Forms.ColumnHeader cState;
        private System.Windows.Forms.ColumnHeader cDefaultValues;
        internal System.Windows.Forms.ListView lvDataSource;
        internal System.Windows.Forms.ColumnHeader chName;
        internal System.Windows.Forms.ColumnHeader chReference;
        internal System.Windows.Forms.ListView lvSecurity;
        internal System.Windows.Forms.ColumnHeader cUser;
        private System.Windows.Forms.ColumnHeader cRole;
        internal System.Windows.Forms.ListView lvDependentItems;
        internal System.Windows.Forms.ColumnHeader columnHeader1;
        internal System.Windows.Forms.ColumnHeader columnHeader2;
        internal System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btPreview;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbFileType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbConfigurationFile;
        private System.Windows.Forms.RadioButton optChooseVariable;
        private System.Windows.Forms.RadioButton optChooseConfigFileConnector;
        private System.Windows.Forms.Button btConfigFileExpression;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btLoad;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TreeView tvReportServerSource;
        private System.Windows.Forms.ComboBox cmbSourceVariables;
        internal System.Windows.Forms.ImageList imagesCollection;
        private System.Windows.Forms.DataGridView grdParameters;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdColParams;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdColType;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdColNullable;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdColState;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdColDefault;
        private System.Windows.Forms.DataGridViewComboBoxColumn grdColVars;
        private System.Windows.Forms.DataGridViewButtonColumn grdColExpression;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btReportName;
        private System.Windows.Forms.ComboBox cmbReportName;
        private System.Windows.Forms.Label lbSelectedReportName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbRenderedReportName;

    }
}