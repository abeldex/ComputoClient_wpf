using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class n_registros
    {
        //metodo para insertar un registro
        public int InsertarRegistro(Entidades.e_registros _registro)
        {
            // Sera obligatorio ingresar dicho dato
            if (string.IsNullOrEmpty(_registro.usuario))
                throw new Exception("El usuario no puede ser un valor nulo o vacio");

            if (string.IsNullOrEmpty(_registro.equipo))
                throw new Exception("La contraseña no puede estar vacia");

            // mandamos llamar el metodo que realiza el procedimiento almacenado en la bd
            return Datos.d_registros.InsertarRegistro(_registro);
        }

        //metodo para actualizar la hora de salida
        //metodo para insertar un registro
        public bool ActualizaSalida(Entidades.e_registros _registro)
        {
            // mandamos llamar el metodo que realiza el procedimiento almacenado en la bd
            return Datos.d_registros.ActualizaSalida(_registro);
        }
    }
}
