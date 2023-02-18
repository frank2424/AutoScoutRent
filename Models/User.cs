using System.ComponentModel.DataAnnotations;

namespace AutoScoutRent.Models
{
        public class User
        {
            [Key]
            public int User_id { get; set; }
            [Required]
            

        public string Username { get; set; }

            //if username is admin access BO
            [Required]
            public string Name { get; set; }
            [Required]
            public string Surname { get; set; }


            [Required]
            //[MinLength]
            public string Password { get; set; }

            public ICollection<Booking>? Bookings { get; set; }


        }
}
