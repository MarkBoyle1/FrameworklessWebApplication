using System;
using System.Collections.Generic;
using FrameworklessWebApplication.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace FrameworklessWebApplication
{
    public class Service
    {
        private IDatabase _database;

        public Service()
        {
            _database = new MockDatabase();
        }

        public List<Person> GetPersonList()
        {
            return _database.GetPersonList();
        }

        public List<Person> AddPerson(string requestBody)
        {
            Person person = CreatePerson(requestBody);
            
            List<Person> personList = _database.GetPersonList();

            if (!personList.Exists(p => p.Name == person.Name))
            {
                personList = _database.AddPerson(person);
            }

            return personList;
        }

        public List<Person> DeletePerson(string requestBody)
        {
            Person person = CreatePerson(requestBody);

            List<Person> personList = _database.GetPersonList();

            if (personList.Exists(p => p.Name == person.Name && p.Name != Constants.InitialPerson))
            {
                personList = _database.DeletePerson(person);
            }
            return personList;
        }

        public List<Person> UpdatePerson(string requestBody, string oldName)
        {
            Person newPerson = CreatePerson(requestBody);
            
            List<Person> personList = _database.GetPersonList();
        
            if (personList.Exists(p => p.Name == oldName))
            {
                personList = _database.UpdatePerson(oldName, newPerson.Name);
            }
        
            return personList;
        }

        private Person CreatePerson(string requestBody)
        {
            if (String.IsNullOrWhiteSpace(requestBody))
            {
                throw new EmptyBodyException();
            }
            
            Person person;

            try
            {
                JObject.Parse(requestBody);
                person = JsonSerializer.Deserialize<Person>(requestBody);
            }
            catch (JsonReaderException)
            {
                person = new Person(requestBody);
            }

            return person;
        }

    }
}