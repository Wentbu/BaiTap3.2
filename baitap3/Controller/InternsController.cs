// Controllers/InternController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BaiOnline3.Data;
using BaiOnline3.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Dynamic;

namespace BaiOnline3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InternController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InternController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetInterns()
        {
            // Get the user's role from the JWT claims
            var roleName = User.FindFirst(ClaimTypes.Role)?.Value;
            if (string.IsNullOrEmpty(roleName)) return Unauthorized("Role not found.");

            // Get the role ID
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == roleName);
            if (role == null) return Unauthorized("Invalid role.");

            // Get allowed columns from AllowAccess
            var allowAccess = await _context.AllowAccesses
                .FirstOrDefaultAsync(a => a.RoleId == role.RoleId && a.TableName == "Intern");

            if (allowAccess == null) return Forbid("No access defined for this role.");

            var allowedColumns = allowAccess.AccessProperties.Split(',').ToList();

            // Fetch interns
            var interns = await _context.Interns.ToListAsync();

            // Create dynamic objects with only allowed columns
            var result = new List<object>();
            foreach (var intern in interns)
            {
                dynamic dynamicIntern = new ExpandoObject();
                var dict = (IDictionary<string, object>)dynamicIntern;

                foreach (var column in allowedColumns)
                {
                    var property = typeof(Intern).GetProperty(column.Trim());
                    if (property != null)
                    {
                        dict[column.Trim()] = property.GetValue(intern);
                    }
                }

                result.Add(dynamicIntern);
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetIntern(int id)
        {
            var roleName = User.FindFirst(ClaimTypes.Role)?.Value;
            if (string.IsNullOrEmpty(roleName)) return Unauthorized("Role not found.");

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == roleName);
            if (role == null) return Unauthorized("Invalid role.");

            var allowAccess = await _context.AllowAccesses
                .FirstOrDefaultAsync(a => a.RoleId == role.RoleId && a.TableName == "Intern");

            if (allowAccess == null) return Forbid("No access defined for this role.");

            var allowedColumns = allowAccess.AccessProperties.Split(',').ToList();

            var intern = await _context.Interns.FindAsync(id);
            if (intern == null) return NotFound();

            dynamic dynamicIntern = new ExpandoObject();
            var dict = (IDictionary<string, object>)dynamicIntern;

            foreach (var column in allowedColumns)
            {
                var property = typeof(Intern).GetProperty(column.Trim());
                if (property != null)
                {
                    dict[column.Trim()] = property.GetValue(intern);
                }
            }

            return Ok(dynamicIntern);
        }
    }
}