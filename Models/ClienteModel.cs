using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CASA_CAMBIO.Models
{
    public class ClienteModel
    {
        [Required(ErrorMessage = "Nombre completo es requerido")]
        public string? NombreCompleto { get; set; }

        [Required(ErrorMessage = "DNI es requerido")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "DNI debe tener 8 dígitos")]
        public string? DNI { get; set; }

        [Required(ErrorMessage = "Email es requerido")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string? Email { get; set; }

        public CambioModel? Transaccion { get; set; }
    }
}