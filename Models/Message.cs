using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace serverSideCapstone.Models
{
    public class Message
    {
        [Key]
        public int MessageId {get; set;}
        public ApplicationUser SendingUser {get; set;}
        public ApplicationUser ReceivingUser {get; set;}

        public string Text {get; set;}
        
        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime TimeMessageSent {get; set;}
    }
}