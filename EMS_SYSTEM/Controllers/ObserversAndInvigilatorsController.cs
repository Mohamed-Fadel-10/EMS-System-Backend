﻿using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.APPLICATION.Repositories.Interfaces.IUnitOfWork;
using EMS_SYSTEM.APPLICATION.Repositories.Services;
using EMS_SYSTEM.APPLICATION.Repositories.Services.UnitOfWork;
using EMS_SYSTEM.DOMAIN.DTO;
using EMS_SYSTEM.DOMAIN.DTO.ObserversAndInvigilators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS_SYSTEM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObserversAndInvigilatorsController : ControllerBase
    {
        private readonly ObserversAndInvigilatorsService _observers;
        public ObserversAndInvigilatorsController(ObserversAndInvigilatorsService observers)
        {
            _observers = observers;
        }

        [HttpGet("{id}", Name ="GetById")]
        [Authorize(Roles = "Observers , Invigilators , FacultyAdmin ,GlobalAdmin")]
        public async Task<IActionResult> GetById(string id)
        {
            if(ModelState.IsValid)
            {
                ResponseDTO response = new();
                response = await _observers.GetByNID(id);

                if(response.IsDone)
                {
                    return Ok(response);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

    }
}
