using System.Security.Claims;
using ChaskiTravel.DAO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace ChaskiTravel.Controllers
{
    public class AccesoController : Controller
    {
        accesoDAO acce = new accesoDAO();
        public readonly IConfiguration _iconfig;
        public AccesoController(IConfiguration iconfig)
        {
            _iconfig = iconfig;
        }


        public IActionResult Logueo()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logueo(string usuario, string pass)
        {
            ClaimsIdentity identity = null;
            bool isAuthenticate = false;
            if (usuario == "admi" && pass == "admi")
            {
                identity = new ClaimsIdentity(new[]
               {
                new Claim(ClaimTypes.Name,usuario),
                new Claim(ClaimTypes.Role,"Administrador")
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                isAuthenticate = true;
            }

            if (usuario == "cliente" && pass == "cliente")
            {
                identity = new ClaimsIdentity(new[]
               {
                new Claim(ClaimTypes.Name,usuario),
                new Claim(ClaimTypes.Role,"Cliente")
}, CookieAuthenticationDefaults.AuthenticationScheme);
                isAuthenticate = true;
            }

            if (isAuthenticate)
            {
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                if (usuario == "admi" && pass == "admi")
                {
                    return RedirectToAction("Create", "ManteTour");
                }
                if (usuario == "cliente" && pass == "cliente")
                {
                    return RedirectToAction("Inicio", "Viajes");

                }

            }
            return View();
        }
        /*  public async Task<IActionResult> Logueo(Usuario reg)
          {

                  var valiUsu = acce.validacion(reg.usuario, reg.pass);
                  if (valiUsu != null)
                  {
                     /* var claims = new List<Claim> {
                     // new Claim(ClaimTypes.Name, reg.usuario)
                     new Claim(ClaimTypes.Name, reg.nombre),
                         new Claim("usuario", reg.usuario)

                        };

                      foreach (string rol in reg.Roles)
                      {
                          claims.Add(new Claim(ClaimTypes.Role, rol));
                      }

                      var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                      await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));


                      return RedirectToAction("Inicio", "Viajes");
                  }
                  else
                  {
                  return View();
              }





          }*/


        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Logueo", "Acceso");
        }
    }
}

/* public readonly IConfiguration _iconfig;
 public AccesoController(IConfiguration iconfig)
 {
     _iconfig = iconfig;
 }
 public IActionResult Logueo()
 {
     return View();
 }
 [HttpPost]
 public IActionResult Logueo(Usuario reg)
 {




     string mensaje = "";
     using (SqlConnection cn = new SqlConnection(_iconfig["ConnectionStrings:cadena"]))

     {
         cn.Open();
         try
         {
             SqlCommand cmd = new SqlCommand("usp_validar_usuario", cn);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.AddWithValue("@usu", reg.usuario);
             cmd.Parameters.AddWithValue("@pass", reg.pass);
             cmd.ExecuteNonQuery();
             return RedirectToAction("Inicio", "Viajes");

         }

         catch (SqlException)
         {
             mensaje = "Usuario incorrecto";
         }

         finally
         {
             cn.Close();
         }

     }

     ViewBag.mensaje = mensaje;

     return View();
 }

 public IActionResult Salir()
 {
     return RedirectToAction("Logueo", "Acceso");
 }
 */

