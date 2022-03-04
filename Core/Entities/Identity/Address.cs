using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Identity
{
    public class Address
    {
        public int id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Street{ get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }


        //one to one between appuser and address --> one user has only one address
        public AppUser AppUser { get; set; }

        [Required]
        //note that appuserid is sring not int
        //we use required because string by default set to null in database
        public string AppUserId { get; set; }


    }
}