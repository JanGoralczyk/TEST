using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KSEFClient
{
    public partial class Form1 : Form
    {
        private KSEFService _ksefService;
        private TextBox txtIdentifier;
        private TextBox txtPassword;
        private Button btnConnect;
        private Button btnCheckStatus;
        private Button btnDisconnect;
        private TextBox txtOutput;
        private Label lblIdentifier;
        private Label lblPassword;
        private Label lblOutput;

        public Form1()
        {
            InitializeComponent();
            _ksefService = new KSEFService();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form1
            this.ClientSize = new Size(600, 500);
            this.Text = "KSEF 2.0 Client - Windows Forms Application";
            this.StartPosition = FormStartPosition.CenterScreen;

            // lblIdentifier
            this.lblIdentifier = new Label();
            this.lblIdentifier.AutoSize = true;
            this.lblIdentifier.Location = new Point(30, 30);
            this.lblIdentifier.Size = new Size(71, 15);
            this.lblIdentifier.Text = "Identifier:";

            // txtIdentifier
            this.txtIdentifier = new TextBox();
            this.txtIdentifier.Location = new Point(120, 27);
            this.txtIdentifier.Size = new Size(200, 23);
            this.txtIdentifier.Text = "";

            // lblPassword
            this.lblPassword = new Label();
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new Point(30, 70);
            this.lblPassword.Size = new Size(60, 15);
            this.lblPassword.Text = "Password:";

            // txtPassword
            this.txtPassword = new TextBox();
            this.txtPassword.Location = new Point(120, 67);
            this.txtPassword.Size = new Size(200, 23);
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Text = "";

            // btnConnect
            this.btnConnect = new Button();
            this.btnConnect.Location = new Point(30, 110);
            this.btnConnect.Size = new Size(100, 30);
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new EventHandler(this.btnConnect_Click);

            // btnCheckStatus
            this.btnCheckStatus = new Button();
            this.btnCheckStatus.Location = new Point(140, 110);
            this.btnCheckStatus.Size = new Size(100, 30);
            this.btnCheckStatus.Text = "Check Status";
            this.btnCheckStatus.UseVisualStyleBackColor = true;
            this.btnCheckStatus.Click += new EventHandler(this.btnCheckStatus_Click);

            // btnDisconnect
            this.btnDisconnect = new Button();
            this.btnDisconnect.Location = new Point(250, 110);
            this.btnDisconnect.Size = new Size(100, 30);
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new EventHandler(this.btnDisconnect_Click);

            // lblOutput
            this.lblOutput = new Label();
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new Point(30, 160);
            this.lblOutput.Size = new Size(48, 15);
            this.lblOutput.Text = "Output:";

            // txtOutput
            this.txtOutput = new TextBox();
            this.txtOutput.Location = new Point(30, 180);
            this.txtOutput.Multiline = true;
            this.txtOutput.ScrollBars = ScrollBars.Vertical;
            this.txtOutput.Size = new Size(540, 280);
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Text = "KSEF 2.0 Client Ready\r\n" +
                                   "Enter your credentials and click Connect to establish session with KSEF 2.0\r\n" +
                                   "Note: This is a demo application. Actual KSEF integration may require certificates and different authentication methods.";

            // Add controls to form
            this.Controls.Add(this.lblIdentifier);
            this.Controls.Add(this.txtIdentifier);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnCheckStatus);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.txtOutput);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdentifier.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                AppendOutput("Please enter both identifier and password.");
                return;
            }

            btnConnect.Enabled = false;
            AppendOutput("Attempting to connect to KSEF 2.0...");

            try
            {
                bool success = await _ksefService.InitializeSessionAsync(txtIdentifier.Text, txtPassword.Text);
                if (success)
                {
                    AppendOutput("Successfully connected to KSEF 2.0!");
                    btnCheckStatus.Enabled = true;
                    btnDisconnect.Enabled = true;
                }
                else
                {
                    AppendOutput("Failed to connect to KSEF 2.0. Please check your credentials.");
                }
            }
            catch (Exception ex)
            {
                AppendOutput($"Error connecting to KSEF 2.0: {ex.Message}");
            }
            finally
            {
                btnConnect.Enabled = true;
            }
        }

        private async void btnCheckStatus_Click(object sender, EventArgs e)
        {
            btnCheckStatus.Enabled = false;
            AppendOutput("Checking session status...");

            try
            {
                string status = await _ksefService.GetSessionStatusAsync();
                AppendOutput($"Status: {status}");
            }
            catch (Exception ex)
            {
                AppendOutput($"Error checking status: {ex.Message}");
            }
            finally
            {
                btnCheckStatus.Enabled = true;
            }
        }

        private async void btnDisconnect_Click(object sender, EventArgs e)
        {
            btnDisconnect.Enabled = false;
            AppendOutput("Terminating session...");

            try
            {
                bool success = await _ksefService.TerminateSessionAsync();
                if (success)
                {
                    AppendOutput("Session terminated successfully.");
                    btnCheckStatus.Enabled = false;
                }
                else
                {
                    AppendOutput("Failed to terminate session.");
                }
            }
            catch (Exception ex)
            {
                AppendOutput($"Error terminating session: {ex.Message}");
            }
            finally
            {
                btnDisconnect.Enabled = true;
            }
        }

        private void AppendOutput(string message)
        {
            if (txtOutput.InvokeRequired)
            {
                txtOutput.Invoke(new Action<string>(AppendOutput), message);
                return;
            }

            txtOutput.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\r\n");
            txtOutput.SelectionStart = txtOutput.Text.Length;
            txtOutput.ScrollToCaret();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _ksefService?.Dispose();
            base.OnFormClosing(e);
        }
    }
}