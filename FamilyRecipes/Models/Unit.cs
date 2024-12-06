namespace FamilyRecipes.Models
{
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Short { get; set; }
        public bool IsMetrical { get; set; }
        public bool IsVolume { get; set; }
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
                //new Unit { Name = "Number",       Short = "pc",    IsMetrical = true,  IsVolume = false, InMl=1.0f},
                new Unit { Name = "Litre",        Short = "l",     IsMetrical = true,  IsVolume = true,  InMl=1000.0f},
                new Unit { Name = "Decilitre",    Short = "dl",    IsMetrical = true,  IsVolume = true,  InMl=100.0f},
                new Unit { Name = "Centilitre",   Short = "cl",    IsMetrical = true,  IsVolume = true,  InMl=10.0f},
                new Unit { Name = "Millilitre",   Short = "ml",    IsMetrical = true,  IsVolume = true,  InMl=1.0f},
                new Unit { Name = "Tablespoon",   Short = "tbsp",  IsMetrical = true,  IsVolume = true,  InMl=15.0f},
                new Unit { Name = "Teaspoon",     Short = "tsp",   IsMetrical = true,  IsVolume = true,  InMl=5.0f},
                new Unit { Name = "Cup (Tea)",    Short = "tcp",   IsMetrical = true,  IsVolume = true,  InMl=250.0f},
                new Unit { Name = "Cup (Coffee)", Short = "ccp",   IsMetrical = true,  IsVolume = true,  InMl=150.0f},
                new Unit { Name = "Drop",         Short = "dr",    IsMetrical = true,  IsVolume = true,  InMl=0.05f},
                new Unit { Name = "Smidgen",      Short = "smi",   IsMetrical = false, IsVolume = true,  InMl=0.12f},
                new Unit { Name = "Pinch",        Short = "pn",    IsMetrical = false, IsVolume = true,  InMl=0.23f},
                new Unit { Name = "Fluid Ounce",  Short = "fl oz", IsMetrical = false, IsVolume = true,  InMl=29.7f},
                new Unit { Name = "Cup",          Short = "cup",   IsMetrical = false, IsVolume = true,  InMl=236.59f},
                new Unit { Name = "Quart",        Short = "qt",     IsMetrical = false, IsVolume = true,  InMl=473.18f},
                new Unit { Name = "Pint",         Short = "pt",     IsMetrical = false, IsVolume = true,  InMl=946.35f},
                new Unit { Name = "Gallon",       Short = "gal",     IsMetrical = false, IsVolume = true,  InMl=3785.41f},
                // Weight measures
                new Unit { Name = "Kilogram",     Short = "kg",    IsMetrical = true,  IsVolume = false, InGr=1000.0f},
                new Unit { Name = "Hectogram",    Short = "hg",    IsMetrical = true,  IsVolume = false, InGr=100.0f},
                new Unit { Name = "Gram",         Short = "g",     IsMetrical = true,  IsVolume = false, InGr=1.0f},
                new Unit { Name = "Milligram",    Short = "g",     IsMetrical = true,  IsVolume = false, InGr=0.001f},
                new Unit { Name = "Ounce",        Short = "oz",    IsMetrical = false, IsVolume = false, InGr=28.35f},
                new Unit { Name = "Pound",        Short = "lb",    IsMetrical = false, IsVolume = false, InGr=453.6f},

            };
        }
    }
}
