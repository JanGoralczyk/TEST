namespace KsefWfaDemo
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnSession = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.txtNip = new System.Windows.Forms.TextBox();
            this.lblNip = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSession
            // 
            this.btnSession.Location = new System.Drawing.Point(12, 45);
            this.btnSession.Name = "btnSession";
            this.btnSession.Size = new System.Drawing.Size(150, 40);
            this.btnSession.TabIndex = 0;
            this.btnSession.Text = "Nawiąż sesję z KSeF";
            this.btnSession.UseVisualStyleBackColor = true;
            this.btnSession.Click += new System.EventHandler(this.btnSession_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(12, 100);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(350, 100);
            this.txtResult.TabIndex = 1;
            // 
            // txtNip
            // 
            this.txtNip.Location = new System.Drawing.Point(52, 12);
            this.txtNip.MaxLength = 10;
            this.txtNip.Name = "txtNip";
            this.txtNip.Size = new System.Drawing.Size(120, 23);
            this.txtNip.TabIndex = 2;
            this.txtNip.Text = "1234567890";
            // 
            // lblNip
            // 
            this.lblNip.AutoSize = true;
            this.lblNip.Location = new System.Drawing.Point(12, 15);
            this.lblNip.Name = "lblNip";
            this.lblNip.Size = new System.Drawing.Size(34, 15);
            this.lblNip.TabIndex = 3;
            this.lblNip.Text = "NIP:";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(384, 221);
            this.Controls.Add(this.lblNip);
            this.Controls.Add(this.txtNip);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnSession);
            this.Name = "Form1";
            this.Text = "KSeF WFA Demo";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button btnSession;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.TextBox txtNip;
        private System.Windows.Forms.Label lblNip;
    }
}