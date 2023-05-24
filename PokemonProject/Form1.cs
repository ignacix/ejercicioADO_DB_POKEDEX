using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PokemonProject
{
    public partial class Form1 : Form
    {
        List<Pokemon> lista;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            actualizarGrilla();
            Text = "Pokemons";
            cbxCampo.Items.Add("Número");
            cbxCampo.Items.Add("Nombre");
            cbxCampo.Items.Add("Descripción");            
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                Pokemon seleccion = (Pokemon)dataGridView1.CurrentRow.DataBoundItem;
                cargarImagen(seleccion);                        
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAltaPokemon alta = new frmAltaPokemon();
            alta.ShowDialog();
            actualizarGrilla();
        }

        private void cargarImagen(Pokemon poke)
        {
            try
            {
                pictureBox1.Load(poke.UrlImagen);                
            }
            catch (Exception)
            {

                pictureBox1.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRacb74og5L8lqvlWiiECKiAgCf5KMDVvqidU9NTcUmYw&s");
            }
        }

        private void actualizarGrilla()
        {
            PokemonNegocio PokemonNegocio = new PokemonNegocio();
            try
            {
                lista = PokemonNegocio.listar();
                dataGridView1.DataSource = lista;
                ocultarColumnas();
                cargarImagen(lista[0]);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                Pokemon pokemon;
                pokemon = dataGridView1.CurrentRow.DataBoundItem as Pokemon;
                frmAltaPokemon Modificar = new frmAltaPokemon(pokemon);
                Modificar.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
            actualizarGrilla();

        }

        private void btnEliminarFisico_Click(object sender, EventArgs e)
        {
            Eliminacion(true);
        }

        private void btnEliminarLogico_Click(object sender, EventArgs e)
        {
            Eliminacion();            
        }
        private void Eliminacion(bool eleccion = false)
        {
            try
            {
                PokemonNegocio negocio = new PokemonNegocio();
                Pokemon pokeSelect = dataGridView1.CurrentRow.DataBoundItem as Pokemon;
                if (eleccion)
                {
                    DialogResult resultado = MessageBox.Show("¿De verdad quiere Borrar el Pokemon?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (resultado == DialogResult.Yes)
                    {                        
                        negocio.eliminar(pokeSelect.Id);
                    }
                }
                else
                {
                    negocio.eliminarLogico(pokeSelect.Id);
                }
            }   
            catch (Exception)
            {

                throw;
            }
            actualizarGrilla() ;
            
        }


        private void ocultarColumnas()
        {
            dataGridView1.Columns["UrlImagen"].Visible = false;
            dataGridView1.Columns["Id"].Visible = false;
        }

        private void txbFiltrar_TextChanged(object sender, EventArgs e)
        {
            List<Pokemon> listaFiltrada;
            string filtro = txbFiltrar.Text;

            if (txbFiltrar.Text!="")
            {
                listaFiltrada = lista.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Tipo.Descripcion.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                listaFiltrada = lista;
            }
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = listaFiltrada;
                ocultarColumnas();
        }

        private void cbxCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cbxCampo.SelectedItem.ToString();
            

            if (opcion=="Número")
            {
                cbxCriterio.Items.Clear();
                cbxCriterio.Items.Add("Mayor a");
                cbxCriterio.Items.Add("Menor a");
                cbxCriterio.Items.Add("Igual a");
            }
            else
            {
                cbxCriterio.Items.Clear();
                cbxCriterio.Items.Add("Comienza con");
                cbxCriterio.Items.Add("Termina con");
                cbxCriterio.Items.Add("Contiene");
            }

            
        }

        private void btnFiltroAvanzado_Click(object sender, EventArgs e)
        {
            PokemonNegocio negocio = new PokemonNegocio();

            try
            {
                string campo = cbxCampo.SelectedItem.ToString();
                string criterio = cbxCriterio.SelectedItem.ToString();
                string filtro = tbxFiltroAvanzado.Text;
                dataGridView1.DataSource = negocio.filtrar(campo, criterio, filtro);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString()); ;
            }
        }
    }
}
 