using Microsoft.EntityFrameworkCore;
using IRIS_Web_Rec25_241EC152.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IRIS_Web_Rec25_241EC152.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Sports Infrastructure Entities
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Court> Courts { get; set; }
        public DbSet<EquipmentBooking> EquipmentBookings { get; set; }
        public DbSet<CourtBooking> CourtBookings { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial data
            modelBuilder.Entity<Equipment>().HasData(
                new Equipment { Id = 1, Name = "Cricket Bat", Category = "Cricket", Status = "Available", TotalQuantity = 14, AvailableQuantity = 14, Condition = "Good" },
                new Equipment { Id = 2, Name = "Football", Category = "Football", Status = "Available", TotalQuantity = 10, AvailableQuantity = 14, Condition = "Good" },
                new Equipment { Id = 3, Name = "Badminton Racket", Category = "Badminton", Status = "Available", TotalQuantity = 8, AvailableQuantity = 14, Condition = "Good" },
                new Equipment { Id = 4, Name = "Tennis Racket", Category = "Tennis", Status = "Available", TotalQuantity = 4, AvailableQuantity = 14, Condition = "Slightly Used" },
                new Equipment { Id = 5, Name = "Basketball", Category = "Basketball", Status = "Available", TotalQuantity = 6, AvailableQuantity = 14, Condition = "New" },
                new Equipment { Id = 6, Name = "Table Tennis Paddle", Category = "Table Tennis", Status = "Available", TotalQuantity = 12, AvailableQuantity = 14, Condition = "Good" },
                new Equipment { Id = 8, Name = "Volleyball", Category = "Volleyball", Status = "Available", TotalQuantity = 4, AvailableQuantity = 14, Condition = "New" },
                new Equipment { Id = 9, Name = "Skipping Rope", Category = "Fitness", Status = "Available", TotalQuantity = 10, AvailableQuantity = 14, Condition = "Good" },
                new Equipment { Id = 10, Name = "Dumbbells (5kg)", Category = "Fitness", Status = "Available", TotalQuantity = 5, AvailableQuantity = 14, Condition = "New" }
            );

            modelBuilder.Entity<Court>().HasData(
            new Court
            {
                Id = 1,
                Name = "Badminton Court 1",
                Location = "Sports Complex - North Wing",
                Type = "Badminton",
                Capacity = 4,
                OpeningTime = TimeSpan.FromHours(8),  // 8:00 AM
                ClosingTime = TimeSpan.FromHours(22), // 10:00 PM
                Availability = CourtAvailability.Available
            },
            new Court
            {
                Id = 2,
                Name = "Badminton Court 2",
                Location = "Sports Complex - North Wing",
                Type = "Badminton",
                Capacity = 4,
                OpeningTime = TimeSpan.FromHours(9),  // 9:00 AM
                ClosingTime = TimeSpan.FromHours(21), // 9:00 PM
                Availability = CourtAvailability.UnderMaintenance
            },
            new Court
            {
                Id = 3,
                Name = "Tennis Court 1",
                Location = "Outdoor Facility - East Side",
                Type = "Tennis",
                Capacity = 2,
                OpeningTime = TimeSpan.FromHours(7),  // 7:00 AM
                ClosingTime = TimeSpan.FromHours(20), // 8:00 PM
                Availability = CourtAvailability.Available
            },
            new Court
            {
                Id = 4,
                Name = "Basketball Court",
                Location = "Main Arena",
                Type = "Basketball",
                Capacity = 10,
                OpeningTime = TimeSpan.FromHours(6),  // 6:00 AM
                ClosingTime = TimeSpan.FromHours(23), // 11:00 PM
                Availability = CourtAvailability.Available
            },
            new Court
            {
                Id = 5,
                Name = "Squash Court 1",
                Location = "Indoor Facility - Level 2",
                Type = "Squash",
                Capacity = 2,
                OpeningTime = TimeSpan.FromHours(10), // 10:00 AM
                ClosingTime = TimeSpan.FromHours(22), // 10:00 PM
                Availability = CourtAvailability.Reserved
            },
            new Court
            {
                Id = 6,
                Name = "Table Tennis Area",
                Location = "Recreation Zone",
                Type = "Table Tennis",
                Capacity = 2,
                OpeningTime = TimeSpan.FromHours(8),  // 8:00 AM
                ClosingTime = TimeSpan.FromHours(20), // 8:00 PM
                Availability = CourtAvailability.Available
            },
            new Court
            {
                Id = 7,
                Name = "Outdoor Tennis Court",
                Location = "South Field",
                Type = "Tennis",
                Capacity = 2,
                OpeningTime = TimeSpan.FromHours(8),  // 8:00 AM
                ClosingTime = TimeSpan.FromHours(18), // 6:00 PM
                Availability = CourtAvailability.Closed
            });
        }
    }
}
