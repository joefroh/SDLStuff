using System;
using System.Collections.Generic;
using SDL2;
using SDL2.JoeyClasses;

namespace SDLJoeyTest
{
    public class Engine
    {
        private static readonly Engine _instance = new Engine();

        #region Consts

        public const int WindowWidth = 800;
        public const int WindowHeight = 600;

        #endregion

        #region Privates

        private readonly EventHandler _eventHandler;
        private readonly SDL.SDL_Rect _fullWindowRect;
        private readonly List<IGameObject> _gameObjects;
        private readonly SDL_Renderer _renderer;
        private readonly SDL_Window _window;
        private bool _closed;

        #endregion

        public Engine()
        {
            //Init SDL
            if (SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) != 0)
            {
                //We can make the exceptions more specific later... but honestly who gives a shit?
                throw new Exception("C# failed to init SDL.");
            }   
            //Creates Windows
            _window = new SDL_Window("Matrix Epiphany", 100, 100, WindowWidth, WindowHeight,
                SDL_Window.Flags.Shown);
            //Creates Renderer
            _renderer = new SDL_Renderer(_window, -1, SDL_Renderer.Flags.Accelerated | SDL_Renderer.Flags.PresentVSync);

           
/*
            if (_renderer == IntPtr.Zero || _window._window == IntPtr.Zero)
            {
                throw new Exception("Failed to create window or renderer.");
            }
 */

            _eventHandler = new EventHandler();
            RegisterHandlers();

            _gameObjects = new List<IGameObject>();
            _fullWindowRect = new SDL.SDL_Rect {w = WindowWidth, h = WindowHeight, x = 0, y = 0};
        }

        public static Engine Instance
        {
            get { return _instance; }
        }

        public SDL_Renderer Renderer
        {
            get { return _renderer; }
        }

        private void RegisterHandlers()
        {
            _eventHandler.RegisterEventMethod(Quit, SDL.SDL_EventType.SDL_QUIT);
        }

        public void Run()
        {
            LoadLevel(new Level1());
            while (!_closed)
            {
                _eventHandler.PollNewEvents();
                UpdateGameObjects();
                Draw();
            }
        }

        public void LoadLevel(ILevel lvl)
        {
          lvl.LoadLevel();  
        }

        private void Draw()
        {
           _renderer.Clear();
            _renderer.SetRenderDrawColor(0, 15, 15, 15);
            DrawGameObjects();
            _renderer.Display();
        }

        private void DrawGameObjects()
        {
            foreach (IGameObject gameObject in _gameObjects)
            {
                gameObject.Draw(_renderer, _fullWindowRect);
            }
        }

        private void UpdateGameObjects()
        {
            foreach (var gameObject in _gameObjects)
            {
                gameObject.Update();
            }
        }
        public void RegisterGameObject(IGameObject obj)
        {
            _gameObjects.Add(obj);
        }

        public void RegisterEventHandler(Action<SDL.SDL_Event> handler, SDL.SDL_EventType type)
        {
            _eventHandler.RegisterEventMethod(handler,type);
        }

        private void Quit(SDL.SDL_Event e)
        {
            //Free loaded images
            //_texture.free();

            //Destroy window	
            SDL.SDL_DestroyRenderer(_renderer._renderer);
            _window.Dispose();

            //Quit SDL subsystems
            SDL_image.IMG_Quit();
            SDL.SDL_Quit();
            _closed = true;
        }
    }
}