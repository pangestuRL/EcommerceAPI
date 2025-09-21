namespace EcommerceAPI.Models.DTO
{
    public class RegisterRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // plain password (nanti di-hash)
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string Role { get; set; } = "customer";
    }
}
