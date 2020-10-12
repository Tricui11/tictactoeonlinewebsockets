using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Task5.Models
{
    public class User : IdentityUser
    {
        public ICollection<ChatUser> Chats { get; set; }        
    }
}