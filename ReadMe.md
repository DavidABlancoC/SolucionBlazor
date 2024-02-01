/////////////////////////////////////////////////////////////////////////////////////////////

Documentacion Solucion Blazor .Net 7 Para Control de Reservas de Hoteleria (Operaciones CRUD).
(Mejor visto en cualquier editor de codigo o notepad++).

/////////////////////////////////////////////////////////////////////////////////////////////

X. Notas iniciales del proceso.

Estructura del proyecto:

A. Generacion de Base de Datos SQL (SQL Server).
    A.X. Notas de desarrollo de la base de datos.

B. Desarrollo proyecto Blazor Server.
    B.1 Desarrollo de proyecto base y proyecto Server en Visual Studio (ASP.NET Core Web API .NET 7).
    B.2 Anexo de librerias EntityFramework.SQLServer / EntityFrameworkCore.Tools.
    B.3 Construccion y anexion de modelo de estructura de base de datos del proyecto Blazor.
    B.4 Construccion y anexion de componentes compartidos Data transfer Objects DTOs para Control de Informacion y API de respuesta.
    B.5 Construccion y anexion de controladores API de servicios CRUD.

C. Desarrollo de proyecto Blazor Client.
    C.1. Desarrollo Blazor Client (Blazor app WebAssembly .NET 7) / Relacion de Dependencia con Blazor Server.
    C.2. Desarrollo Interfaces de servicio.
    C.3. Desarrollo de Servicios de Clase (Conexion entre Interfaces - Server Service).
    C.4. Desarrollo de visualizacion de Datos (Razor objects).
    C.5. Desarrollo de visualizacion de control y manipulacion de datos.

D. Notas finales.

---------------------------------------------------------------------------------------------------------------------------------------

-	X. Notas del proceso.


		(Me disculpo de antemano por las tildes, uso el teclado en ingles para programar).

		Estoy muy agradecido de poder tener la oportunidad de acceder a esta prueba tecnica, porque mas alla de haberla completado,
		aprendi bastante con respecto al desarrollo de este tipo de proyectos: pude medir mis conocimientos anteriores, aplicar algunos,
		cuestionar otros y aprender otros para realizar proyectos "tangibles" o por lo menos de uso basico.

		Estoy comprometido con seguir el camino del desarrollo de software y estoy seguro que con el tiempo podre realizar proyectos completos 
		con las especificaciones necesarias de los clientes.


		Dicho esto hay varias cosas que siendo honesto con la propuesta de la prueba tecnica de documentar todo el proceso, tengo que aclarar:

		-   Cuando lei el documento, supe que se trataba de realizar un desarrollo Full Stack (Puedo equivocarme con los terminos) para un 
			sitio web, desarrollo con el cual tengo muy poca experiencia (apenas una nocion basica de funcionamiento).

		-   Busque informacion y tutoriales para poder realizar el desarrollo. Este desarrollo esta particularmente basado en un
			tutorial relativamente completo que encontre. Hay muchas cosas de las cuales no estoy seguro como y porque se integran
			de esa manera en particular, pero estoy seguro que estudiando a fondo ese tutorial, puedo aprender los detalles de los
			componentes y como se integran al proyecto (la documentacion de este proyecto es parte de ese ejercicio de estudio).

		-   Cometi un error al leer el documento. Inicialmente instale todo lo que tenia que ver con Blazor e incluso llegue a realizar
			una instalacion de prueba de una version muy reciente de Visual Studio que contiene un nuevo proceso de desarrollo automatizado de 
			operaciones CRUD que solo esta disponible para esa version (Visual Studio 2022 17.9 .NET 8 ). Iba en una etapa avanzada del
			desarrollo en ese entorno, y relei el documento para darme cuenta que debia comenzar de nuevo para usar .NET 7.

		-   Me disculpo de antemano si estas notas iniciales las consideran innecesarias o impertinentes, pero creo que parte de trabajar
			en el desarrollo de software y debido a la INMENSA extension de este sector, es necesario ser honesto y comunicar al equipo de
			trabajo lo que se sabe o no de la manera de desarrollar cualquier proyecto en el cual uno este involucrado, para poder determinar
			lo que es necesario conocer, aprender y solucionar para lograr el objetivo y la calidad del proyecto.

		-   Me disculpo de antemano si la documentacion es demasiado extensa. No tengo conocimiento de como se desarrolla un documento
			de este tipo, pero lo hago de esta manera teniendo en cuenta la propuesta de la prueba tecnica de hacerlo lo mas detallado
			posible, especialmente a lo que respecta al proceso de desarrollo del proyecto.



		Muchas gracias por su tiempo.


----------------------------------------------------------------------------------------------------------------------------------------

-	A. Generacion de Base de Datos SQL (SQL Server).

	La primera fase del proyecto es el desarrollo de la base de datos SQL. 

	Esta tiene dos propositos:

	-   Estructurar las tablas, sus respectivas columnas, los diferentes tipos de datos que cada columna debe recibir y las relaciones que
		deben existir entre ellas.

	-   Usar la estructura de la base de datos para exportar su comportamiento para su uso en el proyecto CRUD.

	Para ello se uso un script SQL que se anexa al paquete del proyecto (para la creacion de la base de datos automatizada), 
	el cual crea la base de datos, la selecciona y crea las tablas con las respectivas columnas, tipos de datos, 
	restricciones y relaciones entre si.

	Plataforma de desarrollo: 

	SQL Server Management Studio				    19.2.56.2
	SQL Server Management Objects (SMO)				16.200.48050.0+9bd30730a8cbcdac9d9788ba6605f3dda96e6b89
	Microsoft T-SQL Parser						    17.0.23.0+0d40faadb307b5d5fe930d64f47d2285ed3d0831
	Microsoft Analysis Services Client Tools		16.0.20054.0
	Microsoft Data Access Components (MDAC)			10.0.22621.3007
	Microsoft MSXML									3.0 6.0 
	Microsoft .NET Framework						4.0.30319.42000
	Operating System								10.0.22621

	- SQL SCRIPT:

		CREATE DATABASE DBCrudHoteleria                   // Creacion de base de datos.
		USE DBCrudHoteleria                               // Seleccion de base de datos.             

		CREATE TABLE Usuario (                            // Creacion tabla 'Usuarios' con sus respectivos datos, tipos y restricciones.
			IdUsuario INT PRIMARY KEY IDENTITY (1,1),     // Asignacion Primary Key para su posterior relacion con las demas tablas.
			Nombre VARCHAR(50) NOT NULL,
			Apellidos VARCHAR(60) NOT NULL,
			Mail VARCHAR(60) NOT NULL UNIQUE,
			Direccion VARCHAR(60) NOT NULL
		)

		CREATE TABLE Hotel (
			IdHotel INT PRIMARY KEY IDENTITY (1,1),
			Nombre VARCHAR(50) NOT NULL,
			Pais VARCHAR(50) NOT NULL,
			Latitud DECIMAL(8,6) NOT NULL,
			Longitud DECIMAL(9,6) NOT NULL,
			Descripcion TEXT,
			Activo TEXT NOT NULL,
			Cant_Habitaciones VARCHAR(8) NOT NULL
		)

		CREATE TABLE Habitacion (
			IdHabitacion INT PRIMARY KEY IDENTITY (1,1),
			IdHotel INT REFERENCES Hotel(IdHotel) NOT NULL,
			Numero_Habitacion VARCHAR(8) NOT NULL,
			Estado TEXT NOT NULL,
		)

		CREATE TABLE Reserva (
			IdReserva INT PRIMARY KEY IDENTITY (1,1),
			IdUsuario INT REFERENCES Usuario(IdUsuario) NOT NULL,
			IdHotel INT REFERENCES Hotel(IdHotel) NOT NULL,
			IdHabitacion INT REFERENCES Habitacion(IdHabitacion) NOT NULL,
			Fecha_Entrada DATE NOT NULL,
			Fecha_Salida DATE NOT NULL,
			Fecha_Reserva DATE NOT NULL DEFAULT CURRENT_TIMESTAMP,
			Estado TEXT NOT NULL,
		)

	-	A.X. Notas de desarrollo de la base de datos

		Tengo algunas nociones basicas del desarrollo de una base de datos SQL. Tengo claro lo que se requiere para estructurarlas
		con los diferentes tipos de datos necesarios y las conexiones necesarias. Hay partes del desarrollo de una base de datos
		que se omiten en este proyecto por razones de conocimiento y tiempo, pero que renozco que normalmente forman parte de ella:

		-   Tablas de control de operaciones.                     // Registro operaciones
		-	Tablas predefinidas de visualizacion de informacion.  // Visualizacion de informacion comun en las busquedas
		-   Indexacion de informacion.							  // Optimizacion
		-	Controles de seguridad.                               // Prevencion de ataques por injeccion.

		Al intentar desarrollar el script SQL, tuve algunas complicaciones:

		-	Normalmente en otros sistemas SQL, es posible asignarle a una columna de texto valores predeterminados
			mediante CHECK o ENUM. En SQL Server no pude lograr asignar estas restricciones.

		Al realizar la base de datos me encontre con algunas restricciones al integrarla al proyecto de Blazor:

		-	Inicialmente asigne los datos de latitud y longitud como un tipo real, pero al integrarla con blazor, omitia estas dos columnas.
			el tipo DECIMAL paso sin problemas.

		-	No tengo claro que resultados puede tener la integracion con Blazor si se realizan restricciones de valores de columnas de texto.

----------------------------------------------------------------------------------------------------------------------------------------

-	B. Desarrollo proyecto Blazor Server.

	Para el desarrollo de la solucion Blazor, se usa la siguiente version de Visual Studio:

	Microsoft Visual Studio Community 2022 (64-bit) - Current Version 17.8.5

	-	B.1 Desarrollo de proyecto base y proyecto Server en Visual Studio (ASP.NET Core Web API .NET 7).

		-	Creacion proyecto vacio: 

				Nombre = SolucionBlazor.

		-	Creacion proyecto Server (ASP.NET Core Web API / .NET 7.0).

				Configuracion creacion de proyecto:

					-	DESHABILITADO / Configurar para HTTPS
					-	DESHABILITADO / Habilitar Docker
					-	HABILITADO    / Usar controladores
					-	HABILITADO    / Habilitar compatibilidad con OpenAPI.

				Nombre = BlazorCrud.Server

	-	B.2 Anexo de librerias EntityFramework.SQLServer / EntityFrameworkCore.Tools.

		Proyecto BlazorCrud.Server

			- Seleccion Dependencias
				- Administrar paquetes de NuGet // Administrador de paquetes para .NET
					- Instalar Microsoft.EntityFrameworkCore.SqlServer    7.0.3 (Permite usar Microsoft SQL Server con Entity Framework Core)
					- Instalar Microsoft.EntityFrameworkCore.Tools        7.0.3 (Usado para administrar migraciones e integraciones de bases de datos
																				mediante "ingenieria inversa" del esquema de la base de datos).

	-	B.3 Construccion y anexion de modelo de estructura de base de datos del proyecto Blazor.

		CONSTRUCCION:

		- proyecto BlazorCrud.Server

			- Creacion carpeta Models

		Visual Studio
			- Herramientas
				- Administrador de paquetes NuGet
					- Consola del Administrador de paquetes NuGet

						Ejecucion del comando:

						Scaffold-DbContext "Server=DAVIDBC\SQLEXPRESS; Database=DBCrudHoteleria; Trusted_Connection=True; TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models

		- Descripcion del comando

		Scaffold-DbContext : Permite generara clases de tipo de entidad y un contexto de base de datos DbContext mediante "ingenieria inversa"
							Lee la base de datos y construye las clases con las correspondientes columnas y les agrega las propiedades.
							Tambien genera propiedades de conexion entre las tablas basada en las relaciones fijadas en la base de datos.
							(Es necesario saber el porque de la estructura de la clase, porque se genera con esos elementos).

		Server=DAVIDBC\SQLEXPRESS   : Servidor en el cual se encuentra la base de datos.

		Database=DBCrudHoteleria    : Base de datos a leer

		Trusted_Connection=True     : Establece que la conexion es segura/conocida. (Es necesario explorar mas acerca de las consecuencias 
																					de esta opcion)

		TrustServerCertificate=True : Establece que se conoce el certificado del servido. (Es necesario explorar mas acerca de las consecuencias 
																						de esta opcion)

		Microsoft.EntityFrameworkCore.SqlServer : Uso de la libreria para integrar la base de datos de SQL Server.

		-OutPutDir Models : Establece la carpeta de salida de los archivos a generar.

		Este procedimiento genero los siguientes archivos en el proyecto BlazorCrud.Server:

			- DbCrudHoteleriaContext.cs // Contiene varios comandos en el cual se conectan las clases de la base de datos con sus 
										// correspondientes propiedades
			- Habitacion.cs
			- Hotel.cs
			- Reserva.cs
			- Usuario.cs

		Ejemplo de archivo de clase generado por el procedimiento:

		- Habitacion.cs

			// Librerias del Sistema

			using System;
			using System.Collections.Generic;

			// Nombre de espacio para referencia

			namespace BlazorCrud.Server.Models;

			// Declaracion de clase Habitacion basado en la base de datos SQL

			public partial class Habitacion
			{
				public int IdHabitacion { get; set; }

				public int IdHotel { get; set; }

				public string NumeroHabitacion { get; set; } = null!;

				public string Estado { get; set; } = null!;

				// Conexion con las tablas: Esta tabla referencia a la tabla Hotel mediante IdHotel

				public virtual Hotel IdHotelNavigation { get; set; } = null!;

				// Conexion con las tablas: Esta tabla se referencia en la tabla Reserva mediante IdHabitacion

				public virtual ICollection<Reserva> Reservas { get; } = new List<Reserva>();
			}

		No me queda claro porque los diferentes elementos se definen de cierta manera (partial, virtual, ICollection(Interfaz), IdHotelNavigation).

		ANEXION:

		- BlazorCrud.Server
			- DbcrudHoteleriaContext.cs

				Linea 26:

				protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
				=> optionsBuilder.UseSqlServer("Server=DAVIDBC\SQLEXPRESS; Database=DBCrudHoteleria; Trusted_Connection=True;    TrustServerCertificate=True;")

				Segun el documento generado por el comando, esta linea debe ser retirada por razones de seguridad. (No me queda claro que sucede si no se retira).
				
				Las lineas se modifican de la siguiente manera:

				Linea 26:

				protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){ }

		El comando se mueve al archivo appsetting.json de la siguiente manera:

		-	BlazorCrud.Server
			- appsetting.json

				Linea 2:

				"ConnectionStrings": {
					"SqlString": "Server=DAVIDBC\\SQLEXPRESS; Database=DBCrudHoteleria; Trusted_Connection=True; TrustServerCertificate=True;"
				},

		No me queda claro como se invocan algunos componentes, pero la cadena "SqlString" se invoca en "Program.cs".

		Se integra el modelo de la base de datos y las librerias correspondientes al programa principal:

		- BlazorCrud.Server

		- Program.cs

			Linea 1:

			using BlazorCrud.Server.Models;
			using Microsoft.EntityFrameworkCore;

			// Se agrega el servicio en el mismo archivo:

		-	Program.cs

			Linea 13:

			builder.Services.AddDbContext<DbcrudHoteleriaContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("SqlString"));
			});

			Agrega el contexto de la base de datos, e indica como configuracion la cadena que se debe buscar para iniciar la 
			conexion con SQL Server a traves del metodo GetConnectionString.



	-	B.4 Construccion y anexion de componentes compartidos Data transfer Objects DTOs para Control de Informacion y API de respuesta.

		A continuacion se construye una Biblioteca de clases para configurar y controlar los datos de la base de datos, funciona como un puente entre el servido y el cliente:

		- SolucionBlazor
			- Biblioteca de Clases para .NET o .NET Standard.
				- Nombre = BlazorCrud.Shared
					- Configuracion:
						- Framework = .NET 7.0 (Soporte tecnico de terminos estandar)

		Se construyen en esta parte del proyecto los DTO (Data Transfer Object), los cuales, contienen las propiedades del objeto (en este proyecto,
		las propiedades de las columnas de las tablas y como se comportan tanto en el servidor como en el cliente).

		DTOs del proyecto:

		- BlazorCrud.Shared
			- HabitacionDTO.cs
			- HotelDTO.cs
			- ReservaDTO.cs
			- UsuarioDTO.cs
			- ResponseAPI.cs // Usado para verificar si los datos enviados son correctos y el procedimiento a seguir.
							// No me queda claro el porque debe existir y porque se construye de esta manera.

		Ejemplo de DTO:

		- BlazorCrud.Shared
			- Habitacion DTO.cs

				// Librerias del sistema + Libreria DataAnnotations (Permite definir el comportamiento de los datos en el cliente)

				using System;
				using System.Collections.Generic;
				using System.Linq;
				using System.Text;
				using System.Threading.Tasks;
				using System.ComponentModel.DataAnnotations;

				namespace BlazorCrud.Shared
				{
					public class HabitacionDTO
					{                                                              // Id Habitacion
						[Display(Name = "Codigo Habitacion")]                      // Parte de la libreria DataAnnotations
						public int IdHabitacion { get; set; }                      

						[Display(Name = "Codigo Hotel")]
						public int IdHotel { get; set; }                           // Id hotel

						[Display(Name = "Numero de Habitacion")]
						[Required(ErrorMessage = "El campo {0} es requerido.")]
						public string NumeroHabitacion { get; set; } = null!;      // Numero de Habitacion

						[Required (ErrorMessage = "El campo {0} es requerido.")]
						public string Estado { get; set; } = null!;                // Estado

						public HotelDTO Hotel { get; set; }                        // Conexion con tabla Hotel         
					}
				}

		Este tipo de archivo debe ser exacto en nombres, tipos y propiedades de la base de datos y de las clases de los modelos de la base de datos.
		No me queda claro si es posible manipular los valores que puede tener un elemento, por ejemplo: Estado deberia ser igual a "Libre", "Ocupado", "Reservado"

	-	B.5 Construccion y anexion de controladores API de servicios CRUD.

		Luego se procede a realizar los controladores API para los servicios del proyecto.

		Inicialmente es necesario realizar una referencia del proyecto de BlazorCrud.Shared a BlazorCrud.Server.

		- BlazorCrud.Server
			- Dependencias
				- Agregar referencia del proyecto
					- BlazorCrud.Shared

		A continuacion se comienza a agregar los controladores del proyecto:

		- BlazorCrud.Server
			- Controllers
				- Controlador
					- API
						- Plantilla Controlador de API: en blanco

		Una vez creado el controlador, se desarrolla de la siguiente manera:

		Modelo de Controlador: Usuario.cs (Contiene todos los controladores CRUD)

		- UsuarioController.cs

			// Librerias ASPNetCore

			using Microsoft.AspNetCore.Http;
			using Microsoft.AspNetCore.Mvc;

			// Referencias locales del projecto

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
					private readonly DbcrudHoteleriaContext _dbContext;                    // Servicio de base de datos

					public UsuarioController(DbcrudHoteleriaContext dbContext)
					{
						_dbContext = dbContext;
					}		

					[HttpGet]                                                              // Servicio de Lista de Usuarios
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

					
					[HttpGet]                                                               // Servicio de Busqueda de Usuario por ID
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

					[HttpPost]                                                             // Servicio de Ingreso de Usuario
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

					[HttpPut]                                                               // Servicio de Modificacion de Usuario
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

					[HttpDelete]                                                             // Servicio de Eliminacion de Usuario
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

		B.5.X Notas de Seccion:

		-	No me queda muy claro porque se definen los elementos de esta manera, pero es parte del estudio de esta seccion desmenusar
			las caracteristicas de las definiciones, las variables y como se relacionan entre si.

		-	Los otros controladores poseen una estructura muy similar por no decir exactas, lo que cambia son los nombres y las variables de datos,
			por ejemplo:

			UsuarioController.cs

			UsuarioDTO.IdUsuario = dbUsuario.IdUsuario;

			HotelController.cs

			HotelDTO.IdHotel = dbHotel.IdHotel;

		-	No todos los controladores tienen la capacidad de eliminar datos (Solo Usuarios posee esa capacidad).
		-	Hay funcionalidades que pienso, es posible definir en esta seccion. (Como revisar si una habitacion esta ocupada y evitar Overbooking).

		Finalmente se modifica el archivo Program.cs para agregar el siguiente servicio:

		- Program.cs

			Linea 18:

			builder.Services.AddCors(options =>
			{
				options.AddPolicy("newPolicy", app =>
				{
					app.AllowAnyOrigin()
					.AllowAnyHeader()
					.AllowAnyMethod();
				});
			});

			Linea 37:

			app.UseCors("newPolicy");

		-	Agrega el mecanismo CORS (Cross Origin Resource Sharing). No me queda claro como afecta el comportamiento de la aplicacion, pero 
			es posible buscar la informacion para conocer mas acerca del tema. 
			
		-	En el tutorial, el proyecto de servidor y el proyecto de cliente seran ejecutadas en URLs distintas, CORS permiten compartir informacion
			sin muchas restricciones.

----------------------------------------------------------------------------------------------------------------------------------------

-	C. Desarrollo de proyecto Blazor Client

	-	C.1. Desarrollo Blazor Client (Blazor app WebAssembly .NET 7) / Relacion de Dependencia con Blazor Server.

		En esta seccion, se desarrolla la parte del cliente de la aplicacion desde el proyecto base.

		- SolucionBlazor
		- Agregar
			- Nuevo Proyecto
				-	Aplicacion Blazor para WebAssembly

					CONFIGURACION PROYECTO

					Nombre = BlazorCrud.Client
					Framework = .Net 7.0
					Autenticacion de campo = Ninguno.

					-	DESHABILITADO / Configurar para HTTPS.
					-	DESHABILITADO / ASP.NET Core hospedado.
					-	DESHABILITADO    / Aplicacion web progresiva.
					-	DESHABILITADO   / No usar instrucciones de niver superior.

		-	C.1.X. Notas de seccion:

		-	Las plantillas que podia seleccionar no eran las mismas que se sugerian en el tutorial, aunque tenia las mismas configuraciones.
			Como resultado, la barra de navegacion no estaba bien configurada y no se renderizaba apropiadamente los elementos.

		-	Fue necesario usar otra plantilla que si contenia estos elementos definidos e implementarlos realizando correciones.

		A continuacion se agrega la relacion de dependencia de BlazorCrud.Shared

		- BlazorCrud.Client
			- Dependencias
				- Agregar Referencia del proyecto
					- BlazorCrud.Shared

		Finalmente se realiza la conexion de consumo de la API mediante la modificacion de "Program.cs"

		- BlazorCrud.Client
			- Program.cs

				Linea 12:

				builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5028") });

				Se modifica la Uri, la cual se extrae haciendo una ejecucion de la seccion del servidor.

	-	C.2. Desarrollo Interfaces de servicio.

		Inicialmente se crea una carpeta dentro del proyecto BlazorCrud.Client que va a contener las interfaces y los servicios

		- BlazorCrud.Client
			- Services
				-IUsuario.cs

		Luego se comienza el desarrollo de las interfaces:

		Ejemplo de interfaz: IUsuario.cs

		- IUsuario.cs

			using BlazorCrud.Shared;                             // Agregar la referencia del proyecto Blazor.Shared

			namespace BlazorCrud.Client.Services
			{
				public interface IUsuarioService
				{
					Task<List<UsuarioDTO>> Lista();				 // Lista de Usuarios

					Task<UsuarioDTO> Buscar(int id);			 // Buscar Usuario por Id

					Task<int> Guardar(UsuarioDTO usuario);		 // Guardar Usuario

					Task<int> Editar(UsuarioDTO usuario);		 // Editar Usuario

					Task<bool> Eliminar(int id);				 // Eliminar Usuario
				}
			}

	-	C.3. Desarrollo de Servicios de Clase (Conexion entre Interfaces - Server Service).

		A continuacion se crea una nueva clase asociada a la interfaz

		- BlazorCrud.Client
			- Services
				-UsuarioService.cs

		Luego se comienza el desarrollo del servicio CRUD:

		- BlazorCrud.Client
			-UsuarioService.cs

				using BlazorCrud.Shared;
				using System.Net.Http.Json;

				namespace BlazorCrud.Client.Services
				{
					public class UsuarioService : IUsuarioService   				// Conexion con interfaz IUsuarioService
					{
						private readonly HttpClient _http;

						public UsuarioService(HttpClient http) 
						{
							_http = http;
						}

						public async Task<List<UsuarioDTO>> Lista()  				// Metodo de Lista
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

						public async Task<UsuarioDTO> Buscar(int id)				// Metodo de Buscar por ID
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

						public async Task<int> Guardar(UsuarioDTO usuario)			// Metodo de Guardar como UsuarioDTO usuario
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


						public async Task<int> Editar(UsuarioDTO usuario)			// Metodo de Editar un UsuarioDTO usuario
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

						public async Task<bool> Eliminar(int id)					// Metodo de Eliminar por ID
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

		La estructura, tanto de la interfaz como del servicio es muy similar en los demas elementos, asi que se implementa con respecto a cada tabla
		que se requiere manipular.

		Finalmente en Servicios de BlazorCrud.Client tenemos:

		- BlazorCrud.Client
			- Services
				- IHabitacionService.cs
				- HabitacionService.cs
				- IHotelService.cs
				- HotelService.cs
				- IReservaService.cs
				- ReservaService.cs
				- IUsuarioService.cs
				- UsuarioService.cs
				
		C.3.X. Notas de seccion:

		Desconozco con exactitud el funcionamiento de los siguientes metodos:

			GetFromJsonAsync	// Trae informacion asincrona
			PostAsJsonAsync		// Guarda informacion asincrona	 
			PutAsJsonAsync		// Edita informacion asincrona	 
			DeleteAsync			// Elimina informacion asincrona

			async y await son atributos de la operacion para realizar tareas de manipulacion de datos.  

		Finalmente en esta parte del proceso se agregan unas directivas a "Program.cs" para enlazar los diferentes servicios a la aplicacion.

		- BlazorCrud.Client
			- Program.cs

				Linea 14:

				builder.Services.AddScoped<IUsuarioService, UsuarioService>();
				builder.Services.AddScoped<IHotelService, HotelService>();
				builder.Services.AddScoped<IHabitacionService, HabitacionService>();
				builder.Services.AddScoped<IReservaService, ReservaService>();


	-	C.4. Desarrollo de visualizacion de Datos (componentes Razor).

		A continuacion, se desarrollan los componentes razor dentro del proyecto BlazorCrud.Client, 
		los cuales funcionan como elementos hibridos entre HTML y C#.

		- BlazorCrud.Client
			- Pages
				- Agregar
					- Componente de Razor

		El componente razor se desarrolla de la siguiente manera:

		Ejemplo Hoteles.cs

		- Hoteles.cs
			@page "/hoteles"														// Direccion que usara el navegador para acceder a la pagina.
																					// Tambien se usa para referenciarlo en elementos interactivos.
			@using BlazorCrud.Shared;												// Elementos de relacion de la aplicacion
			@using CurrieTechnologies.Razor.SweetAlert2;
			@using BlazorCrud.Client.Services;

			@inject SweetAlertService SwtAlert;
			@inject IHotelService hotelService;										// Interfaz del servicio

			<h3>@titulo</h3>

			<a class="btn btn-success btn-sm mb-3" href="hotel">Nuevo Hotel</a>		// Elementos HTML de visualizacion

			<table class="table">													// Tabla de elementos de base de datos
				<thead>
					<tr>
						<th>Nombre</th>
						<th>Pais</th>
						<th>Latitud</th>
						<th>Longitud</th>
						<th>Descripcion</th>
						<th>Activo</th>
						<th>Habitaciones</th>
						<th></th>
					</tr>
				</thead>
				<tbody>
					@if (listaHotel == null)
					{
						<tr>
							<td colspan="5" align="center">
								<p>No hay entradas disponibles</p>
							</td>
						</tr>
					}
					else
					{
						@foreach (var item in listaHotel)							// Llama a los Datos de la base de datos
							<tr>
								<td>@item.Nombre</td>
								<td>@item.Pais</td>
								<td>@item.Latitud</td>
								<td>@item.Longitud</td>
								<td>@item.Descripcion</td>
								<td>@item.Activo</td>
								<td>@item.CantHabitaciones</td>
								<td>
									<a class="btn btn-primary btn-sm" href="hotel/@item.IdHotel">
										<i class="oi oi-pencil"></i>
									</a>
								</td>
							</tr>
						}
					}
				</tbody>
			</table>

			@code {																	// Comienzo codigo C#
				string titulo = string.Empty;

				List<HotelDTO>? listaHotel = null;

				protected override async Task OnInitializedAsync()					// Metodo de listado de base de datos
				{
					titulo = "Hoteles";
					listaHotel = await hotelService.Lista();
				}
			}

		Los demas componentes razor tienen una estructura muy similar de visualizacion, tan solo cambian los nombres de referencia
		para mostrar el texto de los encabezados de la tabla y la informacion de la base de datos.

	-	C.5. Desarrollo de visualizacion de control y manipulacion de datos.

		Para finalizar este proyecto, se realizan los formularios de entrada/edicion de datos

		Creamos un componente Razor:

		- BlazorCrud.Client
				- Pages
					- Agregar
						- Componente Razor

		El componente razor se desarrolla de la siguiente manera:

		Ejemplo Hotel.cs

		- Hotel.cs																				
			@page "/hotel"																	// Direccion que usara el navegador para acceder a la pagina.
																							// Tambien se usa para referenciarlo en elementos interactivos.
			@page "/hotel/{idHotelEditar:int}"

			@using BlazorCrud.Shared;														// Referencias de elementos del proyecto
			@using BlazorCrud.Client.Services;
			@using Microsoft.AspNetCore.Components.Forms

			@inject IHotelService hotelService;												// Referencia de interfaz de Hotel
			@inject NavigationManager navegacion;											

			<h3>@titulo</h3>																// Inicio codigo HTML

			<EditForm Model="hotel" OnValidSubmit="OnValidSubmit">							// Formulario de creacion/modificacion de datos
				<DataAnnotationsValidator></DataAnnotationsValidator>

				<div class="mb-3">
					<label class="form-label">Nombre</label>								// Etiqueta de seccion Nombre del formulario
					<InputText class="form-control" @bind-Value="hotel.Nombre"></InputText> // Tipo de obtencion de datos para Nombre 
					<ValidationMessage For="@(() => hotel.Nombre)"></ValidationMessage>		// Validacion de entrada
				</div>

				<div class="mb-3">
					<label class="form-label">Pais</label>
					<InputText class="form-control" @bind-Value="hotel.Pais"></InputText>
					<ValidationMessage For="@(() => hotel.Pais)"></ValidationMessage>
				</div>

				<div class="mb-3">
					<label class="form-label">Latitud</label>
					<InputNumber class="form-control" @bind-Value="hotel.Latitud"></InputNumber>
					<ValidationMessage For="@(() => hotel.Latitud)"></ValidationMessage>
				</div>

				<div class="mb-3">
					<label class="form-label">Longitud</label>
					<InputNumber class="form-control" @bind-Value="hotel.Longitud"></InputNumber>
					<ValidationMessage For="@(() => hotel.Longitud)"></ValidationMessage>
				</div>

				<div class="mb-3">
					<label class="form-label">Descripcion</label>
					<InputText class="form-control" @bind-Value="hotel.Descripcion"></InputText>
					<ValidationMessage For="@(() => hotel.Descripcion)"></ValidationMessage>
				</div>

				<div class="mb-3">
					<label class="form-label">Activo</label>
					<InputSelect class="form-control" @bind-Value="hotel.Activo">

						<option value="">--Seleccionar Estado--</option>
						@foreach(var item in listaEstados)
						{
							<option value="@item">@item</option>
						}

					</InputSelect>
					<ValidationMessage For="@(() => hotel.Activo)"></ValidationMessage>
				</div>

				<div class="mb-3">
					<label class="form-label">Habitaciones</label>
					<InputText class="form-control" @bind-Value="hotel.CantHabitaciones"></InputText>
					<ValidationMessage For="@(() => hotel.CantHabitaciones)"></ValidationMessage>
				</div>

				<button class="btn btn-primary" type="submit">						// Boton de envio de datos
					@btnTexto
				</button>

				<a class="btn btn-warning" href="hoteles">Volver</a>

			</EditForm>

			@code 																	// Inicio codigo C#
			{
				[Parameter]
				public int idHotelEditar { get; set; } = 0;

				string titulo = string.Empty;
				string btnTexto = string.Empty;

				List<string> listaEstados = new List<string>();

				HotelDTO hotel = new HotelDTO();

				protected override async Task OnInitializedAsync()					// Metodo que ejecuta el codigo interno al cargar el componente Razor
				{
					listaEstados.Add("Activo");
					listaEstados.Add("Inactivo");

					if (idHotelEditar != 0)
					{
						hotel = await hotelService.Buscar(idHotelEditar);
						btnTexto = "Actualizar Hotel";
						titulo = "Editar Hotel";
					}
					else
					{
						btnTexto = "Actualizar Hotel";
						titulo = "Nuevo Hotel";
					}			
				}

				private async Task OnValidSubmit()									// Metodo que se ejecuta al determinar si los datos son validos
				{
					int idDevuelto = 0;

					if (idHotelEditar == 0)
					{
						// if (listaEstados.Equals("Activo"))
						// {
						//     hotel.Activo = "Activo";
						// }
						// else if (listaEstados.Equals("Inactivo"))
						// {
						//     hotel.Activo = "Inactivo";
						// }

						idDevuelto = await hotelService.Guardar(hotel);
					}
					else
					{
						idDevuelto = await hotelService.Editar(hotel);
					}

					if (idHotelEditar != 0)
					{
						navegacion.NavigateTo("/hoteles");
					}
				}
			}

		Los demas componentes Razor contiene una estructura muy similar. Dado que no tengo la experienca para realizar este tipo de desarrollos, 
		no me queda claro como se establecen las relaciones entre el codigo C# y el codigo HTML, salvo en algunos casos MUY BASICOS. 

		C.5.X Notas de Seccion

		Por el momento no fue posible agregar varias de las funcionalidades de la solucion en la seccion del cliente:

			-	Cancelar una reserva y marcar el estado como cancelado.
			-	Modificar el estado de una habitacion si ha sido reservada.
			-	Realizar cualquier tipo de consulta.
			-	Realizar las respectiva condiciones para evitar el overbooking y el traslapamiento de Check-In / Check-Out

----------------------------------------------------------------------------------------------------------------------------------------

-	E. Notas finales

	Agradezco la oportunidad de presentar esta prueba tecnica de desarrollo de soluciones para operaciones CRUD de una base de datos,
	es claro que aun tengo muchas cosas que aprender, pero pienso que puedo diferenciar los diferentes pasos que se requieren en general
	para conformar un desarrollo de este tipo. Seguire estudiando este caso con el fin de implementar poco a poco cada requerimiento de 
	la solucion, a medida que estudio los diferentes elementos que conforman un proyecto como este.


	Muchas Gracias.

