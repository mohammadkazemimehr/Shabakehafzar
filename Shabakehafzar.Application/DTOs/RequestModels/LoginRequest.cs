using FluentValidation;

namespace Shabakehafzar.Application.DTOs.RequestModels
{
    public class LoginRequest
    {
        public string Password { get; set; }
        public string UserName { get; set; }
    }

    public class LoginRequestValidation : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidation()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("نام کاربری الزامی است.")
                .Length(0, 50)
                .WithMessage("نام کاربری باید کمتر از 50 حرف باشد.");

            RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("رمز عبور الزامی است.");
        }
    }
}
