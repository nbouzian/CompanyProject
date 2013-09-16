using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
namespace EuroCompany.ViewModels
{
    public class CompanyEditViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Market { get; set; }
        [EmailAddress]
        public string Mail { get; set; }

        public byte[] Logo { get; set; }

        public HttpPostedFileBase NewLogo { get; set; }
        public CompanyEditViewModel()
        {
            this.ID = 0;
            this.Name = "Error";
        }

        public CompanyEditViewModel(Company company)
        {
            this.ID = company.ID;
            this.Name = company.Name;
            this.Market = company.Market;
            this.Mail = company.Mail;
            this.Logo = company.Logo;
        }

    }
}