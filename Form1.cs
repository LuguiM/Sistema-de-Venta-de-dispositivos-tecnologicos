using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data.OleDb;

namespace Venta_de_dispositivos_tecnologicos
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection conexion_access = new OleDbConnection();
            conexion_access.ConnectionString = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = C:\sistema\sistema.mdb;Persist Security Info=False;";

            conexion_access.Open();

            OleDbDataAdapter consulta = new OleDbDataAdapter("SELECT * FROM tusuario", conexion_access);

            DataSet resultado = new DataSet();
            consulta.Fill(resultado);

            foreach (DataRow registro in resultado.Tables[0].Rows)
            {
                if ((textBox1.Text == registro["nombre"].ToString()) && (textBox2.Text == registro["clave"].ToString()))
                {
                    fmenu fm = new fmenu();
                    fm.Show();
                    this.Hide();
                }
            }
            try
            {

            }
        

        catch (Exception err)
            {
                MessageBox.Show(err.Message);
                MessageBox.Show("Error de usuario o clave de acceso", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
            }
            conexion_access.Close();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conexion = new SqlConnection(@"server=.\DESKTOP-PMRS7B8\SQL4; Initial Catalog = master; Integrated Security=True;");
                conexion.Open();

                string consultax;
                consultax = "select usuario, pass from Usuario where usuario = '" + textBox1.Text + "'And pass = '" + textBox2.Text + "' ";
                SqlCommand consulta = new SqlCommand(consultax, conexion);
                consulta.ExecuteNonQuery();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(consulta);

                da.Fill(ds, "usuario");

                DataRow registro;
                registro = ds.Tables["usuario"].Rows[0];

                if ((textBox1.Text == registro["usuario"].ToString()) || (textBox2.Text == registro["pass"].ToString()))
                {
                    fmenu fm = new fmenu();
                    fm.Show();
                    this.Hide();
                }
            }
            catch
            {
                MessageBox.Show("Error de usuario o clave de acceso", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    

    private void button3_Click(object sender, EventArgs e)
        {
            MySqlConnection conectar = new MySqlConnection("server=localhost;database=sistema_usuarios;id=root;password=grupocuatro;");
            conectar.Open();

            MySqlCommand codigo = new MySqlCommand();
            MySqlConnection conectanos = new MySqlConnection();
            codigo.Connection = conectar;

            codigo.CommandText = ("Select *from usuarios where usuarios= '" + textBox1.Text + "' and password = '" + textBox2 + "'  ");

            MySqlDataReader leer = codigo.ExecuteReader();

            if (leer.Read())
            {
                MessageBox.Show("Bienvenidos");
                fmenu fm = new fmenu();
                fm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario o Contraseña incorrectos");
            }
            conectar.Close();

        }
    }
}
