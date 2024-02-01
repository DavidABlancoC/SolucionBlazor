using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class HabitacionService : IHabitacionService
    {
        private readonly HttpClient _http;

        public HabitacionService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<HabitacionDTO>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<HabitacionDTO>>>("api/Habitacion/Lista");

            if (result!.EsCorrecto)
            {
                return result.Valor!;
            }
            else
            {
                throw new Exception(result.Mensaje);
            }
        }

        public async Task<HabitacionDTO> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<HabitacionDTO>>($"api/Habitacion/Buscar/{id}");

            if (result!.EsCorrecto)
            {
                return result.Valor!;
            }
            else
            {
                throw new Exception(result.Mensaje);
            }
        }

        public async Task<int> Guardar(HabitacionDTO habitacion)
        {
            var result = await _http.PostAsJsonAsync("api/Habitacion/Guardar", habitacion);
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

        public async Task<int> Editar(HabitacionDTO habitacion)
        {
            var result = await _http.PutAsJsonAsync($"api/Habitacion/Editar/{habitacion.IdHabitacion}", habitacion);
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
