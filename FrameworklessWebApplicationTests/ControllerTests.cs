using System;
using System.Collections.Generic;
using System.Net;
using FrameworklessWebApplication;
using Xunit;

namespace FrameworklessWebApplicationTests
{
    public class ControllerTests
    {
        private Controller _controller = new Controller("11:30:14 am", "Wednesday, 23 February 2022");
        
        [Fact]
        public void given_HttpVerbEqualsGET_when_ProcessRequest_then_return_ListContainingMark()
        {
            string httpVerb = "GET";
            string body = String.Empty;
            string nameInUrl = String.Empty;
            bool requestsGreeting = false;
            
            Request request = new Request(httpVerb, body, nameInUrl, requestsGreeting);

            Response response = _controller.ProcessRequest(request);
            
            Assert.Equal("[{\"Name\":\"Mark\"}]", response.Body);
        }
        
        [Fact]
        public void given_HttpVerbEqualsPOST_and_BodyEqualsBob_when_ProcessRequest_then_return_ListContainingMarkAndBob()
        {
            string httpVerb = "POST";
            string body = "Bob";
            string nameInUrl = String.Empty;
            bool requestsGreeting = false;
            
            Request request = new Request(httpVerb, body, nameInUrl, requestsGreeting);

            Response response = _controller.ProcessRequest(request);
            
            Assert.Equal("[{\"Name\":\"Mark\"},{\"Name\":\"Bob\"}]", response.Body);
        }
        
        [Fact]
        public void given_HttpVerbEqualsGET_and_GreetingIsRequested_when_ProcessRequest_then_return_GreetingWithMark()
        {
            string httpVerb = "GET";
            string body = String.Empty;
            string nameInUrl = String.Empty;
            bool requestsGreeting = true;

            Request request = new Request(httpVerb, body, nameInUrl, requestsGreeting);

            Response response = _controller.ProcessRequest(request);
            
            Assert.Equal("Hello Mark, the time on the server is 11:30:14 am Wednesday, 23 February 2022", response.Body);
        }
    }
}