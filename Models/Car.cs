using System.ComponentModel.DataAnnotations;


namespace AutoScoutRent.Models
{
    public class Car
    {

        [Key]
        public int Car_id { get; set; }
        public string Make { get; set; }

        public string Model { get; set; }

        public int Cost { get; set; }
        
        public int Capacity { get; set; }

        public string Status {get; set; }
        public string ImagePath { get; set; }


        public Booking? Booking { get; set; }

    }
}
