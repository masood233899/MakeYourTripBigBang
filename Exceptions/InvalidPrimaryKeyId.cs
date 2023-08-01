namespace MakeYourTrip.Exceptions
{
    public class InvalidPrimaryKeyId: Exception
    {
        private readonly string message;
        public InvalidPrimaryKeyId()
        {
            message = "Primary Key Ids must be empty since it's auto generated";
        }
        public InvalidPrimaryKeyId(string message)
        {
            this.message = message;
        }
        public override string Message
        {
            get { return message; }
        }
    }
}
