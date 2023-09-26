using System;

namespace AppHotel.ApplicationService.Exceptions
{
    public class BadRequestApplicationExeption : Exception
    {
        public BadRequestApplicationExeption(String Message) : base(Message)
        {
        }
    }
}
