using System;
using System.Net.Mime;
using System.Runtime.InteropServices;

namespace SDL2.JoeyClasses
{
    public class SDL_Renderer : IDisposable
    {
        #region Privates

        private const string nativeLibName = "SDL2.dll";

        public readonly IntPtr _renderer;

        #endregion

        #region Public Interface

        public SDL_Renderer(SDL_Window window, int index, Flags flags)
        {
            _renderer = SDL_CreateRenderer(window._window, index, flags);
        }


        public void DrawRectangle(SDL.SDL_Rect rect, byte a, byte r, byte g, byte b)
        {
            SetRenderDrawColor(a, r, g, b);
            SDL_RenderFillRect(_renderer, ref rect);
        }

        public void SetRenderDrawColor(byte a, byte r, byte g, byte b)
        {
            SDL_SetRenderDrawColor(_renderer, r, g, b, a);
        }

        public void Display()
        {
            SDL_RenderPresent(_renderer);
        }

        public void Clear()
        {
            SDL_RenderClear(_renderer);
        }

        public IntPtr CreateTextureFromSurface(IntPtr surface)
        {
            return SDL_CreateTextureFromSurface(_renderer, surface);
        }

        public void RenderCopy(IntPtr texture, SDL.SDL_Rect srcrect, SDL.SDL_Rect dstrect)
        {
            SDL_RenderCopy(_renderer, texture, ref srcrect, ref dstrect);
        }

        #endregion

        [Flags]
        public enum Flags
        {
            Software = 0x01,
            Accelerated = 0x02,
            PresentVSync = 0x04,
            TargetTexture = 0x08
        }

        #region Extern Calls

        /* IntPtr refers to an SDL_Renderer*, window to an SDL_Window* */

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_CreateRenderer(
            IntPtr window,
            int index,
            Flags flags
            );

        /* renderer refers to an SDL_Renderer* */

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetRenderDrawColor(
            IntPtr renderer,
            byte r,
            byte g,
            byte b,
            byte a
            );

        /* renderer refers to an SDL_Renderer* */

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderFillRect(
            IntPtr renderer,
            ref SDL.SDL_Rect rect
            );

        /* renderer refers to an SDL_Renderer* */

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderClear(IntPtr renderer);

        /* renderer refers to an SDL_Renderer* */

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_RenderPresent(IntPtr renderer);


        /* IntPtr refers to an SDL_Texture*
		 * renderer refers to an SDL_Renderer*
		 * surface refers to an SDL_Surface*
		 */

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_CreateTextureFromSurface(
            IntPtr renderer,
            IntPtr surface
            );

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture* */

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopy(
            IntPtr renderer,
            IntPtr texture,
            ref SDL.SDL_Rect srcrect,
            ref SDL.SDL_Rect dstrect
            );

        #endregion

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}