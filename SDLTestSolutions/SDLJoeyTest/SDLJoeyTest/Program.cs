using System;
using SDL2;

namespace SDLJoeyTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Engine ourEngine = Engine.Instance;
            ourEngine.Run();
        }
    }
}