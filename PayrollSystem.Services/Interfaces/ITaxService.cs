namespace PayrollSystem.Services.Interfaces
{
    public interface ITaxService
    {
        decimal GetTaxAmount(decimal totalAmount);
    }
}
