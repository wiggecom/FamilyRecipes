namespace FamilyRecipes.Models
{
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMetrical { get; set; }

        public Unit()
        {

        }
        public static List<Unit> GetUnits()
        {
            return new List<Unit>
            {
                new Unit { IsMetrical = true, Name = "Litre(s)"},
                new Unit { IsMetrical = true, Name = "Decilitre(s)"},
                new Unit { IsMetrical = false, Name = "Ounce(s)"},
                new Unit { IsMetrical = false, Name = "Quart(s)"},
                new Unit { IsMetrical = false, Name = "Gallon(s)"}
            };
        }
    }
}
