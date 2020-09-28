using Infrastructure.Interfaces;

namespace WebApi
{
    public class CurrentUserService : ICurrentUserService
    {
        public string Email => "test@test.test";
    }
}