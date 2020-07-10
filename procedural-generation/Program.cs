using System.Diagnostics;

namespace procedural_generation
{
    class Program
    {
        static void Main(string[] args)
        {
            var map = new Map();
            var bitmap = map.Draw();
            var path = @"map.bmp";
            bitmap.Save(path);
            Process.Start("cmd", @"/c " + path);
        }
    }
}
