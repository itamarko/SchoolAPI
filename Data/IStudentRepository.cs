﻿using SchoolAPI.Data.Entities;

namespace SchoolAPI.Data
{
    public interface IStudentRepository
    {
        IEnumerable<Student> AllStudents { get; }
        Student GetById(int id);
        Student Add(Student student);
        Student Update (Student student);
        bool Remove(int id);
    }
}
