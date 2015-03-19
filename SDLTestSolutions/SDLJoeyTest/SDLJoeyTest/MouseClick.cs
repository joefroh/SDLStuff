using System;
using SDL2;
using SDL2.JoeyClasses;

namespace SDLJoeyTest
{
    internal class MouseClick : IGameObject
    {
        private int _width;
        private int _height;
        private SDL.SDL_Rect _rect;
        private int mouseOriginX, mouseOriginY, mouseDestX, mouseDestY;
        private bool quit;

        public MouseClick()
        {

            quit = true;
            _width = 20;
            _height = 20;
            Engine.Instance.RegisterGameObject(this);
            Engine.Instance.RegisterEventHandler(handleEvent, SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN);
            Engine.Instance.RegisterEventHandler(handleEvent, SDL.SDL_EventType.SDL_MOUSEBUTTONUP);

        }

        public void Draw(SDL_Renderer renderer, SDL.SDL_Rect destRect)
        {
            //SDL.SDL_RenderClear(renderer);
            renderer.DrawRectangle(_rect, 0xFF, 0xFF, 0x00, 0x00);
        }

        public void Update()
        {
            if (quit == false)
            {
                SDL.SDL_GetMouseState(out mouseDestX, out mouseDestY);
                int highlightX = mouseDestX - mouseOriginX;
                int highlightY = mouseDestY - mouseOriginY;

                _rect = new SDL.SDL_Rect() { x = mouseOriginX, y = mouseOriginY, h = highlightY, w = highlightX };
            }
            else
            {
                _rect = new SDL.SDL_Rect() { x = mouseOriginX, y = mouseOriginY, h = _height, w = _width };
            }
        }
        //Takes left mouse click and adjusts the dots target
        public void handleEvent(SDL.SDL_Event e)
        {
            if (e.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN)
            {
                quit = false;
                //Get mouse position 
                SDL.SDL_GetMouseState(out mouseOriginX, out mouseOriginY);
            }
            if (e.type == SDL.SDL_EventType.SDL_MOUSEBUTTONUP)
            {
                quit = true;
            }

        }
    }
}