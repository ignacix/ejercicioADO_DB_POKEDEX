using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;

namespace Negocio
{
    public class PokemonNegocio
    {
        public List<Pokemon> listar()
        {
            List<Pokemon> lista = new List<Pokemon>();
            SqlDataReader dr;
            AccesoDatos acceso = new AccesoDatos();

            try
            {
                acceso.SetearConsulta("select Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion as Tipo, D.Descripcion as Debilidad, E.Id as 'IdTIpo', D.Id as 'IdDebilidad', P.Id as 'IdPokemon' from POKEMONS P, ELEMENTOS E, ELEMENTOS D where P.IdTipo = E.Id and P.IdDebilidad = D.Id");                
                acceso.RealizarLectura();
                dr = acceso.Reader;
                while (dr.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Id = (int)dr["IdPokemon"];
                    aux.Numero = (int)dr["Numero"];
                    aux.Nombre = (string)dr["Nombre"];
                    aux.Descripcion = (string)dr["Descripcion"];

                    if (!(dr.IsDBNull(dr.GetOrdinal("UrlImagen"))))                    
                        aux.UrlImagen = (string)dr["UrlImagen"];
                    aux.Tipo = new Elemento();
                    aux.Tipo.Descripcion = (string)dr["Tipo"];
                    aux.Tipo.Id = (int)dr["IdTipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Descripcion = (string)dr["Debilidad"];
                    aux.Debilidad.Id = (int)dr["IdDebilidad"];
                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception)
            {
                 throw;
            }
            finally
            {
                acceso.CerrarConexion();  
            }
        }

        public void agregar(Pokemon poke)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            accesoDatos.SetearConsulta($"insert into POKEMONS (Numero, Nombre, Descripcion,UrlImagen, IdTipo, IdDebilidad) values ({poke.Numero}, '{poke.Nombre}', '{poke.Descripcion}','{poke.UrlImagen}',{poke.Tipo.Id},{poke.Debilidad.Id})");
            accesoDatos.RealizarConsulta();
            accesoDatos.CerrarConexion();
        }

        public void Modificar(Pokemon poke)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                accesoDatos.SetearConsulta("update POKEMONS set Numero=@Numero, Nombre=@Nombre, Descripcion=@Descripcion, UrlImagen =@UrlImagen, IdTipo=@IdTipo, IdDebilidad = @IdDebilidad where Id=@Id");
                accesoDatos.seterParametros("@Numero", poke.Numero);
                accesoDatos.seterParametros("@Nombre", poke.Nombre);
                accesoDatos.seterParametros("@Descripcion", poke.Descripcion);
                accesoDatos.seterParametros("@UrlImagen", poke.UrlImagen);
                accesoDatos.seterParametros("@IdTipo", poke.Tipo.Id);
                accesoDatos.seterParametros("@IdDebilidad", poke.Debilidad.Id);
                accesoDatos.seterParametros("@Id", poke.Id);

                accesoDatos.RealizarConsulta();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
        }               

        

    }
}
