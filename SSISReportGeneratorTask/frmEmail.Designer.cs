namespace SSISReportGeneratorTask100
{
    partial class frmEmail
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btSubject = new System.Windows.Forms.Button();
            this.txSubject = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btBody = new System.Windows.Forms.Button();
            this.txBody = new System.Windows.Forms.TextBox();
            this.btOk = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.cmbSMTPSrv = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbFrom = new System.Windows.Forms.ComboBox();
            this.cmbTo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "To:";
            // 
            // btSubject
            // 
            this.btSubject.Location = new System.Drawing.Point(389, 82);
            this.btSubject.Name = "btSubject";
            this.btSubject.Size = new System.Drawing.Size(47, 23);
            this.btSubject.TabIndex = 8;
            this.btSubject.Text = "f(x)";
            this.btSubject.UseVisualStyleBackColor = true;
            this.btSubject.Click += new System.EventHandler(this.btSubject_Click);
            // 
            // txSubject
            // 
            this.txSubject.Location = new System.Drawing.Point(91, 84);
            this.txSubject.Name = "txSubject";
            this.txSubject.Size = new System.Drawing.Size(292, 20);
            this.txSubject.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Subject:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Body:";
            // 
            // btBody
            // 
            this.btBody.Location = new System.Drawing.Point(389, 111);
            this.btBody.Name = "btBody";
            this.btBody.Size = new System.Drawing.Size(47, 23);
            this.btBody.TabIndex = 11;
            this.btBody.Text = "f(x)";
            this.btBody.UseVisualStyleBackColor = true;
            this.btBody.Click += new System.EventHandler(this.btBody_Click);
            // 
            // txBody
            // 
            this.txBody.Location = new System.Drawing.Point(91, 112);
            this.txBody.Multiline = true;
            this.txBody.Name = "txBody";
            this.txBody.Size = new System.Drawing.Size(292, 154);
            this.txBody.TabIndex = 12;
            // 
            // btOk
            // 
            this.btOk.Location = new System.Drawing.Point(227, 272);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 14;
            this.btOk.Text = "Ok";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(308, 272);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 13;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // cmbSMTPSrv
            // 
            this.cmbSMTPSrv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSMTPSrv.FormattingEnabled = true;
            this.cmbSMTPSrv.Location = new System.Drawing.Point(91, 5);
            this.cmbSMTPSrv.Name = "cmbSMTPSrv";
            this.cmbSMTPSrv.Size = new System.Drawing.Size(345, 21);
            this.cmbSMTPSrv.TabIndex = 57;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 8);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(74, 13);
            this.label13.TabIndex = 56;
            this.label13.Text = "SMTP Server:";
            // 
            // cmbFrom
            // 
            this.cmbFrom.FormattingEnabled = true;
            this.cmbFrom.Location = new System.Drawing.Point(91, 32);
            this.cmbFrom.Name = "cmbFrom";
            this.cmbFrom.Size = new System.Drawing.Size(345, 21);
            this.cmbFrom.TabIndex = 62;
            // 
            // cmbTo
            // 
            this.cmbTo.FormattingEnabled = true;
            this.cmbTo.Location = new System.Drawing.Point(91, 58);
            this.cmbTo.Name = "cmbTo";
            this.cmbTo.Size = new System.Drawing.Size(345, 21);
            this.cmbTo.TabIndex = 63;
            // 
            // frmEmail
            // 
            this.AcceptButton = this.btOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(453, 308);
            this.ControlBox = false;
            this.Controls.Add(this.cmbTo);
            this.Controls.Add(this.cmbFrom);
            this.Controls.Add(this.cmbSMTPSrv);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.txBody);
            this.Controls.Add(this.btBody);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btSubject);
            this.Controls.Add(this.txSubject);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmEmail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Email properties";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btSubject;
        private System.Windows.Forms.TextBox txSubject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btBody;
        private System.Windows.Forms.TextBox txBody;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.ComboBox cmbSMTPSrv;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbFrom;
        private System.Windows.Forms.ComboBox cmbTo;
    }
}