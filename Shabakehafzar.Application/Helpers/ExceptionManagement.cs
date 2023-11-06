using Shabakehafzar.Application.Exception;

namespace Shabakehafzar.Application.Helpers
{
    public static class ExceptionResponceHandler
    {
        public static void ThrowManagedException(string message)
        {
            throw new ManagedException(message);
        }

        public static void ThrowNotFoundException(string message)
        {
            throw new NotFoundException(message);
        }
    }
}
