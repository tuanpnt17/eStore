using VNPAY.NET;

namespace eStore.Helpers
{
    public class VnpayPayment
    {
        public VnpayPayment(IVnpay vnpay, IConfiguration configuration)
        {
            vnpay.Initialize(
                configuration["Vnpay:TmnCode"]!,
                configuration["Vnpay:HashSecret"]!,
                configuration["Vnpay:BaseUrl"]!,
                configuration["Vnpay:CallbackUrl"]!
            );
        }
    }
}
