﻿using Nez.Sprites;
using Nez;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Runtime.CompilerServices;

namespace Battleship.src.Controllers
{
    public class GridTiny : Entity
    {
        GameManager GameManager { get; set; }
        SpriteRenderer SpriteRenderer { get; set; }
        public Vector2 _relativePosition;


        public Texture2D _texture;
        private Collider Collider { get; set; }
        public bool shipInGrid;

        public GridTiny(Texture2D cellTexture, Vector2 position, Vector2 relativePosition, GameManager _gameManager)
        {
            GameManager = _gameManager;
            SpriteRenderer = new SpriteRenderer(cellTexture);
            SpriteRenderer.Origin = new Vector2(cellTexture.Width / 2, cellTexture.Height / 2);
            Collider = new BoxCollider();

            this._texture = cellTexture;
            this.Position = position;
            this._relativePosition = relativePosition;

            AddComponent(Collider);
            AddComponent(SpriteRenderer);
        }
        
        public override void Update()
        {
            base.Update();
            if(shipInGrid && (SpriteRenderer.Color != Color.Blue))
            {
                SpriteRenderer.Color = Color.Blue;
            }
        }

    }
}
