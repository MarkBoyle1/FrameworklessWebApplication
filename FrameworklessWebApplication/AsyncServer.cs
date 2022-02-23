using System;
using System.IO;
using System.Net;
using System.Text;

namespace FrameworklessWebApplication
{
    public class AsyncServer
    {
        private Controller _controller;
        private RequestParser _requestParser;

        public AsyncServer()
        {
            _controller = new Controller();
            _requestParser = new RequestParser();
        }

        public void RunServer()
        {
            var prefixes = new string[]
            {
                "http://localhost:8080/person/"
            };
            
            HttpListener listener = new HttpListener();
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }

            listener.Start();
            
            while (true)
            {
                IAsyncResult result = listener.BeginGetContext(new AsyncCallback(ListenerCallback),listener);
                result.AsyncWaitHandle.WaitOne();
            }
            
            listener.Close();
        }

        private void ListenerCallback(IAsyncResult result)
        {
            HttpListener listener = (HttpListener) result.AsyncState;
            
            // Call EndGetContext to complete the asynchronous operation.
            HttpListenerContext context = listener.EndGetContext(result);
            
            Console.WriteLine($"{context.Request.HttpMethod} {context.Request.Url}");
            Request request = _requestParser.ParseRequest(context);
            Response response = _controller.ProcessRequest(request);
            var buffer = Encoding.UTF8.GetBytes(response.Body);

            // Get a response stream and write the response to it.
            context.Response.ContentLength64 = buffer.Length;
            context.Response.StatusCode = response.StatusCode;
            Stream output = context.Response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            
            // You must close the output stream.
            output.Close();
        }
    }
}