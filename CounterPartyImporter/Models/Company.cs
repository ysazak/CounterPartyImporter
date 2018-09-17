using System.ComponentModel.DataAnnotations;

namespace CounterPartyDomain.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string ExternalId { get; set; }
        [Required]
        public string TradingName { get; set; }
        [Required]
        public string LegalName { get; set; }
        [Required]
        public CompanyType CompanyType { get; set; }
        [Required]
        public bool Unused { get; set; }
        [Required]
        public bool IsForwarder { get; set; }
        [Required, MaxLength(50)]
        public string Phone { get; set; }
        [Required, MaxLength(50)]
        public string Fax { get; set; }
        public int? AddressId { get; set; }
        public int? MailAddressId { get; set; }        
        [Required]
        public bool IsCustomClerance { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public bool IsCarrier { get; set; }
        [Required]
        public bool IsWareHouse { get; set; }
        public string ChamberOfCommerce { get; set; }
        public string ChamberOfCommerceCi { get; set; }
        public string CountryVAT { get; set; }
        [Required]
        public bool IsExchangeBroker { get; set; }
    }
}
