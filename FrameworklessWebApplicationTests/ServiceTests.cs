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
            Service service = new Service();

            List<Person> answer = service.GetPersonList();

            Assert.Contains(answer, person => person.Name == Constants.InitialPerson);
        }
        
        [Fact]
        public void given_HttpVerbEqualsPOST_and_BodyEqualsBob_when_GetPersonList_then_AddBobToPersonList()
        {
            Service service = new Service();

            List<Person> answer = service.AddPerson("Bob");
            
            Assert.Contains(answer, person => person.Name == Constants.InitialPerson);
            Assert.Contains(answer, person => person.Name == "Bob");
        }
        
        [Fact]
        public void given_HttpVerbEqualsDELETE_and_BodyEqualsMary_when_GetPersonList_then_RemoveMaryFromPersonList()
        {
            Service service = new Service();
            service.AddPerson("Mary");

            List<Person> answer = service.DeletePerson("Mary");
            
            Assert.Contains(answer, person => person.Name == Constants.InitialPerson);
            Assert.DoesNotContain(answer, person => person.Name == "Mary");
        }
        
        [Fact]
        public void given_HttpVerbEqualsDELETE_and_BodyEqualsMark_and_InitialNameEqualsMark_when_GetPersonList_then_DoNotRemoveMarkFromPersonList()
        {
            Service service = new Service();

            List<Person> answer = service.DeletePerson(Constants.InitialPerson);
            
            Assert.Contains(answer, person => person.Name == Constants.InitialPerson);
        }
        
        [Fact]
        public void given_HttpVerbEqualsPOST_and_BodyEqualsMary_and_PersonListAlreadyContainsMary_when_GetPersonList_then_DoNotAddMaryToPersonList()
        {
            Service service = new Service();
            service.AddPerson("Mary");

            List<Person> answer = service.AddPerson("Mary");
            
            Assert.Equal(2, answer.Count);
        }
    }
}