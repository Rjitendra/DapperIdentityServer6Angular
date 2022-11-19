using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechBox.Model.Dto;
using TechBox.Service.Interfaces;

namespace TechBox.API.Controllers
{

    public class StudentController : BaseController
    {
        private readonly IStudent _student;
        public StudentController(IStudent student)
        {
            _student = student;
        }
        [HttpGet]
        public async Task<IActionResult> GetSudents()
        {
            try
            {
                var companies = await _student.GetStudents();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id}", Name = "StudentById")]
        public async Task<IActionResult> GetStudent(int id)
        {
            try
            {
                var student = await _student.GetStudent(id);
                if (student == null)
                    return NotFound();

                return Ok(student);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateStudent(StudentCreationDto student)
        {
            try
            {
                var createdStudent = await _student.CreateStudent(student);
                return CreatedAtRoute("StudentById", new { id = createdStudent.Id }, createdStudent);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, StudentUpdateDto student)
        {
            try
            {
                var dbStudent = await _student.GetStudent(id);
                if (dbStudent == null)
                    return NotFound();

                await _student.UpdateStudent(id, student);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                var dbCompany = await _student.GetStudent(id);
                if (dbCompany == null)
                    return NotFound();

                await _student.DeleteStudent(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
