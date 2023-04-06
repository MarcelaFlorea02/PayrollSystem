using PayrollSystem.Services.Interfaces;

namespace PayrollSystem.Services.Implementations
{
    public class TaxService : ITaxService
    {
        private decimal taxRate;
        private decimal tax;
        public decimal GetTaxAmount(decimal totalAmount)
        {
            if (totalAmount <= 1042)
            {
                //Tax Free Rate
                taxRate = .0m;
                tax = totalAmount * taxRate;
            }
            else if (totalAmount > 1042 && totalAmount <= 3125)
            {
                //basic tax rate
                taxRate = .20m;
                tax = (1042 * .0m) + ((totalAmount - 1042) * taxRate);
            }
            else if (totalAmount > 3125 && totalAmount <= 12500)
            {
                //higher tax rate
                taxRate = .40m;
                tax = (1042 * .0m) + ((3125 - 1042) * .20m) + ((totalAmount - 3125) * taxRate);
            }
            else if (totalAmount > 12500)
            {
                //additional tax rate 
                taxRate = .45m;
                tax = (1042 * .0m) + ((3125 - 1042) * .20m) + ((12500 - 3125) * .40m) + ((totalAmount - 12500) * taxRate);
            }
            return tax;
        }
    }
}

