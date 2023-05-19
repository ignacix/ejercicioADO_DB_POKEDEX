using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Pokemon seleccion = (Pokemon)dataGridView1.CurrentRow.DataBoundItem;
            cargarImagen(seleccion);
            
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
                dataGridView1.Columns["UrlImagen"].Visible = false;
                dataGridView1.Columns["Id"].Visible= false;
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
            Pokemon pokemon;
            pokemon = dataGridView1.CurrentRow.DataBoundItem as Pokemon;
            frmAltaPokemon Modificar = new frmAltaPokemon(pokemon);
            Modificar.ShowDialog();
            actualizarGrilla();

        }
    }
}
