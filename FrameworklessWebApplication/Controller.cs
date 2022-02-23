using System;
using System.Collections.Generic;
using FrameworklessWebApplication.Exceptions;
using Newtonsoft.Json;

namespace FrameworklessWebApplication
{
    public class Controller
    {
        private string _time;
        private string _date;
        private Service _service;
        private int _statusCode;

        public Controller(string time = "", string date = "")
        {
            _time = time;
            _date = date;
            _service = new Service();
        }
        public Response ProcessRequest(Request request)
        {
            List<Person> personList;

            try
            {
                switch (request.HttpVerb)
                {
                    case Constants.POST:
                        personList = _service.AddPerson(request.Body);
                        _statusCode = Constants.StatusCodeCreated;
                        break;
                    case Constants.DELETE:
                        personList = _service.DeletePerson(request.Body);
                        _statusCode = Constants.StatusCodeOk;
                        break;
                    case Constants.PUT:
                        personList = _service.UpdatePerson(request.Body, request.NameInUrl);
                        _statusCode = Constants.StatusCodeOk;
                        break;
                    case Constants.GET:
                        personList = _service.GetPersonList();
                        _statusCode = Constants.StatusCodeOk;
                        break;
                    default:
                        return new Response(String.Empty, Constants.StatusCodeMethodNotAllowed);
                }
            }
            catch (EmptyBodyException)
            {
                return new Response(Messages.EmptyBodyMessage, Constants.StatusCodesBadRequest);
            }
            
            string responseBody;

            if(request.RequestsGreeting)
            {
                responseBody = Messages.MakeGreeting(personList, _time, _date);
            }
            else
            {
                responseBody = JsonConvert.SerializeObject(personList);
            }

            Response response = new Response(responseBody, _statusCode);
            
            return response;
        }
    }
}