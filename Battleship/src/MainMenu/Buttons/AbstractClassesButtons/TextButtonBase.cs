using Battleship.src.Controllers.UI.Effects;
using Battleship.src.MainMenu.Buttons.AbstractClassesButtons;
using Battleship.src.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Tweens;
using System;

namespace Battleship.src.MainMenu.Buttons.AbstractClasses
{
    public class TextButtonBase : Entity, MenuItemsInterface
    {

        // Components
        private Collider Collider { get; set; }

        Vector2 scaleOver;
        Vector2 originalScale;
        Vector2 scaleFactor = new Vector2(0.05f, 0.05f);


        TextEntity _textEntity;
        WiggleEffect WiggleEffect;

        Scene _Scene;

        public Entity _Entity { get; set; }


        public TextButtonBase(string Text, Vector2 _position, GameControllers GameControllers)
        {
            _Entity = this;
            _Scene = GameControllers._Scene;

            // Text entity
            _textEntity = new TextEntity(Text, _position, GameControllers);
            originalScale = _textEntity.Scale;
            scaleOver = originalScale + new Vector2(0.1f, 0.1f);

            Collider = new BoxCollider(_textEntity._textComponent.Width, _textEntity._textComponent.Height);
            AddComponent(Collider);

            WiggleEffect = new WiggleEffect(_textEntity);

            Position = _position;
            onApper();

        }

        public void onApper()
        {

            _textEntity.TweenLocalScaleTo(originalScale, 0.05f)
            .SetEaseType(EaseType.ExpoOut)
            .Start();
        }
        public void onDestroy()
        {
            _textEntity._textComponent.TweenColorTo(Color.Transparent, 0.05f)
          .SetEaseType(EaseType.ExpoOut)
          .SetCompletionHandler((x) =>
          {
              Destroy();
          })
          .Start();
        }

        public override void Update()
        {
            base.Update();

            _textEntity.Position = _textEntity.Position + WiggleEffect.WiggleSinWave();

            Vector2 mousePosition = Scene.Camera.ScreenToWorldPoint(Input.MousePosition);
            if (Collider.Bounds.Contains(mousePosition))
            {
                if (_textEntity.Scale.X <= scaleOver.X && _textEntity.Scale.Y <= scaleOver.Y)
                {
                    _textEntity.Scale += scaleFactor;
                }

                if (Input.LeftMouseButtonPressed)
                {
                    onClick();
                }
            }
            else
            {
                if (_textEntity.Scale.X > originalScale.X && _textEntity.Scale.Y > originalScale.Y)
                {
                    _textEntity.Scale -= scaleFactor;
                }
            }
        }

        public void AddOnScene()
        {
            _textEntity.AddOnScene();
            _Scene.AddEntity(this);
        }
        public void DestroyFromScene()
        {
            _textEntity.Destroy();
            Destroy();
        }

        public virtual void onClick()
        {
            /* ACCIONES BOTON */
        }


    }
}
