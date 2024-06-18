﻿using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.APPLICATION.Repositories.Interfaces.IUnitOfWork;
using EMS_SYSTEM.APPLICATION.Repositories.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS_SYSTEM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalAdminController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGlobalService _globalService;

        public GlobalAdminController(IUnitOfWork unitOfWork,IGlobalService _globalService)
        {
            _unitOfWork = unitOfWork;
            this._globalService = _globalService;
        }
        [HttpGet("GetAllFaculties")]
        public async Task<IActionResult> GetAllFaculties()
        {
            var Response = await _globalService.GetAllFaculties();
            if(Response.IsDone) {
                return StatusCode(Response.StatusCode,Response.Model);           
            }
            return BadRequest(ModelState);
        }
        [HttpGet("GetFacultyByName")]
        public async Task<IActionResult> GetFacultyByName(string FacultyName)
        {
            if (ModelState.IsValid)
            {
                var Response = await _globalService.GetFacultyByName(FacultyName);
                if (Response.IsDone)
                {
                    return StatusCode(Response.StatusCode, Response.Model);
                }
                return StatusCode(Response.StatusCode, Response.Message);
            }
            return BadRequest(ModelState);
        }
    }
}
