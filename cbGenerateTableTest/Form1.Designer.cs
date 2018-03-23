namespace cbGenerateTableTest
{
    partial class frmGenerateTableTestForm
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
            this.btnGenerateBasicTable = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGenerateBasicTable
            // 
            this.btnGenerateBasicTable.Location = new System.Drawing.Point(13, 13);
            this.btnGenerateBasicTable.Name = "btnGenerateBasicTable";
            this.btnGenerateBasicTable.Size = new System.Drawing.Size(75, 56);
            this.btnGenerateBasicTable.TabIndex = 0;
            this.btnGenerateBasicTable.Text = "Generate Basic Table";
            this.btnGenerateBasicTable.UseVisualStyleBackColor = true;
            this.btnGenerateBasicTable.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmGenerateTableTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 332);
            this.Controls.Add(this.btnGenerateBasicTable);
            this.Name = "frmGenerateTableTestForm";
            this.Text = "Generate Table Test";
            this.Load += new System.EventHandler(this.frmGenerateTableTestForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGenerateBasicTable;
    }
}

