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
        public List<string> FavoriteRecipes { get; set; } = new List<string>();
        public bool PreferMetrical { get; set; } // Set default for recipes

        //TODO: Add form to add or change users,
        //if FamilyUser.Any() > require password for form
        //else show form

        public FamilyUser()
        {
            
        }

        public static FamilyUser GetFakeAdmin()
        {
            FamilyUser user = new FamilyUser{
                Id = 666,
                Name = "Mr Crowley", 
                Password = "password", 
                IsLoggedIn = true, 
                IsAdmin = true,
                IsAdult = true,
                UserImage = "UserImage", 
                PreferMetrical = true
            };
            return user;
        }

        public static FamilyUser GetFakeUser(string name, bool preferMetrical)
        {
            Random rnd = new Random();
            FamilyUser user = new FamilyUser
            {
                Id = rnd.Next(100, 1000),
                Name = name,
                Password = "password",
                IsLoggedIn = true,
                IsAdmin = true,
                IsAdult = true,
                UserImage = "UserImage",
                PreferMetrical = preferMetrical
            };
            return user;
        }
    }
}
