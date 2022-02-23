using System.Collections.Generic;

namespace FrameworklessWebApplication
{
    public interface IDatabase
    {
        public List<Person> GetPersonList();

        public List<Person> AddPerson(Person person);

        public List<Person> DeletePerson(Person person);

        public List<Person> UpdatePerson(string oldName, string newName);
    }
}