using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDL2;
using SDL2.JoeyClasses;

namespace JoeyClassTest
{
    [TestClass]
    public class SDL_WindowTests
    {
        [TestMethod]
        public void Constructor()
        {
            SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);
            var window = new SDL_Window("Window", 100, 100, 100, 100, SDL_Window.Flags.Minimized);
            Assert.IsNotNull(window);
        }

        [TestMethod]
        public void NameGetter()
        {
            SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);
            var window = new SDL_Window("TestingName", 100, 100, 100, 100, SDL_Window.Flags.Minimized);

            var name = window.Name;
            Assert.AreEqual("TestingName", name);
        }

        [TestMethod]
        public void NameSetter()
        {
            SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);
            var window = new SDL_Window("TestingName", 100, 100, 100, 100, SDL_Window.Flags.Minimized);

            window.Name = "New Name!";
            Assert.AreEqual("New Name!", window.Name);
        }

        [TestMethod]
        public void Position()
        {
            SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);
            var window = new SDL_Window("Window", 50, 75, 100, 100, SDL_Window.Flags.Minimized);

            var x = window.PosX;
            var y = window.PosY;

            Assert.AreEqual(x, 50);
            Assert.AreEqual(y, 75);
        }

        [TestMethod]
        public void Size()
        {
            SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);
            var window = new SDL_Window("Window", 100, 100, 50, 75, SDL_Window.Flags.Minimized);

            var w = window.Width;
            var h = window.Height;

            Assert.AreEqual(w, 50);
            Assert.AreEqual(h, 75);
        }

        [TestMethod]
        public void Width()
        {
            SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);
            var window = new SDL_Window("Window", 100, 100, 50, 75, SDL_Window.Flags.Minimized);

            window.Width = 200;

            Assert.AreEqual(200, window.Width);
        }

        [TestMethod]
        public void Height()
        {
            SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);
            var window = new SDL_Window("Window", 100, 100, 50, 75, SDL_Window.Flags.Minimized);

            window.Height = 200;

            Assert.AreEqual(200, window.Height);
        }

        [TestMethod]
        public void MinimumSize()
        {
            SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);
            var window = new SDL_Window("Window", 100, 100, 100, 100, SDL_Window.Flags.Minimized);
            var minx = window.MinX;
            var miny = window.MinY;

            Assert.IsNotNull(minx);
            Assert.IsNotNull(miny);
        }

        [TestMethod]
        public void MaximumSize()
        {
            SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);
            var window = new SDL_Window("Window", 100, 100, 100, 100, SDL_Window.Flags.Shown);
            var maxX = window.MaxX;
            var maxY = window.MaxY;

            Assert.IsNotNull(maxX);
            Assert.IsNotNull(maxY);
        }
    }
}
