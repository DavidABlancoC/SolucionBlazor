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
    public class HotelController : ControllerBase
    {
        // Servicio de base de datos

        private readonly DbcrudHoteleriaContext _dbContext;

        public HotelController(DbcrudHoteleriaContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseAPI = new ResponseAPI<List<HotelDTO>>();
            var listaHotelDTO = new List<HotelDTO>();

            try
            {
                foreach(var item in await _dbContext.Hoteles.ToListAsync())
                {
                    listaHotelDTO.Add(new HotelDTO
                    {
                        IdHotel = item.IdHotel,
                        Nombre = item.Nombre,
                        Pais = item.Pais,
                        Latitud = item.Latitud,
                        Longitud = item.Longitud,
                        Descripcion = item.Descripcion,
                        Activo = item.Activo,
                        CantHabitaciones = item.CantHabitaciones,
                    });
                }

                responseAPI.EsCorrecto = true;
                responseAPI.Valor = listaHotelDTO;
            }
            catch(Exception ex)
            {
                responseAPI.EsCorrecto = false;
                responseAPI.Mensaje = ex.Message;
            }

            return Ok(responseAPI);
        }

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

        // Servicio de Ingreso de Hotel

        [HttpPost]
        [Route("Guardar")]

        public async Task<IActionResult> Guardar(HotelDTO hotel)
        {
            var responseAPI = new ResponseAPI<int>();

            try
            {
                var dbHotel = new Hotel
                {
                    Nombre = hotel.Nombre,
                    Pais = hotel.Pais,
                    Latitud = hotel.Latitud,
                    Longitud = hotel.Longitud,
                    Descripcion = hotel.Descripcion,
                    Activo = hotel.Activo,
                    CantHabitaciones = hotel.CantHabitaciones,
            };

                _dbContext.Hoteles.Add(dbHotel);
                await _dbContext.SaveChangesAsync();

                if (dbHotel.IdHotel != 0)
                {
                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = dbHotel.IdHotel;
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

        // Servicio de Modificacion de Hotel

        [HttpPut]
        [Route("Editar/{id}")]

        public async Task<IActionResult> Editar(HotelDTO hotel, int id)
        {
            var responseAPI = new ResponseAPI<int>();

            try
            {
                var dbHotel = await _dbContext.Hoteles.FirstOrDefaultAsync(e => e.IdHotel == id);

                if (dbHotel != null)
                {
                    dbHotel.Nombre = hotel.Nombre;
                    dbHotel.Pais = hotel.Pais;
                    dbHotel.Latitud = hotel.Latitud;
                    dbHotel.Longitud = hotel.Longitud;
                    dbHotel.Descripcion = hotel.Descripcion;
                    dbHotel.Activo = hotel.Activo;
                    dbHotel.CantHabitaciones = hotel.CantHabitaciones;

                    _dbContext.Hoteles.Update(dbHotel);
                    await _dbContext.SaveChangesAsync();

                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = dbHotel.IdHotel;
                }
                else
                {
                    responseAPI.EsCorrecto = false;
                    responseAPI.Mensaje = "Hotel no encontrado";
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
