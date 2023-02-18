using AutoScoutRent.Data;
using AutoScoutRent.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoScoutRent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {

        private readonly DataContext _dbContext;

        public CarController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            if (_dbContext.Cars == null)
            {
                return NotFound();
            }

            return await _dbContext.Cars.ToListAsync();
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Car>> GetCar(int id)
        {
            if (_dbContext.Cars == null)
            {
                return NotFound();
            }
            var car = await _dbContext.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return car;
        }
        [HttpPost]

        public async Task<IActionResult> PostCar(string make,string model,int cost,int capacity,string status,string imagePath)
        {
            Car car = new Car
            {
                Make = make,
                Model = model,
                Cost = cost,
                Capacity = capacity,
                Status = status,
                ImagePath = imagePath
            };
            _dbContext.Cars.Add(car);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCar), new { id = car.Car_id }, car);
        }



        private bool CarExists(int id)
        {
            return (_dbContext.Cars?.Any(e => e.Car_id == id)).GetValueOrDefault();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Car_id)
            {
                return BadRequest();
            }
            _dbContext.Entry(car).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _dbContext.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _dbContext.Cars.Remove(car);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }



    }
}
