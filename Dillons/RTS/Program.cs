using System;
using System.Threading.Tasks;
using SDL2;

namespace RTS
{
    //Texture wrapper class
    partial class LTexture
    {
        //Initializes variables
        public LTexture() { }

        //Loads image at specified path
        public extern bool loadFromFile(string path);

        //Creates image from font string
        public extern bool loadFromRenderedText(string textureText, SDL.SDL_Color textColor);

        //Deallocates texture
        public extern void free();

        //Set color modulation
        public extern void setColor(uint red, uint green, uint blue);

        //Set blending
        public extern void setBlendMode(SDL.SDL_BlendMode blending);

        //Set alpha modulation
        public extern void setAlpha(uint alpha);

        //Renders texture at given point
        public extern void render(int x, int y, SDL.SDL_Rect clip, double angle, SDL.SDL_Point center, SDL.SDL_RendererFlip flip = SDL.SDL_RendererFlip.SDL_FLIP_NONE);

        //Gets image dimensions
        public extern int getWidth();
        public extern int getHeight();

        //The actual hardware texture
        private IntPtr mTexture;

        //Image dimensions
        private int mWidth;
        private int mHeight;
    }

    //The application time based timer
    partial class LTimer
    {
        //Initializes variables
        public extern LTimer();

        //The various clock actions
        public extern void start();
        public extern void stop();
        public extern void pause();
        public extern void unpause();

        //Gets the timer's time
        public extern uint getTicks();

        //Checks the status of the timer
        public extern bool isStarted();
        public extern bool isPaused();

        //The clock time when the timer started
        private UInt32 mStartTicks;

        //The ticks stored when the timer was paused
        private UInt32 mPausedTicks;

        //The timer status
        private bool mPaused;
        private bool mStarted;
    }
    //The dot that will move around on the screen
    partial class Dot
    {
        //The dimensions of the dot
        public const int DOT_WIDTH = 20;
        public const int DOT_HEIGHT = 20;
        
        //Maximum axis velocity of the dot
        public const int DOT_VEL = 10;

        //Initializes the variables
        public extern Dot();

        //Takes key presses and adjusts the dot's velocity
        public extern void handleEvent(SDL.SDL_Event e);

        //Moves the dot
        public extern void move();

        //Shows the dot on the screen
        public extern void render();

        //The X and Y offsets of the dot
        private int mPosX, mPosY;

        //The velocity of the dot
        private int mVelX, mVelY;
    }

    class Program
    {
        //Initialize
        IntPtr mTexture;
        int mWidth = 0;
        int mHeight = 0;
        //Initialize the offsets
        int mPosX = 0;
        int mPosY = 0;

        //Initialize the velocity
        int mVelX = 0;
        int mVelY = 0;

        //The dimensions of the dot
        //THESE ARE SUPPOSED TO BE IN THE DOT CLASS ABOVE - Dillon
        public const int DOT_WIDTH = 20;
        public const int DOT_HEIGHT = 20;

        //Maximum axis velocity of the dot
        public const int DOT_VEL = 10;
        
        //Maybe these three call are useless -Dillon
        //Starts up SDL and creates window
        //bool init();

        //Loads media
        //bool loadMedia();

        //Frees media and shuts down SDL
        //void close();

        //The window we'll be rendering to
        IntPtr gWindow;

        //The window renderer
        IntPtr gRenderer;

        //Scene textures
        LTexture gDotTexture;

        void LTexture.LTexture()
        {
            //initialize should go here
        }

        bool LTexture.loadFromFile(string path)
        {
            //The final texture
            IntPtr newTexture;

            //Load image at specified path
            IntPtr loadedSurface = SDL_image.IMG_Load(path);
            if (loadedSurface == IntPtr.Zero)
            {
                Console.WriteLine("Unable to load image! SDL_image Error " + path);
            }
            else
            {
                //Color key image
                SDL.SDL_SetColorKey(loadedSurface, (int)SDL.SDL_bool.SDL_TRUE, SDL.SDL_MapRGB(loadedSurface, 0, 0xFF, 0xFF));

                //Create texture from surface pixels
                newTexture = SDL.SDL_CreateTextureFromSurface(gRenderer, loadedSurface);
                if (newTexture == IntPtr.Zero)
                {
                    Console.WriteLine("Unable to create texture! SDL Error:");
                }
                else
                {
                    //Get image dimensions
                    mWidth = 10;
                    mHeight = 10;
                }

                //Get rid of old loaded surface
                SDL.SDL_FreeSurface(loadedSurface);
            }
            return true;
        }
        bool LTexture.loadFromRenderedText()
        {
            //Get rid of preexisting texture
            //LTexture.free();

            //Render text surface
            //CAN'T GET THIS FUNCTION TO WORK
            IntPtr textSurface = SDL_ttf.TTF_RenderText_Solid(SDL_ttf.  gFont, SDL_ttf.textureText, SDL_ttf.textColor);
            if (textSurface != IntPtr.Zero)
            {
                //Create texture from surface pixels
                mTexture = SDL.SDL_CreateTextureFromSurface(gRenderer, textSurface);
                if (mTexture == IntPtr.Zero)
                {
                    Console.WriteLine("Unable to create texture from rendered text! SDL Error");
                }
                else
                {
                    //Get image dimensions
                    mWidth = textSurface->w;
                    mHeight = textSurface->h;
                }

                //Get rid of old surface
                SDL.SDL_FreeSurface(textSurface);
            }
            else
            {
                Console.WriteLine("Unable to render text surface! SDL_ttf Error: ");
            }


            //Return success
            return mTexture != IntPtr.Zero;
        }
        void LTexture.free()
        {
            //Free texture if it exists
            if (mTexture != IntPtr.Zero)
            {
                SDL.SDL_DestroyTexture(mTexture);
                mTexture = IntPtr.Zero;
                mWidth = 0;
                mHeight = 0;
            }
        }
        void LTexture.setColor(byte red, byte green, byte blue)
        {
            //Modulate texture rgb
            SDL.SDL_SetTextureColorMod(mTexture, red, green, blue);
        }

        void setBlendMode(SDL.SDL_BlendMode blending)
        {
            //Set blending function
            SDL.SDL_SetTextureBlendMode(mTexture, blending);
        }
        void setAlpha(byte alpha)
        {
            //Modulate texture alpha
            SDL.SDL_SetTextureAlphaMod(mTexture, alpha);
        }
        void render(int x, int y, SDL.SDL_Rect clip, double angle, SDL.SDL_Point center, SDL.SDL_RendererFlip flip)
        {
            //Set rendering space and render to screen
            SDL.SDL_Rect renderQuad = { x, y, mWidth, mHeight };

            //Set clip rendering dimensions
            if (clip !=  0 )
            {
                renderQuad.w = clip->w;
                renderQuad.h = clip->h;
            }

            //Render to screen
            SDL.SDL_RenderCopyEx(gRenderer, mTexture, ref clip, ref renderQuad, angle, ref center, flip);
        }

        int LTexture.getWidth()
        {
	        return mWidth;
        }

        int LTexture.getHeight()
        {
	        return mHeight;
        }

        void Dot.Dot()
        {
            //Initialize the offsets
            mPosX = 0;
            mPosY = 0;

            //Initialize the velocity
            mVelX = 0;
            mVelY = 0;
        }

        void Dot.handleEvent( SDL.SDL_Event e )
        {
            //If a key was pressed
	        if( e.type.ToString().Contains("SDL_KEYDOWN") && e.key.repeat == 0 )
            {
                //Adjust the velocity
                switch( e.key.keysym.sym )
                {
                    case SDL.SDL_Keycode.SDLK_UP: mVelY -= DOT_VEL; break;
                    case SDL.SDL_Keycode.SDLK_DOWN: mVelY += DOT_VEL; break;
                    case SDL.SDL_Keycode.SDLK_LEFT: mVelX -= DOT_VEL; break;
                    case SDL.SDL_Keycode.SDLK_RIGHT: mVelX += DOT_VEL; break;
                }
            }
            //If a key was released
            else if( e.type.ToString().Contains("SDLK_UP") && e.key.repeat == 0 )
            {
                //Adjust the velocity
                switch( e.key.keysym.sym )
                {
                    case SDL.SDL_Keycode.SDLK_UP: mVelY += DOT_VEL; break;
                    case SDL.SDL_Keycode.SDLK_DOWN: mVelY -= DOT_VEL; break;
                    case SDL.SDL_Keycode.SDLK_LEFT: mVelX += DOT_VEL; break;
                    case SDL.SDL_Keycode.SDLK_RIGHT: mVelX -= DOT_VEL; break;
                }
            }
        }

        void Dot.move()
        {
            //Move the dot left or right
            mPosX += mVelX;

            //If the dot went too far to the left or right
            if( ( mPosX < 0 ) || ( mPosX + DOT_WIDTH > 600 ) )
            {
                //Move back
                mPosX -= mVelX;
            }

            //Move the dot up or down
            mPosY += mVelY;

            //If the dot went too far up or down
            if( ( mPosY < 0 ) || ( mPosY + DOT_HEIGHT > 400 ) )
            {
                //Move back
                mPosY -= mVelY;
            }
        }

        void Dot.render()
        {
            //Show the dot
	        gDotTexture.render( mPosX, mPosY );
        }

        bool init()
        {
            //Initialization flag 
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
                //if (SDL.SDL_SetHint(SDL.SDL_HINT_RENDER_SCALE_QUALITY, "1"))
                //{
                //    Console.WriteLine("Warning: Linear texture filtering not enabled!");
                //}
                //Create window 
                gWindow = SDL.SDL_CreateWindow("RTS", SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, 640, 480, SDL.SDL_WindowFlags.SDL_WINDOW_OPENGL);
                if (gWindow == IntPtr.Zero)
                {
                    Console.WriteLine("Window could not be created!");
                    success = false;
                }
                else
                {
                    //Create vsynced renderer for window
                    gRenderer = SDL.SDL_CreateRenderer(gWindow, -1, (uint)SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | (uint)SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);
                    if (gRenderer == IntPtr.Zero)
                    {
                        Console.WriteLine("Renderer could not be created! SDL Error:");
                        success = false;
                    }
                    else
                    {
                        //Initialize renderer color
                        SDL.SDL_SetRenderDrawColor(gRenderer, 0xFF, 0xFF, 0xFF, 0xFF);

                        //Initialize PNG loading
                        SDL_image.IMG_InitFlags imgFlags = SDL_image.IMG_InitFlags.IMG_INIT_PNG;
                        if ((int)imgFlags != 1)
                        {
                            Console.WriteLine("SDL_image could not initialize! SDL_image Error");
                            success = false;
                        }
                    }
                }
            }
            return success;
        }



        static bool loadMedia()
        {
            //Loading success flag
            bool success = true;

            //Load dot texture
            if (!gDotTexture.loadFromFile("dot.bmp"))
            {
                Console.WriteLine("Failed to load dot texture!\n");
                success = false;
            }

            return success;
        }

        void close()
        {
            //Free loaded images
            gDotTexture.free();

            //Destroy window	
            SDL.SDL_DestroyRenderer(gRenderer);
            SDL.SDL_DestroyWindow(gWindow);
            gWindow = IntPtr.Zero;
            gRenderer = IntPtr.Zero;

            //Quit SDL subsystems
            SDL_image.IMG_Quit();
            SDL.SDL_Quit();
        }

        static void Main()
        {
            if (!init())
            {
                Console.WriteLine("Failed to initialize!");
            }
            //Load media 
            if (!loadMedia())
            {
                Console.WriteLine("Failed to load media!");
            }
            else
            {
                //Main loop flag 
                bool quit = false;
                //Event handler 
                SDL.SDL_Event e;
                //The dot that will be moving around on the screen
                Dot dot;

                while (!quit)
                {
                    //Handle events on queue
                    while (SDL.SDL_PollEvent(out e) != 0)
                    {
                        //Console.WriteLine(e.type);
                        //User requests quit
                        if (e.type.ToString().Contains("SDL_QUIT")) //not the proper way to look for SDL_QUIT
                        {
                            quit = true;
                        }

                        //Handle input for the dot
                        dot.handleEvent(e);

                    }
                    //Move the dot
                    dot.move();

                    //Clear screen
                    SDL.SDL_SetRenderDrawColor(gRenderer, 0xFF, 0xFF, 0xFF, 0xFF);
                    SDL.SDL_RenderClear(gRenderer);

                    //Render objects
                    dot.render();

                    //Update screen
                    SDL.SDL_RenderPresent(gRenderer);
                }
            }
            // Clean up
            close();
        }
    }
}
