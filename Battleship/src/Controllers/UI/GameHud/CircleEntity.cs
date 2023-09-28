﻿using Battleship.src.Controllers.UI.Effects;
using Battleship.src.MainMenu.Buttons.AbstractClassesButtons;
using Battleship.src.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System;

namespace Battleship.src.Controllers.UI.GameHud
{
    public class CircleEntity : Entity
    {

        public TextEntity linkedText;
        private SpriteRenderer spriteRenderer;
        private GameManager GameManager;
        private WiggleEffect WiggleEffect;
        Scene Scene;
        GameControllers GameControllers;

        public CircleEntity(GameControllers GameControllers, Texture2D texture, Vector2 position) 
        {
            this.GameManager = GameControllers.GameManager;

            spriteRenderer = new SpriteRenderer(texture);
            spriteRenderer.RenderLayer = -15;
            spriteRenderer.Origin = new Vector2(texture.Width / 2, texture.Height / 2);
            AddComponent(spriteRenderer);
            
            WiggleEffect = new WiggleEffect(this);

            this.Position = position;
        }
        


        public override void Update() 
        { 
            base.Update();
            //this.Position = this.Position + WiggleEffect.Wiggle();
            this.Scale = WiggleEffect.WiggleScale();
            this.Position = this.Position+ WiggleEffect.WiggleSinWave();
            linkedText.Position = this.Position - new Vector2(10,8);
        }

        public void AddEntityOnScene()
        {
            linkedText = new TextEntity("0", this.Position + new Vector2(5, 0), GameControllers);
            linkedText._textComponent.RenderLayer = this.spriteRenderer.RenderLayer - 10;
            //linkedText.GenerateShadowClone();
            Scene.AddEntity(linkedText);
            Scene.AddEntity(this);
        }
    }
}