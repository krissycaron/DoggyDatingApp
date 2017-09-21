using System.Collections.Generic;

namespace serverSideCapstone.Models.ViewModels
{
    public class MessageIndexViewModel
    {
        public List<Message> ReceivedMessages {get; set;} = new List<Message>();
        public List<Message> SentMessages {get; set;} = new List<Message>();
    }
}