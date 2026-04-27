using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Accounting.Application.DTOs.Requests.Configuration
{
    public class CreateCompanyRequest
    {
        [Display(Name = "Codigo de compañia")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "El campo {0} debe tener exactamente {1} caracteres")]
        public string code { get; set; }

        [Display(Name = "Razon social")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} debe ser menor igual a {1} caracteres")]
        public string company_name { get; set; }

        [Display(Name = "Direccion")]
        [StringLength(maximumLength: 200, ErrorMessage = "El campo {0} debe ser menor igual a {1} caracteres")]
        public string address { get; set; }

        [Display(Name = "Ruc")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "El campo {0} debe tener exactamente {1} caracteres")]
        public string ruc { get; set; }

        [Display(Name = "Correo")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
        [StringLength(maximumLength: 40, ErrorMessage = "El campo {0} debe ser menor igual a {1} caracteres")]
        public string? email { get; set; }

        [Display(Name = "Telefono")]
        [StringLength(maximumLength: 25, ErrorMessage = "El campo {0} debe ser menor igual a {1} caracteres")]
        public string? phone { get; set; }
        public byte[]? logo { get; set; }
    }
}
