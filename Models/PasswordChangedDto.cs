using System.ComponentModel.DataAnnotations;

namespace Seat_Reservation.Models;

public class PasswordChangeDto(){

    [Key]
    public int User_Id { get; set; }
    public required string Email{ get; set; }
    public required string phone_number { get; set; }

}