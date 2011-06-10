﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.DataTransformationServices.Controls;
using Microsoft.SqlServer.Dts.Runtime;

namespace SSISReportGeneratorTask100
{
    public partial class frmEmail : Form
    {
        private TaskHost _taskHost;

        public frmEmail(TaskHost taskHost, Connections connections)
        {
            InitializeComponent();
            _taskHost = taskHost;
            Connections = connections;
            LoadSMTPConnections();
            LoadVariablesInComboBoxes();

            cmbFrom.Text = (_taskHost.Properties[Keys.FROM].GetValue(_taskHost) != null)
                                ? _taskHost.Properties[Keys.FROM].GetValue(_taskHost).ToString()
                                : string.Empty;
            cmbTo.Text = (_taskHost.Properties[Keys.FROM].GetValue(_taskHost) != null)
                                ? _taskHost.Properties[Keys.RECIPIENTS].GetValue(_taskHost).ToString()
                                : string.Empty;
            txSubject.Text = (_taskHost.Properties[Keys.FROM].GetValue(_taskHost) != null)
                                ? _taskHost.Properties[Keys.EMAIL_SUBJECT].GetValue(_taskHost).ToString()
                                : string.Empty;
            txBody.Text = (_taskHost.Properties[Keys.FROM].GetValue(_taskHost) != null)
                                ? _taskHost.Properties[Keys.EMAIL_BODY].GetValue(_taskHost).ToString()
                                : string.Empty;
            if (_taskHost.Properties[Keys.FROM].GetValue(_taskHost) != null)
                cmbSMTPSrv.SelectedIndex = Tools.FindStringInComboBox(cmbSMTPSrv, _taskHost.Properties[Keys.SMTP_SERVER].GetValue(_taskHost).ToString(), -1);
        }

        public TaskHost TaskHost
        {
            get { return _taskHost; }
            set { _taskHost = value; }
        }

        public Connections Connections { get; set; }

        private void btSubject_Click(object sender, EventArgs e)
        {
            using (ExpressionBuilder expressionBuilder = ExpressionBuilder.Instantiate(_taskHost.Variables,
                                                                                       _taskHost.VariableDispenser,
                                                                                       Type.GetType("String"),
                                                                                       txSubject.Text))
            {
                if (expressionBuilder.ShowDialog() == DialogResult.OK)
                {
                    txSubject.Text = expressionBuilder.Expression;
                }
            }
        }

        private void btBody_Click(object sender, EventArgs e)
        {
            using (ExpressionBuilder expressionBuilder = ExpressionBuilder.Instantiate(_taskHost.Variables,
                                                                                       _taskHost.VariableDispenser,
                                                                                       Type.GetType("String"),
                                                                                       txBody.Text))
            {
                if (expressionBuilder.ShowDialog() == DialogResult.OK)
                {
                    txBody.Text = expressionBuilder.Expression;
                }
            }
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            _taskHost.Properties[Keys.FROM].SetValue(_taskHost, cmbFrom.Text);
            _taskHost.Properties[Keys.RECIPIENTS].SetValue(_taskHost, cmbTo.Text);
            _taskHost.Properties[Keys.EMAIL_SUBJECT].SetValue(_taskHost, txSubject.Text);
            _taskHost.Properties[Keys.EMAIL_BODY].SetValue(_taskHost, txBody.Text);
            _taskHost.Properties[Keys.SMTP_SERVER].SetValue(_taskHost, cmbSMTPSrv.Text);
            Close();
        }

        /// <summary>
        /// Loads the config file connections.
        /// </summary>
        private void LoadSMTPConnections()
        {

            cmbSMTPSrv.Items.AddRange(Connections.Cast<ConnectionManager>().Where(connection => connection.CreationName.Contains("SMTP"))
                                                                      .Select(connection => connection.Name).ToArray());
        }


        /// <summary>
        /// Loads the variables in combo boxes.
        /// </summary>
        private void LoadVariablesInComboBoxes()
        {
            var result = _taskHost.Variables.Cast<Variable>().Where(variable => Type.GetTypeCode(Type.GetType("System.String")) == variable.DataType)
                                             .Select(variable => string.Format("@[{0}::{1}]", variable.Namespace, variable.Name)).ToArray();

            cmbFrom.Items.AddRange(result);
            cmbTo.Items.AddRange(result);
        }
    }
}