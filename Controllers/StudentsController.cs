using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Data;
using SchoolAPI.Data.Entities;
using SchoolAPI.Models;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public StudentsController(IStudentRepository studentRepository, 
                                    IMapper mapper,
                                    LinkGenerator linkGenerator)
        {
            _studentRepository = studentRepository;
            this._mapper = mapper;
            this._linkGenerator = linkGenerator;
        }

        /// <summary>
        /// Returns list of all students
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<StudentModel>> Get(int? departmentId)
        {
            try
            {
                IEnumerable<Student> students = _studentRepository.AllStudents;
                if (departmentId.HasValue)
                {
                    students = _studentRepository.AllStudents.Where(s => s.DepartmentId == departmentId);
                }
                List<StudentModel> result = _mapper.Map<List<StudentModel>>(students);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }

        [HttpGet("id")]
        public ActionResult<StudentModel> Get(int id)
        {
            try
            {
                Student student = _studentRepository.GetById(id);
                StudentModel result = _mapper.Map<StudentModel>(student);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }

        [HttpPost]
        public ActionResult<StudentModel> Post(StudentModel student)
        {
            try
            {
                Student studentDomainModel = _mapper.Map<Student>(student);
                var newStudentDM = _studentRepository.Add(studentDomainModel);
                StudentModel result = _mapper.Map<StudentModel>(newStudentDM);

                string location = _linkGenerator.GetPathByAction("Get", "Students", new { id = result.Id});
                return Created(location, result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }
    }
}
