using Battleship.src.MainMenu.Buttons.AbstractClassesButtons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.UI;
using static System.Net.Mime.MediaTypeNames;

namespace Battleship.src.Scenes
{
    public class winScreen : Scene
    {
        public override void Initialize()
        {
            Color miColor = new Color(0x91, 0xCD, 0xAE, 255);
            ClearColor = miColor;

            var textFont = Content.Load<SpriteFont>("Fonts/rockinRecordFont");

            var textEntity = new TextEntity("Ganaste",
                new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT / 2),
                textFont
                );
            AddEntity(textEntity);

        }
    }

}
