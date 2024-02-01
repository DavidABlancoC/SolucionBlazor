using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// Referencias locales

using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // Servicio de base de datos

        private readonly DbcrudHoteleriaContext _dbContext;

        public UsuarioController(DbcrudHoteleriaContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Servicio de Lista de Usuarios

        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Lista()
        {
            var responseAPI = new ResponseAPI<List<UsuarioDTO>>();
            var listaUsuarioDTO = new List<UsuarioDTO>();

            try
            {
                foreach (var item in await _dbContext.Usuarios.ToListAsync())
                {
                    listaUsuarioDTO.Add(new UsuarioDTO
                    {
                        IdUsuario = item.IdUsuario,
                        Nombre = item.Nombre,
                        Apellidos = item.Apellidos,
                        Mail = item.Mail,
                        Direccion = item.Direccion,                   
                    });
                }

                responseAPI.EsCorrecto = true;
                responseAPI.Valor = listaUsuarioDTO;
            }
            catch (Exception ex)
            {
                responseAPI.EsCorrecto = false;
                responseAPI.Mensaje = ex.Message;
            }

            return Ok(responseAPI);
        }

        // Servicio de Busqueda de Usuario por ID

        [HttpGet]
        [Route("Buscar/{id}")]

        public async Task<IActionResult> Buscar(int id)
        {
            var responseAPI = new ResponseAPI<UsuarioDTO>();
            var UsuarioDTO = new UsuarioDTO();

            try
            {
                var dbUsuario = await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == id);

                if (dbUsuario != null)
                {
                    UsuarioDTO.IdUsuario = dbUsuario.IdUsuario;
                    UsuarioDTO.Nombre = dbUsuario.Nombre;
                    UsuarioDTO.Apellidos = dbUsuario.Apellidos;
                    UsuarioDTO.Mail = dbUsuario.Mail;
                    UsuarioDTO.Direccion = dbUsuario.Direccion;

                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = UsuarioDTO;
                }
                else
                {
                    responseAPI.EsCorrecto = false;
                    responseAPI.Mensaje = "No encontrado";
                }
            }
            catch (Exception ex)
            {
                responseAPI.EsCorrecto = false;
                responseAPI.Mensaje = ex.Message;
            }

            return Ok(responseAPI);
        }

        // Servicio de Ingreso de Usuario

        [HttpPost]
        [Route("Guardar")]

        public async Task<IActionResult> Guardar(UsuarioDTO usuario)
        {
            var responseAPI = new ResponseAPI<int>();

            try
            {
                var dbUsuario = new Usuario
                {
                    Nombre = usuario.Nombre,
                    Apellidos = usuario.Apellidos,
                    Mail = usuario.Mail,
                    Direccion = usuario.Direccion,
                };
                
                _dbContext.Usuarios.Add(dbUsuario);
                await _dbContext.SaveChangesAsync();

                if (dbUsuario.IdUsuario != 0) 
                {
                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = dbUsuario.IdUsuario;
                }
                else
                {
                    responseAPI.EsCorrecto = false;
                    responseAPI.Mensaje = "No encontrado";
                }                
            }
            catch (Exception ex)
            {
                responseAPI.EsCorrecto = false;
                responseAPI.Mensaje = ex.Message;
            }

            return Ok(responseAPI);
        }

        // Servicio de Modificacion de Usuario

        [HttpPut]
        [Route("Editar/{id}")]

        public async Task<IActionResult> Editar(UsuarioDTO usuario, int id)
        {
            var responseAPI = new ResponseAPI<int>();

            try
            {
                var dbUsuario = await _dbContext.Usuarios.FirstOrDefaultAsync(e => e.IdUsuario == id);
                
                if (dbUsuario != null)
                {
                    dbUsuario.Nombre = usuario.Nombre;
                    dbUsuario.Apellidos = usuario.Apellidos;
                    dbUsuario.Mail = usuario.Mail;
                    dbUsuario.Direccion = usuario.Direccion;

                    _dbContext.Usuarios.Update(dbUsuario);
                    await _dbContext.SaveChangesAsync();

                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = dbUsuario.IdUsuario;
                }
                else
                {
                    responseAPI.EsCorrecto = false;
                    responseAPI.Mensaje = "Usuario no encontrado";
                }
            }
            catch (Exception ex)
            {
                responseAPI.EsCorrecto = false;
                responseAPI.Mensaje = ex.Message;
            }

            return Ok(responseAPI);
        }

        // Servicio de Eliminacion de Usuario

        [HttpDelete]
        [Route("Eliminar/{id}")]

        public async Task<IActionResult> Eliminar(int id)
        {
            var responseAPI = new ResponseAPI<int>();

            try
            {
                var dbUsuario = await _dbContext.Usuarios.FirstOrDefaultAsync(e => e.IdUsuario == id);

                if (dbUsuario != null)
                {
                    _dbContext.Usuarios.Remove(dbUsuario);
                    await _dbContext.SaveChangesAsync();

                    responseAPI.EsCorrecto = true;             
                }
                else
                {
                    responseAPI.EsCorrecto = false;
                    responseAPI.Mensaje = "Usuario no encontrado";
                }
            }
            catch (Exception ex)
            {
                responseAPI.EsCorrecto = false;
                responseAPI.Mensaje = ex.Message;
            }

            return Ok(responseAPI);
        }
    }
}
