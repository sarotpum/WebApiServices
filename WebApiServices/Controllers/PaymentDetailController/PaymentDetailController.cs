using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedService.DBContext;
using SharedService.Models.PaymentDetail;

namespace WebApiServices.Controllers.PaymentDetailController
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentDetailController : ControllerBase
    {
        private readonly DatasContext _datasContext;

        public PaymentDetailController(DatasContext datasContext)
        {
            _datasContext = datasContext;
        }

        // GET: api/PaymentDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDetailModel>>> GetPaymentDetails()
        {
            return await _datasContext.PaymentDetail.ToListAsync();
        }

        // PUT: api/PaymentDetail/Create/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PaymentDetail(int id, PaymentDetailModel paymentDetail)
        {
            if (id != paymentDetail.PMId)
                return BadRequest();

            _datasContext.Entry(paymentDetail).State = EntityState.Modified;

            try
            {
                await _datasContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentDetailExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // GET: api/PaymentDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetailModel>> GetPaymentDetail(int id)
        {
            var paymentDetail = await _datasContext.PaymentDetail.FindAsync(id);
            if (paymentDetail == null)
                return NoContent();

            return paymentDetail;
        }

        // POST: api/PaymentDetail
        [HttpPost]
        public async Task<ActionResult<PaymentDetailModel>> PostPaymentDetail(PaymentDetailModel paymentDetail)
        {
            await _datasContext.PaymentDetail.AddAsync(paymentDetail);
            await _datasContext.SaveChangesAsync();
            return CreatedAtAction("GetPaymentDetail", new { id = paymentDetail.PMId }, paymentDetail);
        }

        // DELETE: PaymentDetail/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PaymentDetailModel>> DeletePaymentDetail(int id)
        {
            var paymentDetail = await _datasContext.PaymentDetail.FindAsync(id);

            if (paymentDetail == null)
                NoContent();

            _datasContext.PaymentDetail.Remove(paymentDetail);
            await _datasContext.SaveChangesAsync();
            return paymentDetail;
        }

        private bool PaymentDetailExists(int id)
        {
            return _datasContext.PaymentDetail.Any(x => x.PMId == id);
        }
    }
}
