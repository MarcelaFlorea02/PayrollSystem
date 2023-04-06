using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollSystem.Entity
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string EmployeeNo { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string MiddleName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(200)]
        public string FullName { get; set; }
        [MaxLength(50)]
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateJoined { get; set; }
        [MaxLength(100)]
        public string Role { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [Required, MaxLength(50)]
        public string NationalInsuranceNo { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        [Required, MaxLength(150)]
        public string Address { get; set; }
        [Required, MaxLength(50)]
        public string City { get; set; }
        [Required, MaxLength(50)]
        public string PostCode { get; set; }
        public IEnumerable<Payment> PaymentRecords { get; set; }
    }
}
