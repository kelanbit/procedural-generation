using System.Drawing;

namespace procedural_generation
{
    class Node
    {
        public Point Location { get; set; }
        public Size Size { get; set; }
        public float Elevation { get; set; }
        public float Moisture { get; set; }
        public Color Biome => GetColor();

        public Node(int posX, int posY, float elevation, float moisture, int width = 1, int height = 1)
        {
            this.Location = new Point(posX, posY);
            this.Size = new Size(width, height);
            this.Elevation = elevation;
            this.Moisture = moisture;
        }

        private Color GetColor()
        {
            if (Elevation < (float)BiomeType.Water)
            {
                return Color.FromArgb(62, 96, 193);
            }
            if (Elevation < (float)BiomeType.WaterLevel)
            {
                return Color.FromArgb(88,121,237);
            }
            if (Elevation < (float)BiomeType.Beach)
            {
                return Color.FromArgb(214, 190, 120);
            }
            if (Elevation < (float)BiomeType.Grassland)
            {
                return Color.FromArgb(116, 169, 99);
            }
            if (Elevation < (float)BiomeType.Forest)
            {
                return Color.FromArgb(62, 126, 99);
            }
            if (Elevation < (float)BiomeType.Tundra)
            {
                return Color.FromArgb(165, 189, 127);
            }
            if (Elevation < (float)BiomeType.HighRock)
            {
                return Color.FromArgb(191,211,176);
            }
            else
            {
                return Color.FromArgb(211, 210, 215);
            }
        }

        private enum BiomeType
        {
            Water=5,
            WaterLevel=7,
            Beach=14,
            Grassland=35,
            Forest=60,
            Tundra=80,
            HighRock=88,
            Snow=95
        }
    }
}
