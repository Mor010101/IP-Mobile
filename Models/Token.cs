namespace Mobile_IP.Models;

public class TokenClass
{
    private string token;
    private string validity;
    private string refreshToken;
    private string guidId;
    private string userId;
    private string userEmail;
    private string expiredTime;

    public string Token { get => token; set => token = value; } 
    public string Validity { get => validity; set => validity = value; }
    public string RefreshToken { get => refreshToken; set => refreshToken = value; }
    public string GuidId { get => guidId; set => guidId = value; }
    public string UserId { get => userId; set => userId = value; }
    public string UserEmail { get => userEmail; set => userEmail = value; }
    public string ExpiredTime { get => expiredTime; set => expiredTime = value; }
}
