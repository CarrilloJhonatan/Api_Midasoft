using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALL.Models;

namespace DALL.Data
{
    public class MidasoftContext : DbContext
    {
        private static MidasoftContext midasoftContext = null;
        public MidasoftContext()
            : base("MidasoftContext")
        {

        }
        public DbSet<grupo_familiar> grupo_familiars { get; set; }
        public DbSet<usuarios> usuarioss { get; set; }

        public static MidasoftContext Create()
        {
            //if (midasoftContext == null)
            //midasoftContext = new MidasoftContext();

            return new MidasoftContext();
        }
    }
}
