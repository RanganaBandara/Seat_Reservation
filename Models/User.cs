using System.ComponentModel.DataAnnotations;

namespace Seat_Reservation.Models;

public class User{

    [Key]
    public int User_Id { get; set; }
    public required string Email{ get; set; }
    
    public required string password { get; set; }

    public required string phone_number { get; set; }

    public  DateTime Created_Time{get;set;}=DateTime.Now;

    public DateTime Update_Time{get;set;}




}