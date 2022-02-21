using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace FrameworklessWebApplication
{
    public class SynchronousServer
    {
        public void RunServer()
        {
            var nameList = new List<string>(){"Mark"};
            
            var server = new HttpListener();
            server.Prefixes.Add("http://localhost:8080/");
            server.Start();
            while (true)
            {
                var context = server.GetContext();  // Gets the request
               
                if(context.Request.HttpMethod == "POST")
                {
                    var rawRequestBody = new StreamReader(context.Request.InputStream).ReadToEnd();
                    
                    nameList.Add(rawRequestBody);
                }
                else if(context.Request.HttpMethod == "DELETE")
                {
                    var rawRequestBody = new StreamReader(context.Request.InputStream).ReadToEnd();
                 
                    if (nameList.Contains(rawRequestBody))
                    {
                        nameList.Remove(rawRequestBody);
                    }
                }

                Console.WriteLine($"{context.Request.HttpMethod} {context.Request.Url}");
                var time = DateTime.Now.ToLongTimeString();
                var date = DateTime.Now.ToLongDateString();
                
                StringBuilder names = new StringBuilder();
                
                foreach (var name in nameList)
                {
                    names.Append(name);
                }
            
                Console.WriteLine("Getting response ready");
                var buffer = Encoding.UTF8.GetBytes($"Hello {names}, the time on the server is {time} {date}");
                context.Response.ContentLength64 = buffer.Length;
                Console.WriteLine("Sending response");
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                // forces send of response
            }
            
            server.Stop();  // never reached...
            
            
        }
    }
}