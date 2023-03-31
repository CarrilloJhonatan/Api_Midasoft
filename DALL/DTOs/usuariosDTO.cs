using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALL.DTOs
{
    public class usuariosDTO
    {
        [Required(ErrorMessage = "El campo usuario es Requerido")]
        public string usuario { get; set; }
        [Required(ErrorMessage = "El campo contrasena es Requerido")]
        public string contrasena { get; set; }
        public DateTime fecsys { get; set; }
    }
}
