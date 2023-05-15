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
            PokemonNegocio PokemonNegocio = new PokemonNegocio();
            lista = PokemonNegocio.listar();
            dataGridView1.DataSource = lista;
            dataGridView1.Columns["UrlImagen"].Visible = false;
            cargarImagen(lista[0]);            
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;            
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
    }
}
