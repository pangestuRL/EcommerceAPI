namespace EcommerceAPI.Models.DTO
{
    public class UpdateProfileRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Password { get; set; }
    }
}