using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;
namespace DAO
{
    public class CompanyDAOEntity
    {
        //private Model1Container context;

        //public Model1Container Context
        //{
        //    get { return context; }
        //    set { context = value; }
        //}

        public CompanyDAOEntity()
        {
        }

        public List<Model.Company> GetAllCompany()
        {
            List<Company> allCompany = new List<Company>();
            using (Model1Container context = new Model1Container())
            {
                allCompany = context.Company.ToList();
            }
            return allCompany;
        }

        public Model.Company GetCompagnyByID(int ID)
        {
            Company company;
            using (Model1Container context = new Model1Container())
            {
                company = context.Company.Find(ID);
            }
            return company;
        }

        //dans cette methode gestion des recherches multis criteres 
        public List<Company> GetCompanyByCriteria(string NameSearchCriteria, string MarketSearchCriteria, string MailSearchCriteria)
        {
            List<Company> allCompany = new List<Company>();

            using (Model1Container context = new Model1Container())
            {
                if (string.IsNullOrEmpty(NameSearchCriteria))
                {
                    allCompany = context.Company.Where(c => c.Mail.Contains(MailSearchCriteria) && c.Market.Contains(MarketSearchCriteria)).ToList();
                }
                else if (string.IsNullOrEmpty(MailSearchCriteria))
                {
                    allCompany = context.Company.Where(c => c.Name.Contains(NameSearchCriteria) && c.Market.Contains(MarketSearchCriteria)).ToList();
                }
                else if (string.IsNullOrEmpty(MarketSearchCriteria))
                {
                    allCompany = context.Company.Where(c => c.Name.Contains(NameSearchCriteria) && c.Mail.Contains(MailSearchCriteria)).ToList();
                }
                else
                {
                    allCompany = context.Company.Where(c => c.Name.Contains(NameSearchCriteria) && c.Mail.Contains(MailSearchCriteria) && c.Market.Contains(MarketSearchCriteria)).ToList();
                }
            }

            return allCompany;
        }

        public List<Company> GetCompanyByName(string NameSearchCriteria)
        {
            List<Company> allCompany = new List<Company>();

            using (Model1Container context = new Model1Container())
            {
                allCompany = context.Company.Where(c => c.Name.Contains(NameSearchCriteria)).ToList();
            }
            return allCompany;

        }

        public List<Company> GetAllCompanyByMarket(string MarketSearchCriteria)
        {
            List<Company> allCompany = new List<Company>();

            using (Model1Container context = new Model1Container())
            {
                allCompany = context.Company.Where(c => c.Market.Contains(MarketSearchCriteria)).ToList();
            }
            return allCompany;
        }

        public List<Company> GetAllCompanyByMail(string MailSearchCriteria)
        {
            List<Company> allCompany = new List<Company>();

            using (Model1Container context = new Model1Container())
            {
                allCompany = context.Company.Where(c => c.Mail.Contains(MailSearchCriteria)).ToList();
            }
            return allCompany;
        }

        public bool UpdateCompany(Company companyfromVM)
        {
            Company company;
            bool succes = true;
            try
            {
                using (Model1Container context = new Model1Container())
                {
                    company = context.Company.Find(companyfromVM.ID);
                    context.Entry(company).CurrentValues.SetValues(companyfromVM);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                succes = false;
            }
            return succes;
        }

        public bool CreateCompany(Company companyfromVM)
        {
            Company company;
            bool succes = true;
            try
            {
                using (Model1Container context = new Model1Container())
                {
                    company = context.Company.Add(companyfromVM);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                succes = false;
            }
            return succes;
        }

        public List<Company> GetAllCompanyForMarket(string market)
        {
            List<Company> allCompany = new List<Company>();

            using (Model1Container context = new Model1Container())
            {
                allCompany = context.Company.Where(c => c.Market.ToLower().Equals(market.ToLower())).ToList();
            }
            return allCompany;
        }
    }
}
