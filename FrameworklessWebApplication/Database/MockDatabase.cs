using System.Collections.Generic;

namespace FrameworklessWebApplication
{
    public class MockDatabase : IDatabase
    {
        private List<Person> _personList;

        public MockDatabase()
        {
            _personList = new List<Person>()
            {
                new Person(Constants.InitialPerson)
            };
        }

        public List<Person> GetPersonList()
        {
            return _personList;
        }

        public List<Person> AddPerson(Person person)
        {
            _personList.Add(person);
            return _personList;
        }

        public List<Person> DeletePerson(Person person)
        {
            int index = _personList.FindIndex(p => p.Name == person.Name);
            _personList.RemoveAt(index);
            return _personList;
        }

        public List<Person> UpdatePerson(string oldName, string newName)
        {
            int index = _personList.FindIndex(p => p.Name == oldName);
            _personList[index].Name = newName;
            return _personList;
        }
    }
}