﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Data.Entity
{
    public class Tenant : Base
    {


        public Tenant() { Schedules = new List<Schedule>();
            ProductsInTenant=new List<ProductInTenant>();
        }
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public SAUser Owner { get; set; }
        public string Description { get; set; }
        public string LogoURL { get; set; }
        public string Color { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        //working hours, link fb,
        public string Email { get; set; }
        //public double Rating { get; set; }
        public virtual List<Schedule> Schedules { get; set; }
        public virtual List<ProductInTenant>  ProductsInTenant { get; set; }

        public string StartingHour { get; set; }
        public string EndHour { get; set; }
        public string FacebookLink { get; set; }
        public string InstagramLink { get; set; }





        public enum Category
        {
            COSMETICS,
            MASSAGE,
            BARBER,

        }
    }
}
