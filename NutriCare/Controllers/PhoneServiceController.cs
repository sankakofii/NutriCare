using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NutriCare.Models;
using NutriCare.VerificationService;
using Twilio.Rest.Verify.V2.Service;
using Twilio;

namespace NutriCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneServiceController : ControllerBase
    {
        private readonly IPhoneService _phoneService;
        private readonly DataContext _context;

        public PhoneServiceController(IPhoneService phoneVerificationService, DataContext context)
        {
            _phoneService = phoneVerificationService;
            _context = context;
        }

        [HttpPost("send_registration_code")]
        public async Task<IActionResult> SendPhoneVerificationMessageForRegistration(string phoneNumber)
        {
            //check if the number is associated with account already, if not, send the code
            Account? alreadyExistingAccount = await _context.Accounts
                .Where(n => n.PhoneNumber == phoneNumber)
                .FirstOrDefaultAsync();

            if (alreadyExistingAccount == null)
            {
                return Ok(_phoneService.SendPhoneVerification(phoneNumber));
            }
            return BadRequest(alreadyExistingAccount);
        }

        [HttpPost("verify_code")]
        public IActionResult SendCodeToVerifyPhoneNumber(string codeFromSMS, string phoneNumber)
        {
            string verificationStatus = _phoneService.CheckVerificationCode(codeFromSMS, phoneNumber);

            if (verificationStatus == "pending")
            {
                return BadRequest("The provided code is incorrect, please check your SMS and input the code from the received message.");
            }
            else if (verificationStatus != "approved")
            {
                return BadRequest("Please try again later.");
            }
            return Ok(verificationStatus);
        }
    }
}
