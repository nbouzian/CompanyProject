using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAO;

namespace Business
{
    public class CompanyServices
    {
        public List<Model.Company> GetAllCompany()
        {
            CompanyDAOEntity companyDao = new CompanyDAOEntity();
           return companyDao.GetAllCompany();
        }

        public Model.Company GetCompagnyByID(int ID)
        {
            CompanyDAOEntity companyDao = new CompanyDAOEntity();
            return companyDao.GetCompagnyByID(ID);
        }


        public List<Company> GetCompanyByCriteria(string NameSearchCriteria, string MarketSearchCriteria, string MailSearchCriteria)
        {
            CompanyDAOEntity companyDao = new CompanyDAOEntity();
            if ((!string.IsNullOrEmpty(NameSearchCriteria)) && string.IsNullOrEmpty(MarketSearchCriteria) && string.IsNullOrEmpty(MailSearchCriteria))
            {
                 return companyDao.GetCompanyByName(NameSearchCriteria);
            }
            else if (string.IsNullOrEmpty(NameSearchCriteria) && (!string.IsNullOrEmpty(MarketSearchCriteria)) && string.IsNullOrEmpty(MailSearchCriteria))
            {
                return companyDao.GetAllCompanyByMarket(MarketSearchCriteria);
            }
            else if (string.IsNullOrEmpty(NameSearchCriteria) && string.IsNullOrEmpty(MarketSearchCriteria) && (!string.IsNullOrEmpty(MailSearchCriteria)))
            {
                return companyDao.GetAllCompanyByMail(MailSearchCriteria);
            }
            else
            {
                return companyDao.GetCompanyByCriteria(NameSearchCriteria, MarketSearchCriteria, MailSearchCriteria);
            }
        }


        public bool UpdateCompany(Company companyfromVM)
        {
            CompanyDAOEntity companyDao = new CompanyDAOEntity();
            return companyDao.UpdateCompany(companyfromVM);
        }

        public bool CreateCompany(Company companyfromVM)
        {
            CompanyDAOEntity companyDao = new CompanyDAOEntity();
            return companyDao.CreateCompany(companyfromVM);
        }

        public List<Company> GetAllCompanyForMarket(string market)
        {
            CompanyDAOEntity companyDao = new CompanyDAOEntity();
            return companyDao.GetAllCompanyForMarket(market);
        }
    }
}
