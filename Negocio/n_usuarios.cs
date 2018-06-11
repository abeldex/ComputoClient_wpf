using System;
using System.Collections.Generic;

namespace Negocio
{
    public class n_usuarios
    {
        //metodo para el login
        public int Login(Entidades.e_usuarios _usuario)
        {
            // Sera obligatorio ingresar dicho dato
            if (string.IsNullOrEmpty(_usuario.usuario))
                throw new Exception("El usuario no puede ser un valor nulo o vacio");

            if (string.IsNullOrEmpty(_usuario.pass))
                throw new Exception("La contraseña no puede estar vacia");

            // mandamos llamar el metodo que realiza el procedimiento almacenado en la bd
            return Datos.d_usuarios_cc.Login(_usuario);
        }

        //metodo para mostrar los camiones registrados
        public List<Entidades.e_usuarios> Listar(string dato)
        {
            return Datos.d_usuarios_cc.Leer(dato);
        }

        public bool InsertarUsuario(Entidades.e_usuarios _usuarios)
        {
            // Sera obligatorio ingresar dicho dato
            if (string.IsNullOrEmpty(_usuarios.usuario))
                throw new Exception("El usuario no puede ser un valor nulo o vacio");

            if (string.IsNullOrEmpty(_usuarios.pass))
                throw new Exception("La contraseña no puede estar vacia");

            _usuarios.castigos = 0;

            _usuarios.causa_castigo = 0;

            // mandamos llamar el metodo que realiza el procedimiento almacenado en la bd
            return Datos.d_usuarios_cc.InsertarUsuario(_usuarios);
        }

        public bool Delete(Entidades.e_usuarios _usuarios)
        {
            // Sera obligatorio ingresar dicho dato
            if (string.IsNullOrEmpty(_usuarios.usuario))
                throw new Exception("El usuario no puede ser un valor nulo o vacio");

            // mandamos llamar el metodo que realiza el procedimiento almacenado en la bd
            return Datos.d_usuarios_cc.EliminarUsuario(_usuarios);
        }

    }
}
