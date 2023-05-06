using System.Data;
using ChaskiTravel.Models;
using Microsoft.Data.SqlClient;

namespace ChaskiTravel.DAO
{
    public class consultaDAO
    {
        // consulta de pedidos
        public IEnumerable<Cliente> listadoConsulta(string procedure, SqlParameter[] pars)
        {
            List<Cliente> temporal = new List<Cliente>();
            conexionDAO cn = new conexionDAO();
            using (cn.getcn)
            {
                cn.getcn.Open();
                SqlCommand cmd = new SqlCommand(procedure, cn.getcn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(pars.ToArray());
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Cliente()
                    {
                        idpedido=dr.GetInt32(0),
                        fpedido=dr.GetDateTime(1), 
                        nombre = dr.GetString(2),
                        apePaterno = dr.GetString(3),
                        apeMaterno = dr.GetString(4),
                        dni = dr.GetString(5),

                        telefono = dr.GetInt32(6),


                        email = dr.GetString(7)
                    });
                }
                cn.getcn.Close();
            }
            return temporal;
        }
    }
}
