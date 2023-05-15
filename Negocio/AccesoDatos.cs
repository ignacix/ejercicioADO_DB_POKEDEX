using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Net;

namespace Negocio
{
    public class AccesoDatos
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader reader;

        public SqlDataReader Reader { get { return reader; } }

        public AccesoDatos()
        {
            this.con = new SqlConnection("server=.\\SQLEXPRESS; database=POKEDEX_DB ; integrated security=true");
            this.cmd = new SqlCommand();
        }
        public void SetearConsulta(string query)
        {
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = query;
        }

        public void RealizarLectura()
        {
            cmd.Connection = con;
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();            
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RealizarConsulta()
        {
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void CerrarConexion()
        {
            if (reader != null)
            {
                reader.Close();
            }
            con.Close();
        }






    }
}
