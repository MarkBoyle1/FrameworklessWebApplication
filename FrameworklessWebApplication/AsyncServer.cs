using System;
using System.Net;

namespace FrameworklessWebApplication
{
    public class AsyncServer
    {
        private Controller _controller;

        public AsyncServer()
        {
            _controller = new Controller();
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

            _controller.ProcessRequest(context);
        }
    }
}