using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Data;
using Entidades;

namespace Datos
{
    public class d_registros
    {
        //metodo para insertar un registro 
        public static int InsertarRegistro(Entidades.e_registros _registro)
        {
            try
            {
                using (var con = new MySqlConnection(conexion.LeerCC))
                {
                    using (var cmd = new MySqlCommand("Insertar_Registro", con))
                    {
                        //establecemos los parametros
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new MySqlParameter("_cuenta", _registro.usuario));
                        cmd.Parameters.Add(new MySqlParameter("_maquina", _registro.equipo));
                        cmd.Parameters.Add(new MySqlParameter("_fecha", _registro.fecha));
                        cmd.Parameters.Add(new MySqlParameter("_hora_inicio", _registro.hora_inicio));

                        //abrimos conexion y ejecutamos
                        con.Open();
                        // Ejecutamos el comando y regresamos el resultado (True = correcto, False = error)
                        cmd.ExecuteNonQuery();
                        int Id = 0;
                        object ores = MySqlHelper.ExecuteScalar(con, "SELECT LAST_INSERT_ID();");
                        if (ores != null)
                        {
                            // Odd, I got ulong here.
                            ulong qkwl = (ulong)ores;
                            Id = (int)qkwl;
                        }

                        return Convert.ToInt32(Id);
                    }
                }
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }

        }

        //metodo para actualizar la hora de salida al dar click al boton de finalizar
        public static bool ActualizaSalida(e_registros _registro)
        {
            try
            {
                using (var con = new MySqlConnection(conexion.LeerCC))
                {
                    using (var cmd = new MySqlCommand("UPDATE registro_uso_CC SET hora_fin = @hora_fin WHERE id_registro = @id", con))
                    {
                        //establecemos los parametros
                        cmd.Parameters.Add(new MySqlParameter("hora_fin", _registro.hora_fin));
                        cmd.Parameters.Add(new MySqlParameter("id", _registro.id_registro));

                        //abrimos conexion y ejecutamos
                        con.Open();
                        // Ejecutamos el comando y regresamos el resultado (True = correcto, False = error)                  
                        return Convert.ToBoolean(cmd.ExecuteNonQuery());
                    }
                }
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }
    }
}
