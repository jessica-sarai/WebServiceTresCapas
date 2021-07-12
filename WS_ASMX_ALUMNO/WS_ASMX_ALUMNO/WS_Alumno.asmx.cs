using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;


namespace WS_ASMX_ALUMNO
{
    /// <summary>
    /// Descripción breve de WS_Alumno
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class WS_Alumno : System.Web.Services.WebService
    {
        private string _conecion;

        public WS_Alumno()
        {
            _conecion = ConfigurationManager.ConnectionStrings["EstatusTich"].ConnectionString;
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }

        [WebMethod]
        public string eliminar2(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_conecion))
                {
                    SqlCommand cmd = new SqlCommand("SP_EliminarAlumno", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    int contador=cmd.ExecuteNonQuery();

                    
                    if (contador>0)
                    {
                        return "Una fila afectada";
                    }
                    else 
                    {
                        return "Ninguna fila afectada";
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                
                return "Error al eliminar";
            }

           
        }

        [WebMethod]

        public string eliminarModal(string id)
        {
            string mensaje;
           
            try
            {
                int idR = Convert.ToInt32(id);
                using (SqlConnection con = new SqlConnection(_conecion))
                {
                    SqlCommand cmd = new SqlCommand("SP_EliminarAlumno", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", idR);
                     cmd.ExecuteNonQuery();
                    con.Close();
                   
                    
                }
                mensaje = "EXITO";
            }
            catch
            {

                mensaje = "Error..";
            }
            string respuesta = JsonConvert.SerializeObject(
                new
                {
                    respuesta = mensaje
                }
                );

            return respuesta;
        }

    }
}
