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
    /// Lógica de interacción para ListarEmpleado.xaml
    /// </summary>
    public partial class ListarEmpleados : Window
    {
        public class Empleado
        {
            public int IdEmpleado { get; set; }
            public string Apellidos { get; set; }
            public string Nombre { get; set; }
            public string cargo { get; set; }
            public string Tratamiento { get; set; }
        }
        private string connectionString = "Data Source=DAVILA-FERNANDO\\SQLEXPRESS;Initial Catalog=NeptunoDB;User Id=Davila;Password=Davila12";
        //private DataTable dataTable = new DataTable();

        public ListarEmpleados()
        {
            InitializeComponent();
            CargarDatos();
        }
        private void EliminarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridEmpleados.SelectedItem != null)
            {
                Empleado empleadoSeleccionado = (Empleado)dataGridEmpleados.SelectedItem;
                int idEmpleado = empleadoSeleccionado.IdEmpleado;
                EliminarEmpleado(idEmpleado);
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un empleado para eliminar.");
            }
        }


        private void CargarDatos()
        {
            try
            {
                List<Empleado> empleados = new List<Empleado>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("USP_LISTAREMPLEADOS", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int IdEmpleado = reader.GetInt32(reader.GetOrdinal("IdEmpleado"));
                        string Apellidos = reader.GetString(reader.GetOrdinal("Apellidos"));
                        string Nombre = reader.GetString(reader.GetOrdinal("Nombre"));
                        string cargo = reader.GetString(reader.GetOrdinal("cargo"));
                        string Tratamiento = reader.GetString(reader.GetOrdinal("Tratamiento"));
                        empleados.Add(new Empleado { IdEmpleado = IdEmpleado, Apellidos = Apellidos, Nombre = Nombre, cargo = cargo, Tratamiento = Tratamiento });
                    }
                    connection.Close();
                }
                dataGridEmpleados.ItemsSource = empleados;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ActualizarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el empleado seleccionado
            if (dataGridEmpleados.SelectedItem != null)
            {
                // Obtener el ID del empleado seleccionado
                Empleado empleadoSeleccionado = (Empleado)dataGridEmpleados.SelectedItem;
                int idEmpleado = empleadoSeleccionado.IdEmpleado;

                // Abrir una nueva ventana de actualización de empleado con el ID del empleado seleccionado
                ActualizarEmpleado ventanaActualizarEmpleado = new ActualizarEmpleado(idEmpleado);
                ventanaActualizarEmpleado.Show(); // Llamar al método Show() sobre la instancia de ActualizarEmpleado

                // Actualizar los datos en el DataGrid después de cerrar la ventana de actualización
                CargarDatos();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un empleado para actualizar.");
            }
        }


        private void EliminarEmpleado(int idEmpleado)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("USP_ELIMINAREMPLEADO", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                MessageBox.Show("Empleado eliminado correctamente.");
                CargarDatos(); // Actualizar la lista después de eliminar el empleado
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar eliminar empleado: " + ex.Message);
            }
        }


    }
}
