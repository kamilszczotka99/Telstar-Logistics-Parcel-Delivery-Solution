using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Telstar_Logistics_Parcel_Delivery_Solution.Data;
using Telstar_Logistics_Parcel_Delivery_Solution.Models;

namespace Telstar_Logistics_Parcel_Delivery_Solution.Controllers
{

    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("booking")]
        public Booking BookingGet([FromBody] Booking booking)
        {
            var book = _context.Bookings.FirstOrDefault(x => x.Email == booking.Email);
            {
                if (book != null)
                {
                    _context.Add(book);
                    _context.SaveChanges();
                    return book;
                }
                else return null;
            }


        }
    }
}



