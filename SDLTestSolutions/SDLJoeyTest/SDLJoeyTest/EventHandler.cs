using System;
using System.Collections.Generic;
using SDL2;

namespace SDLJoeyTest
{
    //FYI this would normally be built in to the Engine, which would be responsible for running the gameloop and delegating events to the game logic
    public class EventHandler
    {
        //Dictionary to map event types to the actions that handle the event
        private readonly Dictionary<SDL.SDL_EventType, List<Action<SDL.SDL_Event>>> eventHandlers;

        //creating a class local var for efficiency, saves the cost of recreating, if we just recycle
        private List<Action<SDL.SDL_Event>> actions;
        private SDL.SDL_Event e;

        //Constructor. DUH
        public EventHandler()
        {
            eventHandlers = new Dictionary<SDL.SDL_EventType, List<Action<SDL.SDL_Event>>>();
        }

        //This will resolve our eventing problem. We need to poll for all the events currently on the event stack
        public void PollNewEvents()
        {
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                //Console.WriteLine(e.type);
                HandleEvents(e);
            }
        }

        //Used to connect an event type to a handler. Current implementation will throw if we try to push more than one handler per event type.
        public void RegisterEventMethod(Action<SDL.SDL_Event> eventHandler, SDL.SDL_EventType type)
        {
            List<Action<SDL.SDL_Event>> list = null;
            if (eventHandlers.TryGetValue(type, out list))
            {
                list.Add(eventHandler);
            }
            else
            {
                eventHandlers.Add(type, new List<Action<SDL.SDL_Event>> {eventHandler});
            }
        }

        //Essentially does a lookup on the events based on event type, if it finds a handler for an event type, it calls it
        private void HandleEvents(SDL.SDL_Event sdlEvent)
        {
            if (eventHandlers.TryGetValue(e.type, out actions))
            {
                foreach (var action in actions)
                {
                    action.Invoke(sdlEvent);
                }
            }
        }
    }
}