using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALL.Models;
using DALL.Data;

namespace DALL.Repositories.Implements
{
    public class grupo_familiarRepository : GenericRepository<grupo_familiar> , Igrupo_familiarRepository
    {
        public grupo_familiarRepository(MidasoftContext midasoftContext) : base(midasoftContext)
        {
                
        }
    }
}
