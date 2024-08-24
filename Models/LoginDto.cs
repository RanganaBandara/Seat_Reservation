using System.ComponentModel.DataAnnotations;

namespace Seat_Reservation.Models;

public class LoginDto{

    [Key]
    public int Id { get; set; }
    public string Email{get;set;}=string.Empty;
    public string Password{get;set;}=string.Empty;
}