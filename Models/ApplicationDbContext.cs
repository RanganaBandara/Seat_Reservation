
using Microsoft.EntityFrameworkCore;
namespace Seat_Reservation.Models;

public class ApplicationDbContext:DbContext
{

    public ApplicationDbContext(DbContextOptions options):base(options){


    }
    public DbSet<User>Users { get;set;}
    public DbSet<LoginDto>LoginDtos{get;set;}
}