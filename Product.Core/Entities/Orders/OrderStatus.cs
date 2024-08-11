using System.Runtime.Serialization;

namespace Product.Core.Entities
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Đang duyệt")]
        pending,
        [EnumMember(Value = "Đã thanh toán")]
        paymentRecived,
        [EnumMember(Value = "Thanh toán thất bại")]
        PaymentFailed
    }
}