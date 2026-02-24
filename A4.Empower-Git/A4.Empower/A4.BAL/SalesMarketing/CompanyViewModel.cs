using System;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace A4.BAL
{
   public class CompanyViewModel
    {
        public CompanyViewModel()
        {
            listCompany = new List<CompanyModel>();
        }
        public List<CompanyModel> listCompany { get; set; }
        public int TotalCount { get; set; }
    }

    public class CompanyModel
    {
        public CompanyModel()
        {
            lstCompanyContacts = new List<CompanyContactsModel>();
        }
        public string Id { get; set; }

        [Required(ErrorMessage = "Please enter Company Name")]
        public string ComapnyName { get; set; }

        [Required(ErrorMessage = "Please enter Address")]
        public string CompanyAddress { get; set; }

        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Please use alphabets only")]
        [Required(ErrorMessage = "Please enter city")]
        public string City { get; set; }

        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Please use alphabets only")]
        [Required(ErrorMessage = "Please enter State")]
        public string State { get; set; }

        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Please use alphabets only")]
        [Required(ErrorMessage = "Please enter country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Please enter zip code")]
        [RegularExpression("^[0-9]{6}$", ErrorMessage = "Please enter valid Zip Code")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Please enter email id")]
        [EmailAddress(ErrorMessage = "Please enter valid email id")]
        public string EmailId { get; set; }


        [Required(ErrorMessage = "Please enter TelePhone no")]
        [RegularExpression(@"\+?\d[\d -]{8,12}\d", ErrorMessage = "Please enter valid TelePhone no.")]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }

        public List<CompanyContactsModel> lstCompanyContacts { get; set; }
    }

    public class CompanyContactsModel
    {
        public string Id { get; set; }

        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Please use alphabets only")]
        [Required(ErrorMessage = "Please enter first name")]
        public string FirstName { get; set; }


        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Please use alphabets only")]
        [Required(ErrorMessage = "Please enter last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter Mobile no")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Please enter valid Mobile no.")]
        [DataType(DataType.PhoneNumber)]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Please enter TelePhone no")]
        [RegularExpression(@"\+?\d[\d -]{8,12}\d", ErrorMessage = "Please enter valid TelePhone no.")]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Please enter Designation")]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Please use alphabets only")]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Please enter email id")]
        [EmailAddress(ErrorMessage = "Please enter valid email id")]
        public string EmailId { get; set; }

        public string SalesCompanyId { get; set; }
    }
}
