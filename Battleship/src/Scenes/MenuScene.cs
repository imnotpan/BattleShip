using Battleship.src.Networking;
using Microsoft.Xna.Framework;
using Nez;
using Nez.UI;


namespace Battleship.src.Scenes
{
    public class MenuScene : Scene
    {
        UICanvas UIC;
        public TextButton ExitButton { get; set; }
        Servidor _servidor;
        Cliente _cliente;

        public override void Initialize()
        {
            Entity uiCanvas = CreateEntity("ui-canvas");
            _servidor = new Servidor();
            _cliente = new Cliente();

            UIC = uiCanvas.AddComponent(new UICanvas());
            ExitButton = UIC.Stage.AddElement(new TextButton("Cliente", Skin.CreateDefaultSkin()));
            ExitButton.SetPosition(10f, 30f);
            ExitButton.SetSize(60f, 20f);
            ExitButton.OnClicked += OnClientButtonClicked;

            UIC = uiCanvas.AddComponent(new UICanvas());
            ExitButton = UIC.Stage.AddElement(new TextButton("Servidor", Skin.CreateDefaultSkin()));
            ExitButton.SetPosition(10f, 60);
            ExitButton.SetSize(60f, 20f);
            ExitButton.OnClicked += OnServerButtonClicked;

        }
        private void OnClientButtonClicked(Button button)
        {
            _cliente.Connection();

        }

        private void OnServerButtonClicked(Button button)
        {
            _servidor.Start();


        }
    }
}

