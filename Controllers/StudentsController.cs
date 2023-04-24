using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Data;
using SchoolAPI.Data.Entities;
using SchoolAPI.Models;

namespace SchoolAPI.Controllers
{
    [Route("api/v{version:apiversion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
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
        [MapToApiVersion("1.0")]
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

        [HttpPost]
        [MapToApiVersion("1.1")]
        public ActionResult<StudentModel> NewPost(StudentModel student)
        {
            try
            {
                Student studentDomainModel = _mapper.Map<Student>(student);
                var newStudentDM = _studentRepository.Add(studentDomainModel);
                StudentModel result = _mapper.Map<StudentModel>(newStudentDM);

                string location = _linkGenerator.GetPathByAction("Get", "Students", new { id = result.Id });
                return Created(location, result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }

        [HttpPut]
        public ActionResult<StudentModel> Put(StudentModel student)
        {
            try
            {
                var studentDomainModel = _mapper.Map<Student>(student);
                var updatedStudent = _studentRepository.Update(studentDomainModel);
                var result = _mapper.Map<StudentModel>(updatedStudent);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }

        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool isSuccess = _studentRepository.Remove(id);

                return Ok(isSuccess);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }
    }
}
