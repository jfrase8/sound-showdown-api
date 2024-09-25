namespace SoundShowdownAPI.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Emails { get; set; } = [];
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
    }
}
