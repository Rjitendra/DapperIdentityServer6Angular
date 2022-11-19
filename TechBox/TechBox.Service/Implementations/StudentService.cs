namespace TechBox.Service.Implementations
{
    using Dapper;
    using System;
    using System.Data;
    using TechBox.Model.Contexts;
    using TechBox.Model.Dto;
    using TechBox.Model.Entity;
    using TechBox.Service.Interfaces;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
    public class StudentService : IStudent
    {
        private readonly DapperContext _context;
        public StudentService(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            var sql = @"select Id,Name,CurrentGradeId,c.GradeName,c.GradeId,c.GradeName,c.Section
                from dbo.Students p 
                inner join dbo.Grades c on p.CurrentGradeId = c.GradeId";

            using (var connection = _context.CreateConnection())
            {
                var students = await connection.QueryAsync<Student, Grade, Student>(sql, (student, grade) =>
                {
                    student.CurrentGrade = grade;

                    return student;
                }, splitOn: "GradeId");
                return students.ToList();
            }
        }

        public async Task<Student> GetStudent(int id)
        {
            var query = @"select p.Id,p.Name,p.CurrentGradeId,c.GradeId,c.GradeName,c.Section
                from dbo.Students p 
                inner join dbo.Grades c on p.CurrentGradeId = c.GradeId WHERE p.Id = @id";
            using (var connection = _context.CreateConnection())
            {
                var student = await connection.QueryAsync<Student, Grade, Student>(query, map: (u, c) =>
                {
                    u.CurrentGrade = c;
                    return u;
                }, splitOn: "GradeId", param: new { id });

                return student.ToList()[0];
            }
        }

        public async Task<Student> CreateStudent(StudentCreationDto student)
        {
            var query = "INSERT INTO dbo.Students (Name, CurrentGradeId) VALUES (@Name, @CurrentGradeId)" +
                "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("Name", student.Name, DbType.String);
            parameters.Add("CurrentGradeId", student.CurrentGradeId, DbType.Int32);


            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdCompany = new Student
                {
                    Id = id,
                    Name = student.Name,
                    CurrentGradeId = student.CurrentGradeId,

                };

                return createdCompany;
            }
        }

        public async Task UpdateStudent(int id, StudentUpdateDto student)
        {
            var query = "UPDATE dbo.Students SET Name = @Name, CurrentGradeId = @CurrentGradeId WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", student.Name, DbType.String);
            parameters.Add("CurrentGradeId", student.CurrentGradeId, DbType.Int32);


            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteStudent(int id)
        {
            var query = "DELETE FROM dbo.Students WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

    }
}
