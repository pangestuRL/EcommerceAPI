namespace EcommerceAPI.Models.DTO
{
    public class UserProfileResponse
    {
        public int UserID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string Role { get; set; } = "customer";
        public bool IsActive { get; set; }
    }
}
