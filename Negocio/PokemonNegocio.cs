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
                acceso.SetearConsulta("select Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion as Tipo, D.Descripcion as Debilidad from POKEMONS P, ELEMENTOS E, ELEMENTOS D where P.IdTipo = E.Id and P.IdDebilidad = D.Id");
                acceso.RealizarConsulta();
                dr = acceso.Reader;
                while (dr.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Numero = (int)dr["Numero"];
                    aux.Nombre = (string)dr["Nombre"];
                    aux.Descripcion = (string)dr["Descripcion"];
                    aux.UrlImagen = (string)dr["UrlImagen"];
                    aux.Tipo.Descripcion = (string)dr["Tipo"];
                    aux.Debilidad.Descripcion = (string)dr["Debilidad"];
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
              


    }
}
