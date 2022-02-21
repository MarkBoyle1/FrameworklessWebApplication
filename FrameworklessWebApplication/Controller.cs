using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FrameworklessWebApplication
{
    public class Controller
    {
        private Service _service;

        public Controller()
        {
            List<Person> initialPersonList = new List<Person>()
            {
                new Person("Mark")
            };
            _service = new Service(initialPersonList);
        }
        public HttpListenerResponse ProcessRequest(HttpListenerContext context)
        {
            string url = context.Request.Url.ToString();
            string httpVerb = context.Request.HttpMethod;
            string requestBody = new StreamReader(context.Request.InputStream).ReadToEnd();
            
            string[] splitURL = url.Split('/');
            string nameInUrl = splitURL[4];
            
            List<Person> personList = _service.GetPersonList(httpVerb, requestBody, nameInUrl);
            
            string responseBody;

            if (nameInUrl == "greeting")
            {
                responseBody = GetGreeting(personList);
            }
            else
            {
                responseBody = JsonConvert.SerializeObject(personList);
            }
            
            var buffer = Encoding.UTF8.GetBytes(responseBody);

            // Obtain a response object.
            HttpListenerResponse response = context.Response;
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);

            // You must close the output stream.
            output.Close();

            return response;
        }
        
        
        private string GetGreeting(List<Person> personList)
        {
            var time = DateTime.Now.ToLongTimeString();
            var date = DateTime.Now.ToLongDateString();

            StringBuilder names = new StringBuilder();

            foreach (var p in personList)
            {
                names.Append(FormatName(p.Name, personList));
            }
            
            return $"Hello {names}, the time on the server is {time} {date}";
        }
        
        private string FormatName(string name, List<Person> nameList)
        {
            if (name == nameList.Last().Name)
            {
                return name;
            }

            if (name == nameList[nameList.Count - 2].Name)
            {
                return name + " and ";
            }

            return name + ", ";
        }
    }
}