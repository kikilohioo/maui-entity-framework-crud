using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ComShopApp.DataAccess;

public class ShopDBContext : DbContext
{
    // buena practica hacerlo en este formato y Categories es el nobre que va a heredar para SQL
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Client> Clients { get; set; }
    //public DbSet<Purchase> Purchases { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("ShopComputer");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasData(
            new Category(1, "Electronicos"),
            new Category(2, "Computadoras"),
            new Category(3, "Dispositivos de escritorio"),
            new Category(4, "Telefonos Moviles"),
            new Category(5, "Audio y microfonos"),
            new Category(6, "Artefactos del hogar"),
            new Category(7, "Juguetes y juegos")
            );

        modelBuilder.Entity<Product>()
            .HasData(
                new Product(1, "Laptop Dell XPS 15", "Laptop de alto rendimiento para trabajo y gaming", 2500.00m, 2),
                new Product(2, "iPhone 14 Pro", "Teléfono móvil con excelente cámara y rendimiento", 1200.00m, 4),
                new Product(3, "Teclado Mecánico RGB", "Teclado para gaming con iluminación LED", 150.00m, 3),
                new Product(4, "Audífonos Sony WH-1000XM4", "Audífonos con cancelación de ruido", 350.00m, 5),
                new Product(5, "Monitor LG Ultrawide 34''", "Monitor ultra ancho para productividad", 600.00m, 3),
                new Product(6, "Robot Aspiradora Xiaomi", "Aspiradora inteligente para el hogar", 300.00m, 6),
                new Product(7, "PlayStation 5", "Consola de videojuegos de última generación", 500.00m, 7),
                new Product(8, "Reloj Inteligente Garmin", "Reloj deportivo con GPS y métricas avanzadas", 250.00m, 10),
                new Product(9, "Tablet Samsung Galaxy Tab S8", "Tablet con pantalla AMOLED y S-Pen incluido", 800.00m, 4),
                new Product(10, "Impresora Epson EcoTank L3250", "Impresora multifuncional con sistema de tinta continua", 200.00m, 3),
                new Product(11, "Cámara Canon EOS R6", "Cámara profesional con grabación 4K", 2500.00m, 1),
                new Product(12, "Router WiFi 6 ASUS", "Router de alta velocidad con tecnología WiFi 6", 180.00m, 1),
                new Product(13, "Disco Duro Externo 2TB Seagate", "Almacenamiento portátil con gran capacidad", 100.00m, 2),
                new Product(14, "Mouse Logitech MX Master 3", "Mouse inalámbrico ergonómico para productividad", 120.00m, 3),
                new Product(15, "TV Samsung QLED 55''", "Televisor con tecnología QLED y resolución 4K", 900.00m, 6),
                new Product(16, "Nintendo Switch OLED", "Consola híbrida con pantalla OLED mejorada", 350.00m, 7),
                new Product(17, "SSD NVMe 1TB Samsung 980 Pro", "Almacenamiento ultrarrápido para PC y consolas", 200.00m, 2),
                new Product(18, "GoPro Hero 11 Black", "Cámara de acción con estabilización avanzada", 500.00m, 1)
            );

        modelBuilder.Entity<Client>()
            .HasData(
                new Client(1, "Juan Pérez", "Av. Principal 123"),
                new Client(2, "María López", "Calle Secundaria 456"),
                new Client(3, "Carlos Ramírez", "Boulevard Central 789"),
                new Client(4, "Ana Torres", "Pasaje Residencial 101"),
                new Client(5, "Pedro Gómez", "Zona Comercial 202")
            );

    }
}

// se utilizan records porque son diseñados solo para gestionar datos, es mejor que las clases en este caso de uso
public record Category(int Id, string Name);

public record Product(int Id, string Name, string Description, decimal Price, int CategoryId)
{
    public Category Category { get; set; }
}

public record Client(int Id, string Name, string Address);

public record Purchase(
    [property: JsonPropertyName("clientId")] int ClientId,
    [property: JsonPropertyName("productId")] int ProductId,
    [property: JsonPropertyName("cantidad")] int Quantity,
    [property: JsonPropertyName("productoNombre")] string ProductName,
    [property: JsonPropertyName("productoPrecio")] decimal ProductPrice,
    [property: JsonPropertyName("total")] decimal Total
    );