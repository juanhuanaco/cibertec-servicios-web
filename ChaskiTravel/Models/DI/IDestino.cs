namespace ChaskiTravel.Models.DI
{
    public interface IDestino
    {
        IEnumerable<Destino> listado();
        Destino buscar(int id);
        string agregar(Destino d);
        string actualizar(Destino d);
        string eliminar(Object obj);

    }
}
