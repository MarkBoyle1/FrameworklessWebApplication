namespace FrameworklessWebApplication
{
    public class Request
    {
        public string HttpVerb;
        public string Body;
        public string NameInUrl;
        public bool RequestsGreeting;

        public Request(string httpVerb, string body, string nameInUrl, bool requestsGreeting)
        {
            HttpVerb = httpVerb;
            Body = body;
            NameInUrl = nameInUrl;
            RequestsGreeting = requestsGreeting;
        }
    }
}