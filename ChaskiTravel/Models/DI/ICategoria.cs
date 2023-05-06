namespace ChaskiTravel.Models.DI
{
    public interface ICategoria
    {
        IEnumerable<Categoria> listado();
        Categoria buscar(int id);
        string agregar(Categoria d);
        string actualizar(Categoria d);
        string eliminar(Object obj);

    }
}
