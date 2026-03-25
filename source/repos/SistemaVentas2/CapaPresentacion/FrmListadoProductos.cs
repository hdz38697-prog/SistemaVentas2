using CapaNegocio;
using System;
using System.Data;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmListadoProductos : Form
    {
        public FrmListadoProductos()
        {
            InitializeComponent();
        }

        private void FrmListadoProductos_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            Mostrar();
        }

        private void Mostrar()
        {
            dlistado.DataSource = CNProducto.Listar();
        }

        private void BuscarNombre()
        {
            dlistado.DataSource = CNProducto.BuscarNombre(txtbuscar.Text);
        }

        private void BuscarCodigo()
        {
            dlistado.DataSource = CNProducto.BuscarCodigo(txtbuscar.Text);
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            if (rbtnnombre.Checked)
            {
                BuscarNombre();
            }
            else if (rbtncodigo.Checked)
            {
                BuscarCodigo();
            }
            else
            {
                MessageBox.Show("Seleccione un criterio de búsqueda.",
                    "Sistema de Ventas",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnnuevo_Click(object sender, EventArgs e)
        {
            FrmRegistrarProducto form = new FrmRegistrarProducto();
            form.Show();
            form.Insert = true;
            this.Hide();
        }

        private void btneditar_Click(object sender, EventArgs e)
        {
            FrmRegistrarProducto form = new FrmRegistrarProducto();
            form.Edit = true;
            form.Insert = false;


            form.txtidproducto.Text = dlistado.CurrentRow.Cells["idproducto"].Value.ToString();
            form.txtcodigo.Text = dlistado.CurrentRow.Cells["codigo"].Value.ToString();
            form.txtnombre.Text = dlistado.CurrentRow.Cells["nombre"].Value.ToString();
            form.txtdescripcion.Text = dlistado.CurrentRow.Cells["descripcion"].Value.ToString();
            form.dtfechaingreso.Value = Convert.ToDateTime(this.dlistado.CurrentRow.Cells["f_ingreso"].Value);
            form.dtfechaingreso.Value = Convert.ToDateTime(this.dlistado.CurrentRow.Cells["f_ingreso"].Value);
            form.txtpcompra.Text = dlistado.CurrentRow.Cells["precio_compra"].Value.ToString();
            form.txtpventa.Text = dlistado.CurrentRow.Cells["precio_venta"].Value.ToString();
            form.txtcantidad.Text = dlistado.CurrentRow.Cells["stock"].Value.ToString();
            form.cmbidcategoria.Text = this.dlistado.CurrentRow.Cells["idcategoria"].Value.ToString();

            string estado = dlistado.CurrentRow.Cells["estado"].Value.ToString();

            if (estado == "ACTIVO")
            {
                form.rbtnactivo.Checked = true;

            }
            else
            {
                form.rbtninactivo.Checked = true;

            }

            form.Show();
            this.Hide();
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion = MessageBox.Show(
                    "¿Realmente desea eliminar el registro?",
                    "Sistema de ventas",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question);

                if (opcion == DialogResult.OK)
                {
                    int idproducto = Convert.ToInt32(dlistado.CurrentRow.Cells["idproducto"].Value);
                    CNProducto.Eliminar(idproducto);

                    MessageBox.Show("Registro eliminado",
                        "Sistema de ventas",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    Mostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Buscar()
        {
            if (rbtnnombre.Checked)
            {
                this.dlistado.DataSource = CNProducto.BuscarNombre(this.txtbuscar.Text);
            }
            else if (rbtncodigo.Checked)
            {
                this.dlistado.DataSource = CNProducto.BuscarCodigo(this.txtbuscar.Text);
            }
        }


        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            this.Buscar();
        }
    }
}