using System;
using System.Windows.Forms;
using CIRFMF.Ksef.Client;
using CIRFMF.Ksef.Client.Models;

namespace KsefWfaDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnSession_Click(object sender, EventArgs e)
        {
            try
            {
                var config = new KsefClientConfig
                {
                    BaseUrl = "https://test-ksef.mf.gov.pl/api/v2/",
                };

                var client = new KsefClient(config);

                var contextIdentifier = new ContextIdentifier
                {
                    Identifier = txtNip.Text,
                    SubjectType = ContextIdentifierSubjectType.Taxpayer
                };

                var sessionInitResult = await client.Session.InitByContextIdentifierAsync(contextIdentifier);

                txtResult.Text = sessionInitResult != null
                    ? $"Session Token: {sessionInitResult.Token}\nExpires: {sessionInitResult.ExpiresAtUtc}"
                    : "Brak odpowiedzi z KSeF.";
            }
            catch (Exception ex)
            {
                txtResult.Text = $"Błąd: {ex.Message}";
            }
        }
    }
}