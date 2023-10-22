using Battleship.src.Controllers.UI.GameLoopButtons;
using Battleship.src.MainMenu.Buttons.AbstractClasses;
using Battleship.src.MainMenu.Buttons.AbstractClassesButtons;
using Battleship.src.MainMenu.Buttons.ConnectionsButtons.Client;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.UI;
using System;
using System.Collections.Generic;

namespace Battleship.src.Controllers.UI.GameHud
{
    public class GameHud
    {
        public RedyButton RedyButton;
        public AttackButton AttackButton;
        public ClientDisconnect ClientDisconnect;


        public CircleEntity CircleEntityPlayer;
        public CircleEntity CircleEntityEnemy;
        GameControllers GameControllers;


        public GameHud(GameControllers GameControllers)
        {
            this.GameControllers = GameControllers;

        }

        public void Initialize()
        {


            var RedyButtonPosition = new Vector2(Constants.PIX_SCREEN_WIDTH - Constants.PIX_SCREEN_WIDTH / 2,
                                    Constants.PIX_SCREEN_HEIGHT / 2 - 64);
            RedyButton = new RedyButton("SHIPS", RedyButtonPosition, GameControllers);

            var AttackButtonPosition = new Vector2(Constants.PIX_SCREEN_WIDTH - Constants.PIX_SCREEN_WIDTH / 2,
                                    Constants.PIX_SCREEN_HEIGHT / 2 - 32);
            AttackButton = new AttackButton("ATTACK", AttackButtonPosition, GameControllers);

            var clientDisconnectPosition = new Vector2(Constants.PIX_SCREEN_WIDTH - Constants.PIX_SCREEN_WIDTH / 2,
                          Constants.PIX_SCREEN_HEIGHT / 2 - 96);
            ClientDisconnect = new ClientDisconnect("DISCONNECT", clientDisconnectPosition, GameControllers);


            // Se despliegan botones
            RedyButton.AddOnScene(GameControllers.Scene);
            RedyButton.setSceneState(false);

            AttackButton.AddOnScene(GameControllers.Scene);
            AttackButton.setSceneState(false);

            ClientDisconnect.AddOnScene(GameControllers.Scene);

        }

        public void setRedyViewHUD()
        {
            //Deactivate
            AttackButton.setSceneState(false);

            //Activate
            RedyButton.setSceneState(true);
        }

        public void setAttackViewHUD()
        {
            //Deactivate
            RedyButton.setSceneState(false);

            //Activate
            AttackButton.setSceneState(true);

        }

        public void DisableGameHud()
        {

            ClientDisconnect.setSceneState(false);
            AttackButton.setSceneState(false);
            RedyButton.setSceneState(false);
            AttackButton.setSceneState(false);

            Console.WriteLine("Disable Menu");
        }

        public void Update()
        {
            
        }
    }
}
