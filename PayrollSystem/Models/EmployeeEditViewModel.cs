using Microsoft.AspNetCore.Http;
using PayrollSystem.Entity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;

namespace PayrollSystem.Models
{
    public class EmployeeEditViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Employee Number is required")]
        public string EmployeeNo { get; set; }
        [Required(ErrorMessage = "FirstName is required"), StringLength(50, MinimumLength = 2), Display(Name = "First Name")]
        public string FirstName { get; set; }
        [StringLength(50), Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "LastName is required"), StringLength(50, MinimumLength = 2), Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string FullName
        {
            get { return FirstName + (string.IsNullOrEmpty(MiddleName) ? " " : (" " + (char?)MiddleName[0] + ".").ToUpper()) + " " + LastName; }
        }
        public string Gender { get; set; }
        [DataType(DataType.Date), Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }
        [DataType(DataType.Date), Display(Name = "Date joined")]
        public DateTime DateJoined { get; set; } = DateTime.UtcNow;
        [Required(ErrorMessage = "Job Role is required"), StringLength(100)]
        public string Role { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        // first character can be A,B,C,E,G,H,J,K,L,M,N,O,P,R,S,T,W,X,Y,Z; second... , third any number 0-9, last A,B,C,D or space 
        [Required(ErrorMessage = "NationalInsuranceNo is required"), StringLength(50), Display(Name = "NI No"), RegularExpression(@"^[A-CEGHJ-PR-TW-Z]{1}[A-CEGHJ-NPR-TW-Z]{1}[0-9]{6}[A-D\s]$")]
        public string NationalInsuranceNo { get; set; }
        [Display(Name = "Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }
        [Required, StringLength(150)]
        public string Address { get; set; }
        [Required, StringLength(50)]
        public string City { get; set; }
        [Required, StringLength(50)]
        public string PostCode { get; set; }
        [Display(Name = "Photo")]
        public IFormFile ImageURL { get; set; }
    }
}
