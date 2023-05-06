using System.ComponentModel.DataAnnotations;

namespace ChaskiTravel.Models
{
    public class Categoria
    {
        /*IdCategoria int primary key not null,
NombreCategoria varchar(15) not null*/
        [Display(Name = "Código Categoria"), Required] public int IdCategoria { get; set; }
        [Display(Name = "Nombre"), Required] public String NombreCategoria { get; set; }

        public Categoria()
        {


            NombreCategoria = "";

        }
    }
}

