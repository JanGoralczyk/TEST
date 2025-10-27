using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KSEFClient
{
    public class KSEFService
    {
        private readonly HttpClient _httpClient;
        private const string KSEF_BASE_URL = "https://ksef.mf.gov.pl/api";
        private string? _sessionToken;

        public KSEFService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<bool> InitializeSessionAsync(string identifier, string password)
        {
            try
            {
                var loginData = new
                {
                    identifier = identifier,
                    password = password
                };

                var json = JsonConvert.SerializeObject(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Note: This is a simplified example. Real KSEF authentication may require
                // certificate-based authentication or different endpoints
                var response = await _httpClient.PostAsync($"{KSEF_BASE_URL}/session/init", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<dynamic>(responseContent);
                    _sessionToken = result?.sessionToken;
                    return !string.IsNullOrEmpty(_sessionToken);
                }
                
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string> GetSessionStatusAsync()
        {
            if (string.IsNullOrEmpty(_sessionToken))
                return "No active session";

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _sessionToken);
                
                var response = await _httpClient.GetAsync($"{KSEF_BASE_URL}/session/status");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return $"Session active: {content}";
                }
                
                return $"Session check failed: {response.StatusCode}";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<bool> TerminateSessionAsync()
        {
            if (string.IsNullOrEmpty(_sessionToken))
                return true;

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _sessionToken);
                
                var response = await _httpClient.DeleteAsync($"{KSEF_BASE_URL}/session/terminate");
                
                if (response.IsSuccessStatusCode)
                {
                    _sessionToken = null;
                    return true;
                }
                
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}