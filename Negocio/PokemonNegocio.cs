﻿using System;
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
                acceso.SetearConsulta("select Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion as Tipo, D.Descripcion as Debilidad, E.Id as 'IdTIpo', D.Id as 'IdDebilidad', P.Id as 'IdPokemon' from POKEMONS P, ELEMENTOS E, ELEMENTOS D where P.IdTipo = E.Id and P.IdDebilidad = D.Id And P.Activo=1");                
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
            accesoDatos.SetearConsulta($"insert into POKEMONS (Numero, Nombre, Descripcion,UrlImagen, IdTipo, IdDebilidad, Activo) values ({poke.Numero}, '{poke.Nombre}', '{poke.Descripcion}','{poke.UrlImagen}',{poke.Tipo.Id},{poke.Debilidad.Id}, 1)");
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

        public void eliminar(int id)
        {
            try
            {
                AccesoDatos accesoDatos = new AccesoDatos();
                accesoDatos.SetearConsulta("delete from POKEMONS where Id = @Id");
                accesoDatos.seterParametros("@Id", id);
                accesoDatos.RealizarConsulta();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void eliminarLogico(int Id)
        {
            try
            {
                AccesoDatos acceso = new AccesoDatos();
                acceso.SetearConsulta("update POKEMONS set Activo = 0 where Id = @Id");
                acceso.seterParametros("@Id", Id);
                acceso.RealizarConsulta();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<Pokemon> filtrar(string campo, string criterio, string filtro)
        {
            List<Pokemon> lista = new List<Pokemon>();
            SqlDataReader dr;
            AccesoDatos acceso = new AccesoDatos();

            string consulta = "select Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion as Tipo, D.Descripcion as Debilidad, E.Id as 'IdTIpo', D.Id as 'IdDebilidad', P.Id as 'IdPokemon' from POKEMONS P, ELEMENTOS E, ELEMENTOS D where P.IdTipo = E.Id and P.IdDebilidad = D.Id And P.Activo=1 and ";

            try
            {
                if (campo == "Número")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "Numero > " + filtro;
                            break;
                        case "Menor a":
                            consulta += "Numero < " + filtro;
                            break;
                        default:
                            consulta += "Numero = " + filtro;
                            break;
                    }
                }
                else if (campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "Nombre like '" + filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += "Nombre like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "Nombre like '%" + filtro + "%'";
                            break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "P.Descripcion like '"+ filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += "P.Descripcion like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "P.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }

                acceso.SetearConsulta(consulta);
                acceso.RealizarLectura();
                dr =  acceso.Reader;

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
            }
            catch (Exception ex)
            {

                throw ex;
            }           
            return lista;
        }
        

    }
}
