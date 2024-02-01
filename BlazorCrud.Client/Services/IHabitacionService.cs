using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface IHabitacionService
    {
        Task<List<HabitacionDTO>> Lista();

        Task<HabitacionDTO> Buscar(int id);

        Task<int> Guardar(HabitacionDTO habitacion);

        Task<int> Editar(HabitacionDTO habitacion);
    }
}
