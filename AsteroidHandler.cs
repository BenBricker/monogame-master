using System;
using System.Collections.Generic;
using System.Text;
using App05MonoGame.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace App05MonoGame
{
    class AsteroidHandler
    {
        private const int Height_Off_Display = -100;
        private const int speedLow = 200;
        private const int speedHigh = 600;

        private List<Sprite> asteroids;
        private Texture2D _largeAsteroidImage;
        private Texture2D _smallAsteroidImage;
        private int _screenHeight;
        private int _screenWidth;
       
        public AsteroidHandler(Texture2D largeAsteroidImage, Texture2D smallAsteroidImage, int height, int width)
        {
            asteroids = new List<Sprite>();
            _largeAsteroidImage = largeAsteroidImage;
            _smallAsteroidImage = smallAsteroidImage;
            _screenHeight = height;
            _screenWidth = width;
        }

        /// <summary>
        /// This is the initial setup for a single image sprite 
        /// </summary>
        public void SetupAsteroid()
        {
            var rand = new Random();
            Texture2D asteroid;
            if (rand.Next(0, 2) == 0)
            {
                asteroid = _largeAsteroidImage;
            }
            else
            {
                asteroid = _smallAsteroidImage;
            }

            int xPos = rand.Next(0, _screenWidth);
            int asteroidSpeed = rand.Next(speedLow, speedHigh);
            Sprite asteroidSprite = new Sprite(asteroid, xPos, Height_Off_Display)
            {
                Direction = new Vector2(0, 1),
                Speed = asteroidSpeed,

                Rotation = MathHelper.ToRadians(3),
                RotationSpeed = 2f,
            };

            asteroids.Add(asteroidSprite);

        }

        public int Update(GameTime gameTime)
        {
            int numberOfAsteroidsAvoided = 0;
            foreach (Sprite asteroidSprite in asteroids)
            {
                asteroidSprite.Update(gameTime);
                if (asteroidSprite.Position.Y > _screenHeight)
                {
                    var rand = new Random();
                    if (rand.Next(0, 2) == 0)
                    {
                        asteroidSprite.Image = _largeAsteroidImage;
                    }
                    else
                    {
                        asteroidSprite.Image = _smallAsteroidImage;
                    }
                    asteroidSprite.ResetPosition(rand.Next(0, _screenWidth), Height_Off_Display);
                    asteroidSprite.Speed = rand.Next(speedLow, speedHigh);
                }
            }
            return numberOfAsteroidsAvoided;
        }

        public bool HasCollided(Sprite otherSprite)
        {
            bool hasCollided = false;
            foreach (Sprite asteroidSprite in asteroids)
            {
                if (asteroidSprite.HasCollided(otherSprite))
                {
                    hasCollided = true;
                }
            }

            return hasCollided;

        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Sprite asteroidSprite in asteroids)
            {
                asteroidSprite.Draw(spriteBatch);
            }
        }
    }
}
