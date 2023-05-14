using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ElementoNegocio
    {       
        public List<Elemento> listar()
        {
            List<Elemento> lista = new List<Elemento> ();
            AccesoDatos acceso = new AccesoDatos();

            try
            {
                acceso.SetearConsulta("select Id, Descripcion from ELEMENTOS");
                acceso.RealizarConsulta();

                
                while (acceso.Reader.Read())
                {
                    Elemento aux = new Elemento ();
                    aux.Id = (int)acceso.Reader["Id"];
                    aux.Descripcion = (string)acceso.Reader["Descripcion"];

                    lista.Add (aux);
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                acceso.CerrarConexion();
            }


            return lista;
        }
    }
}
