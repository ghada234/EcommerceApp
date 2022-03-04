using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregatte
{
    //ownd entity 
    //owned by order entity not relattion to it

   public class Address
    {

        public Address()
        {
                    
        }
        public Address(string firstName, string lastName, string street, string city, string zipCode)
        {
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            City = city;
            ZipCode = zipCode;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }

    }
}
