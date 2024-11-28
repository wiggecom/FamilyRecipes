namespace FamilyRecipes.Models
{
    public class FamilyUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; } // can be reset by IsAdmin
        public bool IsLoggedIn { get; set; }
        public bool IsAdmin { get; set; } // first user is IsAdmin by default
        public bool IsAdult { get; set; } // can only be set by IsAdmin
        public string UserImage { get; set; } // Image

        //TODO: Add form to add or change users,
        //if FamilyUser.Any() > require password for form
        //else show form

        public FamilyUser()
        {
            
        }
    }
}
