using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Caso1NET
{

    
    public class Canton
    {
        public int ID{get; set;}
        public string Nombre { get; set; }
        
           }
    public class Partido 
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
    }
    public class Accion 
    {
        public int ID { get; set; }
        public int ID_Partido { get; set; }
        public string Descripcion { get; set; }
    }

    public class Entregable { 
        public int ID { get; set; }
        public int ID_Accion { get; set; }
        public int ID_Partido { get; set; }
        public int KPI { get; set; }
        public string Ente_KPI { get; set; }
        public string Descripcion { get; set; }
        public DateTime Entrega { get; set; }

    }
    internal class Consulta3 : DbContext
    {
        public Consulta3(DbContextOptions<Consulta3> options) : base(options)
        {

        }
        public DbSet<Partido> CatalogPartidos { get; set; }
        public DbSet<Canton> CatalogCantones { get; set; }
        public DbSet<Entregable> CatalogEntregables { get; set; }
    }
}
