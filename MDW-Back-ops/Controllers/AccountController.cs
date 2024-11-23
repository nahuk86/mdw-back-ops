using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace MDW_Back_ops.Controllers
{

    public class AccountController : Controller
    {
        [HttpPost]
        [Route("api/login")]

        public JsonResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Autenticación lógica
                if (model.Email == "test@domain.com" && model.Password == "12345")
                {
                    return Json(new { success = true, token = "exampleToken" });
                }
                return Json(new { success = false, message = "Credenciales inválidas" });
            }
            return Json(new { success = false, message = "Error en el modelo" });
        }

        [HttpPost]
        [Route("api/register")]
        public JsonResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Registro lógico
                return Json(new { success = true, message = "Usuario registrado exitosamente" });
            }
            return Json(new { success = false, message = "Error en el modelo" });
        }
    }

    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

