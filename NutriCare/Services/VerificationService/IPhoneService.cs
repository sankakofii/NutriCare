namespace NutriCare.VerificationService
{
    public interface IPhoneService
    {
        public string SendPhoneVerification(string phoneNumber);
        public string CheckVerificationCode(string codeFromSMS, string phoneNumber);
    }
}
