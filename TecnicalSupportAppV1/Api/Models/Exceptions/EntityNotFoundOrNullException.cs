using System.Transactions;

namespace TecnicalSupportAppV1.Api.Models.Exceptions
{
    public class EntityNotFoundOrNullException : Exception
    {
        public new string Message;

        public EntityNotFoundOrNullException(long id)
        {
            string entityNameMessage = "Entity";
            string message = entityNameMessage + " not found or invalid. ";
            message += id != null ? "Id Number: " + id.ToString() : "";
            Message = message;
        }
    }
}
