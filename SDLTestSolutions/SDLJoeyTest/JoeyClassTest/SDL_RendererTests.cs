using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDL2.JoeyClasses;

namespace JoeyClassTest
{
    [TestClass]
    public class SDL_RendererTests
    {
        [TestMethod]
        public void Constructor()
        {
            var window = new SDL_Window("Window", 100, 100, 100, 100, SDL_Window.Flags.Minimized);
            var renderer = new SDL_Renderer(window, -1, SDL_Renderer.Flags.Accelerated);
            Assert.IsNotNull(renderer);
        }
    }
}