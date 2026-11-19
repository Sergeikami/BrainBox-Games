using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApiProjecte.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiProjecte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolsController : ControllerBase
    {
        // GET: api/<RolsController>
        [HttpGet]
        public ActionResult<IEnumerable<Rol>> Get()
        {
            List<Rol> rols = new List<Rol>();

            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT id_rol, nombre_rol FROM roles", BD.Connexio);

                if (BD.Connexio.State != System.Data.ConnectionState.Open)
                {
                    BD.Connexio.Open();
                }

                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Rol r = new Rol
                    {
                        Id_rol = Convert.ToInt32(reader["id_rol"]),
                        Nombre_rol = reader["nombre_rol"].ToString() ?? string.Empty,
                    };
                    rols.Add(r);
                }

                reader.Close();
                return Ok(rols);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            finally
            {
                if (BD.Connexio.State == System.Data.ConnectionState.Open)
                {
                    BD.Connexio.Close();
                }
            }
        }


















        // GET api/<UsuarisController>/5
        [HttpGet("{id}")]
        public ActionResult<Rol> Get(int id)
        {
            Rol? rol = null;

            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT id_rol, nombre_rol FROM roles WHERE id_rol = @Id_rol", BD.Connexio);
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@Id_rol", id);

                if (BD.Connexio.State != System.Data.ConnectionState.Open)
                {
                    BD.Connexio.Open();
                }

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    rol = new Rol
                    {
                        Id_rol = Convert.ToInt32(reader["id_rol"]),
                        Nombre_rol = reader["nombre_rol"].ToString() ?? string.Empty,
                    };
                }

                reader.Close();

                if (rol == null)
                {
                    return NotFound(new { missatge = "No s'ha trobat cap usuari amb aquest ID" });
                }

                return Ok(rol);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            finally
            {
                if (BD.Connexio.State == System.Data.ConnectionState.Open)
                {
                    BD.Connexio.Close();
                }
            }
        }

    }
}
