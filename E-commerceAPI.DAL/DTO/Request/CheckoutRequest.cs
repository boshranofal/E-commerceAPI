using E_commerceAPI.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.DAL.DTO.Request
{
    public class CheckoutRequest
    {
        public StatusPaymentMethodEnum PaymentMethod { get; set; }
    }
}
