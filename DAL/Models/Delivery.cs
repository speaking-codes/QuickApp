﻿using DAL.Enums;

namespace DAL.Models
{
    public class Delivery : AuditableEntity
    {
        public int Id { get; set; }
        public EnumDeliveryType DeliveryType { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public virtual Customer Customer { get; set; }
    }
}