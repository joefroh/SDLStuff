using SDL2;
using System;

namespace RTS
{
    public class Dot
    {
        //The dimensions of the dot
        public const int DOT_WIDTH = 20;
        public const int DOT_HEIGHT = 20;

        //Maximum axis velocity of the dot
        public const int DOT_VEL = 10;

        //Initializes the variables

        //The X and Y offsets of the dot
        private int mPosX, mPosY;

        //The velocity of the dot
        private int mVelX, mVelY;

        //The target of the dot
        private int targetX, targetY;

        public Dot(EventHandler handler)
        {
            //Initialize the offsets
            mPosX = 0;
            mPosY = 0;

            //joey - register to the event handler... this should be refactored, simple hack to demonstrate the concept.
            handler.RegisterEventMethod(this.handleEvent,SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN);
        }

        //Takes key presses and adjusts the dot's velocity
        public void handleEvent(SDL.SDL_Event e)
        {
            //If a key was pressed
            if (e.type.ToString().Contains("SDL_KEYDOWN") && e.key.repeat == 0)
            {
                //Adjust the velocity
                switch (e.key.keysym.sym)
                {
                    case SDL.SDL_Keycode.SDLK_UP:
                        //up arrow
                        break;
                    case SDL.SDL_Keycode.SDLK_DOWN:
                        break;
                    case SDL.SDL_Keycode.SDLK_LEFT:
                        break;
                    case SDL.SDL_Keycode.SDLK_RIGHT:
                        break;
                }
            }
            //If a key was released
            else if (e.type.ToString().Contains("SDLK_UP") && e.key.repeat == 0)
            {
                //Adjust the velocity
                switch (e.key.keysym.sym)
                {
                    case SDL.SDL_Keycode.SDLK_UP:
                        //up arrow
                        break;
                    case SDL.SDL_Keycode.SDLK_DOWN:
                        break;
                    case SDL.SDL_Keycode.SDLK_LEFT:
                        break;
                    case SDL.SDL_Keycode.SDLK_RIGHT:
                        break;
                }
            }
            //If mouse event happened 
            else if( e.type.ToString().Contains("SDL_MOUSEBUTTONDOWN")) { 
                    //Get mouse position 
                    SDL.SDL_GetMouseState( out targetX, out targetY );
                    //Render red filled quad
                    var mouseClickRect = new SDL.SDL_Rect { x = 640 / 4, y = 480 / 4, w = 20, h = 20 };
                    SDL.SDL_SetRenderDrawColor(GlobalSingleton.Instance.gRenderer, 255, 255, 0, 255);
                    SDL.SDL_RenderFillRect(GlobalSingleton.Instance.gRenderer, ref mouseClickRect);
                }
        }

        //Moves the dot
        public void move()
        {
            //Store the mouse state
            //Console.WriteLine("Mouse at: " + targetX + " -- " + targetY);
            //Console.WriteLine("Dot at: " + mPosX + " -- " + mPosY);
            //Since we're using floats, sometimes the dot location and mouse position are off by 1 pixel
            // due to rounding. So this just checks if the object is within 3 pixels of it's destination
            if (Math.Abs(targetX - mPosX) > 3 & Math.Abs(targetY - mPosY) > 3)
            {
                //Calculate the distance from the square to the mouse's X and Y position
                float XDistance = mPosX - targetX;
                float YDistance = mPosY - targetY;

                //Calculate the required rotation by doing a two-variable arc-tan
                float rotation = (float)Math.Atan2(YDistance, XDistance);

                //Move square towards mouse by closing the gap 3 pixels per update
                mPosX -= (int)(3 * Math.Cos(rotation));
                mPosY -= (int)(3 * Math.Sin(rotation));
            }
            
        }

        //Shows the dot on the screen
        public void render()
        {
            // destination is a rectangle defining the render target to which you want to draw
            SDL.SDL_Rect destinationRectangle = new SDL.SDL_Rect() { x = 0, y = 0, w = 640, h = 480 };
            //Show the dot
            GlobalSingleton.Instance.gDotTexture.render(mPosX, mPosY, destinationRectangle );

        }
    }
}