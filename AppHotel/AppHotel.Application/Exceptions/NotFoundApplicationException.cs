using System;

namespace AppHotel.ApplicationService.Exceptions
{
    public class NotFoundApplicationException : Exception
    {
        public NotFoundApplicationException(String Message) : base(Message)
        {
        }
    }
}
