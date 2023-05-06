using System.Data;
using ChaskiTravel.DAO;
using ChaskiTravel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace ChaskiTravel.Controllers
{
    [Authorize]
    public class ChaskiTravelController : Controller
    {
        viajesDAO filtro = new viajesDAO();

        public readonly IConfiguration _iconfig;
        public ChaskiTravelController(IConfiguration iconfig)
        {
            _iconfig = iconfig;
        }
        IEnumerable<Destino> listadoSinParametro()
        {
            List<Destino> temporal = new List<Destino>();
            conexionDAO cn = new conexionDAO();
            using (cn.getcn)
            {
                cn.getcn.Open();
                SqlCommand cmd = new SqlCommand("usp_consultarSin_destino", cn.getcn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Destino()
                    {
                        idDestino = dr.GetInt32(0),
                        pais = dr.GetString(1),
                        ciudad = dr.GetString(2),
                        nomHotel = dr.GetString(3),

                        categoriaHotel = dr.GetString(4),
                        precioHotel = dr.GetDecimal(5),

                        NombreCategoria = dr.GetString(6),
                        descripcionTour = dr.GetString(7),
                        precioTour = dr.GetDecimal(8),
       
                        UnidadesEnExistencia = dr.GetInt16(9)
                    });
                }
                cn.getcn.Close();
            }
            return temporal;
        }
      
        [Authorize(Roles = "Cliente")]
        public IActionResult Portal(string pais = "")
        {
            SqlParameter[] parametros = new SqlParameter[]
              {
                new SqlParameter("@pa",pais),
              };

           if (HttpContext.Session.GetString("canasta") == null)
            {
                HttpContext.Session.SetString("canasta", JsonConvert.SerializeObject(new List<Compra>()));
            }


            return View(filtro.listado("usp_consultar_destino", parametros));


        }
        [Authorize(Roles = "Cliente")]
        public IActionResult Seleccionar(int id = 0)
        {
            //buscar el producto por id
            Destino reg = listadoSinParametro().FirstOrDefault(p => p.idDestino == id);
            return View(reg);
        }
        [HttpPost]
        public IActionResult Seleccionar(int codigo, int cantidad)
        {
            //buscar el producto por su codigo
           Destino reg = listadoSinParametro().FirstOrDefault(p => p.idDestino == codigo);

            //definir una Compra y almacenar los datos 
            Compra it = new Compra()
            {
                codigo = codigo,
                pais = reg.pais,
                ciudad = reg.ciudad,
                nomHotel= reg.nomHotel,
                categoriaHotel= reg.categoriaHotel,
                precioHotel = reg.precioHotel,
                NombreCategoria= reg.NombreCategoria,
                descripcionTour =reg.descripcionTour,
                precioTour=reg.precioTour,
                UnidadesEnExistencia= reg.UnidadesEnExistencia,
                cantidad= cantidad
            };

            //deserializar el Session canasta para almacenar it
            List<Compra> temporal = JsonConvert.DeserializeObject<List<Compra>>(
                          HttpContext.Session.GetString("canasta"));
            //agregar
            temporal.Add(it);

            //serializar
            HttpContext.Session.SetString("canasta", JsonConvert.SerializeObject(temporal));
            ViewBag.mensaje = "Se ha registrado correctamente";
            return View(reg);
        }
      [Authorize(Roles = "Cliente")]

        public ActionResult Resumen()
        {
            //enviar a la vista la lista deserializada del Session Canasta
            List<Compra> temporal = JsonConvert.DeserializeObject<List<Compra>>(
                         HttpContext.Session.GetString("canasta"));
            return View(temporal);
        }
        
       public IActionResult Delete(int id)
        {
            
            List<Compra> temporal = JsonConvert.DeserializeObject<List<Compra>>(
                         HttpContext.Session.GetString("canasta"));

            temporal.Remove(temporal.FirstOrDefault(p => p.codigo == id));

            HttpContext.Session.SetString("canasta", JsonConvert.SerializeObject(temporal));

            return RedirectToAction("Resumen");
        }
        
        public IActionResult Comprar()
        {
            //enviamos a vista un nuevo Cliente para el pedido
            return View(new Cliente());
        }
        
        [HttpPost]
        public IActionResult Comprar(Cliente reg)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(_iconfig["ConnectionStrings:cadena"]))
            {
                cn.Open();
                SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    //ejecutar el proceso donde agrega_pedido
                    SqlCommand cmd = new SqlCommand("usp_agrega_pedido", cn, tr);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@idpedido", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@nom", reg.nombre);
                    cmd.Parameters.AddWithValue("@apePat", reg.apePaterno);
                    cmd.Parameters.AddWithValue("@apeMat", reg.apeMaterno);
                    cmd.Parameters.AddWithValue("@dni", reg.dni);
                    cmd.Parameters.AddWithValue("@fono", reg.telefono);
                    cmd.Parameters.AddWithValue("@email", reg.email);
                    cmd.ExecuteNonQuery();
                    int idpedido = (int)cmd.Parameters["@idpedido"].Value; 

                    List<Compra> temporal = JsonConvert.DeserializeObject<List<Compra>>(
                            HttpContext.Session.GetString("canasta"));

                    foreach (Compra item in temporal)
                    {
                        cmd = new SqlCommand("exec usp_agrega_detalle @idpedido,@idDestino,@cantidad,@precio", cn, tr);
                        cmd.Parameters.AddWithValue("@idpedido", idpedido);
                        cmd.Parameters.AddWithValue("@idDestino", item.codigo);
                        cmd.Parameters.AddWithValue("@cantidad", item.cantidad);
                        cmd.Parameters.AddWithValue("@precio", item.precio);
                        cmd.ExecuteNonQuery();
                    }

                    foreach (Compra item in temporal)
                    {
                        cmd = new SqlCommand("exec usp_actualiza_stock @idDestino,@cant", cn, tr);
                        cmd.Parameters.AddWithValue("@idDestino", item.codigo);
                        cmd.Parameters.AddWithValue("@cant", item.cantidad);
                        cmd.ExecuteNonQuery();
                    }

                    tr.Commit(); 
                    mensaje = "Se ha registrado la compra";
                }
                catch (SqlException ex)
                {
                    mensaje = ex.Message;
                    tr.Rollback(); 
                }
                finally { cn.Close(); }
            }

            ViewBag.mensaje = mensaje;
            return View(reg);
        }
    }
}
