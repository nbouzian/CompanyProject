using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Business;
using EuroCompany.ViewModels;
using ImageResizer;
using System.IO;

namespace EuroCompany.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        private List<SelectListItem> GetMarketList(string selected, bool allOption = false)
        {
            List<SelectListItem> listeMarket = new List<SelectListItem>();
            SelectListItem mAll = new SelectListItem();
            SelectListItem mHealth = new SelectListItem();
            SelectListItem mAdvertising = new SelectListItem();
            SelectListItem mFinance = new SelectListItem();
            SelectListItem mICT = new SelectListItem();
            if (allOption)
            {
                mAll.Value = "";
                mAll.Text = "All";
                listeMarket.Add(mAll);
                mAll.Selected = true;
            }
            // mAll.Selected = true;
            mHealth.Value = "Health";
            mHealth.Text = "Health";

            mAdvertising.Value = "Advertising";
            mAdvertising.Text = "Advertising";

            mFinance.Value = "Finance";
            mFinance.Text = "Finance";

            mICT.Value = "ICT";
            mICT.Text = "ICT";

            listeMarket.Add(mAdvertising);
            listeMarket.Add(mHealth);
            listeMarket.Add(mFinance);
            listeMarket.Add(mICT);
            if (!string.IsNullOrEmpty(selected))
            {
                var selecteditem = listeMarket.Where(m => m.Value.ToLower().Equals(selected.ToLower())).FirstOrDefault();
                if (selecteditem != null)
                {
                    selecteditem.Selected = true;
                }
            }
            return listeMarket;
        }


        public ActionResult Index(string NameSearchCriteria, string MarketSearchCriteria, string MailSearchCriteria)
        {
            ViewBag.NameCriteria = string.IsNullOrEmpty(NameSearchCriteria) ? string.Empty : NameSearchCriteria;
            ViewBag.MarketCriteria = string.IsNullOrEmpty(MarketSearchCriteria) ? string.Empty : MarketSearchCriteria;
            ViewBag.MailCriteria = string.IsNullOrEmpty(MailSearchCriteria) ? string.Empty : MailSearchCriteria;
            ViewBag.ListeMarket = GetMarketList(string.Empty, true);
            List<CompanyListViewModel> listCompanyVM = new List<CompanyListViewModel>();

            List<Company> listAllCompany = new List<Company>();
            CompanyServices companyService = new CompanyServices();
            if (string.IsNullOrEmpty(NameSearchCriteria) && string.IsNullOrEmpty(MarketSearchCriteria) && string.IsNullOrEmpty(MailSearchCriteria))
            {
                listAllCompany = companyService.GetAllCompany();
            }
            else
            {
                listAllCompany = companyService.GetCompanyByCriteria(NameSearchCriteria, MarketSearchCriteria, MailSearchCriteria);
            }
            foreach (Company company in listAllCompany)
            {
                listCompanyVM.Add(new CompanyListViewModel(company));
            }
            return View(listCompanyVM);
        }

        public ActionResult Detail(int Id, string mode)
        {
            CompanyServices companyService = new CompanyServices();
            Company company = companyService.GetCompagnyByID(Id);
            CompanyDetailViewModel companyVM;
            if (company == null)
            {
                ViewBag.ErrorMessage = "No Company found for this ID: " + Id.ToString();
                companyVM = new CompanyDetailViewModel();
            }
            else
            {
                companyVM = new CompanyDetailViewModel(company);
                List<Company> listeCompetitor = companyService.GetAllCompanyForMarket(company.Market);
                foreach (Company c in listeCompetitor)
                {
                    if (c.ID != company.ID)
                    {
                        companyVM.competitors.Add(c.ID, c.Name);
                    }
                }
            }

            ViewBag.Mode = mode;
            return View(companyVM);
        }
        public ActionResult Edit(int Id)
        {
            CompanyServices companyService = new CompanyServices();
            Company company = companyService.GetCompagnyByID(Id);
            CompanyEditViewModel companyVM;

            if (company == null)
            {
                ViewBag.ErrorMessage = "No Company found for this ID: " + Id.ToString();
                companyVM = new CompanyEditViewModel();
                ViewBag.ListeMarket = GetMarketList(string.Empty);
            }
            else
            {
                companyVM = new CompanyEditViewModel(company);
                ViewBag.ListeMarket = GetMarketList(company.Market);

            }
            return PartialView(companyVM);
        }

        [HttpPost]
        public ActionResult Edit(CompanyEditViewModel companyUpdated)
        {
            CompanyServices companyService = new CompanyServices();

            if (ModelState.IsValid)
            {
                Company companyfromVM = new Company() { ID = companyUpdated.ID, Name = companyUpdated.Name, Mail = companyUpdated.Mail, Market = companyUpdated.Market, Logo = companyUpdated.Logo };

                //Resize the image 
                if (companyUpdated.NewLogo != null)
                {
                    //ResizeSettings resizeCropSettings = new ResizeSettings(320, 200, FitMode.Stretch, "png");
                    ResizeSettings resizeCropSettings = new ResizeSettings("width=320&height=200&mode=Pad&scale=both&format=png");

                    HttpPostedFileBase file = companyUpdated.NewLogo;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        ImageBuilder.Current.Build(file, ms, resizeCropSettings);
                        companyfromVM.Logo = ms.ToArray();
                    }
                }
                bool sucess = companyService.UpdateCompany(companyfromVM);
            }
            return RedirectToAction("Detail", new { Id = companyUpdated.ID });
        }

        public ActionResult Create()
        {
            ViewBag.ListeMarket = GetMarketList(string.Empty);
            return View();
        }

        [HttpPost]
        public ActionResult Create(CompanyCreateViewModel companyCreated)
        {
            CompanyServices companyService = new CompanyServices();
            //ligne suivante corrige un petit probleme du modelstate .champ ID comme requis ( car non nullable (int) ) changer le type(int?) ne me semble pas trés coherent...
            ModelState.Remove("ID");
            if (ModelState.IsValid)
            {
                Company companyfromVM = new Company() { Name = companyCreated.Name, Mail = companyCreated.Mail, Market = companyCreated.Market };
                //Resize the image 
                if (companyCreated.Logo != null)
                {
                    //ResizeSettings resizeCropSettings = new ResizeSettings(320, 200, FitMode.Stretch, "png");
                    ResizeSettings resizeCropSettings = new ResizeSettings("width=320&height=200&mode=Pad&scale=both&format=png");
                    HttpPostedFileBase file = companyCreated.Logo;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        ImageBuilder.Current.Build(file, ms, resizeCropSettings);
                        companyfromVM.Logo = ms.ToArray();
                    }
                }
                bool sucess = companyService.CreateCompany(companyfromVM);
            }
            return RedirectToAction("Index");
        }


    }
}
