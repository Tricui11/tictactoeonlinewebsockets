using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task5.Database;
using Task5.Models;

namespace Task5.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private AppDbContext _ctx;
        public HomeController(AppDbContext ctx) => _ctx = ctx;
        public IActionResult Index()
        {
            var chats = _ctx.Chats
                .Include(x => x.Users)
                .Where(x => !x.Users
                    .Any(y => y.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value))
                    .ToList();

            return View(chats);
        }
        [Route("666")]
        public IActionResult IndexRouted()
        {
            var chats = _ctx.Chats
                .Include(x => x.Users)
                .Where(x => !x.Users
                    .Any(y => y.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value))
                    .ToList();

            return RedirectToAction("Index");
        }

        [HttpGet("{id}")]
        public IActionResult Chat(int id)
        {
            var chat = _ctx.Chats
                .Include(x => x.Messages)
                .Include(x => x.Users)
                .FirstOrDefault(x => x.Id == id);
            return View(chat);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(
            int chatId,
            string message,
            string result,
            string turnMark)
        {
            var Message = new Message
            {
                ChatId = chatId,
                Text = message,
                Name = User.Identity.Name,
                Result = result,
                turnMark = turnMark,
                Timestamp = DateTime.Now
            };

            _ctx.Messages.Add(Message);

 //           _ctx.Chats.FirstOrDefault(c => c.Id == chatId).Type = ChatType.Private;

            await _ctx.SaveChangesAsync();

            return RedirectToAction("Chat", new { id = chatId });
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(string name)
        {
            var chat = new Chat
            {
                Name = name,
                Type = ChatType.Room
            };

            chat.Users.Add(new ChatUser{
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                Role = UserRole.Creater
            });

            _ctx.Chats.Add(chat);

            await _ctx.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public async Task<IActionResult> JoinRoom(int id)
        {
            var chatUser = new ChatUser
            {
                ChatId = id,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                Role = UserRole.Joiner
            };

            _ctx.ChatUsers.Add(chatUser);

            await _ctx.SaveChangesAsync();
            
            return RedirectToAction("Chat", "Home", new { id = id });
        }
    }
}