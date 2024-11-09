namespace comp584webapi.DTO
{
    public class LoginResponse
    {
        public bool success { get; set; }
        public required string message { get; set; } = null!;
        public required string token { get; set; } = null!;
    }
}
