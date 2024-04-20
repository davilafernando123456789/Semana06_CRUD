using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Semana06
{
    /// <summary>
    /// Lógica de interacción para ActualizarEmpleado.xaml
    /// </summary>
    public partial class ActualizarEmpleado : Window
    {
        public int IdEmpleado { get; set; }
        public string Apellidos { get; set; }
        public string Nombre { get; set; }
        public string Cargo { get; set; }
        public string Tratamiento { get; set; }
        public string FechaNacimiento { get; set; }
        public string FechaContratacion { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Region { get; set; }
        public string CodPostal { get; set; }
        public string Pais { get; set; }
        public string TelDomicilio { get; set; }
        public string Extension { get; set; }
        public string Notas { get; set; }
        public int? Jefe { get; set; } // Cambio: Jefe puede ser nulo
        public double SueldoBasico { get; set; }

        private int idEmpleado;
        private string connectionString = "Data Source=DAVILA-FERNANDO\\SQLEXPRESS;Initial Catalog=NeptunoDB;User Id=Davila;Password=Davila12";

        public ActualizarEmpleado(int idEmpleado)
        {
            InitializeComponent();
            DataContext = this; // Establecer el objeto ActualizarEmpleado como el contexto de datos
            this.idEmpleado = idEmpleado;
            CargarDatosEmpleado();
        }

        private void CargarDatosEmpleado()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("USP_OBTENEREMPLEADO", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdEmpleado", idEmpleado);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        IdEmpleado = reader.GetInt32(reader.GetOrdinal("IdEmpleado"));
                        Apellidos = reader.GetString(reader.GetOrdinal("Apellidos"));
                        Nombre = reader.GetString(reader.GetOrdinal("Nombre"));
                        Cargo = reader.GetString(reader.GetOrdinal("cargo"));
                        Tratamiento = reader.GetString(reader.GetOrdinal("Tratamiento"));
                        FechaNacimiento = reader.GetDateTime(reader.GetOrdinal("FechaNacimiento")).ToString("yyyy-MM-dd"); // Cambio: Formato de fecha
                        FechaContratacion = reader.GetDateTime(reader.GetOrdinal("FechaContratacion")).ToString("yyyy-MM-dd"); // Cambio: Formato de fecha
                        Direccion = reader.GetString(reader.GetOrdinal("direccion"));
                        Ciudad = reader.GetString(reader.GetOrdinal("ciudad"));
                        Region = reader.GetString(reader.GetOrdinal("region"));
                        CodPostal = reader.GetString(reader.GetOrdinal("codPostal"));
                        Pais = reader.GetString(reader.GetOrdinal("pais"));
                        TelDomicilio = reader.GetString(reader.GetOrdinal("TelDomicilio"));
                        Extension = reader.IsDBNull(reader.GetOrdinal("Extension")) ? null : reader.GetString(reader.GetOrdinal("Extension")); // Cambio: Manejo de valores nulos
                        Notas = reader.IsDBNull(reader.GetOrdinal("notas")) ? null : reader.GetString(reader.GetOrdinal("notas")); // Cambio: Manejo de valores nulos
                        Jefe = reader.IsDBNull(reader.GetOrdinal("Jefe")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("Jefe")); // Cambio: Manejo de valores nulos
                        SueldoBasico = (double)reader.GetDecimal(reader.GetOrdinal("sueldoBasico"));

                    }
                    reader.Close();
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al cargar datos del empleado: " + ex.Message);
            }
        }

        private void ActualizarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("USP_ACTUALIZAREMPLEADO", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                    command.Parameters.AddWithValue("@Apellidos", Apellidos);
                    command.Parameters.AddWithValue("@Nombre", Nombre);
                    command.Parameters.AddWithValue("@Cargo", Cargo);
                    command.Parameters.AddWithValue("@Tratamiento", Tratamiento);
                    command.Parameters.AddWithValue("@FechaNacimiento", DateTime.ParseExact(FechaNacimiento, "yyyy-MM-dd", null)); // Cambio: Formato de fecha
                    command.Parameters.AddWithValue("@FechaContratacion", DateTime.ParseExact(FechaContratacion, "yyyy-MM-dd", null)); // Cambio: Formato de fecha
                    command.Parameters.AddWithValue("@Direccion", Direccion);
                    command.Parameters.AddWithValue("@Ciudad", Ciudad);
                    command.Parameters.AddWithValue("@Region", Region);
                    command.Parameters.AddWithValue("@CodPostal", CodPostal);
                    command.Parameters.AddWithValue("@Pais", Pais);
                    command.Parameters.AddWithValue("@TelDomicilio", TelDomicilio);
                    command.Parameters.AddWithValue("@Extension", Extension ?? (object)DBNull.Value); // Cambio: Manejo de valores nulos
                    command.Parameters.AddWithValue("@Notas", Notas ?? (object)DBNull.Value); // Cambio: Manejo de valores nulos
                    command.Parameters.AddWithValue("@Jefe", Jefe ?? (object)DBNull.Value); // Cambio: Manejo de valores nulos
                    command.Parameters.AddWithValue("@SueldoBasico", SueldoBasico);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                MessageBox.Show("Empleado actualizado correctamente.");
                this.Close();
                ListarEmpleados listar = new ListarEmpleados();
                listar.Show(); 

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al actualizar empleado: " + ex.Message);
            }
        }
    }
}
