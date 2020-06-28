using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactInformation.Domain.Handlers;
using ContactInformation.Domain.Models;

namespace ContactInformation.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactHandler _contactHandler;

        public ContactController(IContactHandler contactHandler)
        {
            _contactHandler = contactHandler;
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<Contact>>> GetContact()
        {
            var contacts = await _contactHandler.GetContacts();
            return contacts;
        }

        [HttpGet("getbyid/{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            var contact = await _contactHandler.GetContact(id);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> PutContact(int id, Contact contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }

            if (!await _contactHandler.ContactExists(id))
            {
                return NotFound();
            }

            try
            {
                await _contactHandler.UpdateContact(id, contact);
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500);
            }

            return NoContent();
        }

        [HttpPost("add")]
        public async Task<ActionResult<Contact>> PostContact(Contact contact)
        {
            var response = await _contactHandler.AddContact(contact);

            return CreatedAtAction("GetContact", new { id = response.Id }, response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Contact>> DeleteContact(int id)
        {
            var contact = await _contactHandler.DeleteContact(id);
            return contact;
        }
    }
}
