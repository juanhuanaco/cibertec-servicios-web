namespace ChaskiTravel.Models.DI
{
    public interface IHotel
    {
        IEnumerable<Hotel> listado();
        Hotel buscar(int id);
        string agregar(Hotel h);
        string actualizar(Hotel h);
        string eliminar(Object obj);

    }
}

