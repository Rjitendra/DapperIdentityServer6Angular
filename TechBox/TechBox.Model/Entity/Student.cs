﻿

namespace TechBox.Model.Entity
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CurrentGradeId { get; set; }
        public Grade CurrentGrade
        {
            get; set;
        }
    }
}
