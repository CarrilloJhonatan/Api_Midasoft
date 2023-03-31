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
        public MidasoftContext()
            : base("MidasoftContext")
        {

        }
        public DbSet<grupo_familiar> grupo_familiars { get; set; }
        public DbSet<usuarios> usuarioss { get; set; }

        public static MidasoftContext Create()
        {
            return new MidasoftContext();
        }
    }
}
