using ComShopApp.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComShopApp.DataAccess;

public class ShopOutDBContext : DbContext
{
    public DbSet<PurchaseItem> Purchases { get; set; }

    private readonly IDBPathService _dbPathService;

    public ShopOutDBContext(IDBPathService dbPathService)
    {
        _dbPathService = dbPathService;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = $"Filename={_dbPathService.GetPath("shopdatabase.db")}";
        optionsBuilder.UseSqlite(connectionString);
    }
}

public record PurchaseItem(int ClientId, int ProductId, int Quantity, decimal Price)
{
    public int Id { get; set; }
}
