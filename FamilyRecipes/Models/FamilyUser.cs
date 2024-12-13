namespace FamilyRecipes.Models
{
    public class FamilyUser
    {
        public int Id { get; set; }
        public string Name { get; set; } = ""; // should be required property on creation
        public string Password { get; set; } = ""; // optional, can be reset by IsAdmin
        public bool IsLoggedIn { get; set; } = false;
        public bool IsAdmin { get; set; } = false; // first user is IsAdmin by default
        public bool IsAdult { get; set; } = false; // can only be set by IsAdmin
        public string UserImage { get; set; } = "";
        public string UserIcon { get; set; } = "";
        public List<string> FavoriteRecipes { get; set; } = new List<string>();
        public bool PreferMetrical { get; set; } = true; // Default is metrical
        public List<string> Dashboard { get; set; } = new List<string>();

        public FamilyUser()
        {
            
        }

        public static FamilyUser GetFakeAdmin()
        {
            FamilyUser user = new FamilyUser{
                Id = 666,
                Name = "Mr Crowley", 
                Password = "password", 
                IsAdmin = true,
                IsAdult = true,
                UserImage = "AdminUserImage.png", 
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
                IsAdmin = false,
                IsAdult = true,
                UserImage = "UserImage.png",
                PreferMetrical = preferMetrical
            };
            return user;
        }

        public static FamilyUser GetFakeChild(string name)
        {
            Random rnd = new Random();
            FamilyUser user = new FamilyUser
            {
                Id = rnd.Next(100, 1000),
                Name = name,
                Password = "password",
                IsLoggedIn = false,
                IsAdmin = false,
                IsAdult = false,
                UserImage = "ChildImage.png",
            };
            return user;
        }
    }
}
