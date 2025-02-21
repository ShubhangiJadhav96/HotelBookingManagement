using HotelApplication.HotelServices;
using HotelDomain.HotelModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelDomain.HotelModel;
using HotelApplication.HotelServices;

[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost("AddBooking")]
    public async Task<IActionResult> AddBooking([FromBody] Booking booking)
    {
        if (booking == null)
            return BadRequest("Invalid booking data.");

        var result = await _bookingService.AddBookingAsync(booking);
        if (result)
            return Ok(new { message = "Booking added successfully." });

        return StatusCode(500, "Error adding booking.");
    }

    [HttpGet("GetBookings")]
    public async Task<IActionResult> GetBookings()
    {
        var bookings = await _bookingService.GetAllBookingsAsync();
        return Ok(bookings);
    }

    [HttpGet("GetBookingById/{id}")]
    public async Task<IActionResult> GetBookingById(int id)
    {
        var booking = await _bookingService.GetBookingByIdAsync(id);
        if (booking == null)
            return NotFound("Booking not found.");

        return Ok(booking);
    }
}
