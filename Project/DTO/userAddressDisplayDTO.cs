namespace EcommerseApplication.DTO
{
    public class userAddressDisplayDTO
    {
        public int Id { get; set; }
        public string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string? telephone { get; set; }
        public string mobile { get; set; }
        public string? arabicAddressLine1 { get; set; }
        public string? arabicAddressLine2 { get; set; }
        public string? arabicCity { get; set; }

        public string? arabicCountry { get; set; }
        public int? UserId { get; set; }
    }
}
