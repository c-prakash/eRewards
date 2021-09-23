using MediatR;
using System.Runtime.Serialization;

namespace eRewards.Services.Transactions.API.Application.Commands
{
    [DataContract]
    public class NewActionCommand : IRequest<bool>
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string UniqueToken { get; set; }

        [DataMember]
        public int AccountNo { get; set; }

        [DataMember]
        public string UserID { get; set; }

        [DataMember]
        public string Payload { get; set; }

        [DataMember]
        public string Sender { get; set; }

        public NewActionCommand()
        {

        }
        public NewActionCommand(string actionName, string token, int accountNo, string userId, string payload, string sender)
        {
            Name = actionName;
            UniqueToken = token;
            AccountNo = accountNo;
            UserID = userId;
            Payload = payload;
            Sender = sender;
        }
    }
}
