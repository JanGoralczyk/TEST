using System;

namespace KSEFClient
{
    public class KSEFModels
    {
        public class SessionInitRequest
        {
            public string identifier { get; set; } = "";
            public string password { get; set; } = "";
            public string contextNip { get; set; } = "";
            public string contextName { get; set; } = "";
        }

        public class SessionInitResponse
        {
            public string sessionToken { get; set; } = "";
            public DateTime timestamp { get; set; }
            public int challengeSeq { get; set; }
        }

        public class SessionStatusResponse
        {
            public string status { get; set; } = "";
            public DateTime timestamp { get; set; }
            public string sessionToken { get; set; } = "";
        }

        public class KSEFErrorResponse
        {
            public string code { get; set; } = "";
            public string message { get; set; } = "";
            public string details { get; set; } = "";
            public DateTime timestamp { get; set; }
        }
    }
}