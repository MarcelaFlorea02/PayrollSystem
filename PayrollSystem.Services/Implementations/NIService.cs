using PayrollSystem.Services.Interfaces;

namespace PayrollSystem.Services.Implementations
{
    public class NIService : INIService
    {
        private decimal NIRate;
        private decimal NIAmount;
        public decimal GetNIContribution(decimal totalAmount)
        {
            if (totalAmount < 719)
            {
                //lower earning limit rate & below primary threshold
                NIRate = .0m;
                NIAmount = totalAmount * NIRate;
            }
            else if (totalAmount >= 719 && totalAmount <= 4167)
            {
                //between primary threshold and upper earnings limit 
                NIRate = .12m;
                NIAmount = ((totalAmount - 719) * NIRate);
            }
            else if (totalAmount > 4167)
            {
                //above upper earnings limit 
                NIRate = .02m;
                NIAmount = ((4167 - 719) * .12m) + ((totalAmount - 4167) * NIRate);
            }
            return NIAmount;
        }
    }
}

