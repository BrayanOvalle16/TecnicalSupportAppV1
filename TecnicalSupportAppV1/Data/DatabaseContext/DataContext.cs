using TecnicalSupportAppV1.Api.Models;

namespace TecnicalSupportAppV1.Data.DatabaseContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<ContactInformation> ContactInformations { get; set; }
        public DbSet<ServiceOrder> ServiceOrders { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Technician> Technicians { get; set; }
        public DbSet<Roles> Roles { get; set; }
    }
}
