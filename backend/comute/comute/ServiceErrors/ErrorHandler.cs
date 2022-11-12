using ErrorOr;

namespace comute.ServiceErrors
{
    public static class ErrorHandler
    {
        public static class CarPool
        {
            public static Error NotFound => Error.NotFound(
                code: "CarPool.NotFound",
                description: "Car pool(s) are not found"
            );
        }
        public static class JoinCarPool
        {
            public static Error NotFound => Error.NotFound(
               code: "JoinCarPool.NotFound",
               description: "Joined car pool(s) are not found"
           );
        }
    }
}
