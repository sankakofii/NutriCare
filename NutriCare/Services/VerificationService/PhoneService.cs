using Twilio.Rest.Verify.V2.Service;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System.Text.Json;
using System.Configuration;

namespace NutriCare.VerificationService
{
    public class PhoneService : IPhoneService
    {
        private readonly IConfiguration _configuration;
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _serviceSid;

        public PhoneService(IConfiguration configuration)
        {
            _configuration = configuration;
            _accountSid = _configuration.GetSection("TwilioConfig").GetSection("TWILIO_ACCOUNT_SID").Value;
            _authToken = _configuration.GetSection("TwilioConfig").GetSection("TWILIO_AUTH_TOKEN").Value;
            _serviceSid = _configuration.GetSection("TwilioConfig").GetSection("TWILIO_SERVICE_SID").Value;
        }
        public string SendPhoneVerification(string phoneNumber)
        {

            TwilioClient.Init(_accountSid, _authToken);

            var verification = VerificationResource.Create(
              to: phoneNumber,
              channel: "sms",
              pathServiceSid: _serviceSid
            );

            return verification.Status;
        }

        public string CheckVerificationCode(string codeFromSMS, string phoneNumber)
        {
            TwilioClient.Init(_accountSid, _authToken);

            var verificationCheck = VerificationCheckResource.Create(
                to: phoneNumber,
                code: codeFromSMS,
                pathServiceSid: _serviceSid
                );

            return verificationCheck.Status;
        }
    }
}

    

