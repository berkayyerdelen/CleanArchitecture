namespace Core.Domains.Product.Queries.GetAggregateResults
{
    public class GetAggregateResultsLookUpModel
    {
        public string CategoryName { get; set; }
        public decimal SumPrice { get; set; }
        public decimal AvaragePrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}