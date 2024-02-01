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
    public class HabitacionController : ControllerBase
    {
        // Servicio de base de datos

        private readonly DbcrudHoteleriaContext _dbContext;

        public HabitacionController(DbcrudHoteleriaContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseAPI = new ResponseAPI<List<HabitacionDTO>>();
            var listaHabitacionDTO = new List<HabitacionDTO>();

            try
            {
                foreach (var item in await _dbContext.Habitaciones.Include(d => d.IdHotelNavigation).ToListAsync())
                {
                    listaHabitacionDTO.Add(new HabitacionDTO
                    {
                        IdHabitacion = item.IdHabitacion,
                        IdHotel = item.IdHotel,
                        NumeroHabitacion = item.NumeroHabitacion,     
                        Estado = item.Estado,
                        Hotel = new HotelDTO
                        {
                            IdHotel = item.IdHotelNavigation.IdHotel,
                            Nombre = item.IdHotelNavigation.Nombre,
                        }
                    });
                }

                responseAPI.EsCorrecto = true;
                responseAPI.Valor = listaHabitacionDTO;
            }
            catch (Exception ex)
            {
                responseAPI.EsCorrecto = false;
                responseAPI.Mensaje = ex.Message;
            }

            return Ok(responseAPI);
        }

        // Servicio de Busqueda de Habitacion por ID

        [HttpGet]
        [Route("Buscar/{id}")]

        public async Task<IActionResult> Buscar(int id)
        {
            var responseAPI = new ResponseAPI<HabitacionDTO>();
            var HabitacionDTO = new HabitacionDTO();

            try
            {
                var dbHabitacion = await _dbContext.Habitaciones.FirstOrDefaultAsync(x => x.IdHabitacion == id);

                if (dbHabitacion != null)
                {
                    HabitacionDTO.IdHabitacion = dbHabitacion.IdHabitacion;
                    HabitacionDTO.IdHotel = dbHabitacion.IdHotel;
                    HabitacionDTO.NumeroHabitacion = dbHabitacion.NumeroHabitacion;
                    HabitacionDTO.Estado = dbHabitacion.Estado;          

                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = HabitacionDTO;
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

        // Servicio de Ingreso de Habitacion

        [HttpPost]
        [Route("Guardar")]

        public async Task<IActionResult> Guardar(HabitacionDTO habitacion)
        {
            var responseAPI = new ResponseAPI<int>();

            try
            {
                var dbHabitacion = new Habitacion
                {
                    IdHotel = habitacion.IdHotel,
                    NumeroHabitacion = habitacion.NumeroHabitacion,
                    Estado = habitacion.Estado,
                    
                };

                _dbContext.Habitaciones.Add(dbHabitacion);
                await _dbContext.SaveChangesAsync();

                if (dbHabitacion.IdHabitacion != 0)
                {
                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = dbHabitacion.IdHabitacion;
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

        // Servicio de Modificacion de Habitacion

        [HttpPut]
        [Route("Editar/{id}")]

        public async Task<IActionResult> Editar(HabitacionDTO habitacion, int id)
        {
            var responseAPI = new ResponseAPI<int>();

            try
            {
                var dbHabitacion = await _dbContext.Habitaciones.FirstOrDefaultAsync(e => e.IdHabitacion == id);

                if (dbHabitacion != null)
                {
                    dbHabitacion.IdHotel = habitacion.IdHotel;
                    dbHabitacion.NumeroHabitacion = habitacion.NumeroHabitacion;
                    dbHabitacion.Estado = habitacion.Estado;

                    _dbContext.Habitaciones.Update(dbHabitacion);
                    await _dbContext.SaveChangesAsync();

                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = dbHabitacion.IdHabitacion;
                }
                else
                {
                    responseAPI.EsCorrecto = false;
                    responseAPI.Mensaje = "Habitacion no encontrada";
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

