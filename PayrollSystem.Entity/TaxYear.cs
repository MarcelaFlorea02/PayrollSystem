using System.ComponentModel.DataAnnotations;

namespace PayrollSystem.Entity
{
    public class TaxYear
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string YearOfTax { get; set; }
    }

}
