﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TradersMarketplace.Models
{
    public class Crudl
    {
        [Key]
        public int ProductId { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        public Guid Categoryid { get; set; }
        [Display(Name = "Category")]
        public string Category { get; set; }
        public SelectList CategoryiesList { get; set; }
        [Display(Name = "Subcategory")]
        public string Subcategory { get; set; }
        public SelectList SubcategoriesList { get; set; }
        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }
        [Display(Name = "")]
        public int QtyAvailable { get; set; }
        [Display(Name = "")]
        public decimal Price { get; set; }
        [Display(Name = "")]
        public string ImagePAth { get; set; }
        public int SellerId { get; set; }
        public int CommissionTypeId { get; set; }
        [Display(Name = "Commission Type")]
        public string CommissionType { get; set; }
        public SelectList CommissionTypesList { get; set; }
        [Display(Name = "Commission Amount")]
        public decimal CommissionAmount { get; set; }
    }

    public class Dbcontext : DbContext
    {
        public DbSet<Crudl> OrderStatuses { get; set; }
    }
}