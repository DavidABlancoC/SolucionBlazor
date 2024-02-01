using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class ReservaService : IReservaService
    {
        private readonly HttpClient _http;

        public ReservaService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ReservaDTO>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<ReservaDTO>>>("api/Reserva/Lista");

            if (result!.EsCorrecto)
            {
                return result.Valor!;
            }
            else
            {
                throw new Exception(result.Mensaje);
            }
        }

        public async Task<ReservaDTO> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<ReservaDTO>>($"api/Reserva/Buscar/{id}");

            if (result!.EsCorrecto)
            {
                return result.Valor!;
            }
            else
            {
                throw new Exception(result.Mensaje);
            }
        }

        public async Task<int> Guardar(ReservaDTO reserva)
        {
            var result = await _http.PostAsJsonAsync("api/Reserva/Guardar", reserva);
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

        public async Task<int> Editar(ReservaDTO reserva)
        {
            var result = await _http.PutAsJsonAsync($"api/Reserva/Editar/{reserva.IdReserva}", reserva);
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
