using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALL.Models;
using DALL.Data;

namespace DALL.Repositories.Implements
{
    public class usuariosRepository : GenericRepository<usuarios>
    {
        public usuariosRepository(MidasoftContext midasoftContext) : base(midasoftContext)
        {
                
        }
    }
}
