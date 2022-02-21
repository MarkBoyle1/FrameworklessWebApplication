using System.Collections.Generic;
using FrameworklessWebApplication;
using Xunit;

namespace FrameworklessWebApplicationTests
{
    public class ServiceTests
    {
        [Fact]
        public void given_InitialPersonListContainsMark_when_GetPersonList_then_return_ListContainingMark()
        {
            List<Person> personList = new List<Person>()
            {
                new Person("Mark")
            };
            Service service = new Service(personList);

            List<Person> answer = service.GetPersonList("GET");

            Assert.Contains(answer, person => person.Name == "Mark");
        }
        
        [Fact]
        public void given_HttpVerbEqualsPOST_and_BodyEqualsBob_when_GetPersonList_then_AddBobToPersonList()
        {
            List<Person> personList = new List<Person>()
            {
                new Person("Mark")
            };
            Service service = new Service(personList);

            List<Person> answer = service.GetPersonList("POST", "Bob");
            
            Assert.Contains(answer, person => person.Name == "Mark");
            Assert.Contains(answer, person => person.Name == "Bob");
        }
        
        [Fact]
        public void given_HttpVerbEqualsDELETE_and_BodyEqualsMary_when_GetPersonList_then_RemoveMaryFromPersonList()
        {
            List<Person> personList = new List<Person>()
            {
                new Person("Mark"),
                new Person("Mary")
            };
            Service service = new Service(personList);

            List<Person> answer = service.GetPersonList("DELETE", "Mary");
            
            Assert.Contains(answer, person => person.Name == "Mark");
            Assert.DoesNotContain(answer, person => person.Name == "Mary");
        }
        
        [Fact]
        public void given_HttpVerbEqualsDELETE_and_BodyEqualsMark_and_InitialNameEqualsMark_when_GetPersonList_then_DoNotRemoveMarkFromPersonList()
        {
            List<Person> personList = new List<Person>()
            {
                new Person("Mark")
            };
            Service service = new Service(personList);

            List<Person> answer = service.GetPersonList("DELETE", "Mark");
            
            Assert.Contains(answer, person => person.Name == "Mark");
        }
        
        [Fact]
        public void given_HttpVerbEqualsPOST_and_BodyEqualsMary_and_PersonListAlreadyContainsMary_when_GetPersonList_then_DoNotAddMaryToPersonList()
        {
            List<Person> personList = new List<Person>()
            {
                new Person("Mark"),
                new Person("Mary")
            };
            Service service = new Service(personList);

            List<Person> answer = service.GetPersonList("POST", "Mary");
            
            Assert.Equal(2, answer.Count);
        }
    }
}