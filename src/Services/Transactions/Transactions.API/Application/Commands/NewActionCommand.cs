using MediatR;
using System;
using System.Runtime.Serialization;

namespace ezLoyalty.Services.Actions.API.Application.Commands
{
    [DataContract]
    public class NewActionCommand : IRequest<bool>
    {
        [DataMember]
        public int ActionId { get; set; }

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

        [DataMember]
        public DateTime CreatedAt { get; set; }
        public NewActionCommand()
        {

        }

        public NewActionCommand(int actionId, string token, int accountNo, string userId, string payload, string sender, DateTime createdAt)
        {
            ActionId = actionId;
            UniqueToken = token;
            AccountNo = accountNo;
            UserID = userId;
            Payload = payload;
            Sender = sender;
            CreatedAt = createdAt;
        }
    }
}
