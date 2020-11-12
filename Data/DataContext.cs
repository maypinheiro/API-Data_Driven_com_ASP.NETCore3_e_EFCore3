//Representação do Banco de Dados em Memória !
//Permite que seja feito o Mapeamento a orientacao de nossa aplicação em relacao ao banco de dados.

/*  precisamos instalar OS PACOTE
dotnet add package Micosoft.EntityFrameworkCore.InMemory 
dotnet add package Micosoft.EntityFrameworkCore.sqlServe
 */

using Microsoft.EntityFrameworkCore; 
using Shoop.Models;

namespace Shoop.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {

        }


        //Dbset é a representação da nossas Tabelas em MEMÓRIA .
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
