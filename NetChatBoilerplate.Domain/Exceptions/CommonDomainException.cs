namespace NetChatBoilerplate.Domain.Exceptions
{
    using System;

    public class CommonDomainException : Exception
    {
        public CommonDomainException()
        { }

        public CommonDomainException(string message)
            : base(message)
        { }

        public CommonDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
