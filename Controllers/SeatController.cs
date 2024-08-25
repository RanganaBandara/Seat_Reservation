using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Seat_Reservation.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seat_Reservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SeatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Seats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seat>>> GetSeats()
        {
            return await _context.Seats.Include(s => s.Reservation).ToListAsync();
        }

        // GET: api/Seats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Seat>> GetSeat(int id)
        {
            var seat = await _context.Seats.Include(s => s.Reservation).FirstOrDefaultAsync(s => s.SeatId == id);

            if (seat == null)
            {
                return NotFound();
            }

            return seat;
        }

        // POST: api/Seats/Reserve
        [HttpPost("Reserve")]
        public async Task<ActionResult<Seat>> ReserveSeat([FromBody] Reservation reservation)
        {
            var seat = await _context.Seats.FindAsync(reservation.SeatId);

            if (seat == null || seat.IsReserved)
            {
                return BadRequest("Seat is not available.");
            }

            seat.IsReserved = true;
            reservation.Seat = seat;  // Assign the seat to the reservation
            seat.Reservation = reservation;

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSeat), new { id = seat.SeatId }, seat);
        }

        // DELETE: api/Seats/CancelReservation/5
        [HttpDelete("CancelReservation/{id}")]
        public async Task<IActionResult> CancelReservation(int id)
        {
            var reservation = await _context.Reservations.Include(r => r.Seat).FirstOrDefaultAsync(r => r.ReservationId == id);

            if (reservation == null)
            {
                return NotFound();
            }

            var seat = reservation.Seat;
            seat.IsReserved = false;
            seat.Reservation = null;

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
