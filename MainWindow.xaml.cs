using System;
using System.Collections.Generic;
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
using Microsoft.EntityFrameworkCore;
using Personas.BLL;
using Personas.Entidades;
using Personas.UI.Consulta;

namespace Personas
{
   
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Limpiar()
        {
            IdTextBox.Text = string.Empty;
            NombreTextBox.Text = string.Empty;
            
        }

        private Persona LlenaClases()
        {
            Persona persona = new Persona();
            if (string.IsNullOrWhiteSpace(IdTextBox.Text))
            {
                IdTextBox.Text = "0";
            }
            else persona.PersonaId = Convert.ToInt32(IdTextBox.Text);
            persona.Nombre = NombreTextBox.Text;
            
            return persona;
        }

        private void LlenaCampos(Persona persona)
        {
            IdTextBox.Text = Convert.ToString(persona.PersonaId);
            NombreTextBox.Text = persona.Nombre;
            
        }

        private bool Validar()
        {
            bool paso = true;


            if (NombreTextBox.Text == string.Empty)
            {
                MessageBox.Show(NombreTextBox.Text, "No puede estar vacio");
                NombreTextBox.Focus();
                paso = false;
            }

            return paso;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            Persona persona = new Persona();
            int.TryParse(IdTextBox.Text, out id);

            Limpiar();

            persona = PersonaBll.Buscar(id);

            if (persona != null)
            {

                LlenaCampos(persona);
            }
            else
            {
                MessageBox.Show("Persona no Encontrada");
            }
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            Persona persona;
            bool paso = false;

            if (!Validar())
                return;

            persona = LlenaClases();

            if (string.IsNullOrWhiteSpace(IdTextBox.Text) || IdTextBox.Text == "0")
                paso = PersonaBll.Guardar(persona);
            else
            {
                if (!Existe())
                {
                    MessageBox.Show("No Se puede Modificar porque no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                paso = PersonaBll.Modificar(persona);
            }

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado!!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No fue posible guardar!!", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool Existe()
        {
            Persona persona = PersonaBll.Buscar(Convert.ToInt32(IdTextBox.Text));

            return (persona != null);
        }
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            id = Convert.ToInt32(IdTextBox.Text);

            Limpiar();

            if (PersonaBll.Eliminar(id))
                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show(IdTextBox.Text, "No se puede eliminar una persona que no existe");
        }

        private void ConsultaButton_Click(object sender, RoutedEventArgs e)
        {
            Consulta consulta = new Consulta();
            consulta.Show();
        }
    }
}
