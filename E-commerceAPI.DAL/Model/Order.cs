using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.DAL.Model
{
   public enum StatusOrderEnum
    {
        Pending=1,
        Cancel=2,
        approved=3,
        Shipped=4,
        Delivered=5,
        
    }
    public enum StatusPaymentMethodEnum
    {
        Visa=2,
        Cach=1,

    }
    public class Order
    {
        public int Id { get; set; }
        public StatusOrderEnum Status { get; set; }= StatusOrderEnum.Pending;
        //تاريخ الطلب
        public DateTime OrderDate { get; set; }
        //تاريخ شحن الطلبية
        public DateTime ShippedDate { get; set; }

        public decimal TotalAmount { get; set; }

        public StatusPaymentMethodEnum PaymentMethod { get; set; }

        // لما استخدم الفيزا بستخدمهم
        public string?PaymentId { get; set; }
       // public string? TransactionId {  get; set; }

        public string? CarrierName { get; set; }// اسم شركة الشحن
        public string? TrackingNumber { get; set; }// الرقم يلي بميز الطرد عند الشركة

        //العلاقة بين الاوردر وبين اليوزر 
        public string UserId {  get; set; }
        public ApplicationUser User { get; set; }

    }
}
