using System;
using System.Runtime.InteropServices;

namespace SDL2.JoeyClasses
{
    public class SDL_Window : IDisposable
    {
        #region Privates

        private const string nativeLibName = "SDL2.dll";

        #endregion

        #region Internals
        //TODO figure out friending and wrap the renderer so i dont have to expose the window pointer publically, or make it internal

        internal readonly IntPtr _window;
        #endregion

        [Flags]
        public enum Flags
        {
            Fullscreen = 0x00000001,
            OpenGL = 0x00000002,
            Shown = 0x00000004,
            Hiddden = 0x00000008,
            Borderless = 0x00000010,
            Resizable = 0x00000020,
            Minimized = 0x00000040,
            Maximized = 0x00000080,
            Input_Grabbed = 0x00000100,
            Input_Focus = 0x00000200,
            Mouse_Focus = 0x00000400,

            FullScreen_Desktop =
                (Fullscreen | 0x00001000),
            Foregin = 0x00000800,
            Allow_HighDPI = 0x00002000 /* Only available in 2.0.1 */
        }

        #region Public Interface

        public SDL_Window(string windowName, int x, int y, int width, int height, Flags flags)
        {
            _window = SDL_CreateWindow(windowName, x, y, width, height, flags);
        }

        public string Name
        {
            get { return GetName(this); }
            set { SetName(this, value); }
        }

        public int Width
        {
            get { return GetWidth(this); }
            set { SetWidth(this, value); }
        }

        public int Height
        {
            get { return GetHeight(this); }
            set { SetHeight(this, value); }
        }

        public int PosX
        {
            get { return GetPosX(this); }
            //set;
        }

        public int PosY
        {
            get { return GetPosY(this); }
            // set;
        }

        public int MaxX
        {
            get { return GetMaxX(this); }
            // set;
        }

        public int MaxY
        {
            get { return GetMaxY(this); }
            // set;
        }

        public int MinX
        {
            get { return GetMinX(this); }
            //set;
        }

        public int MinY
        {
            get { return GetMinY(this); }
            //set;
        }

        #endregion

        #region Accessors

        private static string GetName(SDL_Window win)
        {
            return SDL_GetWindowTitle(win._window);
        }

        private static int GetWidth(SDL_Window win)
        {
            int w = 0;
            int h = 0;
            SDL_GetWindowSize(win._window, out w, out h);

            return w;
        }

        private static int GetHeight(SDL_Window win)
        {
            int w = 0;
            int h = 0;
            SDL_GetWindowSize(win._window, out w, out h);

            return h;
        }

        private static int GetPosX(SDL_Window win)
        {
            int x = 0;
            int y = 0;

            SDL_GetWindowPosition(win._window, out x, out y);

            return x;
        }

        private static int GetPosY(SDL_Window win)
        {
            int x = 0;
            int y = 0;

            SDL_GetWindowPosition(win._window, out x, out y);

            return y;
        }

        private static int GetMinX(SDL_Window win)
        {
            int x = 0;
            int y = 0;
            SDL_GetWindowMinimumSize(win._window, out x, out y);
            return x;
        }

        private static int GetMinY(SDL_Window win)
        {
            int x = 0;
            int y = 0;
            SDL_GetWindowMinimumSize(win._window, out x, out y);
            return y;
        }

        private static int GetMaxX(SDL_Window win)
        {
            int x = 0;
            int y = 0;

            SDL_GetWindowMaximumSize(win._window, out x, out y);
            return x;
        }

        private static int GetMaxY(SDL_Window win)
        {
            int x = 0;
            int y = 0;

            SDL_GetWindowMaximumSize(win._window, out x, out y);
            return y;
        }

        #endregion

        #region Mutators

        private static void SetName(SDL_Window win, string name)
        {
            SDL_SetWindowTitle(win._window, name);
        }

        private static void SetHeight(SDL_Window win, int h)
        {
            int w = 0;
            int temph = 0;

            SDL_GetWindowSize(win._window, out w, out temph);

            SDL_SetWindowSize(win._window, w, h);
        }

        private static void SetWidth(SDL_Window win, int w)
        {
            int tempw = 0;
            int h = 0;

            SDL_GetWindowSize(win._window, out tempw, out h);

            SDL_SetWindowSize(win._window, w, h);
        }

        #endregion

        #region Extern Calls

        /// <summary>
        ///     Use this function to create a window with the specified position, dimensions, and flags.
        /// </summary>
        /// <param name="title">the title of the window, in UTF-8 encoding</param>
        /// <param name="x">the x position of the window, SDL_WINDOWPOS_CENTERED, or SDL_WINDOWPOS_UNDEFINED</param>
        /// <param name="y">the y position of the window, SDL_WINDOWPOS_CENTERED, or SDL_WINDOWPOS_UNDEFINED</param>
        /// <param name="w">the width of the window</param>
        /// <param name="h">the height of the window</param>
        /// <param name="flags">
        ///     0, or one or more <see cref="SDL.SDL_WindowFlags" /> OR'd together;
        ///     see Remarks for details
        /// </param>
        /// <returns>
        ///     Returns the window that was created or NULL on failure; call <see cref="SDL_GetError()" />
        ///     for more information. (refers to an <see cref="SDL_Window" />)
        /// </returns>
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr SDL_CreateWindow(
            [In] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (LPUtf8StrMarshaler))] string title,
            int x,
            int y,
            int w,
            int h,
            Flags flags
            );

        /// <summary>
        ///     Use this function to destroy a window.
        /// </summary>
        /// <param name="window">the window to destroy (<see cref="SDL_Window" />)</param>
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void SDL_DestroyWindow(IntPtr window);

        /* window refers to an SDL_Window* */

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void SDL_GetWindowMaximumSize(
            IntPtr window,
            out int max_w,
            out int max_h
            );

        /* window refers to an SDL_Window* */

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void SDL_GetWindowMinimumSize(
            IntPtr window,
            out int min_w,
            out int min_h
            );

        /* IntPtr refers to an SDL_Surface*, window to an SDL_Window* */

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr SDL_GetWindowSurface(IntPtr window);

        /* window refers to an SDL_Window* */

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return:
            MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (LPUtf8StrMarshaler),
                MarshalCookie = LPUtf8StrMarshaler.LeaveAllocated)]
        private static extern string SDL_GetWindowTitle(
            IntPtr window
            );

        /* window refers to an SDL_Window* */

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void SDL_GetWindowSize(
            IntPtr window,
            out int w,
            out int h
            );

        /* window refers to an SDL_Window* */

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void SDL_GetWindowPosition(
            IntPtr window,
            out int x,
            out int y
            );


        /* window refers to an SDL_Window* */

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void SDL_SetWindowTitle(
            IntPtr window,
            [In] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (LPUtf8StrMarshaler))] string title
            );

        /* window refers to an SDL_Window* */

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetWindowSize(
            IntPtr window,
            int w,
            int h
            );

        #endregion

        #region IDisposable

        public void Dispose()
        {
            SDL_DestroyWindow(_window);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}