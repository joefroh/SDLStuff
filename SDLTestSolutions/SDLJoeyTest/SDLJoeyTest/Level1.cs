using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDLJoeyTest
{
    public class Level1 : ILevel
    {
        public void LoadLevel()
        {
           Dot dot = new Dot();
           MouseClick mouseClick = new MouseClick();
        }

        public void UnloadLevel()
        {
            throw new NotImplementedException();
        }
    }
}
