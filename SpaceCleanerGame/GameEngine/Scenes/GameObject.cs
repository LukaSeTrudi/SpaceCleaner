using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Components;
using GameEngine.Enums;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Scenes
{
    public enum CollisionType
    {
        None = 0,
        Circle = 1,
        Rectangle = 2,
    }
    
    public abstract class GameObject : BaseDrawGameComponent
    {
        public Transform transform = new Transform();
        public GameObject parent = null;
        public Boolean initialized = false;
    
        List<GameComponent> components = new List<GameComponent>();
        public List<GameObject> children = new List<GameObject>();

        private List<GameObject> childrenToAdd = new List<GameObject>();
        private List<GameObject> childrenToDestroy = new List<GameObject>();
        public Tag tag = Tag.Untagged;

        public float zoomedSprite = 1f;

        public Boolean flipX = false;
        public Boolean flipY = false;
        
        private float timeUntilDestroy = 0;
        private Boolean shouldDestroy = false;

        private int timerEvery20frames = 0;
        private int timerEvery40frames = 0;
        private int timerEvery60frames = 0;
        private int timerEvery120frames = 0;

        public Boolean at20frames = false;
        public Boolean at40frames = false;
        public Boolean at60frames = false;
        public Boolean at120frames = false;
        public GameObject() : base(null)
        {
        }

        public float getObjectRadius()
        {
            Rectangle bounds = this.objectBounds();
            return Math.Max(bounds.Width, bounds.Height) / 2;
        }

        public Vector2 getWorldPosition()
        {
            if (parent == null)
                return transform.position;
            else
                return Vector2.Add(parent.getWorldPosition(), transform.position);
        }

        public Vector2 getWorldScale()
        {
            if (parent == null)
                return transform.scale;
            else
                return Vector2.Multiply(parent.getWorldScale(), transform.scale);
        }

        public float getWorldRotation()
        {
            if (parent == null)
                return transform.rotation;
            else
                return parent.getWorldRotation() + transform.rotation;
        }

        public void MoveTowards(Vector2 pos, float multiplier)
        {
            Vector2 dirVector = Vector2.Normalize(pos - this.getWorldPosition());
            this.transform.position += dirVector * multiplier;
        }
        public void MoveTowardsX(int newX, float multiplier)
        {
            Vector2 worldPos = this.getWorldPosition();
            if (Math.Abs(worldPos.X - newX) < 20) return;

            if(this.getWorldPosition().X < newX)
            {
                this.transform.position.X += multiplier;
            } else {
                this.transform.position.X -= multiplier;
            }
        }
        public Rectangle objectBounds()
        {
            SpriteRenderer renderer = (SpriteRenderer)getComponentOfType(ComponentType.SpriteRenderer);
            if (renderer == null) return new Rectangle(getWorldPosition().ToPoint(), Point.Zero);

            return renderer.globalBounds();
        }

        public void addChild(GameObject child)
        {
            child.parent = this;
            childrenToAdd.Add(child);
        }

        public void removeChild(GameObject child)
        {
            childrenToDestroy.Add(child);
        }

        public void addComponent(GameComponent component)
        {
            components.Add(component);
        }

        public List<GameComponent> getComponentsOfType(ComponentType componentType)
        {
            List<GameComponent> filtered = new List<GameComponent>();
            for(int i = 0; i < components.Count; i++)
            {
                if (this.isComponentOfType(components[i], componentType))
                {
                    filtered.Add(components[i]);
                }
            }
            return filtered;
        }

        public GameComponent getComponentOfType(ComponentType componentType)
        {
            List<GameComponent> gameComponents = this.getComponentsOfType(componentType);
            if (gameComponents.Count == 0) return null;
            return gameComponents[0];
        }

        private Boolean isComponentOfType(GameComponent gameComponent, ComponentType componentType)
        {
            switch(componentType)
            {
                case ComponentType.Script:
                    return gameComponent is Script;
                case ComponentType.SpriteRenderer:
                    return gameComponent is SpriteRenderer;
                case ComponentType.Transform:
                    return gameComponent is Transform;
                default:
                    return false;
            }
        }

        public void removeComponent(GameComponent component)
        {
            components.Remove(component);
        }

        public void Destroy(float time)
        {
            shouldDestroy = true;
            timeUntilDestroy = time;
        }
        public void Destroy()
        {
            this.onBeforeDestroy();
            this.Enabled = false;
            if(this.parent != null)
            {
                this.parent.removeChild(this);
            } else
            {
                SceneManager.Instance.currentScene.removeGameObject(this);
            }
        }
        public virtual void onBeforeDestroy()
        {

        }

        public void syncChildren()
        {
            foreach (GameObject go in childrenToDestroy)
            {
                this.children.Remove(go);
            }
            childrenToDestroy.Clear();

            foreach (GameObject go in childrenToAdd)
            {
                this.children.Add(go);
            }
            childrenToAdd.Clear();
        }

        #region Collisions
        public Boolean collidable = false;
        public CollisionType collisionType = CollisionType.None;
        public Boolean colliding(GameObject other)
        {
            if (!this.collidable || collisionType == CollisionType.None || !other.collidable || other.collisionType == CollisionType.None || !this.Enabled || !other.Enabled)
                return false;

            Rectangle firstBounds = this.objectBounds();
            Rectangle secondBounds = other.objectBounds();

            if (this.collisionType == CollisionType.Rectangle && other.collisionType == CollisionType.Rectangle)
            {
                return firstBounds.Intersects(secondBounds);
            }

            if (this.collisionType == CollisionType.Circle && other.collisionType == CollisionType.Circle)
            {
                float objectDist = Vector2.Distance(this.getWorldPosition(), other.getWorldPosition());
                float radiusJoined = this.getObjectRadius() + other.getObjectRadius();
                return objectDist < radiusJoined;
            }

            if (this.collisionType == CollisionType.Rectangle && other.collisionType == CollisionType.Circle)
            {
                float circleRadius = other.getObjectRadius();
                if (circleRadius <= 0) return false;
                // ce je tocka v rectanglu je zihr nt
                if (firstBounds.Contains(secondBounds.Center)) return true;

                Vector2 circleDist = new Vector2(Math.Abs(firstBounds.Center.X - secondBounds.Center.X), Math.Abs(firstBounds.Center.Y - secondBounds.Center.Y));
                
                if (circleDist.X > (firstBounds.Width / 2 + circleRadius)) return false;
                if (circleDist.Y > (firstBounds.Height / 2 + circleRadius)) return false;

                if (circleDist.X <= (firstBounds.Width / 2)) return true;
                if (circleDist.Y <= (firstBounds.Height / 2)) return true;

                float cornerDistance_sq = (float)(Math.Pow(circleDist.X - firstBounds.Width / 2, 2) + Math.Pow(circleDist.Y - firstBounds.Height / 2, 2));
                return cornerDistance_sq <= Math.Pow(circleRadius, 2);
            }
            if (this.collisionType == CollisionType.Circle && other.collisionType == CollisionType.Rectangle)
            {
                return other.colliding(this);
            }
            return false;
        }

        public virtual void onCollision(GameObject other)
        {

        }
        #endregion
        public override void Update(GameTime gameTime)
        {
            syncChildren();
            if (!Enabled) return;
            foreach (GameObject obj in children)
            {
                obj.Update(gameTime);
            }
            foreach (GameComponent component in components)
            {
                component.Update(gameTime);
            }
            if(shouldDestroy)
            {
                timeUntilDestroy -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(timeUntilDestroy <= 0)
                {
                    Destroy();
                    return;
                }
            }
            at20frames = false;
            at40frames = false;
            at60frames = false;
            at120frames = false;
            timerEvery20frames +=1;
            timerEvery40frames+=1;
            timerEvery60frames+=1;
            timerEvery120frames += 1;
            if (timerEvery20frames >= 20)
            {
                at20frames = true;
                timerEvery20frames = 0;
            }
            if(timerEvery40frames >= 40)
            {
                at40frames = true;
                timerEvery40frames = 0;
            }
            if(timerEvery60frames >= 60)
            {
                at60frames = true;
                timerEvery60frames = 0;
            }
            if (timerEvery120frames >= 120)
            {
                at120frames = true;
                timerEvery120frames = 0;
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!Visible) return;

            foreach (GameObject obj in children)
            {
                obj.Draw(gameTime, spriteBatch);
                DebugRenderer.drawGameObjectDebug(spriteBatch, obj);
            }
            foreach (GameComponent component in components)
            {
                if(component is BaseDrawGameComponent)
                {
                    (component as BaseDrawGameComponent).Draw(gameTime, spriteBatch);
                }
            }
            DebugRenderer.drawGameObjectDebug(spriteBatch, this);
        }

        public override void Initialize()
        {
            foreach (GameObject obj in children)
            {
                obj.Initialize();
            }
            initialized = true;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            foreach (GameObject obj in children)
            {
                obj.LoadContent(contentManager);
            }
        }
    }
}
