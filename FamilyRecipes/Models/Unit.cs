namespace FamilyRecipes.Models
{
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Short { get; set; }
        public bool IsMetrical { get; set; }
        public bool IsFluid { get; set; }
        public float InMl { get; set; }
        public float InGr { get; set; }

        public Unit()
        {

        }
        public static List<Unit> GetUnits()
        {
            return new List<Unit>
            {
                // Fluid/Volume measures
                new Unit { Name = "Units",        Short = "pc",    IsMetrical = true,  IsFluid = false, InMl=1.0f},
                new Unit { Name = "Litre",        Short = "l",     IsMetrical = true,  IsFluid = true,  InMl=1000.0f},
                new Unit { Name = "Decilitre",    Short = "dl",    IsMetrical = true,  IsFluid = true,  InMl=100.0f},
                new Unit { Name = "Centilitre",   Short = "cl",    IsMetrical = true,  IsFluid = true,  InMl=10.0f},
                new Unit { Name = "Millilitre",   Short = "ml",    IsMetrical = true,  IsFluid = true,  InMl=1.0f},
                new Unit { Name = "Tablespoon",   Short = "tbsp",  IsMetrical = true,  IsFluid = true,  InMl=15.0f},
                new Unit { Name = "Teaspoon",     Short = "tsp",   IsMetrical = true,  IsFluid = true,  InMl=5.0f},
                new Unit { Name = "Drop",         Short = "dr",    IsMetrical = true,  IsFluid = true,  InMl=0.05f},
                new Unit { Name = "Smidgen",      Short = "smi",   IsMetrical = false, IsFluid = true,  InMl=0.12f},
                new Unit { Name = "Pinch",        Short = "pn",    IsMetrical = false, IsFluid = true,  InMl=0.23f},
                new Unit { Name = "Fluid Ounce",  Short = "fl oz", IsMetrical = false, IsFluid = true,  InMl=29.7f},
                new Unit { Name = "Cup",          Short = "cup",   IsMetrical = false, IsFluid = true,  InMl=29.7f},
                new Unit { Name = "Quart",        Short = "l",     IsMetrical = false, IsFluid = true,  InMl=1000.0f},
                new Unit { Name = "Gallon",       Short = "l",     IsMetrical = false, IsFluid = true,  InMl=1000.0f},
                // Weight measures
                new Unit { Name = "Kilogram",     Short = "kg",    IsMetrical = true,  IsFluid = false, InGr=1000.0f},
                new Unit { Name = "Hectogram",    Short = "hg",    IsMetrical = true,  IsFluid = false, InGr=100.0f},
                new Unit { Name = "Gram",         Short = "g",    IsMetrical = true,  IsFluid = false, InGr=1.0f},
                new Unit { Name = "Weight Ounce", Short = "wt oz", IsMetrical = false, IsFluid = false, InGr=1000.0f}

            };
        }
    }
}
