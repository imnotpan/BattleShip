using Battleship.src.MainMenu.Buttons.AbstractClassesButtons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.src.MainMenu.Buttons.ConnectionsButtons.Host
{
    public class ClientStateText : TextEntity, MenuItemsInterface
    {
        public ClientStateText(string Text, Vector2 StartPosition, SpriteFont font) : base(Text, StartPosition, font)
        {

        }

    }
}
