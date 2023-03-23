using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using GameEngine.Collisions.QuadTree;
using GameEngine.GUI;
using GameEngine.Scenes;
using GameEngine.Utils;
using GameEngine.Vendor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceCleaner.Resources.Structure;

namespace GameEngine
{
    public class Scene : BaseDrawGameComponent
    {
        public List<GameObject> gameObjects = new List<GameObject>();
        private List<GameObject> gameObjectsToAdd = new List<GameObject>();
        private List<GameObject> gameObjectsToRemove = new List<GameObject>();

        public event EventHandler<GameTime> onAfterUpdate;
        private ParallaxImage backgroundParallax;
        
        int collisionTimer = -20;
        const int collisionCheckTime = 3;
        public Scene() : base(null)
        {
        }

        public void setParallax(ParallaxImage parallax)
        {
            backgroundParallax = parallax;
        }

        public T findTypeOf<T>() where T : GameObject
        {
            foreach (GameObject obj in gameObjects)
            {
                if (obj is T)
                {
                    return (T)obj;
                }
            }
            return null;
        }
        public List<T> findMultipleTypeOf<T>() where T : GameObject
        {
            List<T> list = new List<T>();
            foreach (GameObject obj in gameObjects)
            {
                if (obj is T)
                {
                    list.Add((T)obj);
                }
            }
            return list;
        } 
        public void addGameObject(GameObject go)
        {
            go.parent = null;
            gameObjectsToAdd.Add(go);
        }

        public void removeGameObject(GameObject go)
        {
            gameObjectsToRemove.Add(go);
        }

        public override void Initialize()
        {
            foreach (GameObject obj in gameObjectsToAdd)
            {
                this.gameObjects.Add(obj);
            }
            gameObjectsToAdd.Clear();
            foreach (GameObject obj in gameObjects)
            {
                obj.Initialize();
            }
        }

        public virtual void onUpdate(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (backgroundParallax != null) backgroundParallax.update();
            
            foreach (GameObject obj in gameObjectsToAdd)
            {
                this.gameObjects.Add(obj);
            }
            gameObjectsToAdd.Clear();
            foreach (GameObject obj in gameObjectsToRemove)
            {
                this.gameObjects.Remove(obj);
            }
            gameObjectsToRemove.Clear();

            if (!Enabled) return;

            collisionTimer += 1;
            if(collisionTimer >= collisionCheckTime)
            {
                collisionTimer = 0;
                checkCollisions();
            }

            foreach (GameObject obj in gameObjects)
            {
                obj.Update(gameTime);
            }
            onAfterUpdate?.Invoke(this, gameTime);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!Visible || !Enabled) return;


            if (backgroundParallax != null) backgroundParallax.drawControl(spriteBatch);
            foreach (GameObject obj in gameObjects)
            {
                obj.Draw(gameTime, spriteBatch);
            }
        }

        public override void LoadContent(ContentManager contentManager)
        {
            foreach (GameObject obj in gameObjects)
            {
                obj.LoadContent(contentManager);
            }
        }

        private void checkCollisions()
        {
            QuadTree root = new QuadTree(new Rectangle(0, 0, Resolution.VWidth, Resolution.VHeight));
            foreach (GameObject obj in gameObjects)
            {
                traverseScene(obj, root);
            }

            foreach (GameObject obj in gameObjects)
            {
                traverseQuad(obj, root);
            }
        }

        private void traverseQuad(GameObject parent, QuadTree root)
        {
            if (parent.collidable)
            {
                Rectangle parentBounds = parent.objectBounds();
                parentBounds.Inflate(400, 400);
                List<GameObject> collisionPossibilites = root.queryRange(parentBounds);
                foreach(GameObject collisionPossibility in collisionPossibilites) {
                    if(parent.colliding(collisionPossibility) && parent != collisionPossibility)
                    {
                        parent.onCollision(collisionPossibility);
                    }
                }
            }
            foreach (GameObject obj in parent.children)
                traverseQuad(obj, root);
        }

        private void traverseScene(GameObject parent, QuadTree root)
        {
            if (parent.collidable)
                root.insert(parent);
            foreach (GameObject obj in parent.children)
                traverseScene(obj, root);

        }
    }
}
