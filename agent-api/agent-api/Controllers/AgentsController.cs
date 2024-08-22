﻿using agent_api.Dto;
using agent_api.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace agent_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AgentsController(IAgentService agentService) : ControllerBase
    {

        [HttpPost]
        [ProducesResponseType(typeof(TargetDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<AgentDto>> CreateTarget([FromBody] AgentDto agentDto)
        {
            try
            {
                AgentDto newAgent = await agentService.CreateAgentAsync(agentDto);
                return Created("new agent", newAgent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet]
        [ProducesResponseType(typeof(List<TargetDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<AgentDto>>> GetAllAgents()
        {
            try
            {
                return Ok(await agentService.GetAllAgentsAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}/Pin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PinTarget([FromBody] LocationDto pinLocation, long id)
        {
            try
            {
                await agentService.PinAgentLocationAsync(pinLocation, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/Move")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> MoveTarget([FromBody] DirectionDto direction, long id)
        {
            try
            {
                await agentService.MoveAgentLocationAsync(direction, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
