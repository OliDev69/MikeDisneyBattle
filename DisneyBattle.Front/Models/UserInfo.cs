namespace DisneyBattle.Front.Models
{
    public class UserInfo
    {
        public string Id { get; set; }
        public string Pseudo { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public IDictionary<string, string> Claims { get; set; }
    }
}
