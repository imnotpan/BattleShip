using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.src.Controllers
{
    public class GameLogic
    {
        GameControllers GameControllers;
        public GameLogic(GameControllers gameControllers) { 
            this.GameControllers = gameControllers;
        }
    }
}
