using Battleship.src.Controllers;
using Battleship.src.MainMenu.Buttons.AbstractClasses;
using Battleship.src.MainMenu.Buttons.AbstractClassesButtons;
using Battleship.src.MainMenu.Buttons.MainMenuButtons;
using Battleship.src.MainMenu.Buttons.Multiplayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using System;
using System.Collections.Generic;

namespace Battleship.src.MainMenu
{
    public class MainMenuController
    {
        //Controllers
        Scene _Scene;
        GameControllers GameControllers;

        //Buttons
        MultiPlayerButton MultiPlayerButton;
        SinglePlayerButton SinglePlayerButton;

        ConnectButton ConnectButton;
        HostButton HostButton;
        TextEntity IPTEXT;

        string IP_CONNECTION = "";

        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState; // Debes mantener un estado anterior


        List<MenuItemsInterface> MainMenuStack;
        List<MenuItemsInterface> MultiplayerMenuStack;


        public MainMenuController(GameControllers GameControllers)
        {
            this.GameControllers = GameControllers;
            _Scene = GameControllers._Scene;

            //Menu Stacks
            MainMenuStack = new List<MenuItemsInterface>();
            MultiplayerMenuStack = new List<MenuItemsInterface>();

            Initialize();
            MainMenuInitialize();
        }

        public void Update()
        {
            /* Host Text Write */
            IPTEXT._textComponent.Text = IP_CONNECTION;

            currentKeyboardState = Keyboard.GetState();
            if (currentKeyboardState.GetPressedKeys().Length > 0)
            {
                Keys key = currentKeyboardState.GetPressedKeys()[0];
                if (!previousKeyboardState.IsKeyDown(key))
                {
                    char caracterActual = (char)key.GetChar();

                    if (char.IsDigit(caracterActual) || caracterActual == '.')
                    {
                        IP_CONNECTION += caracterActual;
                    }
                    if (key == Keys.Back && IP_CONNECTION.Length > 0)
                    {
                        // Si se presiona la tecla de borrado y hay caracteres en IP_CONNECTION, eliminar el último carácter
                        IP_CONNECTION = IP_CONNECTION.Substring(0, IP_CONNECTION.Length - 1);
                    }
                }
            }

            previousKeyboardState = currentKeyboardState;
        }

        public void Initialize()
        {
            var singlePlayerButtonPos = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT / 2);
            SinglePlayerButton = new SinglePlayerButton("Single Player", singlePlayerButtonPos, GameControllers);
            MainMenuStack.Add(SinglePlayerButton);


            //MultiPlayerButton
            var multiPlayerButtonPos = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT / 2 + 32);
            MultiPlayerButton = new MultiPlayerButton("Multi Player", multiPlayerButtonPos, GameControllers);
            MainMenuStack.Add(MultiPlayerButton);

            //ConnectionButton
            var ConnectionButtonPos = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT / 2 + 16);
            ConnectButton = new ConnectButton("Connect", ConnectionButtonPos, GameControllers);
            MultiplayerMenuStack.Add(ConnectButton);

            var HostButtonPos = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT / 2 + 48);
            HostButton = new HostButton("HOST", HostButtonPos, GameControllers);
            MultiplayerMenuStack.Add(HostButton);

            var IPCONNECTIONPOS = new Vector2(Constants.PIX_SCREEN_WIDTH / 2 - 64, Constants.PIX_SCREEN_HEIGHT / 2 - 32);
            IPTEXT = new TextEntity("...", IPCONNECTIONPOS, GameControllers);
            MultiplayerMenuStack.Add(IPTEXT);

        }


        public void StartGame()
        {
            DisableMenu(MultiplayerMenuStack);
            DisableMenu(MainMenuStack);
            if (GameControllers.GameManager != null) GameControllers.GameManager.StartGame();
            else Console.WriteLine("GameManager is Disable");

        }

        public void MainMenuInitialize()
        {
            DisableMenu(MultiplayerMenuStack);
            GenerateMenu(MainMenuStack);
        }



        public void MultiplayerMenuInitialize()
        {
            DisableMenu(MainMenuStack);
            GenerateMenu(MultiplayerMenuStack);
        }


        public void GenerateMenu(List<MenuItemsInterface> MenuButtons)
        {
            foreach (var button in MenuButtons)
            {
                if (!_Scene.Entities.Contains(button._Entity))
                {
                    button.AddOnScene();

                }
            }
        }


        public void DisableMenu(List<MenuItemsInterface> MenuButtons)
        {
            foreach (var button in MenuButtons)
            {
                if (_Scene.Entities.Contains(button._Entity))
                {
                    button.DestroyFromScene();

                }
            }
        }

    }
}



/*
if(GameManager.GameState != "MAINMENU") 
{ 
    if(!MultiPlayerButton.IsDestroyed) { MultiPlayerButton.onDestroy(); }
    if (!SinglePlayerButton.IsDestroyed) { SinglePlayerButton.onDestroy(); }
}

if(GameManager.GameState == "MULTIPLAYERMENU")
{
    if (!_Scene.Entities.Contains(ConnectButton) && !_Scene.Entities.Contains(IPTEXT))
    {
        ConnectButton.AddOnScene();
        HostButton.AddOnScene();
        IPTEXT.AddOnScene();
    }
    IPTEXT._textComponent.Text = IP_CONNECTION;

    currentKeyboardState = Keyboard.GetState();
    if (currentKeyboardState.GetPressedKeys().Length > 0)
    {
        Keys key = currentKeyboardState.GetPressedKeys()[0];
        if (!previousKeyboardState.IsKeyDown(key))
        {
            char caracterActual = (char)key.GetChar();

            if (char.IsDigit(caracterActual) || caracterActual == '.')
            {
                IP_CONNECTION += caracterActual;
            }
            if (key == Keys.Back && IP_CONNECTION.Length > 0)
            {
                // Si se presiona la tecla de borrado y hay caracteres en IP_CONNECTION, eliminar el último carácter
                IP_CONNECTION = IP_CONNECTION.Substring(0, IP_CONNECTION.Length - 1);
            }
        }
    }

    previousKeyboardState = currentKeyboardState;
    GameManager.IPCONNECTION = IP_CONNECTION;
}

*/