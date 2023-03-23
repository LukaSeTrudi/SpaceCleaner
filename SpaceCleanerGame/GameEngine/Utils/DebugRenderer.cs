using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Utils
{
    public static class DebugRenderer
    {
        public static bool isDebug = false;

        private static Texture2D whiteTexture;
        private static Texture2D circle;

        private static int scale = 10;
        public static void Init(GraphicsDevice device)
        {
            whiteTexture = new Texture2D(device, 1, 1);
            whiteTexture.SetData(new Color[] {  Color.White });
            circle = new Texture2D(device, 1000, 1000);
            circle.SetData(getCircle(1000));
        }

        private static Color[] getCircle(int size)
        {
            int radius = size / 2;
            int center = size / 2;
            float rSquared = radius * radius;

            Color[] result = new Color[size*size];
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    if (Math.Abs(Math.Pow(center - i, 2) + Math.Pow(center - j, 2) - rSquared) <= size*8)
                    {
                        result[i * size + j] = Color.White;
                    } else
                    {
                        result[i * size + j] = Color.Transparent;
                    }
                }
            }
            return result;
        }

        public static void drawGameObjectDebug(SpriteBatch batch, GameObject go)
        {
            if (!isDebug) return;

            if(go.collidable)
            {
                Rectangle objectBounds = go.objectBounds();

                if(go.collisionType == CollisionType.Rectangle)
                {
                    drawBorder(batch, objectBounds, Color.Red);
                } else if(go.collisionType == CollisionType.Circle)
                {
                    int circleRadius = (int)go.getObjectRadius();
                    Rectangle circleBounds = new Rectangle(Vector2.Subtract(objectBounds.Center.ToVector2(), new Vector2(circleRadius)).ToPoint() , new Point(circleRadius*2));
                    drawCircle(batch, circleBounds, Color.Red);
                }

            }
        }

        public static void drawCircle(SpriteBatch batch, Rectangle bounds, Color color)
        {
            if (!isDebug) return;

            batch.Draw(circle, bounds, color);
        }

        public static void drawBorder(SpriteBatch batch, Rectangle bounds, Color color)
        {
            if (!isDebug) return;

            batch.Draw(whiteTexture, new Rectangle(bounds.Location, new Point(bounds.Width, scale)), color); // top
            batch.Draw(whiteTexture, new Rectangle(new Point(bounds.Left, bounds.Bottom), new Point(bounds.Width, scale)), color); // bottom
            batch.Draw(whiteTexture, new Rectangle(new Point(bounds.Left, bounds.Top), new Point(scale, bounds.Height)), color); // left
            batch.Draw(whiteTexture, new Rectangle(new Point(bounds.Right, bounds.Top), new Point(scale, bounds.Height)), color); // right
        }


    }
}
