using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
namespace EuroCompany.ViewModels
{
    public class CompanyListViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Market { get; set; }
        [EmailAddress]
        public string Mail { get; set; }
        public byte[] Logo { get; set; }

        public CompanyListViewModel()
        {

        }
        public CompanyListViewModel(Company company ) {
            this.ID = company.ID;
            this.Name = company.Name;
            this.Market = company.Market;
            this.Mail = company.Mail;
            this.Logo = company.Logo;
        }

    }
}