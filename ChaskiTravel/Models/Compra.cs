using System.ComponentModel.DataAnnotations;

namespace ChaskiTravel.Models
{
    // es el registro, donde se añadirá nuevos campos
    public class Compra
    {
       
      
        [Display(Name = "Código"), Required] public int codigo { get; set; }
        [Display(Name = "País"), Required] public string pais { get; set; }
        [Display(Name = "Ciudad"), Required] public string ciudad { get; set; }
        //[Display(Name = "Código Hotel"), Required] public int idHotel { get; set; }
        [Display(Name = "Hotel"), Required] public string nomHotel { get; set; }
        [Display(Name = "Categoria Hotel"), Required] public string categoriaHotel { get; set; }
        [Display(Name = "Precio Hotel"), Required] public Decimal precioHotel { get; set; }
     //   [Display(Name = "Código Categoria"), Required] public int IdCategoria { get; set; }
        [Display(Name = "Categoria"), Required] public string NombreCategoria { get; set; }

       // [Display(Name = "Código Tour"), Required] public int idTour { get; set; }
        [Display(Name = "Descripción Tour"), Required] public string descripcionTour { get; set; }

        [Display(Name = "Precio Tour"), Required] public Decimal precioTour { get; set; }

        [Display(Name = "Precio"), Required] public Decimal precio { get; set; }
        [Display(Name = "Unidades"), Required] public int UnidadesEnExistencia { get; set; }

        [Display(Name = "N° Personas"), Required] public int cantidad { get; set; }
        [Display(Name = "Unidades"), Required] public decimal monto { get { return (precioHotel + precioTour) * cantidad; } }

       
    }
}
