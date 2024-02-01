﻿using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly HttpClient _http;

        public UsuarioService(HttpClient http) 
        {
            _http = http;
        }

        public async Task<List<UsuarioDTO>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<UsuarioDTO>>>("api/Usuario/Lista");

            if (result!.EsCorrecto)
            {
                return result.Valor!;
            }
            else
            {
                throw new Exception(result.Mensaje);
            }

        }

        public async Task<UsuarioDTO> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<UsuarioDTO>>($"api/Usuario/Buscar/{id}");

            if (result!.EsCorrecto)
            {
                return result.Valor!;
            }
            else
            {
                throw new Exception(result.Mensaje);
            }
        }

        public async Task<int> Guardar(UsuarioDTO usuario)
        {
            var result = await _http.PostAsJsonAsync("api/Usuario/Guardar", usuario);
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


        public async Task<int> Editar(UsuarioDTO usuario)
        {
            var result = await _http.PutAsJsonAsync($"api/Usuario/Editar/{usuario.IdUsuario}", usuario);
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

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/Usuario/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
            {
                return response.EsCorrecto!;
            }
            else
            {
                throw new Exception(response.Mensaje);
            }
        }     
    }
}
