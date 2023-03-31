using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DALL.Models
{
    [Table("grupo_familiar", Schema = "dbo")]
    public class grupo_familiar
    {
        public string usuario { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string cedula { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string genero { get; set; }
        public string parentesco { get; set; }
        public decimal edad { get; set; }
        public string menor_edad { get; set; }
        public DateTime? fecha_nacimiento { get; set; }
        public DateTime fecsys { get; set; }
    }
}
