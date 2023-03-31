using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALL.Models;
using DALL.Repositories;

namespace DALL.Services.Implements
{
    public class usuariosService : GenericService<usuarios>, IusuariosService
    {
        public usuariosService(IusuariosRepository usuariosRepository) : base(usuariosRepository)
        {

        }

    }
}
