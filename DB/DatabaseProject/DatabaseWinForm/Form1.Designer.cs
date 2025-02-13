namespace DatabaseWinForm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            lblTypeQuery = new Label();
            txtBoxQuery = new TextBox();
            btnExecute = new Button();
            btnExit = new Button();
            btnClear = new Button();
            listViewQuery = new ListView();
            SuspendLayout();
            // 
            // lblTypeQuery
            // 
            lblTypeQuery.AutoSize = true;
            lblTypeQuery.BackColor = Color.Transparent;
            lblTypeQuery.Font = new Font("Tahoma", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTypeQuery.ForeColor = Color.Black;
            lblTypeQuery.Location = new Point(32, 38);
            lblTypeQuery.Name = "lblTypeQuery";
            lblTypeQuery.Size = new Size(183, 24);
            lblTypeQuery.TabIndex = 0;
            lblTypeQuery.Text = "Type query here:";
            lblTypeQuery.Click += label1_Click;
            // 
            // txtBoxQuery
            // 
            txtBoxQuery.BackColor = Color.FromArgb(255, 192, 255);
            txtBoxQuery.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBoxQuery.Location = new Point(236, 30);
            txtBoxQuery.Name = "txtBoxQuery";
            txtBoxQuery.Size = new Size(732, 32);
            txtBoxQuery.TabIndex = 1;
            txtBoxQuery.UseWaitCursor = true;
            // 
            // btnExecute
            // 
            btnExecute.BackColor = Color.FromArgb(255, 192, 255);
            btnExecute.Font = new Font("Tahoma", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExecute.ForeColor = Color.Black;
            btnExecute.Location = new Point(104, 452);
            btnExecute.Name = "btnExecute";
            btnExecute.Size = new Size(126, 43);
            btnExecute.TabIndex = 3;
            btnExecute.Text = "Execute";
            btnExecute.UseVisualStyleBackColor = false;
            btnExecute.Click += btnExecute_Click;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.FromArgb(255, 192, 255);
            btnExit.Font = new Font("Tahoma", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExit.Location = new Point(104, 550);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(126, 43);
            btnExit.TabIndex = 6;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.FromArgb(255, 192, 255);
            btnClear.Font = new Font("Tahoma", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnClear.Location = new Point(104, 501);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(126, 43);
            btnClear.TabIndex = 7;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // listViewQuery
            // 
            listViewQuery.BackColor = Color.FromArgb(255, 192, 255);
            listViewQuery.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            listViewQuery.HeaderStyle = ColumnHeaderStyle.None;
            listViewQuery.Location = new Point(236, 78);
            listViewQuery.Name = "listViewQuery";
            listViewQuery.Size = new Size(733, 515);
            listViewQuery.TabIndex = 2;
            listViewQuery.UseCompatibleStateImageBehavior = false;
            listViewQuery.View = View.List;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(980, 605);
            Controls.Add(btnClear);
            Controls.Add(btnExit);
            Controls.Add(btnExecute);
            Controls.Add(listViewQuery);
            Controls.Add(txtBoxQuery);
            Controls.Add(lblTypeQuery);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            MaximumSize = new Size(998, 652);
            Name = "Form1";
            Text = "Database Workbench";
            TransparencyKey = Color.Transparent;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTypeQuery;
        private TextBox txtBoxQuery;
        private Button btnExecute;
        private Button btnExit;
        private Button btnClear;
        private ListView listViewQuery;
    }
}
