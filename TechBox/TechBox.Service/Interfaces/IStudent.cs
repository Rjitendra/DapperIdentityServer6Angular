


namespace TechBox.Service.Interfaces
{
    using TechBox.Model.Dto;
    using TechBox.Model.Entity;
    public interface IStudent
    {
        public Task<IEnumerable<Student>> GetStudents();
        public Task<Student> GetStudent(int id);
        public Task<Student> CreateStudent(StudentCreationDto student);
        public Task UpdateStudent(int id, StudentUpdateDto student);
        public Task DeleteStudent(int id);
        //public Task<Student> GetStudentByGradeId(int id);
        //public Task<Student> GetStudentByGradeMultipleResults(int id);
        //public Task<List<Student>> GetStudentByGradeMultipleMapping();
        //public Task CreateMultipleCompanies(List<StudentCreationDto> students);
    }
}
