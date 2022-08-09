using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BodegasAgricolas
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btningresar_Click(object sender, EventArgs e)
        {
            string usuario, contraseña;
            int columna = 2;
            string[] resultado = new string[columna];
            usuario = txtusuario.Text;
            contraseña = txtcontraseña.Text;
            conexion con = new conexion();
            Menu m = new Menu();
            string sql = "select * from usuario where usuario = '" + usuario + "'";

            resultado = con.buscar(sql, columna);

            if (resultado[0].Equals(usuario) && resultado[1].Equals(contraseña))
            {
                m.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Datos incorrectos");
            }

        }

        private void checkver_CheckedChanged(object sender, EventArgs e)
        {
            txtcontraseña.UseSystemPasswordChar = !checkver.Checked;
        }
    }
}
