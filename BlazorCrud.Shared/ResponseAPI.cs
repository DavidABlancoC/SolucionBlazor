using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCrud.Shared
{
    public class ResponseAPI<T>
    {
        public bool EsCorrecto { get; set; } // Operacion ejecutada correctamente.

        public T? Valor { get; set; }

        public string? Mensaje { get; set; } // Mensajes de error.
    }
}
