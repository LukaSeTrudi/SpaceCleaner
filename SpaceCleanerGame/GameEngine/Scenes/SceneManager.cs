using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameEngine
{
    public class SceneManager
    {
        static SceneManager instance;
        public static SceneManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SceneManager();
                }
                return instance;
            }
        }

        int currentSceneIndex = 0;
        public Scene currentScene
        {
            get
            {
                return scenes[currentSceneIndex];
            }
        }

        List<Scene> scenes = new List<Scene>();
        


        public SceneManager()
        {
        }

        public void setScene(Scene scene)
        {
            if (!this.scenes.Contains(scene))
            {
                this.addScene(scene);
            }
            this.currentSceneIndex = this.scenes.IndexOf(scene);
            this.currentScene.Initialize();
        }

        public void addScene(Scene scene)
        {
            this.scenes.Add(scene);
        }

        public void removeScene(Scene scene)
        {
            this.scenes.Remove(scene);
        }



    }
}
