using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Dominio
{
    public class Pokemon
    {
        public Pokemon()
        {
            this.Tipo = new Elemento();
            this.Debilidad = new Elemento();
        }

        public int Id { get; set; }
        [DisplayName("N°")]
        public int Numero { get; set; }
        public string Nombre { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        public string  UrlImagen { get; set; }
        public Elemento Tipo { get; set; }
        public Elemento Debilidad { get; set; }
    }
}
