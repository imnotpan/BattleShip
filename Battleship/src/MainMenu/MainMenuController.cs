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
        public List<MenuItemsInterface> MultiplayerMenuStack;
        public List<MenuItemsInterface> ClientStack;
        public List<MenuItemsInterface> HostStack;
        public List<MenuItemsInterface> WaitingStack;



        public string MENUSTATE = "SINGLEPLAYER";

        public MainMenuController(GameControllers GameControllers)
        {
            this.GameControllers = GameControllers;
            this.Scene = GameControllers.Scene;

            //Menu Stacks
            MainMenuStack = new List<MenuItemsInterface>();
            MultiplayerMenuStack = new List<MenuItemsInterface>();
            ClientStack = new List<MenuItemsInterface>();
            HostStack = new List<MenuItemsInterface>();
            WaitingStack = new List<MenuItemsInterface>();



            Initialize();
            MainMenuInitialize();
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
                                // Si se presiona la tecla de borrado y hay caracteres en IP_CONNECTION, eliminar el último carácter
                                IP_CONNECTION = IP_CONNECTION.Substring(0, IP_CONNECTION.Length - 1);
                            }
                        }
                    }
                    previousKeyboardState = currentKeyboardState;
                    writeTargetButton._textEntity._textComponent.Text = IP_CONNECTION;

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
                                // Si se presiona la tecla de borrado y hay caracteres en IP_CONNECTION, eliminar el último carácter
                                PORT_CONNECTION = PORT_CONNECTION.Substring(0, PORT_CONNECTION.Length - 1);
                            }
                        }
                    }
                    previousKeyboardState = currentKeyboardState;
                    writeTargetButton._textEntity._textComponent.Text = PORT_CONNECTION;
                }
                if (ClikedTextButton == "GAMESESSIONID")
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
                                GAMESESSIONID += caracterActual;
                            }
                            if (key == Keys.Back && GAMESESSIONID.Length > 0)
                            {
                                // Si se presiona la tecla de borrado y hay caracteres en IP_CONNECTION, eliminar el último carácter
                                GAMESESSIONID = GAMESESSIONID.Substring(0, GAMESESSIONID.Length - 1);
                            }
                        }
                    }
                    previousKeyboardState = currentKeyboardState;
                    writeTargetButton._textEntity._textComponent.Text = GAMESESSIONID;
                }

            }
        }

        public void Initialize()
        {
            var singlePlayerButtonPos = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT / 2);
            SinglePlayerButton = new SinglePlayerButton("Single Player", singlePlayerButtonPos, GameControllers);
            MainMenuStack.Add(SinglePlayerButton);
            SinglePlayerButton.AddOnScene(GameControllers.Scene);

            //MultiPlayerButton
            var multiPlayerButtonPos = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT / 2 + 32);
            MultiPlayerButton = new MultiPlayerButton("Multi Player", multiPlayerButtonPos, GameControllers);
            MainMenuStack.Add(MultiPlayerButton);
            MultiPlayerButton.AddOnScene(GameControllers.Scene);

            //ConnectionButton
            var ConnectionButtonPos = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT / 2 + 16);
            ConnectButton = new ConnectButton("Connect", ConnectionButtonPos, GameControllers);
            MultiplayerMenuStack.Add(ConnectButton);
            ConnectButton.AddOnScene(GameControllers.Scene);

            var HostButtonPos = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT / 2 + 48);
            HostButton = new HostButton("HOST", HostButtonPos, GameControllers);
            MultiplayerMenuStack.Add(HostButton);
            HostButton.AddOnScene(GameControllers.Scene);

            var IPCONNECTIONPOS = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT / 2 - 32);
            IPTEXT = new IPTextButton("INSERT IP", IPCONNECTIONPOS, GameControllers);
            MultiplayerMenuStack.Add(IPTEXT);
            IPTEXT.AddOnScene(GameControllers.Scene);

            var PORTPOSITION = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT / 2 - 64);
            PORTTEXT = new PORTTextButton("INSERT PORT", PORTPOSITION, GameControllers);
            MultiplayerMenuStack.Add(PORTTEXT);
            PORTTEXT.AddOnScene(GameControllers.Scene);

            var GAMESESSIONPOS = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT / 2  - 96);
            gameIDButton = new gameIDButton("INSERT GAMEID", GAMESESSIONPOS, GameControllers);
            MultiplayerMenuStack.Add(gameIDButton);
            gameIDButton.AddOnScene(GameControllers.Scene);

            var BACKPOSITION = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT / 2 + 64);
            BackButton = new BackTextButton("BACK", BACKPOSITION, GameControllers);
            MultiplayerMenuStack.Add(BackButton);
            BackButton.AddOnScene(GameControllers.Scene);

            // CONNECTION MENU
            var DisconnectButtonPosition = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT / 2);
            DisconnectButton = new DisconnectButton("DISCONNET", DisconnectButtonPosition, GameControllers);
            ClientStack.Add(DisconnectButton);
            WaitingStack.Add(DisconnectButton);
            DisconnectButton.AddOnScene(GameControllers.Scene);


            var ClientStartGameButtonPos = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT / 2 - 64);
            ClientStartGameButton = new ClientStartGameButton("READY TO PLAY", ClientStartGameButtonPos, GameControllers);
            ClientStack.Add(ClientStartGameButton);
            ClientStartGameButton.AddOnScene(GameControllers.Scene);

            // CONNECTION MENU
            var CloseServerPos = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT / 2);
            CloseServerButton = new CloseServerButton("CLOSE SERVER", CloseServerPos, GameControllers);
            HostStack.Add(CloseServerButton);
            CloseServerButton.AddOnScene(GameControllers.Scene);

            
            var CreateSessionPos = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT / 2 - 32);
            CreateSession = new CreateSession("CREATE GAME SESSION", CreateSessionPos, GameControllers);
            HostStack.Add(CreateSession);
            CreateSession.AddOnScene(GameControllers.Scene);

            /*
            var clientStatusPos = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT / 2 + 64);
            ClientStateText = new ClientStateText("User Connected: ", clientStatusPos, GameControllers.textFont);
            HostStack.Add(ClientStateText);
            ClientStateText.AddOnScene(GameControllers.Scene);
            */

            var clientStatusPos = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT / 2 + 64);
            ClientStateText = new ClientStateText("Waiting for connections...", clientStatusPos, GameControllers.textFont);
            WaitingStack.Add(ClientStateText);
            ClientStateText.AddOnScene(GameControllers.Scene);



        }


        public void StartGame()
        {
            DisableMenu(MultiplayerMenuStack);
            DisableMenu(MainMenuStack);
            DisableMenu(HostStack);
            DisableMenu(ClientStack);
            DisableMenu(WaitingStack);
        }

        public void WaitingForPlayers()
        {
            DisableMenu(MultiplayerMenuStack);
            DisableMenu(MainMenuStack);
            DisableMenu(HostStack);
            DisableMenu(ClientStack);
            GenerateMenu(WaitingStack);
        }

        public void MainMenuInitialize()
        {
            DisableMenu(MultiplayerMenuStack);
            GenerateMenu(MainMenuStack);
            DisableMenu(HostStack);
            DisableMenu(ClientStack);
            DisableMenu(WaitingStack);

        }
        public void ClientMenuInitialize()
        {
            DisableMenu(MultiplayerMenuStack);
            GenerateMenu(ClientStack);
        }

        public void HostMenuInitialize()
        {
            DisableMenu(MultiplayerMenuStack);
            GenerateMenu(HostStack);
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


