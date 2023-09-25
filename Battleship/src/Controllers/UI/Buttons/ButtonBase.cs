using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Tweens;
using System;

namespace Battleship.src.Controllers.UI.Buttons
{
    public class ButtonBase : Entity
    {
        private Texture2D buttonTexture;

        // Components
        private Collider Collider { get; set; }
        private SpriteRenderer SpriteRenderer { get; set; }
        private bool isOverMouse = false;

        Vector2 scaleOver = new Vector2(1.15f, 1.15f);
        Vector2 originalScale = new Vector2(1f, 1f);
        Vector2 scaleFactor = new Vector2(0.05f, 0.05f);

        Color originalColor;
        Color clickedColor = Color.Black;
        Effect wavyShader;


        public ButtonBase(Texture2D buttonTexture, Vector2 _position) { 
        
            this.buttonTexture = buttonTexture;
             
            SpriteRenderer = new SpriteRenderer(buttonTexture);
            SpriteRenderer.Origin = new Vector2(buttonTexture.Width / 2, buttonTexture.Height / 2);

            Collider = new BoxCollider();
            AddComponent(Collider);
            AddComponent(SpriteRenderer);

            originalColor = SpriteRenderer.Color;
            Position = _position;
            onApper();



        }
        public void onApper()
        {
 
            this.TweenLocalScaleTo(originalScale, 0.05f)
            .SetEaseType(EaseType.ExpoOut)
            .SetCompletionHandler((x) =>
            {

            })
            .Start();
        }
        public void onDestroy()
        {
            SpriteRenderer.TweenColorTo(Color.Transparent, 0.05f)
          .SetEaseType(EaseType.ExpoOut)
          .SetCompletionHandler((x) =>
          {
              this.Destroy();
          })
          .Start();
        }

        public override void Update()
        {
            base.Update();

            Vector2 mousePosition = Scene.Camera.ScreenToWorldPoint(Input.MousePosition);
            if (Collider.Bounds.Contains(mousePosition))
            {
                if (Scale.X <= scaleOver.X && Scale.Y <= scaleOver.Y)
                {
                    Scale += scaleFactor;
                }

                if (Input.LeftMouseButtonPressed)
                {
                    onClick();
                }
            }
            else
            {
                if (Scale.X > originalScale.X && Scale.Y > originalScale.Y)
                {
                    Scale -= scaleFactor;
                }
            }
        }



        public virtual void onClick()
        {
            /*
            SpriteRenderer.TweenColorTo(clickedColor, 0.05f)
             .SetEaseType(EaseType.ExpoOut)
             .SetCompletionHandler((x) =>
             {
                 SpriteRenderer.TweenColorTo(originalColor, 0.05f)
                     .SetEaseType(EaseType.ExpoOut)
                     .Start();
             })
             .Start();
            */
        }


    }
}
