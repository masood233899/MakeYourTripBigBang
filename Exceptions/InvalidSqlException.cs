namespace MakeYourTrip.Exceptions
{
    public class InvalidSqlException: Exception
    {
        private readonly string message;
        public InvalidSqlException(string message)
        {
            this.message = message;
        }
        public InvalidSqlException()
        {
            message = "Sql Related Error";
        }
        public override string Message
        {
            get { return message; }
        }
    }
}
