using Battleship.src.Controllers.UI.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.UI;
using System;

namespace Battleship.src.Controllers.UI.GameHud
{
    public class GameHud
    {
        RedyButton RedyButton;
        AttackButton AttackButton;

        GameManager GameManager;
        Scene Scene;

        //Buttons
        Texture2D ShipsRedyTexture;
        Texture2D AttackButtonTexture;




        public GameHud(Scene Scene, GameManager GameManager) 
        {
            this.GameManager = GameManager;
            this.Scene = Scene;
            Initialize();
        }

        public void Initialize() 
        {
            ShipsRedyTexture = Scene.Content.Load<Texture2D>("Sprites/HUD/GameHud/ShipsInPositionButton");
            AttackButtonTexture = Scene.Content.Load<Texture2D>("Sprites/HUD/GameHud/AttackButton");

            
        }

        public void Update()
        {
            if(GameManager.GameState == "PREPARATION")
            {
                if (RedyButton == null)
                {
                    var RedyButtonPosition = new Vector2(Constants.PIX_SCREEN_WIDTH - Constants.PIX_SCREEN_WIDTH/5 - 16, 
                                                        Constants.PIX_SCREEN_HEIGHT/2 - 64);
                    RedyButton = new RedyButton(ShipsRedyTexture, RedyButtonPosition, GameManager);
                    Scene.AddEntity(RedyButton);
                    Console.WriteLine("REDY BUTTON");
                }
            }

            if (GameManager.GameState == "PLAYERTURN")
            {
                if (AttackButton == null)
                {
                    var AttackButtonPosition = new Vector2(Constants.PIX_SCREEN_WIDTH - Constants.PIX_SCREEN_WIDTH / 5 - 16,
                                                        Constants.PIX_SCREEN_HEIGHT / 2 - 64);
                    AttackButton = new AttackButton(AttackButtonTexture, AttackButtonPosition, GameManager);
                    Scene.AddEntity(AttackButton);
                    Console.WriteLine("ATTACK BUTTON");
                }
            }
        }
    }
}
