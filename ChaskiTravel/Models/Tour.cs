using System.ComponentModel.DataAnnotations;

namespace ChaskiTravel.Models
{
    public class Tour
    {
        /*idTour char (5) primary key not null,
precio decimal (7,2) not null,
descripcion nvarchar(100) not null

        examples display : 
          [Display(Name = "Usuario"),Required]public string login { get; set; }

    [Display(Name ="Password"),Required,StringLength(10)]public string clave { get; set; }
        */
        [Display(Name = "Código"), Required] public int idTour { get; set; }
        [Display(Name = "Precio"), Required] public Decimal precioTour { get; set; }
        [Display(Name = "Descripción"), Required] public String descripcionTour { get; set; }

 
    }
}

