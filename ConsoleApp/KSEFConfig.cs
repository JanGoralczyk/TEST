namespace KSEFClient
{
    public class KSEFConfig
    {
        public string BaseUrl { get; set; } = "https://ksef.mf.gov.pl/api";
        public string TestBaseUrl { get; set; } = "https://ksef-test.mf.gov.pl/api";
        public int TimeoutSeconds { get; set; } = 30;
        public bool UseTestEnvironment { get; set; } = true;
    }
}