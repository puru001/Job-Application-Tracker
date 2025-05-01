using AutoMapper;
using JobApplicationTracker.Data;
using JobApplicationTracker.DTO;
using JobApplicationTracker.Models;
using JobApplicationTracker.Repository.IRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Net;

namespace JobApplicationTracker.Controllers
{
    [Route("api/applications")]
    [ApiController]
    public class JobApplicationAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IJobApplicationRepository _dbJobApplication;
        private readonly IMapper _mapper;
        public JobApplicationAPIController(IJobApplicationRepository dbJobApplication, IMapper mapper)
        {
            _dbJobApplication = dbJobApplication;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllJobApplications()
        {
            try
            {
                IEnumerable<JobApplication> jobApplicationList = await _dbJobApplication.GetAllAsync();
                _response.Result = _mapper.Map<List<JobApplicationDTO>>(jobApplicationList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;

        }

        [HttpGet("{id:int}", Name = "GetJobApplication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // [ProducesResponseType(200, Type = typeof(JobApplicationDTO)]
        public async Task<ActionResult<APIResponse>> GetJobApplication(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var jobApplication = await _dbJobApplication.GetAsync(u => u.Id == id);

                if (jobApplication == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<JobApplicationDTO>(jobApplication);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> CreateJobApplication([FromBody] JobApplicationCreateDTO createDTO)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{ 
                //    return BadRequest(ModelState);
                //}

                // Just for testing response type
                //if (_dbJobApplication.GetAsync(u => u.CompanyName.ToLower() == createDTO.CompanyName.ToLower()
                //                    && u.Position.ToLower() == createDTO.Position.ToLower()) != null)
                //{
                //    ModelState.AddModelError("CustomError", "The company and position for the job already exist");
                //    return BadRequest(ModelState);
                //}

                if (createDTO == null)
                {                    
                    return BadRequest(createDTO);
                }

                JobApplication jobApplication = _mapper.Map<JobApplication>(createDTO);

                await _dbJobApplication.CreateAsync(jobApplication);
                _response.Result = _mapper.Map<JobApplicationDTO>(jobApplication);
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetJobApplication", new { id = jobApplication.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteJobApplication")]
        public async Task<ActionResult<APIResponse>> DeleteJobApplication(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var jobApplication = await _dbJobApplication.GetAsync(u => u.Id == id);

                if (jobApplication == null)
                {
                    return NotFound();
                }
                await _dbJobApplication.RemoveAsync(jobApplication);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:int}", Name = "UpdateJobApplication")]
        public async Task<ActionResult<APIResponse>> UpdateJobApplication(int id, [FromBody] JobApplicationUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }

                JobApplication model = _mapper.Map<JobApplication>(updateDTO);
                await _dbJobApplication.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPatch("{id:int}", Name = "UpdatePartialJobApplication")]
        public async Task<IActionResult> UpdatePartialJobApplication(int id, JsonPatchDocument<JobApplicationUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            var jobApplication = await _dbJobApplication.GetAsync(u => u.Id == id);
            //JobApplicationUpdateDTO applicationDTO = _mapper.Map<JobApplicationUpdateDTO>(jobApplication);
            if (jobApplication == null)
            {
                return BadRequest();
            }
            JobApplicationUpdateDTO applicationDTO = _mapper.Map<JobApplicationUpdateDTO>(jobApplication);
            patchDTO.ApplyTo(applicationDTO, ModelState);
            JobApplication model = _mapper.Map<JobApplication>(applicationDTO);

            await _dbJobApplication.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();

        }


    }
}
