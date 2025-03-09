using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using phoneBook.DataAccess.Services.Contacts;
using phoneBook.Entities.DTO.Contact;
using phoneBook.Entities.IRepository;
using phoneBook.Entities.Models;
using PhoneNumbers;
using System.Net;

namespace Phonebook.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactController : ControllerBase
    {
        private static PhoneNumberUtil _phoneUtil; // To Validate Phone Number
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public ContactController(IMapper mapper, IContactService contactService)
        {
            _mapper = mapper;
            _contactService = contactService;
            _phoneUtil = PhoneNumberUtil.GetInstance();
        }
       

        [HttpGet("GetAllContacts/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetContacts([FromRoute] int pageNumber,[FromRoute]int pageSize)
        {
            var (contacts, totalCount) = await _contactService.GetAllContacts(pageNumber, pageSize);
            var response = new
            {
                Data = contacts,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
            return Ok(ApiResponse<object>.Success(response, "Contacts retrieved successfully"));
        }

        [HttpGet("GetContactById/{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var contact = await _contactService.GetContactById(id);
                if (contact == null)
                    return Ok(ApiResponse<string>.Error("Contact not found"));

                return Ok(ApiResponse<AddUpdateContactDTO>.Success(contact, "Contact retrieved successfully"));
            }
            catch (Exception ex)
            {
                var Errors = new List<string> { $"An error occurred while retrieving Contact with Id {id}", ex.Message };

                var errorResponse = ApiResponse<string>.Error(message: "Failed to retrieve Contact", errors: Errors);

                return StatusCode((int)HttpStatusCode.InternalServerError, errorResponse);
            }
        }
        
        [HttpGet("GetContactByName/{name}")]
        public async Task<IActionResult> GetByName([FromRoute] string name)
        {
            try
            {
                var contact = await _contactService.GetByName(name.Trim());
                if (contact == null)
                {
                    return Ok(ApiResponse<object>.NotFound($"Contact with name '{name}' not found"));
                }

                var successResponse = ApiResponse<Contact>.Success(
                    data: contact,
                    message: $"Contact with name '{name}' retrieved successfully"
                );

                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                var Errors = new List<string> { $"An error occurred while retrieving Contact with name : {name}", ex.Message };

                var errorResponse = ApiResponse<string>.Error(message: "Failed to retrieve Contact", errors: Errors);

                return StatusCode((int)HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        [HttpPost("AddNewContact")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddContact([FromBody] AddUpdateContactDTO NewContactFromReq)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<string>.ValidationError(ModelState, "Validation failed"));

                var createdContact = await _contactService.AddContact(NewContactFromReq);
                return CreatedAtAction(nameof(GetById), new { id = createdContact.Id }, ApiResponse<Contact>.Success(createdContact, "Contact added successfully"));
            }
            catch (Exception ex)
            {
                // TODO : Log the exception

                // Return error response
                var Errors = new List<string> { $"An error occurred while Adding Contact ", ex.Message };

                var errorResponse = ApiResponse<string>.Error(message: "Failed to Add Contact", errors: Errors);

                return StatusCode((int)HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateContact([FromRoute]int id, [FromBody] AddUpdateContactDTO UpdatedContactFromReq)
        {
            try
            {
                // Validate the input model
                if (!ModelState.IsValid)
                {
                    return BadRequest(ApiResponse<string>.ValidationError(modelState: ModelState, message: "Validation failed"));
                }

                var updated = await _contactService.UpdateContact(id, UpdatedContactFromReq);
                if (updated == null)
                    return NotFound(ApiResponse<string>.Error("Contact not found"));

                return Ok(ApiResponse<AddUpdateContactDTO>.Success(updated, "Contact updated successfully"));

            }
            catch (Exception ex)
            {
                // TODO: Log the exception
                var errors = new List<string> { "An error occurred while updating the contact.", ex.Message };

                var errorResponse = ApiResponse<string>.Error(message: "Failed to update contact", errors: errors);

                return StatusCode((int)HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            try
            {
                var deleted = await _contactService.DeleteContact(id);
                if (!deleted)
                    return Ok(ApiResponse<string>.Error("Contact not found"));

                return Ok(ApiResponse<string>.Error("Contact Deleted Succsessfully"));
            }
            catch (Exception ex)
            {
                // TODO: Log the exception
                var errors = new List<string> { "An error occurred while deleting the contact.", ex.Message };

                var errorResponse = ApiResponse<string>.Error(
                    message: "Failed to delete contact",
                    errors: errors
                );

                return StatusCode((int)HttpStatusCode.InternalServerError, errorResponse);
            }
        }

    }
}
