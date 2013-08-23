namespace PCSComProduction.DCP.BO
{
    /// <summary>
    /// Represent class of Product information for running DCP
    /// </summary>
    internal class ProductInfo : BaseInfo
    {
        public decimal SafetyStock { get; set; }
        public int? CategoryId { get; set; }
        public int GroupPriority { get; set; }
        public bool HasParent { get; set; }
    }
}