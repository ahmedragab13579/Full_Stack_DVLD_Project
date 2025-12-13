using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.UpdateDtos;
using DVLD_Application.Services.Interfaces.Humans.Person;
using Microsoft.AspNetCore.Mvc;

namespace DVLD_Persentation.Controllers
{
    [Route("api/people")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        /// <summary>
        /// Retrieves a list of all people.
        /// </summary>
        /// <returns>A list of person details.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllPeople()
        {
            var result = await _personService.GetAllPeopleAsync();
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }

        /// <summary>
        /// Retrieves a person by their ID.
        /// </summary>
        /// <param name="id">The unique identifier of the person.</param>
        /// <returns>The person details if found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonById(int id)
        {
            var result = await _personService.GetPersonByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }

        /// <summary>
        /// Retrieves a person by their National No.
        /// </summary>
        /// <param name="nationalNo">The national number of the person.</param>
        /// <returns>The person details if found.</returns>
        [HttpGet("national-no/{nationalNo}")]
        public async Task<IActionResult> GetPersonByNationalNo(string nationalNo)
        {
            var result = await _personService.GetPersonByNationalNoAsync(nationalNo);
            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }

        /// <summary>
        /// Creates a new person.
        /// </summary>
        /// <param name="dto">The data to create the person.</param>
        /// <returns>The ID of the created person.</returns>
        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] CreateNewPersonDto dto)
        {
            var result = await _personService.CreatePersonAsync(dto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return CreatedAtAction(nameof(GetPersonById), new { id = result.Data }, result.Data);
        }

        /// <summary>
        /// Updates an existing person.
        /// </summary>
        /// <param name="id">The ID of the person to update.</param>
        /// <param name="dto">The updated data.</param>
        /// <returns>A success message.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] UpdatePersonDto dto)
        {
            var result = await _personService.UpdatePersonAsync(dto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok("Person updated successfully.");
        }

        /// <summary>
        /// Deletes a person by their ID.
        /// </summary>
        /// <param name="id">The ID of the person to delete.</param>
        /// <returns>A success message.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var result = await _personService.DeletePersonAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok("Person deleted successfully.");
        }
    }
}
