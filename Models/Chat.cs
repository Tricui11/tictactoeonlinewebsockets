using System;
using System.Collections.Generic;

namespace Task5.Models
{
    public class Chat {
        public Chat()
        {
            Messages = new List<Message>();       
            Users = new List<ChatUser>();    
            Tags = new List<Tag>(); 
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public bool isFull { get; set; }
        public ChatType Type { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<ChatUser> Users { get; set; }
    }
}