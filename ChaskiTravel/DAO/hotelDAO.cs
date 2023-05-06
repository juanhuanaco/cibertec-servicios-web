using ChaskiTravel.Models;
using ChaskiTravel.Models.DI;
using Microsoft.Data.SqlClient;

namespace ChaskiTravel.DAO
{
    public class hotelDAO : IHotel
    {
        public string agregar(Hotel h)
        {
            string mensaje = "";
            conexionDAO cn = new conexionDAO();
            using (cn.getcn)
            {
                cn.getcn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand(
                    "exec usp_agregar_hotel @nom,@cate,@pre,@des", cn.getcn);
                    cmd.Parameters.AddWithValue("@nom", h.nomHotel);
                    cmd.Parameters.AddWithValue("@cate", h.categoriaHotel);
                    cmd.Parameters.AddWithValue("@pre", h.precioHotel);
                    cmd.Parameters.AddWithValue("@des", h.descripcionHotel);

                    cmd.ExecuteNonQuery();
                    mensaje = "Se ha registrado correctamente";
                }
                catch (SqlException ex) { mensaje = ex.Message; }
                finally { cn.getcn.Close(); }
            }
            return mensaje;
        }

        public Hotel buscar(int id)
        {
            /*if (id==null)
                
                return null;
            else*/
            return listado().Where(c => c.idHotel == id).FirstOrDefault();
        }

        public IEnumerable<Hotel> listado()
        {
            List<Hotel> temporal = new List<Hotel>();
            conexionDAO cn = new conexionDAO();
            using (cn.getcn)
            {
                cn.getcn.Open();
                SqlCommand cmd = new SqlCommand("usp_hotel_lis", cn.getcn);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Hotel()
                    {
                        idHotel = dr.GetInt32(0),
                        nomHotel = dr.GetString(1),
                        categoriaHotel = dr.GetString(2),
                        precioHotel = dr.GetDecimal(3),
                        descripcionHotel = dr.GetString(4)

                    });
                }
            }
            return temporal;

        }
        public string actualizar(Hotel h)
        {
            string mensajeEditar = "";
            conexionDAO cn = new conexionDAO();
            using (cn.getcn)
            {
                cn.getcn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand(
                    "exec usp_actualizar_hotel @idHo,@nom,@cate,@pre,@des", cn.getcn);
                    cmd.Parameters.AddWithValue("@idHo", h.idHotel);
                    cmd.Parameters.AddWithValue("@nom", h.nomHotel);
                    cmd.Parameters.AddWithValue("@cate", h.categoriaHotel);
                    cmd.Parameters.AddWithValue("@pre", h.precioHotel);
                    cmd.Parameters.AddWithValue("@des", h.descripcionHotel);

                    cmd.ExecuteNonQuery();
                    mensajeEditar = "Se ha actualizado correctamente";
                }
                catch (SqlException ex) { mensajeEditar = ex.Message; }
                finally { cn.getcn.Close(); }
            }
            return mensajeEditar;
        }
        public string eliminar(Object obj)
        {

            string mensajeEliminar = "";
            conexionDAO cn = new conexionDAO();
            using (cn.getcn)
            {

                cn.getcn.Open();
                try
                {

                    SqlCommand cmd = new SqlCommand("exec usp_eliminar_hotel @idHo", cn.getcn);

                    cmd.Parameters.AddWithValue("@idHo", obj);
                    cmd.ExecuteNonQuery();

                }
                catch (SqlException ex) { mensajeEliminar = ex.Message; }
                finally { cn.getcn.Close(); }
            }
            return mensajeEliminar;
        }
    }

}
