using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Repaso1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<Empleado> empleados = new List<Empleado>();
        List<Asistencia> asistencias = new List<Asistencia>();        
        List<Reporte> reportes = new List<Reporte>();
        private void LeerAsistencia()
        {
            string fileName = @"Asistencia.txt";
            if (File.Exists(fileName))
            {
                FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);
                while (reader.Peek() > -1)
                {
                    Asistencia asistencia = new Asistencia();
                    asistencia.NoEmpleado = Convert.ToInt16(reader.ReadLine());
                    asistencia.HorasTrabajadas = Convert.ToInt16(reader.ReadLine());
                    asistencia.Mes = Convert.ToInt16(reader.ReadLine());

                    asistencias.Add(asistencia);
                }
                reader.Close();
            }

        }

        private void LeerEmpleado()
        {
            string fileName = @"Empleados.txt";

            if (File.Exists(fileName))
            {

                FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);

                while (reader.Peek() > -1)
                {
                    //Lee 1 empleado del archivo en cada vuelta del ciclo
                    Empleado empleado = new Empleado();
                    empleado.NoEmpleado = Convert.ToInt16(reader.ReadLine());
                    empleado.Nombre = reader.ReadLine();
                    empleado.Apellido = reader.ReadLine();
                    empleado.SueldoHora = Convert.ToDecimal(reader.ReadLine());

                    //Guardar el empleado en la lista
                    empleados.Add(empleado);

                }
                reader.Close();
            }            
        }

        private void GuardarEmpleado()
        {
            FileStream stream = new FileStream(@"Empleados.txt", FileMode.OpenOrCreate, FileAccess.Write);            
            StreamWriter writer = new StreamWriter(stream);
            foreach (var empleado in empleados)
            {
                writer.WriteLine(empleado.NoEmpleado);
                writer.WriteLine(empleado.Nombre);
                writer.WriteLine(empleado.Apellido);
                writer.WriteLine(empleado.SueldoHora);
            }            
            writer.Close();
        }

        private void Mostrar ()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = empleados;

            dataGridViewAsistencia.DataSource = null;
            dataGridViewAsistencia.DataSource = asistencias;

            dataGridViewReporte.DataSource = null;
            dataGridViewReporte.DataSource = reportes;
        }

        private void buttonIngreso_Click(object sender, EventArgs e)
        {
            //Guardar los del empleado
            Empleado empleado = new Empleado();
            empleado.NoEmpleado = Convert.ToInt16 (numericUpDownEmpleado.Value);
            empleado.Nombre = textBoxNombre.Text;
            empleado.Apellido = textBoxApellido.Text;
            empleado.SueldoHora = numericUpDownSueldoHora.Value;

            //Guardar al empleado en la lista de empleados
            empleados.Add(empleado);

            //Guardar la lista de empleados en el archivo
            GuardarEmpleado();

            //Mostrar en pantalla
            Mostrar();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LeerEmpleado();
            LeerAsistencia();
            Mostrar();

            numericUpDownEmpleado.Value = empleados.Count + 1;

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void buttonReporte_Click(object sender, EventArgs e)
        {
            foreach (var empleado in empleados)
            {
                foreach (var asistencia in asistencias)
                {
                    if (empleado.NoEmpleado == asistencia.NoEmpleado)
                    {
                        Reporte reporte = new Reporte();
                        reporte.Nombre = empleado.Nombre;
                        reporte.Apellido = empleado.Apellido;
                        reporte.SueldoMes = empleado.SueldoHora
                                           * asistencia.HorasTrabajadas;
                        reporte.Mes = asistencia.Mes;

                        reportes.Add(reporte);
                    }

                }                
            }
            Mostrar();
        }
    }
}
