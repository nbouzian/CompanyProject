using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Model
{
    enum MarketType
    {
        ICT,
        Advertising,
        Finance,
        Health
    } 

    [MetadataType(typeof(CompanyMetaData))]
    public partial class Company { }

    public class CompanyMetaData
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        //[EnumDataType(typeof(MarketType))]
        public string Market { get; set; }
        [EmailAddress]
        public string Mail { get; set; }
    }

}
