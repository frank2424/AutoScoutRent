using AutoScoutRent.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace AutoScoutRent.Data
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
   

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {   
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
         }
        
        
             protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Booking>()
            //   .HasOne(b => b.User)
            //   .WithMany(u => u.Bookings)
            //   .HasForeignKey(b => b.User.User_id);

            //modelBuilder.Entity<Car>()
            //    .HasOne(c => c.Booking)
            //    .WithOne(b => b.Car)    
            //    .HasForeignKey<Booking>(b => b.Car.Car_id);
            modelBuilder.Entity<Booking>()
        .HasOne(b => b.User)
        .WithMany(u => u.Bookings)
        .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Car)
                .WithOne(c => c.Booking)
                .HasForeignKey<Booking>(b => b.CarId);


            modelBuilder.UseSerialColumns();
        }
   
        
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}