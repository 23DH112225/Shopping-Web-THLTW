using Microsoft.Extensions.Options;
using Shopping_Web.Models;
using Shopping_Web.Models.Momo;
using System.Security.Cryptography;
using System.Text;

namespace Shopping_Web.Services.Momo
{
    public class MomoService : IMomoService
    {
        private readonly IOptions<MomoOptionModel> _options;
        public MomoService(IOptions<MomoOptionModel> options)
        {
            _options = options;
        }
        //public async Task<MomoCreatePaymentResponseModel> CreatePaymentMomo(OrderInfo model)
        //{
        //    model.OrderId = DateTime.UtcNow.Ticks.ToString();
        //    model.OrderInformation = "Khách hàng: " + model.FullName + ". Nội dung: " + model.OrderInformation;
        //    var rawData =
        //        $"partnerCode={_options.Value.PartnerCode}" +
        //        $"&accessKey={_options.Value.AccessKey}" +
        //        $"&requestId={model.OrderId}" +
        //        $"&amount={model.Amount}" +
        //        $"&orderId={model.OrderId}" +
        //        $"&orderInfo={model.OrderInformation}" +
        //        $"&returnUrl={_options.Value.ReturnUrl}" +
        //        $"&notifyUrl={_options.Value.NotifyUrl}" +
        //        $"&extraData=";
        //    var signature = ComputeHmacSha256(rawData, _options.Value.SecretKey);
        //    var client = new RestClient(_options.Value.MomoApiUrl);
        //    var request = new RestRequest() { Method = Method.Post };
        //}
        private string ComputeHmacSha256(string message, string secretKey)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var messageBytes = Encoding.UTF8.GetBytes(message);

            byte[] hashBytes;

            using (var hmac = new HMACSHA256(keyBytes))
            {
                hashBytes = hmac.ComputeHash(messageBytes);
            }

            var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            return hashString;
        }
    }
}
