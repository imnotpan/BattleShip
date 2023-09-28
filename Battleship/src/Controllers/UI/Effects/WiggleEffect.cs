using Microsoft.Xna.Framework;
using Nez;
using System;

namespace Battleship.src.Controllers.UI.Effects
{
    public class WiggleEffect
    {
        private float wiggleAmplitude = 0.1f;  // Amplitud del wiggle (ajusta según tu preferencia)
        private float wiggleTime = 0.15f;
        private float timeSinceLastUpdate = 0f;
        private Vector2 currentOffset = Vector2.Zero;
        private Vector2 targetOffset = Vector2.Zero;
        private float smoothingFactor = 5f; // Ajusta este valor para controlar la suavidad


        private Vector2 OriginalPosition;
        private Vector2 originalScale;
        private Entity entity;


        //Scale
        private Vector2 targetOffsetScale = Vector2.Zero;
        private Vector2 currentOffsetScale = Vector2.Zero;

        public WiggleEffect(Entity entity) {
            this.entity = entity;
            originalScale = entity.Scale;
            OriginalPosition = entity.Position;
        }

        public Vector2 Wiggle()
        {
            timeSinceLastUpdate += Time.DeltaTime; // Incrementar el tiempo transcurrido en cada actualización

            if (timeSinceLastUpdate > wiggleTime)
            {
                float xoffset = (float)Nez.Random.Range(-wiggleAmplitude, wiggleAmplitude);
                float yoffset = (float)Nez.Random.Range(-wiggleAmplitude, wiggleAmplitude);
                targetOffset = new Vector2(xoffset, yoffset);
                timeSinceLastUpdate = 0f;
            }
            currentOffset = Vector2.Lerp(currentOffset, targetOffset, smoothingFactor * Time.DeltaTime);

            return currentOffset;
        }

        private float wiggleFrequency = 2.5f; // Frecuencia del wiggle (ajusta según tu preferencia)
        private float wiggleAmplitudBreath = 0.1f;  // Amplitud del wiggle (ajusta según tu preferencia)

        public Vector2 WiggleScale()
        {

            float xOffset = ((float)Math.Sin(Time.TotalTime * wiggleFrequency)) * wiggleAmplitudBreath ;
            Vector2 wiggleOffset = new Vector2(xOffset, xOffset);

            return originalScale + wiggleOffset;
        }



        private float rotateAmplitude = 0.05f;  // Amplitud del wiggle (ajusta según tu preferencia)
        private float rotateFrecuency = 5f; // Frecuencia del wiggle (ajusta según tu preferencia)
        public Vector2 WiggleSinWave()
        {
            // Actualiza el tiempo transcurrido para el efecto wiggle
            // Calcula la posición modificada con el efecto wiggle
            float xOffset = (float)Math.Sin(Time.TotalTime * rotateFrecuency) * rotateAmplitude;
            float yOffset = (float)Math.Cos(Time.TotalTime * rotateFrecuency) * rotateAmplitude;

            Vector2 wiggleOffset = new Vector2(xOffset, yOffset);

            return wiggleOffset;
        }
    }
}
