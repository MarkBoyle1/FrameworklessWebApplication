using System.IO;
using System.Linq;
using System.Net;

namespace FrameworklessWebApplication
{
    public class RequestParser
    {
        public Request ParseRequest(HttpListenerContext context)
        {
            string url = context.Request.Url.ToString();
            string httpVerb = context.Request.HttpMethod;
            string requestBody = new StreamReader(context.Request.InputStream).ReadToEnd();
            
            string[] splitURL = url.Split('/');
            string nameInUrl = splitURL.Length > 4 ? splitURL[4] : null;
            
            bool requestsGreeting = splitURL.Contains(Constants.Greeting);
            
            return new Request(httpVerb, requestBody, nameInUrl, requestsGreeting);
        }
    }
}