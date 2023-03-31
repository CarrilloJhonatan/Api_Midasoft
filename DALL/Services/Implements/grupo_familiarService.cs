using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALL.Models;
using DALL.Repositories;

namespace DALL.Services.Implements
{
    public class grupo_familiarService : GenericService<grupo_familiar>
    {
        public grupo_familiarService(Igrupo_familiarRepository grupo_familiarRepository) : base(grupo_familiarRepository)
        {
                
        }
    }
}
