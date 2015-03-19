using System;
using SDL2;

namespace RTS
{
    public class LTexture
    {
        //Initializes variables
        private int mHeight;
        private IntPtr mTexture;

        //Image dimensions
        private int mWidth;

        //The final texture
        public IntPtr newTexture;

        public LTexture()
        {
            //Initialize
            IntPtr mTexture;
            int mWidth = 0;
            int mHeight = 0;
        }

        //Loads image at specified path
        public bool loadFromFile(string path)
        {  

            //Load image at specified path
            IntPtr loadedSurface = SDL_image.IMG_Load(path);
            if (loadedSurface == IntPtr.Zero)
            {
                Console.WriteLine("Unable to load image! SDL_image Error " + path);
            }
            else
            {
                //Color key image
               // SDL.SDL_SetColorKey(loadedSurface, (int) SDL.SDL_bool.SDL_TRUE, SDL.SDL_MapRGB(loadedSurface, 0, 0xFF, 0xFF));


                //Create texture from surface pixels
                newTexture = SDL.SDL_CreateTextureFromSurface(GlobalSingleton.Instance.gRenderer, loadedSurface);
                if (newTexture == IntPtr.Zero)
                {
                    Console.WriteLine("Unable to create texture! SDL Error:");
                }
                else
                {
                    //Get image dimensions
                    mWidth = 20;
                    mHeight = 20;
                }

                //Get rid of old loaded surface
                SDL.SDL_FreeSurface(loadedSurface);
            }
            return true;
        }

        //Creates image from font string
        public bool LoadFromRenderedText(string textureText, SDL.SDL_Color textColor)
        {
            //Get rid of preexisting texture
            //LTexture.free();

            //Render text surface
            IntPtr fontFile = SDL_ttf.TTF_OpenFont(@"Assets/Fonts/LUCON.TTF", 12);
            IntPtr surface = SDL_ttf.TTF_RenderText_Blended(fontFile, "Dillon Spits HOT FIRE!!!!",
                new SDL.SDL_Color {a = 0, b = 0, g = 0, r = 255});

            if (surface != IntPtr.Zero)
            {
                //Create texture from surface pixels
                mTexture = SDL.SDL_CreateTextureFromSurface(GlobalSingleton.Instance.gRenderer, surface);
                if (mTexture == IntPtr.Zero)
                {
                    Console.WriteLine("Unable to create texture from rendered text! SDL Error");
                }
                else
                {
                    //Get image dimensions
                    mWidth = 100;
                    mHeight = 100;
                }

                //Get rid of old surface
                SDL.SDL_FreeSurface(surface);
            }
            else
            {
                Console.WriteLine("Unable to render text surface! SDL_ttf Error: ");
            }


            //Return success
            return mTexture != IntPtr.Zero;
        }

        //Deallocates texture
        public void free()
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

        //Set color modulation
        public void setColor(byte red, byte green, byte blue)
        {
            //Modulate texture rgb
            SDL.SDL_SetTextureColorMod(mTexture, red, green, blue);
        }

        //Set blending
        public void setBlendMode(SDL.SDL_BlendMode blending)
        {
            //Set blending function
            SDL.SDL_SetTextureBlendMode(mTexture, blending);
        }

        //Set alpha modulation
        public void setAlpha(byte alpha)
        {
            //Modulate texture alpha
            SDL.SDL_SetTextureAlphaMod(mTexture, alpha);
        }

        //Renders texture at given point
        public void render(int x, int y, SDL.SDL_Rect destinationRectangle)
        {
            SDL.SDL_Rect renderQuad = new SDL.SDL_Rect() { x = x, y = y, w = 20, h = 20 };

            //Render to screen
            SDL.SDL_RenderCopy(GlobalSingleton.Instance.gRenderer, GlobalSingleton.Instance.gDotTexture.newTexture, ref destinationRectangle, ref renderQuad);

        }

        //Gets image dimensions
        public int getWidth()
        {
            return mWidth;
        }

        public int getHeight()
        {
            return mHeight;
        }
    }
}