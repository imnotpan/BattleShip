using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.src.Controllers
{
    public class CanvasUI
    {
        private UICanvas _uiCanvas;
        private GameManager GameManager;

        public CanvasUI(GameManager gameManager)
        {
            GameManager = gameManager;

            _uiCanvas = new UICanvas();

        }


    }
}
