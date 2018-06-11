using MySql.Data.MySqlClient;
using System.Data;
using Entidades;
using System;
using System.Collections.Generic;

namespace Datos
{
    public class d_usuarios_cc
    {
        //metodo para el login
        public static int Login(e_usuarios _usuarios)
        {
            try
            {
                using (var con = new MySqlConnection(conexion.LeerCC))
                {
                    using (var cmd = new MySqlCommand("Login", con))
                    {
                        //establecemos los parametros
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new MySqlParameter("_Login", _usuarios.usuario));
                        cmd.Parameters.Add(new MySqlParameter("_Pwd", _usuarios.pass));
                        //abrimos conexion y ejecutamos
                        con.Open();
                        // Ejecutamos el comando y regresamos el resultado (True = correcto, False = error)
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }

        }

        //metodo para obtener los registros
        public static List<e_usuarios> Leer(string dato)
        {
            try
            {
                // Crea un obj. lista de tipo Camion
                var lista = new List<e_usuarios>();
                // Crear el objeto de conexion
                using (var cn = new MySqlConnection(conexion.LeerCC))
                {
                    // crear el comando
                    using (var cmd = new MySqlCommand("SELECT * FROM usuarios_cc WHERE usuario like '%" + dato + "%';", cn))
                    {
                        //Asignar valores a los parametros
                        cmd.Parameters.AddWithValue("usuario", dato);

                        // Abrir el objeto de conexion
                        cn.Open();
                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                //creamos un objeto de tipo usuario
                                var _usuario = new e_usuarios();
                                //obtenemos los valores de los campos de la tabla Camion
                                _usuario.usuario = Convert.ToString(dr[dr.GetOrdinal("usuario")]);
                                _usuario.pass = Convert.ToString(dr[dr.GetOrdinal("pass")]);
                                _usuario.tipo_usuario = int.Parse(Convert.ToString(dr[dr.GetOrdinal("tipo_usuario")]));
                                _usuario.estado = int.Parse(dr[dr.GetOrdinal("estado")].ToString());
                                _usuario.castigos = int.Parse(dr[dr.GetOrdinal("castigos")].ToString());
                                _usuario.fecha = DateTime.Parse(dr[dr.GetOrdinal("fecha")].ToString());
                                // El objeto camion es agregado a la lista
                                lista.Add(_usuario);
                                // liberamos el objeto de la memoria ram
                                _usuario = null;
                            }
                        }

                        // Retorna una lista de datos
                        return lista;
                    }
                }
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }

        }

        public static bool InsertarUsuario(Entidades.e_usuarios _usuarios)
        {
            using (var cn = new MySqlConnection(conexion.LeerCC))
            {
                try
                {
                    using (var cmd = new MySqlCommand("INSERT INTO usuarios_cc (usuario, pass, tipo_usuario, estado, castigos, causa_castigo, fecha) "+
                        "VALUES (@usuario, @pass, @tipo, @estado, @castigo, @causa, @fecha)", cn))
                    {
                        cmd.Parameters.Add(new MySqlParameter("usuario", _usuarios.usuario));
                        cmd.Parameters.Add(new MySqlParameter("pass", _usuarios.pass));
                        cmd.Parameters.Add(new MySqlParameter("tipo", _usuarios.tipo_usuario));
                        cmd.Parameters.Add(new MySqlParameter("estado", _usuarios.estado));
                        cmd.Parameters.Add(new MySqlParameter("castigo", _usuarios.castigos));
                        cmd.Parameters.Add(new MySqlParameter("causa", _usuarios.causa_castigo));
                        cmd.Parameters.Add(new MySqlParameter("fecha", DateTime.Now));
                        //abrimos conexion y ejecutamos
                        cn.Open();
                        // Ejecutamos el comando y regresamos el resultado (True = correcto, False = error)
                        return Convert.ToBoolean(cmd.ExecuteNonQuery());
                    }
                }
                catch (Exception error)
                {

                    throw new Exception(error.Message);
                }
            }
        }

        public static bool EliminarUsuario(Entidades.e_usuarios _usuarios)
        {
            using (var cn = new MySqlConnection(conexion.LeerCC))
            {
                try
                {
                    using (var cmd = new MySqlCommand("DELETE from usuarios_cc WHERE usuario = @usuario", cn))
                    {
                        cmd.Parameters.Add(new MySqlParameter("usuario", _usuarios.usuario));
                        //abrimos conexion y ejecutamos
                        cn.Open();
                        // Ejecutamos el comando y regresamos el resultado (True = correcto, False = error)
                        return Convert.ToBoolean(cmd.ExecuteNonQuery());
                    }
                }
                catch (Exception error)
                {

                    throw new Exception(error.Message);
                }
            }
        }
    }
}
