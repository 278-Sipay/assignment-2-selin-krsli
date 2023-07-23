using Microsoft.EntityFrameworkCore;

namespace SWFilterProject.Data;

public class FilterApiDbContext : DbContext
{
    public FilterApiDbContext(DbContextOptions<FilterApiDbContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-2PUAJM9;Database=FilterApiDb;User Id=selin;Password=selin123456;Encrypt=False");
    }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
