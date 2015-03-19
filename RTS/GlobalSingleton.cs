using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace RTS
{
    public class GlobalSingleton
    {
        private static readonly GlobalSingleton instance = new GlobalSingleton();
        public IntPtr gWindow;

        //The window renderer
        public IntPtr gRenderer;

        //Scene textures
        public LTexture gDotTexture = new LTexture();


        private GlobalSingleton()
        {
            

        }

        public static GlobalSingleton Instance { get { return instance; } }
    }
}
