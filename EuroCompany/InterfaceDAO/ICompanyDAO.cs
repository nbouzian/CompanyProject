using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace InterfaceDAO
{
    public interface ICompanyDAO : IDisposable
    {
        List<Company> GetAllCompany();
        List<Company> GetAllCompanyByName(string name);
        Company GetCompagnyByID(int ID);
        void SetContext(Model1Container ctx);
    }
}
