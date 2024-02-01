using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class HotelService : IHotelService
    {
        private readonly HttpClient _http;

        public HotelService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<HotelDTO>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<HotelDTO>>>("api/Hotel/Lista");

            if (result!.EsCorrecto)
            {
                return result.Valor!;
            }
            else
            {
                throw new Exception(result.Mensaje);
            }
        }

        public async Task<HotelDTO> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<HotelDTO>>($"api/Hotel/Buscar/{id}");

            if (result!.EsCorrecto)
            {
                return result.Valor!;
            }
            else
            {
                throw new Exception(result.Mensaje);
            }
        }

        public async Task<int> Guardar(HotelDTO hotel)
        {
            var result = await _http.PostAsJsonAsync("api/Hotel/Guardar", hotel);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
            {
                return response.Valor!;
            }
            else
            {
                throw new Exception(response.Mensaje);
            }
        }

        public async Task<int> Editar(HotelDTO hotel)
        {
            var result = await _http.PutAsJsonAsync($"api/Hotel/Editar/{hotel.IdHotel}", hotel);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
            {
                return response.Valor!;
            }
            else
            {
                throw new Exception(response.Mensaje);
            }
        }
    }
}
