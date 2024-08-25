using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Seat_Reservation.Models
{
    public class Seat
    {

        [Key]
        public int SeatId { get; set; }
        public string SeatNumber { get; set; }
        public bool IsReserved { get; set; }
        [JsonIgnore] // Prevents circular reference during serialization
        public Reservation ? Reservation { get; set; }
    }
}
