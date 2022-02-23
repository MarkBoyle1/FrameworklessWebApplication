using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrameworklessWebApplication
{
    public class Messages
    {
        public const string EmptyBodyMessage = "Error: Request body is empty.";

        public static string MakeGreeting(List<Person> personList, string time, string date)
        {
            if (time == "" && date == "")
            {
                time = DateTime.Now.ToLongTimeString();
                date = DateTime.Now.ToLongDateString();
            }

            StringBuilder names = new StringBuilder();

            foreach (var p in personList)
            {
                names.Append(FormatName(p.Name, personList));
            }
            
            return $"Hello {names}, the time on the server is {time} {date}";
        }
        
        private static string FormatName(string name, List<Person> nameList)
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