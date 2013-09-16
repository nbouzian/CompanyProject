using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
namespace EuroCompany.ViewModels
{
    public class CompanyCreateViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name",Prompt="Company's name")]
        public string Name { get; set; }

        public string Market { get; set; }

        [EmailAddress]
        public string Mail { get; set; }

        public HttpPostedFileBase Logo { get; set; }


        public CompanyCreateViewModel()
        {
           
        }

    }
}