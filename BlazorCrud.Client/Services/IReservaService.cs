using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface IReservaService
    {
        Task<List<ReservaDTO>> Lista();

        Task<ReservaDTO> Buscar(int id);

        Task<int> Guardar(ReservaDTO habitacion);

        Task<int> Editar(ReservaDTO habitacion);
    }
}
