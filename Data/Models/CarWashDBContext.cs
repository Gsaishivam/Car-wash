using Microsoft.EntityFrameworkCore; // for DbContext
namespace Car_wash.Models;

public class CarWashDBContext:DbContext
{
    public CarWashDBContext(DbContextOptions options):base(options)  //options is the name
    {
        
    }
    public virtual DbSet<Washers> Washers { get; set; }=null!;  // if null! then it will show warning in line 6
    public virtual DbSet<WashPackages> WashPackages { get; set; }=null!;    
    public virtual DbSet<Orders> Orders { get; set; }=null!;
    public virtual DbSet<Reviews> Reviews { get; set; }=null!;
    public virtual DbSet<Users> Users { get; set; }=null!;
    public virtual DbSet<Roles> Roles { get; set; }=null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
}