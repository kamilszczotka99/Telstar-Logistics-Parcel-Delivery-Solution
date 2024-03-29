﻿using Microsoft.EntityFrameworkCore;
using Telstar_Logistics_Parcel_Delivery_Solution.Models;

namespace Telstar_Logistics_Parcel_Delivery_Solution.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> CITY { get; set; }
        public DbSet<Edge> Edges { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Session> Sessions { get; set; }

    }
}
