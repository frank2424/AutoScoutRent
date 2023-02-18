using AutoScoutRent.Data;
using AutoScoutRent.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoScoutRent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {



        private readonly DataContext _dbContext;

        public BookingController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            if (_dbContext.Bookings == null)
            {
                return NotFound();
            }

            return await _dbContext.Bookings.ToListAsync();
        }

        [HttpPost]

        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {

            _dbContext.Bookings.Add(booking);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBookings), new { id = booking.Id }, booking);



        }

        [HttpPost]
        [Route("AddBooking")]
        public async Task<IActionResult> PostBookingV2(int car_id, string date_from, string date_to, int user_id)
        {
            // Convert the string dates to Date objects
            var dateFrom = DateOnly.Parse(date_from);
            var dateTo = DateOnly.Parse(date_to);

            // Retrieve the car from the database
            var car =  _dbContext.Cars.Where(c => c.Car_id == car_id).FirstOrDefault();
            var userExist = _dbContext.Users.Where(x => x.User_id == user_id).FirstOrDefault();

            if (car == null || userExist == null || car.Status == "Booked")
            {
                return NotFound();
            }


            // Create the new booking
            Booking booking = new Booking
            {
                DateFrom = dateFrom,
                DateTo = dateTo,
                User = userExist,
                Car = car,
                UserId= userExist.User_id,
                CarId=car.Car_id,

            };
            car.Status = "Booked";
            _dbContext.Bookings.Add(booking);
            await _dbContext.SaveChangesAsync();
            return Ok();
            //return CreatedAtAction(nameof(GetBookings), new
            //{
            //    id = booking.Id
            //}, booking);
        }







        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            if (_dbContext.Bookings == null)
            {
                return NotFound();
            }
            var booking = await _dbContext.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            _dbContext.Bookings.Remove(booking);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

    }

}

