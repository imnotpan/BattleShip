using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Tweens;
using System;

namespace Battleship.src.Controllers.Grids
{

    public class Grid : Entity
    {

        public Texture2D _texture;
        public Collider Collider { get; set; }

        public bool _onClick = false;
        public SpriteRenderer SpriteRenderer { get; set; }

        public Vector2 _relativePosition;

        /* Logic Properties */
        public bool isOnTempArray = false;
        public bool isInUse = false;
        public Color overColor = Color.Cyan;
        public Color currentColor = Color.White;

        public bool isDestroy = false;
        public bool canClickOnGrid = false;


        public Grid(Texture2D cellTexture, Vector2 position, Vector2 relativePosition)
        {
            SetTag(2);  // 2 -> GRID
            SpriteRenderer = new SpriteRenderer(cellTexture);
            SpriteRenderer.RenderLayer = 1;
            SpriteRenderer.Origin = new Vector2(cellTexture.Width / 2, cellTexture.Height / 2);
            Collider = new BoxCollider();

            _texture = cellTexture;
            Position = position;
            _relativePosition = relativePosition;

            AddComponent(Collider);
            AddComponent(SpriteRenderer);

        }


        public override void Update()
        {
            base.Update();
            if(SpriteRenderer.Color != currentColor) { SpriteRenderer.Color = currentColor; }
        }


        public void GridClickAnim()
        {
            this.TweenLocalScaleTo(new Vector2(1.25f, 1.25f), 0.05f)
            .SetEaseType(EaseType.ExpoOut)
            .SetCompletionHandler((x) =>
            {
                this.TweenLocalScaleTo(new Vector2(1f, 1f), 0.05f)
                .SetEaseType(EaseType.ExpoIn)
                .Start();
            })
            .Start();
        }
    }
}

