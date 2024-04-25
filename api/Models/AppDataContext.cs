namespace API.Models;
using Microsoft.EntityFrameworkCore;
//Classe que representa de entity framework core: code first 
public class AppDataContext : DbContext
{
    //representação das tabelas do banco de dados
    //classes que vão virar tabelas
    public DbSet<Produto> Produtos { get; set; }

    //override onconfig
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //configurar conexao com banco de dados
        optionsBuilder.UseSqlite("Data source=app.db");

    }
}
