using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALL.Models
{
    [Table("usuarios", Schema = "dbo")]
    public class usuarios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string usuario { get; set; }
        public string contrasena { get; set; }
        public DateTime fecsys { get; set; }
    }
}
