

using System.ComponentModel.DataAnnotations;


namespace Seat_Reservation.Models

{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        public int SeatId { get; set; }
        public Seat ? Seat { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime ReservationDate { get; set; }

    }
}
