namespace comp584webapi.DTO
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; } = null!;
    }
}
