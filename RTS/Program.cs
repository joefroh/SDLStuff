using System;
using SDL2;

namespace RTS
{
    internal class Program
    {
        //Main loop flag 
        private static bool quitFlag;

        // Initialize function, called at start up
        private static bool init()
        {
            //As long as success stays true, program will run
            bool success = true;
            //Initialize SDL 
            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
            {
                Console.WriteLine("SDL could not initialize! ");
                success = false;
            }
            else
            {
                //Set texture filtering to linear
                SDL.SDL_SetHint(SDL.SDL_HINT_RENDER_SCALE_QUALITY, "1");
                //Create window 
                GlobalSingleton.Instance.gWindow = SDL.SDL_CreateWindow("RTS", SDL.SDL_WINDOWPOS_UNDEFINED,
                    SDL.SDL_WINDOWPOS_UNDEFINED, 640, 480, SDL.SDL_WindowFlags.SDL_WINDOW_OPENGL);
                if (GlobalSingleton.Instance.gWindow == IntPtr.Zero)
                {
                    Console.WriteLine("Window could not be created!");
                    success = false;
                }
                else
                {
                    //Create vsynced renderer for window
                    GlobalSingleton.Instance.gRenderer = SDL.SDL_CreateRenderer(GlobalSingleton.Instance.gWindow, -1,
                        (uint) SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
                        (uint) SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);
                    if (GlobalSingleton.Instance.gRenderer == IntPtr.Zero)
                    {
                        Console.WriteLine("Renderer could not be created! SDL Error:");
                        success = false;
                    }
                }
            }
            return success;
        }

        // Function for loading all assets, run once at the beginning
        private static bool loadMedia()
        {
            //Loading success flag
            bool success = true;

            //Load dot texture
            if (!GlobalSingleton.Instance.gDotTexture.loadFromFile(@"Assets\Images\dot.bmp"))
            {
                Console.WriteLine("Failed to load dot texture!");
                success = false;
            }

            return success;
        }

        // Clean up function
        private static void close()
        {
            //Free loaded images
            GlobalSingleton.Instance.gDotTexture.free();

            //Destroy window	
            SDL.SDL_DestroyRenderer(GlobalSingleton.Instance.gRenderer);
            SDL.SDL_DestroyWindow(GlobalSingleton.Instance.gWindow);
            GlobalSingleton.Instance.gWindow = IntPtr.Zero;
            GlobalSingleton.Instance.gRenderer = IntPtr.Zero;

            //Quit SDL subsystems
            SDL_image.IMG_Quit();
            SDL.SDL_Quit();
        }

        private static void Quit(SDL.SDL_Event e)
        {
            quitFlag = true;
        }

        private static void Main()
        {
            var eventloop = new EventHandler();
            eventloop.RegisterEventMethod(Quit, SDL.SDL_EventType.SDL_QUIT);
            if (!init() & !loadMedia())
            {
                Console.WriteLine("Failed to initialize or load Media!");
            }
            else
            {
                //Event handler 
                SDL.SDL_Event e;
                //The dot that will be moving around on the screen
                var dot = new Dot(eventloop);

                //Main game loop
                while (!quitFlag)
                {
                    //Handle events on queue
                   eventloop.PollNewEvents();
                    //Move the dot
                    dot.move();

                    //Clear screen
                    SDL.SDL_SetRenderDrawColor(GlobalSingleton.Instance.gRenderer, 50, 200, 255, 255);
                    SDL.SDL_RenderClear(GlobalSingleton.Instance.gRenderer);

                    //Render red filled quad
                    var fillRect = new SDL.SDL_Rect {x = 640/4, y = 480/4, w = 640/2, h = 480/2};
                    SDL.SDL_SetRenderDrawColor(GlobalSingleton.Instance.gRenderer, 0xFF, 0x00, 0x00, 0xFF);
                    SDL.SDL_RenderFillRect(GlobalSingleton.Instance.gRenderer, ref fillRect);


                    //Draw blue horizontal line
                    SDL.SDL_SetRenderDrawColor(GlobalSingleton.Instance.gRenderer, 0x00, 0x00, 0xFF, 0xFF);
                    SDL.SDL_RenderDrawLine(GlobalSingleton.Instance.gRenderer, 0, 480/2, 640, 480/2);

                    //Render objects
                    dot.render();

                    //Update screen
                    SDL.SDL_RenderPresent(GlobalSingleton.Instance.gRenderer);
                }
            }
            // Clean up
            close();
        }
    }
}