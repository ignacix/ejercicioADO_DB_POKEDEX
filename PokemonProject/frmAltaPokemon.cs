using Dominio;
using Negocio;
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
using System.Configuration;

namespace PokemonProject
{
    public partial class frmAltaPokemon : Form
    {
        private Pokemon pokemon = null;
        private OpenFileDialog archivo = null;
        public frmAltaPokemon()
        {
            InitializeComponent();
            Text = "Alta Pokemon";
            button1.Text = "Cargar";
            CargarImagen();
        }
        public frmAltaPokemon(Pokemon pokemon)
        {
            InitializeComponent();
            this.pokemon = pokemon;
            Text = "Modificar Pokemon";
            button1.Text = "Actualizar";
        }

        private void button1_Click(object sender, EventArgs e)
        {        
            PokemonNegocio negocio = new PokemonNegocio();

            try
            {                
                pokemon.Numero = int.Parse(txbNumero.Text);
                pokemon.Nombre = txbNombre.Text;
                pokemon.Descripcion = txbDescripción.Text;
                pokemon.UrlImagen = txbUrl.Text;
                pokemon.Tipo = (Elemento)cbxTipo.SelectedItem;
                pokemon.Debilidad = (Elemento)cbxDebilidad.SelectedItem;


                if (pokemon.Id == 0)
                {
                    negocio.agregar(pokemon);
                    MessageBox.Show("Pokemon se Agregó Correctamente");
                }
                else
                {
                    negocio.Modificar(pokemon);
                    MessageBox.Show("Pokemon Modificado Correctamente");
                }

                if (archivo != null && !txbUrl.Text.ToUpper().Contains("HTPP"))
                {
                    File.Copy(archivo.FileName, ConfigurationManager.AppSettings["images-folder"] + archivo.SafeFileName);
                }


                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message);
            }                                       
        }

        private void frmAltaPokemon_Load(object sender, EventArgs e)
        {
            ElementoNegocio elementoNegocio = new ElementoNegocio();

            try
            {
                cbxTipo.DataSource = elementoNegocio.listar();
                cbxTipo.ValueMember = "Id";
                cbxTipo.DisplayMember = "Descripcion";                
                cbxDebilidad.DataSource = elementoNegocio.listar();
                cbxDebilidad.ValueMember = "Id";
                cbxDebilidad.DisplayMember = "Descripcion";

                if (pokemon != null)
                {
                    txbNumero.Text = pokemon.Numero.ToString();
                    txbNombre.Text = pokemon.Nombre;
                    txbDescripción.Text = pokemon.Descripcion;
                    txbUrl.Text = pokemon.UrlImagen;
                    CargarImagen();
                    cbxTipo.SelectedValue = (int)pokemon.Tipo.Id;
                    cbxDebilidad.SelectedValue = (int)pokemon.Debilidad.Id;

                }
                else
                {
                    pokemon = new Pokemon();
                }
            }
            catch (Exception)
            {
                throw;
            }                      
        }

        private void txbUrl_Leave(object sender, EventArgs e)
        {
            CargarImagen();
        }

        private void CargarImagen()
        {
            try
            {
                pictureBoxAlta.Load(txbUrl.Text);
            }
            catch (Exception)
            {

                pictureBoxAlta.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRacb74og5L8lqvlWiiECKiAgCf5KMDVvqidU9NTcUmYw&s");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            archivo = new OpenFileDialog();
            archivo.Filter = "jpg|*.jpg |png|*.png";
            if (archivo.ShowDialog()==DialogResult.OK )
            {

                txbUrl.Text = archivo.FileName;
                CargarImagen();                
            }
        }
    }
}
