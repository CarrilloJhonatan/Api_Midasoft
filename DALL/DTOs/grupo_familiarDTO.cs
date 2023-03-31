using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DALL.DTOs
{
    public class grupo_familiarDTO
    {
        [Required(ErrorMessage = "El campo usuario es Requerido")]
        public string usuario { get; set; }

        [Required(ErrorMessage = "El campo cedula es Requerido")]
        [StringLength(15)]
        public string cedula { get; set; }

        [Required(ErrorMessage = "El campo nombres es Requerido")]
        public string nombres { get; set; }

        [Required(ErrorMessage = "El campo apellidos es Requerido")]
        public string apellidos { get; set; }
        public string genero { get; set; }
        public string parentesco { get; set; }

        [Required(ErrorMessage = "El campo edad es Requerido")]
        public decimal edad { get; set; }
        public string menor_edad { get; set; }
        //[Required(ErrorMessage = "Es menor de edad, El campo fecha_nacimiento es Requerido")]
        public DateTime? fecha_nacimiento { get; set; }
        public DateTime fecsys { get; set; }
    }
}
