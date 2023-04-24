using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Data;
using SchoolAPI.Data.Entities;
using SchoolAPI.Models;
using System.Collections.Generic;

namespace SchoolAPI.Controllers
{
    [Route("api/students/{studentId}/[controller]")]
    [ApiController]
    public class FinalsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly IFinalRepository _finalRepository;

        public FinalsController(IMapper mapper, LinkGenerator linkGenerator, IFinalRepository finalRepository)
        {
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _finalRepository = finalRepository;
        }

        [HttpGet]
        public ActionResult<List<FinalModel>> Get(int studentId)
        {
            try
            {
                List<FinalModel> result = new List<FinalModel>();

                var finals = _finalRepository.GetFinalsByStudentId(studentId);
                result = _mapper.Map<List<FinalModel>>(finals);

                return result;
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }

        [HttpGet("id")]
        public ActionResult<FinalModel> Get(int studentId, int id)
        {
            try
            {
                FinalModel result = null;

                var finalDM = _finalRepository.GetById(id);
                if (finalDM != null && finalDM.StudentId == studentId)
                {
                    result = _mapper.Map<FinalModel>(finalDM);
                }

                return result;
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }

        [HttpPost]
        public ActionResult<FinalModel> Post(int studentId, FinalModel final)
        {
            try
            {
                FinalModel result = null;

                Final finalDM = _mapper.Map<Final>(final);
                finalDM.StudentId = studentId;
                Final finalResult = _finalRepository.Add(finalDM);
                result = _mapper.Map<FinalModel>(finalResult);

                return result;
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }

        [HttpPut]
        public ActionResult<FinalModel> Put (int studentId, int id, FinalModel final)
        {
            try
            {
                FinalModel result = null;

                Final finalDM = _mapper.Map<Final>(final);
                finalDM.StudentId = studentId;
                finalDM.Id = id;
                var finalDMResult = _finalRepository.Update(finalDM);
                result = _mapper.Map<FinalModel>(finalDMResult);

                return result;
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }
    }
}
