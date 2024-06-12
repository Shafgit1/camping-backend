using Microsoft.AspNetCore.Mvc;
using my_weprog_backend.Data;
using my_weprog_backend.Models;
using System.Xml.Linq;

namespace my_weprog_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private IDataContext _data;

        public BookingController(IDataContext data)
        {
            _data = data;
        }

        [HttpPost("book")]
        public IActionResult BookCamping(Booking booking)
        {
            _data.AddBooking(booking);
            return Ok("Camping spot booked successfully.");
        }
        [HttpGet("getBookingsByUserId/{userId}")]
        public IActionResult GetBookingsByUserId(int userId)
        {
            var bookings = _data.GetBookingsByUserId(userId); 
            return Ok(bookings);
        }

        [HttpGet("getAllBookings")]
        public IActionResult GetAllBookings()
        {
            var bookings = _data.GetAllBookings();
            return Ok(bookings);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteBooking(int id)
        {
            try
            {
                _data.DeleteBooking(id);
                return Ok("Booking deleted successfully.");
            }
            catch (Exception)
            {
                return BadRequest("Unable to delete booking.");
            }
        }
    }
}