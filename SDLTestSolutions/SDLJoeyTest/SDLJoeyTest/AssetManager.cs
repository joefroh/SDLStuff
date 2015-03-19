using System;
using SDL2;

namespace SDLJoeyTest
{
    public static class AssetManager
    {
        /// <summary>
        ///     Loads a texture from a .png file.
        /// </summary>
        /// <param name="path">Path to the .png file.</param>
        /// <returns>An IntPtr to the texture.</returns>
        public static IntPtr LoadTextureFromFile(string path)
        {
            IntPtr texture;
            IntPtr loadedSurface = SDL_image.IMG_Load(path);
            if (loadedSurface == IntPtr.Zero)
            {
                throw new Exception(String.Format("Failed to load the surface from {0}", path));
            }

            texture = Engine.Instance.Renderer.CreateTextureFromSurface(loadedSurface);
            if (texture == IntPtr.Zero)
            {
                throw new Exception("Failed to create texture from surface.");
            }

            return texture;
        }
    }
}