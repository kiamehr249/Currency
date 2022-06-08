namespace Currency.Web.Models
{
    public class ConversionRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public double Amount { get; set; }
        public bool HasError { get; set; }
    }
}
