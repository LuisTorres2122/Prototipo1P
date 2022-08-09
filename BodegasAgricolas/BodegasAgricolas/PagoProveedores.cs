using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BodegasAgricolas
{
    public partial class PagoProveedores : Form
    {
        public PagoProveedores()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void PagoProveedores_Load(object sender, EventArgs e)
        {
            lbfecha.Text = DateTime.Now.Date.ToString("yyyyMMdd");
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string codigo, nombre;
            
            string[] proveedor = new string[6];
            codigo = txtcodigo.Text;
            nombre = txtnombre.Text;
            

            string sql = "select * from proveedores where codigo_proveedor = '"+ codigo +"'";
            conexion con = new conexion();
            proveedor = con.buscar(sql,6);

            txtnombre.Text = proveedor[1];
            txtnit.Text = proveedor[4];


        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            conexion con = new conexion();
            int cantidad, precio;
            cantidad = int.Parse(txtcantidad.Text);
            precio = int.Parse(txtprecio.Text);

            string sql = "insert into compras_detalle values('"+ txtndocumento.Text +  "','"+ txtcproducto.Text +"',"+ txtcantidad.Text+"," + txtprecio.Text +",'"+ txtbodega.Text +"')";
            con.IDU(sql);

           string sql2 = "select * from compras_detalle where documento_compraenca = '" + txtndocumento.Text +"'";
            tabla.DataSource = con.Busqueda(sql2);
            

        }

        private void btntotal_Click(object sender, EventArgs e)
        {
            int  total = 0;   
            foreach (DataGridViewRow row in tabla.Rows)
            {
                if (row.Cells["cantidad_compradet"].Value != null)
                    total += ((Int32)row.Cells["cantidad_compradet"].Value) * ((Int32)row.Cells["costo_compradet"].Value);
                  
            }

           

            txttotal.Text = total.ToString();

            string sql = "Update compras_encabezado Set total_compraenca = " + total + " where documento_compraenca = '"+ txtndocumento.Text +"'";
            conexion con = new conexion();
            con.IDU(sql);
            MessageBox.Show("El total a pagar a el proveedor: "+txtnombre+" Es de: Q"+txttotal);


        }

        private void button2_Click(object sender, EventArgs e)
        {
            conexion con = new conexion();


            string sql = "insert into compras_encabezado values('" + txtndocumento.Text + "','" + txtcodigo.Text + "','" + DateTime.Now.Date.ToString("yyyyMMdd") + "'," + 0 + ",'" + 1 + "')";
            con.IDU(sql);
        }

        private void lbfecha_Click(object sender, EventArgs e)
        {

        }
    }
}
