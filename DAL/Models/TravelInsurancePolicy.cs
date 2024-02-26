﻿using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Travel
    {
        public int Id { get; set; }

        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }

        public virtual TravelMeansType TravelMeansType { get; set; }

        public virtual TravelClassType TravelClassType { get; set; }

        public virtual ConfigurationModel ConfigurationModel { get; set; }

        public short DepartureMunicipalityId { get; set; }
        public virtual Municipality DepartureMunicipality { get; set; }

        public short ArrivalMunicipalityId { get; set; }
        public virtual Municipality ArrivalMunicipality { get; set; }

        public virtual InsurancePolicy InsurancePolicy { get; set; }
    }
}
