using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using MySql.Data.MySqlClient;

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
            lbfecha.Text = DateTime.Now.Date.ToString("yyyy/MM/dd");
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

            crearPDF();


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

        private void crearPDF()
        {
            try
            {

           
            PdfWriter pdfWriter = new PdfWriter("Reporte_proveedor.pdf");
            PdfDocument pdf = new PdfDocument(pdfWriter);
            // 1 pulgada = 72 pt (8 1/2 x 11) (8.5*72) (612x792)
            PageSize tamanioH = new PageSize(792, 612);
            Document documento = new Document(pdf, tamanioH);

            documento.SetMargins(60, 20, 55, 20);

            PdfFont fontColumnas = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            PdfFont fontContenido = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            string[] columnas = { "NO.Documento", "Codigo Proveedor", "Fecha", "Total", "Estatus" };

            float[] tamanios = { 2, 2, 4, 4, 2 };
            Table tabla = new Table(UnitValue.CreatePercentArray(tamanios));
            tabla.SetWidth(UnitValue.CreatePercentValue(100));

            foreach (string columna in columnas)
            {
                tabla.AddHeaderCell(new Cell().Add(new Paragraph(columna).SetFont(fontColumnas)));
            }

            string sql6 = "SELECT e.documento_compraenca, e.codigo_proveedor, e.fecha_compraenca, e.total_compraenca, e.estatus_compraenca FROM compras_encabezado AS e ";

            MySqlConnection conexionBD = conexionR.conexion();
            conexionBD.Open();

            MySqlCommand comando = new MySqlCommand(sql6, conexionBD);
            MySqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {



                tabla.AddCell(new Cell().Add(new Paragraph(reader["documento_compraenca"].ToString()).SetFont(fontContenido)));
                tabla.AddCell(new Cell().Add(new Paragraph(reader["codigo_proveedor"].ToString()).SetFont(fontContenido)));
                tabla.AddCell(new Cell().Add(new Paragraph(reader["fecha_compraenca"].ToString()).SetFont(fontContenido)));
                tabla.AddCell(new Cell().Add(new Paragraph(reader["total_compraenca"].ToString()).SetFont(fontContenido)));
                tabla.AddCell(new Cell().Add(new Paragraph(reader["estatus_compraenca"].ToString()).SetFont(fontContenido)));

            }

            documento.Add(tabla);
            documento.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show(""+e);
            }


        }
    }
}
