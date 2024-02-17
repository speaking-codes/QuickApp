﻿using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class VacationInsurancePolicy : InsurancePolicy
    {
        //public EnumStructureType? StructureType { get; set; }
        public string StructureName { get; set; }
        public string StructureAddres { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public virtual Municipality PlaceStructure { get; set; }

        public virtual StructureType StructureType { get; set; }
    }
}
