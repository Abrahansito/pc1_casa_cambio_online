using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CASA_CAMBIO.Models
{
    public class CambioModel
    {
       [Required(ErrorMessage = "Seleccione moneda de origen")]
        public string? MonedaOrigen { get; set; }

        [Required(ErrorMessage = "Seleccione moneda de destino")]
        public string? MonedaDestino { get; set; }

        [Required(ErrorMessage = "Ingrese una cantidad")]
        [Range(0.01, double.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public decimal Cantidad { get; set; }

        public decimal Resultado { get; set; }
        public decimal TipoCambio { get; set; }
    }

}