using Battleship.src.Networking;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Battleship.src.Controllers.UI.Buttons
{
    public class MultiPlayerButton : ButtonBase
    {

        public MultiPlayerButton(Texture2D buttonTexture, Vector2 _position) : base(buttonTexture, _position)
        {
        }
        public override void onClick()
        {
            base.onClick();
            Console.WriteLine("MultiPlayer");
        }
    }
}
