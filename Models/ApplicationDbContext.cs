
using Microsoft.EntityFrameworkCore;
namespace Seat_Reservation.Models;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {


    }
    public DbSet<User> Users { get; set; }
    public DbSet<LoginDto> LoginDtos { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    public DbSet<PasswordChangeDto> PasswordChangeDtos { get; set; }


}