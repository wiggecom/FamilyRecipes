namespace FamilyRecipes.Models
{
    public class FamilyUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsAdult { get; set; }
        public string UserImage { get; set; } // Image


        public FamilyUser()
        {
            
        }
    }
}
