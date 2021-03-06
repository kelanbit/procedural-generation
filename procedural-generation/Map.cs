﻿using System;
using System.Drawing;
using System.Linq;

namespace procedural_generation
{
    public class Map
    {
        public Size Size { get; set; }
        Node[,] Nodes { get; set; }

        private float maxElevation;
        private float minElevation;


        public Map(int width = 800, int height = 800, int nodeWidth = 1, int nodeHeight = 1)
        {
            this.Size = new Size(width, height);
            this.Nodes = new Node[this.Size.Width, this.Size.Height];

            var rand = new Random();
            var perlin = new Perlin2D(rand.Next(int.MaxValue));
            for (int x = 0; x < Nodes.GetLength(0); x++)
            {
                for (int y = 0; y < Nodes.GetLength(1); y++)
                {
                    var nx = x / (float)width - 0.5f;
                    var ny = y / (float)height - 0.5f;

                    //var noise0 = perlin.Noise(nx, ny, 3, 4f);
                    //var noise1 = 0.5 * perlin.Noise(nx * 2, ny * 2, 3, 4f);
                    //var noise2 = 0.25 * perlin.Noise(nx * 4, ny * 2, 3, 4f);

                    var noise0 = perlin.Noise(nx, ny, 2, 2f);
                    var noise1 = 0.5 * perlin.Noise(nx, ny, 2, 2f);
                    var noise2 = 0.25 * perlin.Noise(nx, ny, 2, 2f);

                    var elevation = noise0 * (float)noise1 * (float)noise2;

                    var moisture = perlin.Noise(nx, ny, 2, 5);


                    Nodes[x, y] = new Node(x, y, elevation, moisture);
                }
            }


            SetNodeElevation();
        }

        public Bitmap Draw()
        {
            var bitmap = new Bitmap(this.Size.Width, this.Size.Height);

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    var node = this.Nodes[x, y];
                    bitmap.SetPixel(x, y, node.Biome);
                }
            }

            return bitmap;
        }

        private void SetNodeElevation(int min = 0, int max = 100)
        {
            var elevations = from Node item in this.Nodes select item.Elevation;
            maxElevation = elevations.Max();
            minElevation = elevations.Min();

            for (int x = 0; x < Nodes.GetLength(0); x++)
            {
                for (int y = 0; y < Nodes.GetLength(1); y++)
                {
                    var node = this.Nodes[x, y];

                    var value = (int)(GetK(max) * node.Elevation + GetB(max));

                    if (value < min) value = min;
                    if (value > max) value = max;

                    node.Elevation = value;
                }
            }
        }

        private float GetB(int max = 255)
        {
            return maxElevation * GetK(max);
        }

        private float GetK(int max = 255)
        {
            return max / (maxElevation - minElevation);
        }

    }
}
