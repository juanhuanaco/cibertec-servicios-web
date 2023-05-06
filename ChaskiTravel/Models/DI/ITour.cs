namespace ChaskiTravel.Models.DI
{
    public interface ITour
    {
        IEnumerable<Tour> listado();
        Tour buscar(int id);
        string agregar(Tour t);

        string actualizar(Tour t);

        string eliminar(Object obj);

    }
}

