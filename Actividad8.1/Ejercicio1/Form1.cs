using Ejercicio1.Models;
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

namespace Ejercicio1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<Cuenta> cuentas = new List<Cuenta>();
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            string nombre = tbNombre.Text;
            int dni = Convert.ToInt32(tbDNI.Text);
            double importe = Convert.ToDouble(tbImporte.Text);

            Cuenta nueva = new Cuenta(dni, nombre, importe);

            cuentas.Sort();
            int idx = cuentas.BinarySearch(nueva);

            if (idx >= 0)
            {
                cuentas[idx].Nombre = nueva.Nombre;
                cuentas[idx].Importe += nueva.Importe;
            }
            else
            {
                cuentas.Add(nueva);
            }

            btnActualizar.PerformClick();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            lsbVer.Items.Clear();
            foreach (Cuenta cuenta in cuentas)
            {
                lsbVer.Items.Add(cuenta);
            }
        }

        private void lsbVer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cuenta selectedCuenta = lsbVer.SelectedItem as Cuenta;
            if ( selectedCuenta != null)
            {
                tbDNI.Text = selectedCuenta.DNI.ToString();
                tbNombre.Text = selectedCuenta.Nombre;
                tbImporte.Text = selectedCuenta.Importe.ToString();
            }
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog1.FileName;
                FileStream fs = null;
                StreamReader sr = null;

                try
                {
                    fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                    sr = new StreamReader(fs);

                    while (sr.EndOfStream == false)
                    {
                        string linea = sr.ReadLine();

                        string dni = linea.Substring(0, 9).Trim();
                        string nombre = linea.Substring(9, 10).Trim();
                        string importe = linea.Substring(19, 9).Trim();

                        Cuenta nuevo = new Cuenta(Convert.ToInt32(dni), nombre, Convert.ToDouble(importe));
                        cuentas.Add(nuevo);
                    }
                }
                catch (Exception ex)  
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (sr!=null) sr.Close();
                    if (fs!=null) fs.Close();
                }
            }
            btnActualizar.PerformClick();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog1.FileName;
                FileStream fs = null;
                StreamWriter sw = null;

                try
                {
                    fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                    sw = new StreamWriter(fs);

                    foreach (Cuenta cuenta in cuentas)
                    {
                        string nombre = cuenta.Nombre;
                        if (cuenta.Nombre.Length > 10)
                        {
                            nombre = nombre.Substring(0, 10);

                            string linea = $"{cuenta.DNI,+9} {nombre,-10} {cuenta.Importe,+9:f2}";

                            sw.WriteLine(linea);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (fs!=null) fs.Close(); 
                    if (sw!=null) sw.Close();
                }
            }
        }
    }
}
