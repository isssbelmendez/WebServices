using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Windows.Input;
using System.Xml;

namespace WServices
{
    /// <summary>
    /// Descripción breve de WebService2
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService2 : System.Web.Services.WebService
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;

        //CREACION DEL METODO PARA INSERTAR DATOS//
        [WebMethod]
        public XmlDocument CreateUsario(string nombre, string apellido, int telefono)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO estudiante (nombre,apellido, telefono) VALUES (@nombre,@apellido, @telefono)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@nombre", nombre);
                command.Parameters.AddWithValue("@apellido", apellido);
                command.Parameters.AddWithValue("@telefono", telefono);

                connection.Open();
                command.ExecuteNonQuery();

            }

            XmlDocument xmlResponse = new XmlDocument();

            XmlElement rootElement = xmlResponse.CreateElement("Response");
            xmlResponse.AppendChild(rootElement);

            XmlElement responseElement = xmlResponse.CreateElement("Message");
            responseElement.InnerText = "Datos registrado correctamente .";
            rootElement.AppendChild(responseElement);

            return xmlResponse;

        }

        // CREACION DEL METODO PARA LEER DATOS
        [WebMethod]
        public DataSet GetUsuarios()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM estudiante";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();

                connection.Open();
                adapter.Fill(dataSet, "estudiante");

                return dataSet;
            }
        }

        //CREACION DEL METODO PARA MODIFICAR DATOS//
        [WebMethod]
        public XmlDocument UpateUsuario(int id, string nuevoNombre, string nuevoApellido, int nuevoTelefono)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE estudiante SET nombre = @nuevoNombre, apellido = @nuevoApellido, telefono = @nuevoTelefono WHERE id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@nuevoNombre", nuevoNombre);
                command.Parameters.AddWithValue("@nuevoApellido", nuevoApellido);
                command.Parameters.AddWithValue("@nuevoTelefono", nuevoTelefono);
                command.Parameters.AddWithValue("id", id);

                connection.Open();
                command.ExecuteNonQuery();
                
            }
            XmlDocument xmlResponse = new XmlDocument();

            XmlElement rootElement = xmlResponse.CreateElement("Response");
            xmlResponse.AppendChild(rootElement);

            XmlElement responseElement = xmlResponse.CreateElement("Message");
            responseElement.InnerText = "Se ha realiado una modificacion correctamente";
            rootElement.AppendChild(responseElement);

            return xmlResponse;
        }


        //CREACION DEL METODO PARA ELIMINAR //
        [WebMethod]
        public XmlDocument DeleteUsuario(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM estudiante WHERE id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                command.ExecuteNonQuery ();
            }
            XmlDocument xmlResponse = new XmlDocument ();

            XmlElement rootElement = xmlResponse.CreateElement("Response");
            xmlResponse.AppendChild(rootElement);

            XmlElement responseElement = xmlResponse.CreateElement("Message");
            responseElement.InnerText = "Registro Eliminado correctamente.";
            rootElement.AppendChild(responseElement);

            return xmlResponse;
        }


    }
}

