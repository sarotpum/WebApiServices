using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedService.DBContext;
using SharedService.Models.Contact;

namespace WebApiServices.Controllers.ContactsContoller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly DatasContext _datasContext;

        public ContactsController(DatasContext datasContext)
        {
            _datasContext = datasContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await _datasContext.Contacts.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContacts([FromRoute] Guid id)
        {
            var contact = await _datasContext.Contacts.FindAsync(id);

            if (contact == null)
                return NotFound();

            return Ok(contact);
        }
        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactModel value)
        {
            var contact = new ContactModel()
            {
                Id = Guid.NewGuid(),
                Address = value.Address,
                Email = value.Email,
                FullName = value.FullName,
                Phone = value.Phone,
            };

            await _datasContext.Contacts.AddAsync(contact);
            await _datasContext.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactModel value)
        {
            var contact = await _datasContext.Contacts.FindAsync(id);

            if (contact != null)
            {
                contact.FullName = value.FullName;
                contact.Address = value.Address;
                contact.Phone = value.Phone;
                contact.Email = value.Email;

                await _datasContext.SaveChangesAsync();
                return Ok(contact);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromBody] Guid id)
        {
            var contact = await _datasContext.Contacts.FindAsync(id);

            if (contact != null)
            {
                _datasContext.Contacts.Remove(contact);
                await _datasContext.SaveChangesAsync();
                return Ok(contact);
            }

            return NotFound();
        }
    }
}
