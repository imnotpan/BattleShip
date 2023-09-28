using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using System;

namespace Battleship.src.MainMenu.Buttons.AbstractClassesButtons
{
    public class TextEntity : Entity, MenuItemsInterface
    {
        public TextComponent _textComponent;
        public TextEntity shadowClone;
        private string Text;

        //Controllers
        Scene _Scene;
        GameControllers GameControllers;

        public Entity _Entity { get; set; }

        public TextEntity(string Text, Vector2 StartPosition, GameControllers GameControllers)
        {
            //Interface
            _Entity = this;

            //Controllers
            _Scene = GameControllers._Scene;

            //Font
            var font = _Scene.Content.Load<SpriteFont>("Fonts/rockinRecordFont");
            var NezSprite = new NezSpriteFont(font);

            //Properties
            this.Text = Text;

            Position = StartPosition;
            Scale = new Vector2(0.3f, 0.3f);

            //Component
            _textComponent = new TextComponent();
            _textComponent.SetFont(NezSprite);
            _textComponent.RenderLayer = -999;
            _textComponent.Text = Text;
            _textComponent.Color = Color.White;
            AddComponent(_textComponent);
        }

        public override void Update()
        {
            base.Update();
            _textComponent.Origin = new Vector2(_textComponent.Width / 2, _textComponent.Height/2 ) ;
            if (shadowClone != null)
            {
                shadowClone._textComponent.OriginNormalized = Vector2.One / 2;
                shadowClone.Position = Position + new Vector2(-2f, 2f);
                shadowClone.Scale = Scale;
                shadowClone._textComponent.Text = _textComponent.Text;

            }
            _textComponent.OriginNormalized = Vector2.One / 2;
        }

        public void GenerateShadowClone()
        {
            // Crea la entidad de sombra
            shadowClone = new TextEntity(Text, Position + new Vector2(-2f, 2f), GameControllers);
            shadowClone._textComponent.Color = Color.Black;
            shadowClone._textComponent.RenderLayer = _textComponent.RenderLayer + 1;

            _Scene.AddEntity(shadowClone);
        }

        public void AddOnScene()
        {
            _Scene.AddEntity(this);
            Console.WriteLine("Añadido a escena");
        }

        public void DestroyFromScene()
        {
            this.Destroy();
        }
    }
}
