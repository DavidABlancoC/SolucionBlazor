using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface IHotelService
    {
        Task<List<HotelDTO>> Lista();

        Task<HotelDTO> Buscar(int id);

        Task<int> Guardar(HotelDTO hotel);

        Task<int> Editar(HotelDTO hotel);
    }
}
