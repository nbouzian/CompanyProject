using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
namespace EuroCompany.ViewModels
{
    public class CompanyDetailViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Market { get; set; }
        [Display(Name="Contact adress")]
        public string Mail { get; set; }
        public byte[] Logo { get; set; }
        //Other Company with the same market
        public Dictionary<int, string> competitors { get; set; }
        public CompanyDetailViewModel()
        {
            this.ID = 0;
            this.Name = "Error";
            competitors = new Dictionary<int, string>();

        }
        public CompanyDetailViewModel(Company company)
        {
            this.ID = company.ID;
            this.Name = company.Name;
            this.Market = company.Market;
            this.Mail = company.Mail;
            this.Logo = company.Logo;
            competitors = new Dictionary<int, string>();
        }

    }
}