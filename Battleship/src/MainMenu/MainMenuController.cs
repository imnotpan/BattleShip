using Battleship.src.MainMenu.Buttons.AbstractClasses;
using Battleship.src.MainMenu.Buttons.AbstractClassesButtons;
using Battleship.src.MainMenu.Buttons.ConnectionsButtons.Client;
using Battleship.src.MainMenu.Buttons.ConnectionsButtons.Host;
using Battleship.src.MainMenu.Buttons.MainMenuButtons;
using Battleship.src.MainMenu.Buttons.Multiplayer;
using Battleship.src.MainMenu.Buttons.MultiplayerButtons;
using Battleship.src.Networking;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Battleship.src.MainMenu
{
    public class MainMenuController
    {
        //Controllers
        Scene Scene;
        GameControllers GameControllers;

        //Buttons
        MultiPlayerButton MultiPlayerButton;
        SinglePlayerButton SinglePlayerButton;

        ConnectButton ConnectButton;
        HostButton HostButton;
        IPTextButton IPTEXT;
        PORTTextButton PORTTEXT;
        gameIDButton gameIDButton;
        BackTextButton BackButton;

        // Connection MENU REQUEST
            // Client
        DisconnectButton DisconnectButton;
        ClientStartGameButton ClientStartGameButton;
        CloseServerButton CloseServerButton;
            // Host
        CreateSession CreateSession;
        public ClientStateText ClientStateText;



        public string ClikedTextButton = "NONE";


        public string IP_CONNECTION = "";
        public string PORT_CONNECTION = "";
        public string GAMESESSIONID = "";
        public string tempText = "";

        public TextButtonBase writeTargetButton;

        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState; // Debes mantener un estado anterior


        public List<MenuItemsInterface> MainMenuStack;
        public List<MenuItemsInterface> clientStack;
        public List<MenuItemsInterface> HostStack;


        public string MENUSTATE = "SINGLEPLAYER";

        public MainMenuController(GameControllers GameControllers)
        {
            this.GameControllers = GameControllers;
            this.Scene = GameControllers.Scene;

            //Menu Stacks
            MainMenuStack = new List<MenuItemsInterface>();
            clientStack = new List<MenuItemsInterface>();
            HostStack = new List<MenuItemsInterface>();


            Initialize();
        }

        public void Update()
        {

            if(writeTargetButton != null)
            {

                if (ClikedTextButton == "IP")
                {
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
                                IP_CONNECTION = IP_CONNECTION.Substring(0, IP_CONNECTION.Length - 1);
                            }
                        }
                    }
                    previousKeyboardState = currentKeyboardState;
                    writeTargetButton._textEntity._textComponent.Text = IP_CONNECTION;
                    var textComponent = writeTargetButton._textEntity._textComponent;
                    textComponent.Origin = textComponent.Origin + new Vector2(textComponent.Width * 2,32);

                }
                if (ClikedTextButton == "PORT")
                {
                    currentKeyboardState = Keyboard.GetState();
                    if (currentKeyboardState.GetPressedKeys().Length > 0)
                    {
                        Keys key = currentKeyboardState.GetPressedKeys()[0];
                        if (!previousKeyboardState.IsKeyDown(key))
                        {
                            char caracterActual = (char)key.GetChar();

                            if (char.IsDigit(caracterActual) || caracterActual == '.')
                            {
                                PORT_CONNECTION += caracterActual;
                            }
                            if (key == Keys.Back && PORT_CONNECTION.Length > 0)
                            {
                                PORT_CONNECTION = PORT_CONNECTION.Substring(0, PORT_CONNECTION.Length - 1);
                            }
                        }
                    }
                    previousKeyboardState = currentKeyboardState;
                    writeTargetButton._textEntity._textComponent.Text = PORT_CONNECTION;
                    var textComponent = writeTargetButton._textEntity._textComponent;
                    textComponent.Origin = textComponent.Origin + new Vector2(textComponent.Width * 2, 32);

                }
            }
        }

        public void Initialize()
        {
            var singlePlayerButtonPos = new Vector2(Constants.PIX_SCREEN_WIDTH / 2 - 64, Constants.PIX_SCREEN_HEIGHT / 2 + 32);
            SinglePlayerButton = new SinglePlayerButton("vs Bot", singlePlayerButtonPos, GameControllers);
            MainMenuStack.Add(SinglePlayerButton);
            SinglePlayerButton.AddOnScene(GameControllers.Scene);

            //MultiPlayerButton
            var multiPlayerButtonPos = new Vector2(Constants.PIX_SCREEN_WIDTH / 2 + 64, Constants.PIX_SCREEN_HEIGHT / 2 + 32);
            MultiPlayerButton = new MultiPlayerButton("vs Player", multiPlayerButtonPos, GameControllers);
            MainMenuStack.Add(MultiPlayerButton);
            MultiPlayerButton.AddOnScene(GameControllers.Scene);

            var HostButtonPos = new Vector2(Constants.PIX_SCREEN_WIDTH / 2 + 64, Constants.PIX_SCREEN_HEIGHT / 2 - 48);
            HostButton = new HostButton("HOST", HostButtonPos, GameControllers);
            MainMenuStack.Add(HostButton);
            HostButton.AddOnScene(GameControllers.Scene);

            var IPCONNECTIONPOS = new Vector2(Constants.PIX_SCREEN_WIDTH / 2 - 64, Constants.PIX_SCREEN_HEIGHT / 2 - 64);
            IPTEXT = new IPTextButton("INSERT IP", IPCONNECTIONPOS, GameControllers);
            MainMenuStack.Add(IPTEXT);
            IPTEXT.AddOnScene(GameControllers.Scene);

            var PORTPOSITION = new Vector2(Constants.PIX_SCREEN_WIDTH / 2 - 64, Constants.PIX_SCREEN_HEIGHT / 2 - 32);
            PORTTEXT = new PORTTextButton("PORT", PORTPOSITION, GameControllers);
            MainMenuStack.Add(PORTTEXT);
            PORTTEXT.AddOnScene(GameControllers.Scene);


            var clientStatusPos = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT / 2 + 64);
            ClientStateText = new ClientStateText("Waiting for connections...", clientStatusPos, GameControllers.textFont);
            HostStack.Add(ClientStateText);
            clientStack.Add(ClientStateText);
            ClientStateText.AddOnScene(GameControllers.Scene);

            var CloseServerPos = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT / 2);
            CloseServerButton = new CloseServerButton("CLOSE SERVER", CloseServerPos, GameControllers);
            HostStack.Add(CloseServerButton);
            CloseServerButton.AddOnScene(GameControllers.Scene);


            var DisconnectButtonPosition = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT / 2);
            DisconnectButton = new DisconnectButton("DISCONNECT", DisconnectButtonPosition, GameControllers);
            clientStack.Add(DisconnectButton);
            DisconnectButton.AddOnScene(GameControllers.Scene);

            Console.WriteLine("MainMenu initialize");

        }


        public void StartGame()
        {
            DisableMenu(MainMenuStack);
            DisableMenu(clientStack);
        }

        public void MainMenuInitialize()
        {
            DisableMenu(clientStack);
            DisableMenu(HostStack);
            GenerateMenu(MainMenuStack);
        }


        public void HostServerWaiting()
        {
            DisableMenu(MainMenuStack);
            DisableMenu(clientStack);
            GenerateMenu(HostStack);
        }
        public void ClientWaiting()
        {
            DisableMenu(MainMenuStack);
            DisableMenu(HostStack);

            GenerateMenu(clientStack);

        }


      


        public void GenerateMenu(List<MenuItemsInterface> MenuButtons)
        {
            foreach (var button in MenuButtons)
            {

               button.setSceneState(true);
            }
        }


        public void DisableMenu(List<MenuItemsInterface> MenuButtons)
        {
            foreach (var button in MenuButtons)
            {

                 button.setSceneState(false);
            }
        }

    }
}


