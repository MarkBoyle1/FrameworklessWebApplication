using System;
using System.Collections.Generic;
using FrameworklessWebApplication;
using Xunit;

namespace FrameworklessWebApplicationTests
{
    public class MockDatabaseTests
    {
        [Fact]
        public void given_PersonNameEqualsBob_when_AddPerson_then_return_ListContainingBob()
        {
            MockDatabase database = new MockDatabase();
            Person person = new Person("Bob");

            List<Person> personList = database.AddPerson(person);

            Assert.Contains(personList, person => person.Name == "Bob");
        }
        
        [Fact]
        public void given_ListContainsOne_when_DeletePerson_then_return_EmptyList()
        {
            MockDatabase database = new MockDatabase();
            Person person = new Person(Constants.InitialPerson);
            
            List<Person> personList = database.DeletePerson(person);

            Assert.Empty(personList);
        }
    }
}