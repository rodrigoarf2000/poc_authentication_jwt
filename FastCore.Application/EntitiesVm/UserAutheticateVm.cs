using System;

namespace FastCore.Application.EntitiesVm
{
    public class UserAutheticateVm
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
        public DateTime ExpiresIn { get; set; } = DateTime.Now;
    }
}