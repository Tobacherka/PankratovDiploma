using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace ApiPunk.Classes
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString;

        public ApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>()
                .ToTable(tb => tb.HasTrigger("trg_UpdateTotalCost_InsertUpdate"))
                .ToTable(tb => tb.HasTrigger("trg_UpdateTotalCost_Delete"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }

    [Table("Users", Schema = "Shop")]
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserPatronymic { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateBirhday { get; set; }
        public DateTime RegistrationDate { get; set; }
    }

    [Table("Products", Schema = "Shop")]
    public class Product
    {
        [Key]
        public int Id { get; set; } // Первичный ключ
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public byte[]? image { get; set; }
    }

    [Table("Orders", Schema = "Shop")]
    public class Order
    {
        public int UserID { get; set; }
        [Key]
        public int OrderID { get; set; }
        public DateTime Date { get; set; }
        public string? Status { get; set; }
        public decimal? TotalCost { get; set; }
        public string? DeliveryMethod { get; set; }
        public string? UserFullName { get; set; }
        public string? UserPhone { get; set; }
        public string? UserEmail { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Address { get; set; }
    }

    [Table("OrderDetails", Schema = "Shop")]
    public class OrderDetail
    {
        [Key]
        public int RecordID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
