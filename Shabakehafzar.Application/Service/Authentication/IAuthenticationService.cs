namespace Shabakehafzar.Application.Service.Authentication
{
    public interface IAuthenticationservice
    {
        Task<UserTokenResponseDto> Login(LoginRequest loginRequestDto);
    }
}
