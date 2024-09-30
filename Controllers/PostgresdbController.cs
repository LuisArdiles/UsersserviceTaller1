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
        [HttpPut("estudiante/{id}")]
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
        [HttpGet("docente/{id}")]
        public async Task<ActionResult<Teacher>> GetTeacherById(Guid id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null) return NotFound();

            return teacher;
        }

        // Obtener estudiante por Id
        [HttpGet("estudiante/{id}")]
        public async Task<ActionResult<Student>> GetStudentById(Guid id)
        {
            var estudiante = await _context.Students.FindAsync(id);
            if (estudiante == null) return NotFound();

            return estudiante;
        }
    }
}