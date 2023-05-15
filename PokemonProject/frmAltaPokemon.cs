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

namespace PokemonProject
{
    public partial class frmAltaPokemon : Form
    {
        public frmAltaPokemon()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Pokemon pokemon = new Pokemon();
            PokemonNegocio negocio = new PokemonNegocio();

            try
            {
                pokemon.Numero = int.Parse(txbNumero.Text);
                pokemon.Nombre = txbNombre.Text;
                pokemon.Descripcion = txbDescripción.Text;

                negocio.agregar(pokemon);
                MessageBox.Show("Pokemon Agregado Correctamente");
            }
            catch (Exception ex)
            {

                MessageBox.Show( ex.Message);
            }
           
                
            


        }
    }
}
