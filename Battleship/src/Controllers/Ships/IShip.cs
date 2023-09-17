using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.src.Controllers.Ships
{
    public interface IShip
    {
        int life { get; }
        int state { get; }
        int length { get; }
        int shipType { get; }
        void placeShip();
        void DestroyShip();
    }
}
