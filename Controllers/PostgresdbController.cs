using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UsersService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersServicedbContext _context;

        public UsersController(UsersServicedbContext context)
        {
            _context = context;
        }

        // 1. Crear docente
        [HttpPost("Teacher")]
        public async Task<ActionResult<Teacher>> CreateTeacher(Teacher teacher)
        {
            teacher.Id = Guid.NewGuid();
            teacher.Password = teacher.Name; // Generar contraseña temporal

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTeacherById), new { id = teacher.Id }, teacher);
        }

        // 2. Crear estudiante
        [HttpPost("Student")]
        public async Task<ActionResult<Student>> CreateEstudiante(Student student)
        {
            student.Id = Guid.NewGuid();

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
        } 

        // 3. Editar docente
        [HttpPut("Teacher/{id}")]
        public async Task<IActionResult> UpdateDocente(Guid id, Teacher updatedTeacher)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
                return NotFound();

            teacher.Name = updatedTeacher.Name;
            teacher.LastName = updatedTeacher.LastName;
            teacher.Email = updatedTeacher.Email;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 4. Editar estudiante
        [HttpPut("Student/{id}")]
        public async Task<IActionResult> UpdateEstudiante(Guid id, Student updatedStudent)
        {
            var estudiante = await _context.Students.FindAsync(id);
            if (estudiante == null)
                return NotFound();

            estudiante.Name = updatedStudent.Name;
            estudiante.LastName = updatedStudent.LastName;
            estudiante.Email = updatedStudent.Email;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 5. Visualizar docentes
        [HttpGet("Teachers")]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
        {
            return await _context.Teachers.ToListAsync();
        }

        // 6. Visualizar estudiantes
        [HttpGet("Students")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        // Obtener docente por Id
        [HttpGet("GetTeacher/{id}")]
        public async Task<ActionResult<Teacher>> GetTeacherById(Guid id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null) return NotFound();

            return teacher;
        }

        // Obtener estudiante por Id
        [HttpGet("GetStudent/{id}")]
        public async Task<ActionResult<Student>> GetStudentById(Guid id)
        {
            var estudiante = await _context.Students.FindAsync(id);
            if (estudiante == null) return NotFound();

            return estudiante;
        }
            // Método para verificar si el correo ya existe
        [HttpGet("exists")]
        public async Task<IActionResult> VerifyEmailExists([FromQuery] Guid id)
        {

            var AdminExists = await _context.Admins.AnyAsync(u => u.Id == id);

            var StudentExists = await _context.Students.AnyAsync(u => u.Id == id);

            var TeacherExists = await _context.Admins.AnyAsync(u => u.Id == id);


            if (AdminExists || TeacherExists || StudentExists)
                return Ok(new { message = "El usuario ya existe.", status = true });
            else
                return NotFound(new { message = "El usuario no existe.", status = false });
        }

        // Método para validar email y contraseña
        [HttpPost("validate-credentials")]
        public async Task<ActionResult<bool>> ValidateCredentials([FromBody] CredentialRequest credentials)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(u => u.Email == credentials.Email);

            var teacher = await _context.Teachers.FirstOrDefaultAsync(u => u.Email == credentials.Email);

            if (admin == null)
            {
                return NotFound("Usuario no encontrado");
            }

            else if (teacher == null)
            {
                return NotFound("Usuario no encontrado");
            }

            if (admin.Password == credentials.Password)
            {
                return Ok(new
                    {
                        message = "Credenciales válidas.",
                        status = true,
                        userId = admin.Id,
                        userRol = admin.Rol
                    });
            }
            else if (teacher.Password == credentials.Password)
                     {
                return Ok(new
                    {
                        message = "Credenciales válidas.",
                        status = true,
                        userId = teacher.Id,
                        userRol = teacher.Rol
                    });
            }
            else
            {
                return BadRequest("Contraseña incorrecta");
            }
        }
    
    }

        public class CredentialRequest
    {
        public string Email { get; set; } = null!; 
        public string Password { get; set; } = null!; 
    }

}