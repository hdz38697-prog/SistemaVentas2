using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CapaPresentacion
{
    public partial class FrmRegistrarProducto : Form
    {
        public bool Insert = false;
        public bool Edit = false;

        public FrmRegistrarProducto()
        {
            InitializeComponent();
        }

        private void FrmRegistrarProducto_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            CargarCategorias();
        }
        private void CargarCategorias()
        {
            cmbidcategoria.DataSource = CNCategoria.Listar();
            cmbidcategoria.DisplayMember = "descripcion";
            cmbidcategoria.ValueMember = "idcategoria";
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            string estado = "";


            try
            {
                if (this.txtnombre.Text == string.Empty || this.txtcodigo.Text == string.Empty)
                {
                    MessageBox.Show("Ingrese los datos del producto", "Sistema de ventas",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (this.Insert == true)
                    {
                        CNProducto.Guardar(
                                        this.txtcodigo.Text,
                                        this.txtnombre.Text,
                                        this.txtdescripcion.Text,
                                        this.dtfechaingreso.Value,
                                        this.dtfechavencimiento.Value,
                                        Convert.ToDouble(this.txtpcompra.Text),
                                        Convert.ToDouble(this.txtpventa.Text),
                                        Convert.ToInt32(this.txtcantidad.Text),
                                        estado,
                                        Convert.ToInt32(cmbidcategoria.SelectedValue));
                        MessageBox.Show("Producto registrado correctamente", "Sistema de ventas",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (this.Edit == true)
                    {
                        CNProducto.Editar(
                                        Convert.ToInt32(this.txtidproducto.Text),
                                        this.txtcodigo.Text,
                                        this.txtnombre.Text,
                                        this.txtdescripcion.Text,
                                        this.dtfechaingreso.Value,
                                        this.dtfechavencimiento.Value,
                                        Convert.ToDouble(this.txtpcompra.Text),
                                        Convert.ToDouble(this.txtpventa.Text),
                                        Convert.ToInt32(this.txtcantidad.Text),
                                        estado,
                                        Convert.ToInt32(cmbidcategoria.SelectedValue));

                        MessageBox.Show("Producto editado correctamente", "Sistema de ventas",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    this.Insert = false;
                    this.Edit = false;

                    FrmListadoProductos form = new FrmListadoProductos();
                    form.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message);
            }
        }
        
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            FrmListadoProductos form = new FrmListadoProductos();
            form.Show();
            this.Hide();
        }

        
    }
}
