using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Propiedades para el enlace de datos
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
        public int Jefe { get; set; }
        public double SueldoBasico { get; set; }


        // Cadena de conexión a la base de datos
        private string connectionString = "Data Source=DAVILA-FERNANDO\\SQLEXPRESS;Initial Catalog=NeptunoDB;User Id=Davila;Password=Davila12";

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("USP_INSERTAR_EMPLEADO", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Agrega los parámetros necesarios para el procedimiento almacenado
                    command.Parameters.AddWithValue("@IdEmpleado", IdEmpleado);
                    command.Parameters.AddWithValue("@Apellidos", Apellidos);
                    command.Parameters.AddWithValue("@Nombre", Nombre);
                    command.Parameters.AddWithValue("@Cargo", Cargo);
                    command.Parameters.AddWithValue("@Tratamiento", Tratamiento);
                    command.Parameters.AddWithValue("@FechaNacimiento", FechaNacimiento);
                    command.Parameters.AddWithValue("@FechaContratacion", FechaContratacion);
                    command.Parameters.AddWithValue("@Direccion", Direccion);
                    command.Parameters.AddWithValue("@Ciudad", Ciudad);
                    command.Parameters.AddWithValue("@Region", Region);
                    command.Parameters.AddWithValue("@CodPostal", CodPostal);
                    command.Parameters.AddWithValue("@Pais", Pais);
                    command.Parameters.AddWithValue("@TelDomicilio", TelDomicilio);
                    command.Parameters.AddWithValue("@Extension", Extension);
                    command.Parameters.AddWithValue("@Notas", Notas);
                    command.Parameters.AddWithValue("@Jefe", Jefe);
                    command.Parameters.AddWithValue("@SueldoBasico", SueldoBasico);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Empleado insertado correctamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar empleado: " + ex.Message);
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
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@IdEmpleado", IdEmpleado);
                    command.Parameters.AddWithValue("@Apellidos", Apellidos);
                    command.Parameters.AddWithValue("@Nombre", Nombre);
                    command.Parameters.AddWithValue("@Cargo", Cargo);
                    command.Parameters.AddWithValue("@Tratamiento", Tratamiento);
                    command.Parameters.AddWithValue("@FechaNacimiento", FechaNacimiento);
                    command.Parameters.AddWithValue("@FechaContratacion", FechaContratacion);
                    command.Parameters.AddWithValue("@Direccion", Direccion);
                    command.Parameters.AddWithValue("@Ciudad", Ciudad);
                    command.Parameters.AddWithValue("@Region", Region);
                    command.Parameters.AddWithValue("@CodPostal", CodPostal);
                    command.Parameters.AddWithValue("@Pais", Pais);
                    command.Parameters.AddWithValue("@TelDomicilio", TelDomicilio);
                    command.Parameters.AddWithValue("@Extension", Extension);
                    command.Parameters.AddWithValue("@Notas", Notas);
                    command.Parameters.AddWithValue("@Jefe", Jefe);
                    command.Parameters.AddWithValue("@SueldoBasico", SueldoBasico);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                MessageBox.Show("Empleado actualizado correctamente.");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al actualizar empleado: " + ex.Message);
            }
        }


        private void Guardar_Click_listar(object sender, RoutedEventArgs e)
        {
            ListarEmpleados listarEmpleados = new ListarEmpleados();
            listarEmpleados.Show();
        }
    }
}
