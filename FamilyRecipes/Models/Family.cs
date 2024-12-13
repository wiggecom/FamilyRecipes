namespace FamilyRecipes.Models
{
    public class Family
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = "";
        public string FamilyName { get; set; } = "";
        public bool IsLoggedIn { get; set; } = false; // Supports multiple families, only log out on request
        public List<FamilyUser> Members { get; set; } = new List<FamilyUser>();

        public Family() { }
    }
}
