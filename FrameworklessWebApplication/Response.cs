namespace FrameworklessWebApplication
{
    public class Response
    {
        public string Body;
        public int StatusCode;
        public Response(string responseBody, int statusCode)
        {
            Body = responseBody;
            StatusCode = statusCode;
        }
    }
}