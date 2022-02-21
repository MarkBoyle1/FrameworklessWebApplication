using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace FrameworklessWebApplication
{
    public class Service
    {
        private List<Person> _personList;

        public Service(List<Person> personList)
        {
            _personList = personList;
        }
        public List<Person> GetPersonList(string httpVerb, string requestBody = null, string nameInUrl = null)
        {
            Person person;
            
            try
            {
                JObject.Parse(requestBody);
                person = JsonSerializer.Deserialize<Person>(requestBody);
            }
            catch (Exception e)
            {
                person = new Person(requestBody);
            }

            if (httpVerb == "POST")
            {
                if (!_personList.Exists(p => p.Name == person.Name))
                {
                    _personList.Add(person);
                }
            }
            else if (httpVerb == "DELETE")
            {
                if (_personList.Exists(p => p.Name == person.Name) && person.Name != "Mark")
                {
                    int index = _personList.FindIndex(p => p.Name == person.Name);
                    _personList.RemoveAt(index);
                }
            }
            else if (httpVerb == "PUT")
            {
                if (_personList.Exists(p => p.Name == nameInUrl))
                {
                    int index = _personList.FindIndex(a => a.Name == nameInUrl);
                    _personList[index].Name = person.Name;
                }
            }

            return _personList;
        }
        
        
        
    }
}