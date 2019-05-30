using System.Linq;
using AppMvcBasica.Models;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Contexts
{
    public class MeuDbContext : DbContext
    {
        public MeuDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Caso você queira padronizar o mapeamento de um determinado tipo siga esse padrão
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e=>e.GetProperties().Where(p=>p.ClrType==typeof(string))))
            {
                property.Relational().ColumnType = "varchar(100)";
            }
            //Aqui é aplicado os mappings com os Dbsets
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuDbContext).Assembly);
            
            //Desabilitando o comportamento padrão cascade, setando null nos filhos de uma entidade excluída (opcional)
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e=>e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
