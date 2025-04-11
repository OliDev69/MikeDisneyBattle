namespace DisneyBattle.Front.Models
{
    public class AuthResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Access_Token { get; set; }
        public string Refresh_Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public UserInfo User { get; set; }
    }
}
