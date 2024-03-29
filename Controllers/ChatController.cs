using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Task5.Database;
using Task5.Hubs;

namespace Task5.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ChatController : Controller
    {
        private IHubContext<ChatHub> _chat;

        public ChatController(
            IHubContext<ChatHub> chat)
        {
            _chat = chat;
        }

        [HttpPost("[action]/{connectionId}/{roomId}")]
        public async Task<IActionResult> JoinRoom(string connectionId, string roomId)
        {
            await _chat.Groups.AddToGroupAsync(connectionId, roomId);
            return Ok();
        }

        [HttpPost("[action]/{connectionId}/{roomId}")]
        public async Task<IActionResult> LeaveRoom(string connectionId, string roomId,
            [FromServices] AppDbContext ctx)
        {
            await _chat.Groups.RemoveFromGroupAsync(connectionId, roomId);

            ctx.Chats.Remove(ctx.Chats.FirstOrDefault(c => c.Id.ToString() == roomId));
            await ctx.SaveChangesAsync();
            
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SendMessage(
            string message,
            string turnMark,
            int roomId,
            [FromServices] AppDbContext ctx)
        {
            var Message = new Task5.Models.Message {
                ChatId = roomId,
                Text = message,
                turnMark = turnMark,
                Name = User.Identity.Name,
                Timestamp = DateTime.Now                
            };       

            ctx.Messages.Add(Message);
            await ctx.SaveChangesAsync();

            await _chat.Clients.Group(roomId.ToString())
                .SendAsync("RecieveMessage", new {
                    Text = Message.Text,
                    Name = Message.Name,
                    turnMark = Message.turnMark,
                    Timestamp = Message.Timestamp.ToString("dd/MM/yyyy hh:mm:ss")                    
                });
                
            return Ok();
        }            
    }
}