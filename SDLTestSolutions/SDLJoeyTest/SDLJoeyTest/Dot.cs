using System;
using SDL2;
using SDL2.JoeyClasses;

namespace SDLJoeyTest
{
    internal class Dot : IGameObject
    {
        private IntPtr texture;
        private int _x;
        private int _y;
        private int _targetX;
        private int _targetY;
        private int _width;
        private int _height;
        private SDL.SDL_Rect _rect;


        public Dot()
        {
            texture = AssetManager.LoadTextureFromFile(@"Assets\Textures\stormtrooper.png");
            _x = _targetX = 600 / 2;
            _y = _targetY = 400 / 2;

            _width = 23;
            _height = 42;
            _rect = new SDL.SDL_Rect() { x = _x, y = _y, h = _height, w = _width };
            Engine.Instance.RegisterGameObject(this);
            Engine.Instance.RegisterEventHandler(handleEvent, SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN);
        }

        public void Draw(SDL_Renderer renderer, SDL.SDL_Rect destRect)
        {
            renderer.Clear();
            renderer.RenderCopy(texture, destRect, _rect);
        }

        public void Update()
        {
            move();
        }
        //Takes left mouse click and adjusts the dots target
        public void handleEvent(SDL.SDL_Event e)
        {
            //Get mouse position 
            SDL.SDL_GetMouseState(out _targetX, out _targetY);
        }

        //Moves the dot
        public void move()
        {
            //Store the mouse state
            //Since we're using floats, sometimes the dot location and mouse position are off by 1 pixel
            // due to rounding. So this just checks if the object is within 3 pixels of it's destination
            if (Math.Abs(_targetX - _x) > 3 & Math.Abs(_targetY - _y) > 3)
            {
                //Calculate the distance from the square to the mouse's X and Y position
                float XDistance = _x - _targetX;
                float YDistance = _y - _targetY;

                //Calculate the required rotation by doing a two-variable arc-tan
                float rotation = (float)Math.Atan2(YDistance, XDistance);

                //Move square towards mouse by closing the gap 3 pixels per update
                _x -= (int)(3 * Math.Cos(rotation));
                _y -= (int)(3 * Math.Sin(rotation));
                _rect = new SDL.SDL_Rect() { x = _x, y = _y, h = _height, w = _width };

            }

        }

    }
}