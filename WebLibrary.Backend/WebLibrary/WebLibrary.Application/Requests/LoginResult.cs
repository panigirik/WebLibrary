namespace WebLibrary.Application.Requests;

public class LoginResult
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }

    public LoginResult(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}