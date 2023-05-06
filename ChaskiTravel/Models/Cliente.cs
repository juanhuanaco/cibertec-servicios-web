using System.ComponentModel.DataAnnotations;

namespace ChaskiTravel.Models
{
    public class Cliente
    {
        [Display(Name = "N°"), Required] public int idpedido { get; set; }
        [Display(Name = "Fecha"), Required] public DateTime fpedido { get; set; }

        [Display(Name = "Nombre"), Required] public string nombre { get; set; }
        [Display(Name = "Apellido Paterno"), Required] public string apePaterno { get; set; }
        [Display(Name = "Apellido Materno"), Required] public string apeMaterno { get; set; }
        [Display(Name = "DNI"), Required] public string dni { get; set; }
        [Display(Name = "Celular"), Required] public int telefono { get; set; }
        [Display(Name = "Correo"), Required] public string email { get; set; }        
      
    }
}
