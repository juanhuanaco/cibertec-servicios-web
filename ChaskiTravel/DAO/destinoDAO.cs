using ChaskiTravel.Models;
using ChaskiTravel.Models.DI;
using Microsoft.Data.SqlClient;

namespace ChaskiTravel.DAO
{
    public class destinoDAO : IDestino
    {
        public string agregar(Destino d)
        {
            string mensaje = "";
            conexionDAO cn = new conexionDAO();
            using (cn.getcn)
            {
                cn.getcn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand(
                    "exec usp_agregar_destino @pais,@ciu,@idHo,@idCate,@idTo,@uni", cn.getcn);
                    cmd.Parameters.AddWithValue("@pais", d.pais);
                    cmd.Parameters.AddWithValue("@ciu", d.ciudad);
                    cmd.Parameters.AddWithValue("@idHo", d.idHotel);
     
                    cmd.Parameters.AddWithValue("@idCate", d.IdCategoria);
                    cmd.Parameters.AddWithValue("@idTo", d.idTour);

                    cmd.Parameters.AddWithValue("@uni", d.UnidadesEnExistencia);


                    cmd.ExecuteNonQuery();
                    mensaje = "Se ha registrado correctamente";
                }
                catch (SqlException ex) { mensaje = ex.Message; }
                finally { cn.getcn.Close(); }
            }
            return mensaje;
        }

        public Destino buscar(int id)
        {
            /*if (string.IsNullOrEmpty(codigo))
                return null;
            else*/
            return listado().Where(c => c.idDestino == id).FirstOrDefault();
        }

        public IEnumerable<Destino> listado()
        {
            List<Destino> temporal = new List<Destino>();
            conexionDAO cn = new conexionDAO();
            using (cn.getcn)
            {
                cn.getcn.Open();
                SqlCommand cmd = new SqlCommand("usp_destino_list", cn.getcn);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Destino()
                    {
                        idDestino = dr.GetInt32(0),
                        pais = dr.GetString(1),
                        ciudad = dr.GetString(2),
                        idHotel = dr.GetInt32(3),
                        IdCategoria = dr.GetInt32(4),
                        idTour=dr.GetInt32(5),
                        UnidadesEnExistencia = dr.GetInt16(6)
                    });
                }
            }
            return temporal;

        }
        public string actualizar(Destino d)
        {
            string mensajeEditar = "";
            conexionDAO cn = new conexionDAO();
            using (cn.getcn)
            {
                cn.getcn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand(
                    "exec usp_actualizar_destino @idDes,@pais,@ciu,@idHo,@idCate,@idTo,@uni", cn.getcn);
                    cmd.Parameters.AddWithValue("@idDes", d.idDestino);
                    cmd.Parameters.AddWithValue("@pais", d.pais);
                    cmd.Parameters.AddWithValue("@ciu", d.ciudad);
                    cmd.Parameters.AddWithValue("@idHo", d.idHotel);
                    cmd.Parameters.AddWithValue("@idCate", d.IdCategoria);
                    cmd.Parameters.AddWithValue("@idTo", d.idTour);
                    cmd.Parameters.AddWithValue("@uni", d.UnidadesEnExistencia);



                    cmd.ExecuteNonQuery();
                    mensajeEditar = "Se ha actualizado correctamente";
                }
                catch (SqlException ex) { mensajeEditar = ex.Message; }
                finally { cn.getcn.Close(); }
            }
            return mensajeEditar;
        }

        public string eliminar(object obj)
        {
            string mensajeEliminar = "";
            conexionDAO cn = new conexionDAO();
            using (cn.getcn)
            {

                cn.getcn.Open();
                try
                {

                    SqlCommand cmd = new SqlCommand("exec usp_eliminar_destino @idDe", cn.getcn);

                    cmd.Parameters.AddWithValue("@idDe", obj);
                    cmd.ExecuteNonQuery();
                    mensajeEliminar = "Se ha eliminado correctamente";
                }
                catch (SqlException ex) { mensajeEliminar = ex.Message; }
                finally { cn.getcn.Close(); }
            }
            return mensajeEliminar;
        }
    }


}
