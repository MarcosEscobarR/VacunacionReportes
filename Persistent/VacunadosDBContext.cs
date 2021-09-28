using Microsoft.EntityFrameworkCore;
using VacunadosReporte.Entities;

namespace VacunadosReporte.Persistent
{
    public class VacunadosDBContext: DbContext
    {
        public VacunadosDBContext(DbContextOptions<VacunadosDBContext> options): base(options)
        {
            
        }
        public DbSet<People> People { get; set; }
    }
}