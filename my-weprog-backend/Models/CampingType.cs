namespace my_weprog_backend.Models
{
    public enum CampingType
    {
        VILLA, 
        VANS,
        TENT, 
        CABIN,
        YURT 
    }

    public class EnumPrices
    {
        private readonly Dictionary<CampingType, decimal> CampingPrice = new Dictionary<CampingType, decimal>
        {
            { CampingType.VANS, 100.0m },
            { CampingType.TENT, 60.0m },
            { CampingType.VILLA, 150.0m },
            { CampingType.CABIN, 120.0m }, 
            { CampingType.YURT, 80.0m } 
        };

        public decimal GetPriceForCampingType(CampingType campingType)
        {
            if (CampingPrice.ContainsKey(campingType))
            {
                return CampingPrice[campingType];
            }
            throw new KeyNotFoundException("Price not found for camping type: " + campingType.ToString());
        }
    }
}

