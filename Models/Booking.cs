using System.ComponentModel.DataAnnotations;

namespace AutoScoutRent.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public DateOnly DateFrom { get; set; }
        public DateOnly DateTo { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        
       public int UserId { get; set; }
        public User User { get; set; }




    }
}
