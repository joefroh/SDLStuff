using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using SDL2;
using SDL2.JoeyClasses;

namespace SDLJoeyTest
{
    public interface IGameObject
    {
        void Draw(SDL_Renderer renderer, SDL.SDL_Rect destRect);
        void Update();
    }
}
