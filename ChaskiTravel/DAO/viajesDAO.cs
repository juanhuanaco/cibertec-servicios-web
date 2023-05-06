using System.Data;
using ChaskiTravel.Models;
using Microsoft.Data.SqlClient;

namespace ChaskiTravel.DAO
{
    public class viajesDAO
    {
        public IEnumerable<Destino> listado(string procedure, SqlParameter[] pars) {
            List<Destino> temporal = new List<Destino>();
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
                    temporal.Add(new Destino()
                    {
                        idDestino = dr.GetInt32(0),
                        pais = dr.GetString(1),
                        ciudad = dr.GetString(2),
                        nomHotel = dr.GetString(3),
                      
                        categoriaHotel= dr.GetString(4),
                      

                        descripcionTour = dr.GetString(5),
                      
                       
                        UnidadesEnExistencia = dr.GetInt16(6)
                    });
                }
                cn.getcn.Close();
            }
            return temporal;
        }
    }
}

