using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;


namespace CapaPresentacion
{
    public partial class FrmListadoClientes : Form
    {
        public FrmListadoClientes()
        {
            InitializeComponent();
        }

        private void FrmListadoClientes_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            Mostrar();
        }

        //metodo para mostrar los registros en el datagridview
        public void Mostrar()
        {
            this.dlistado.DataSource = CNCliente.Listar();
        }
        //metodo para buscar los clientes por nombre
        public void BuscarNombre()
        {
            this.dlistado.DataSource = CNCliente.BuscarNombre(this.txtbuscar.Text);
        }
        //metodo para buscar clientes por DNI
        public void BuscarDni()
        {
            this.dlistado.DataSource = CNCliente.BuscarDni(this.txtbuscar.Text);

        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            if (rbtnnombre.Checked)
            {
                BuscarNombre();
            }
            else if (rbtndni.Checked)
            {
                BuscarDni();
            }
            else
            {
                MessageBox.Show("seleccione un crriterio de busuqeda.",
                    "Sistema de Ventas,",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnnuevo_Click(object sender, EventArgs e)
        {
            FrmRegisClientes form = new FrmRegisClientes();

            form.Insert = true;

            form.Show();
            this.Hide();
        }

        private void btneditar_Click(object sender, EventArgs e)
        {
            FrmRegisClientes form = new FrmRegisClientes();

            form.Edit = true; 

            form.txtidcliente.Text  = this.dlistado.CurrentRow.Cells["idcliente"].Value.ToString();
            form.txtnombre.Text     = this.dlistado.CurrentRow.Cells["nombre"   ].Value.ToString();
            form.txtapellidos.Text  = this.dlistado.CurrentRow.Cells["apellidos"].Value.ToString();
            form.txtdni.Text        = this.dlistado.CurrentRow.Cells["dni"      ].Value.ToString();
            form.txtrfc.Text        = this.dlistado.CurrentRow.Cells["rfc"      ].Value.ToString();
            form.txttelefono.Text   = this.dlistado.CurrentRow.Cells["telefono" ].Value.ToString();

            string estado = this.dlistado.CurrentRow.Cells["estado"].Value.ToString();

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
                DialogResult opcion;
                opcion = MessageBox.Show("¿Realmente desea eliminar el(los) los registro(s)?",
                    "Sistema de ventas",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question);
                if (dlistado.SelectedRows.Count > 0 )
                {
                    if (opcion == DialogResult.OK)
                    {
                        string idcliente = dlistado.CurrentRow.Cells["idcliente"].Value.ToString();
                        CNCliente.Eliminar(Convert.ToInt32(idcliente));

                        MessageBox.Show("Registro eliminado",
                            "sistema de ventas",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        Mostrar();
                    }
                }
                Mostrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
