using disqueria;
using dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace discos
{
    public partial class frmAgregarDisco : Form
    {
        private Discos disco = null;
        private OpenFileDialog archivo = null;
        public frmAgregarDisco(Discos disco)
        {
            InitializeComponent();
            Text = "Modificar disco";
            this.disco = disco;
        }
        public frmAgregarDisco()
        {
            InitializeComponent();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            DiscosNegocio negocio = new DiscosNegocio();
            try
            {
                if (disco == null)
                {
                    disco = new Discos();
                }
                disco.Titulo = txtTitulo.Text;
                disco.FechaLanzamiento = DateTime.Parse(txtFechaLanzamiento.Text);
                disco.CantidadCanciones = int.Parse(txtCantidadCanciones.Text);
                disco.UrlImagenTapa = txtUrlImagenTapa.Text;
                disco.Edicion = (TipoEdicion)cboTipoEdicion.SelectedItem;
                disco.Estilo = (Estilo)cboEstilo.SelectedItem;
                if (disco.Id != 0)
                {
                    negocio.modificar(disco);
                    MessageBox.Show("Disco modificado con éxito.");
                }
                else
                {
                    negocio.agregarDisco(disco);
                    MessageBox.Show("Disco agregado con éxito.");
                }
                if (archivo != null && txtUrlImagenTapa.Text.ToUpper().Contains("HTTPS"))
                    File.Copy(archivo.FileName, ConfigurationManager.AppSettings["disco-app"] + archivo.SafeFileName);
                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void frmAgregarDisco_Load(object sender, EventArgs e)
        {
            EstiloNegocio estiloNegocio = new EstiloNegocio();
            EdicionNegocio edicionNegocio = new EdicionNegocio();
            try
            {
                cboEstilo.DataSource = estiloNegocio.listar();
                cboEstilo.ValueMember = "Id";
                cboEstilo.DisplayMember = "Descripcion";
                cboTipoEdicion.DataSource = edicionNegocio.listar();
                cboTipoEdicion.ValueMember = "Id";
                cboTipoEdicion.DisplayMember = "Descripcion";
                if (disco != null)
                {
                    txtTitulo.Text = disco.Titulo;
                    txtFechaLanzamiento.Text = disco.FechaLanzamiento.ToString();
                    txtCantidadCanciones.Text = disco.CantidadCanciones.ToString();
                    txtUrlImagenTapa.Text = disco.UrlImagenTapa;
                    cargarImagen(disco.UrlImagenTapa);
                    cboEstilo.SelectedValue = disco.Estilo.Id;
                    cboTipoEdicion.SelectedValue = disco.Edicion.Id;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        public void cargarImagen(string imagen)
        {
            try
            {
                pboAgregarDisco.Load(imagen);
            }
            catch (Exception)
            {
                pboAgregarDisco.Load("https://www.came-educativa.com.ar/upsoazej/2022/03/placeholder-4.png");
            }
        }
        private void btnExaminar_Click(object sender, EventArgs e)
        {
            archivo = new OpenFileDialog();
            archivo.Filter = "jpg|*.jpg;|png|*.png";
            try
            {
                if (archivo.ShowDialog() == DialogResult.OK)
                {
                    txtUrlImagenTapa.Text = archivo.FileName;
                    cargarImagen(archivo.FileName);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
