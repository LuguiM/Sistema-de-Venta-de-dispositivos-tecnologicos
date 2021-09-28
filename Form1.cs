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
            OleDbConnection conexion_access = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|login.accdb");
            

            conexion_access.Open();

            string consulta = "select password,usuario from login where password = '" + textBox2.Text + "' and usuario = '" + textBox1.Text + "';";

            OleDbCommand comando = new OleDbCommand(consulta, conexion_access);
            OleDbDataReader leedb;
            leedb = comando.ExecuteReader();
            Boolean existereg = leedb.HasRows;

            if (existereg)
            {
                MessageBox.Show("bienvenido al sistema" + textBox1.Text);
                fmenu f2 = new fmenu();
                f2.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("usuario o contraseña incorrecto trate de nuevo");
                return;

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
