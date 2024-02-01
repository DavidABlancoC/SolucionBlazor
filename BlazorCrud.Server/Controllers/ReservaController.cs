using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// Referencias locales

using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        // Servicio de base de datos

        private readonly DbcrudHoteleriaContext _dbContext;

        public ReservaController(DbcrudHoteleriaContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseAPI = new ResponseAPI<List<ReservaDTO>>();
            var listaReservaDTO = new List<ReservaDTO>();

            try
            {
                foreach (var item in await _dbContext.Reservas.Include(d => d.IdUsuarioNavigation).Include(d => d.IdHotelNavigation).Include(d => d.IdHabitacionNavigation).ToListAsync())
                {
                    listaReservaDTO.Add(new ReservaDTO
                    {
                        IdReserva = item.IdReserva,
                        IdUsuario = item.IdUsuario,
                        IdHotel = item.IdHotel,
                        IdHabitacion = item.IdHabitacion,
                        FechaEntrada = item.FechaEntrada,
                        FechaSalida = item.FechaSalida,
                        FechaReserva = item.FechaReserva,
                        Estado = item.Estado,
                        Usuario = new UsuarioDTO
                        {
                            IdUsuario = item.IdUsuarioNavigation.IdUsuario,
                            Nombre = item.IdUsuarioNavigation.Nombre,
                            Apellidos = item.IdUsuarioNavigation.Apellidos,
                        },
                        Hotel = new HotelDTO
                        {
                            IdHotel = item.IdHotelNavigation.IdHotel,
                            Nombre = item.IdHotelNavigation.Nombre,
                            Pais = item.IdHotelNavigation.Pais,
                        },
                        Habitacion = new HabitacionDTO
                        {
                            IdHabitacion = item.IdHabitacionNavigation.IdHabitacion,
                            NumeroHabitacion = item.IdHabitacionNavigation.NumeroHabitacion,
                            Estado = item.IdHabitacionNavigation.Estado,
                        },
                    });
                }

                responseAPI.EsCorrecto = true;
                responseAPI.Valor = listaReservaDTO;
            }
            catch (Exception ex)
            {
                responseAPI.EsCorrecto = false;
                responseAPI.Mensaje = ex.Message;
            }

            return Ok(responseAPI);
        }

        // Servicio de Busqueda de Reserva por ID

        [HttpGet]
        [Route("Buscar/{id}")]

        public async Task<IActionResult> Buscar(int id)
        {
            var responseAPI = new ResponseAPI<ReservaDTO>();
            var ReservaDTO = new ReservaDTO();

            try
            {
                var dbReserva = await _dbContext.Reservas.FirstOrDefaultAsync(x => x.IdReserva == id);

                if (dbReserva != null)
                {
                    ReservaDTO.IdReserva = dbReserva.IdReserva;
                    ReservaDTO.IdUsuario = dbReserva.IdUsuario;
                    ReservaDTO.IdHotel = dbReserva.IdHotel;
                    ReservaDTO.IdHabitacion = dbReserva.IdHabitacion;
                    ReservaDTO.FechaEntrada = dbReserva.FechaEntrada;
                    ReservaDTO.FechaSalida = dbReserva.FechaSalida;
                    ReservaDTO.FechaReserva = dbReserva.FechaReserva;

                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = ReservaDTO;
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

        // Servicio de Ingreso de Reserva

        [HttpPost]
        [Route("Guardar")]

        public async Task<IActionResult> Guardar(ReservaDTO reserva)
        {
            var responseAPI = new ResponseAPI<int>();

            try
            {
                var dbReserva = new Reserva
                {
                    IdHotel = reserva.IdHotel,
                    IdHabitacion = reserva.IdHabitacion,
                    FechaEntrada = reserva.FechaEntrada,
                    FechaSalida = reserva.FechaSalida,
                    FechaReserva = reserva.FechaReserva,
                };

                _dbContext.Reservas.Add(dbReserva);
                await _dbContext.SaveChangesAsync();

                if (dbReserva.IdReserva != 0)
                {
                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = dbReserva.IdReserva;
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

        // Servicio de Modificacion de Reserva

        [HttpPut]
        [Route("Editar/{id}")]

        public async Task<IActionResult> Editar(ReservaDTO reserva, int id)
        {
            var responseAPI = new ResponseAPI<int>();

            try
            {
                var dbReserva = await _dbContext.Reservas.FirstOrDefaultAsync(e => e.IdReserva == id);
                      
                if (dbReserva != null)
                {     
                    dbReserva.IdUsuario = reserva.IdUsuario;
                    dbReserva.IdHotel = reserva.IdHotel;
                    dbReserva.IdHabitacion = reserva.IdHabitacion;
                    dbReserva.FechaEntrada = reserva.FechaEntrada;
                    dbReserva.FechaSalida = reserva.FechaSalida;
                    dbReserva.FechaReserva = reserva.FechaReserva;

                    _dbContext.Reservas.Update(dbReserva);
                    await _dbContext.SaveChangesAsync();

                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = dbReserva.IdReserva;
                }
                else
                {
                    responseAPI.EsCorrecto = false;
                    responseAPI.Mensaje = "Reserva no encontrada";
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
