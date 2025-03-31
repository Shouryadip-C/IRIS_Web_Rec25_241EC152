using Microsoft.EntityFrameworkCore;
using IRIS_Web_Rec25_241EC152.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IRIS_Web_Rec25_241EC152.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Sports Infrastructure Entities
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Court> Courts { get; set; }
        public DbSet<EquipmentBooking> EquipmentBookings { get; set; }
        public DbSet<CourtBooking> CourtBookings { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial data
            modelBuilder.Entity<Equipment>().HasData(
                new Equipment { Id = 1, Name = "Cricket Bat", Type = "Cricket", Status = Status.Available, Quantity = 14, Condition = "Good" },
                new Equipment { Id = 2, Name = "Football", Type = "Football", Status = Status.Available, Quantity = 10, Condition = "Good" },
                new Equipment { Id = 3, Name = "Badminton Racket", Type = "Badminton", Status = Status.Available, Quantity = 8, Condition = "Good" },
                new Equipment { Id = 4, Name = "Tennis Racket", Type = "Tennis", Status = Status.Available, Quantity = 4, Condition = "Slightly Used" },
                new Equipment { Id = 5, Name = "Basketball", Type = "Basketball", Status = Status.Available, Quantity = 6, Condition = "New" },
                new Equipment { Id = 6, Name = "Table Tennis Paddle", Type = "Table Tennis", Status = Status.Available, Quantity = 12, Condition = "Good" },
                new Equipment { Id = 8, Name = "Volleyball", Type = "Volleyball", Status = Status.Available, Quantity = 4, Condition = "New" },
                new Equipment { Id = 9, Name = "Skipping Rope", Type = "Fitness", Status = Status.Available, Quantity = 10, Condition = "Good" },
                new Equipment { Id = 10, Name = "Dumbbells (5kg)", Type = "Fitness", Status = Status.Available, Quantity = 5, Condition = "New" }
            );

            modelBuilder.Entity<Court>().HasData(
            new Court
            {
                Id = 1,
                Name = "Badminton Court 1",
                Location = "Old Sports Complex",
                Type = "Badminton",
                Capacity = 4,
                OpeningTime = TimeSpan.FromHours(7),  
                ClosingTime = TimeSpan.FromHours(22),
                Availability = CourtAvailability.Available
            },
            new Court
            {
                Id = 2,
                Name = "Badminton Court 2",
                Location = "Old Sports Complex",
                Type = "Badminton",
                Capacity = 4,
                OpeningTime = TimeSpan.FromHours(7),  
                ClosingTime = TimeSpan.FromHours(21),
                Availability = CourtAvailability.Available
            },
            new Court
            {
                Id = 3,
                Name = "Badminton Court 3",
                Location = "Old Sports Complex",
                Type = "Badminton",
                Capacity = 4,
                OpeningTime = TimeSpan.FromHours(7),  
                ClosingTime = TimeSpan.FromHours(21),
                Availability = CourtAvailability.UnderMaintenance
            },
            new Court
            {
                Id = 4,
                Name = "Tennis Court 1",
                Location = "Opposite Mechaical Department",
                Type = "Tennis",
                Capacity = 2,
                OpeningTime = TimeSpan.FromHours(7),  
                ClosingTime = TimeSpan.FromHours(20), 
                Availability = CourtAvailability.Available
            },
            new Court
            {
                Id = 5,
                Name = "Basketball Court",
                Location = "Opposite Mechaical Department",
                Type = "Basketball",
                Capacity = 10,
                OpeningTime = TimeSpan.FromHours(6),  
                ClosingTime = TimeSpan.FromHours(21), 
                Availability = CourtAvailability.Available
            },
            new Court
            {
                Id = 6,
                Name = "Squash Court 1",
                Location = "New Sports Complex",
                Type = "Squash",
                Capacity = 2,
                OpeningTime = TimeSpan.FromHours(10),
                ClosingTime = TimeSpan.FromHours(22),
                Availability = CourtAvailability.Reserved
            },
            new Court
            {
                Id = 7,
                Name = "Table Tennis Area",
                Location = "New Sports Complex",
                Type = "Table Tennis",
                Capacity = 2,
                OpeningTime = TimeSpan.FromHours(8),  
                ClosingTime = TimeSpan.FromHours(20), 
                Availability = CourtAvailability.Available
            },
            new Court
            {
                Id = 8,
                Name = "Kabaddi Room",
                Location = "New Sports Complex",
                Type = "Kabaddi",
                Capacity = 2,
                OpeningTime = TimeSpan.FromHours(8),  
                ClosingTime = TimeSpan.FromHours(18),
                Availability = CourtAvailability.Closed
            });
        }
    }
}
