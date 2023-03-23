using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Scenes;
using Microsoft.Xna.Framework;

namespace GameEngine.Collisions.QuadTree
{

    public class QuadTree
    {
        const int NODE_CAPACITY = 8;

        Rectangle boundary;

        public List<GameObject> points = new List<GameObject>();

        QuadTree leftTopTree;
        QuadTree rightTopTree;
        QuadTree leftBottomTree;
        QuadTree rightBottomTree;

        public QuadTree(Rectangle boundary)
        {
            this.boundary = boundary;
        }

        public Boolean insert(GameObject gameObject)
        {
            if (!boundary.Contains(gameObject.getWorldPosition())) return false;

            if (points.Count < NODE_CAPACITY && leftTopTree == null)
            {
                points.Add(gameObject);
                return true;
            }

            if (leftTopTree == null)
                subdivide();

            if (leftTopTree.insert(gameObject)) return true;
            if (rightTopTree.insert(gameObject)) return true;
            if (leftBottomTree.insert(gameObject)) return true;
            if (rightBottomTree.insert(gameObject)) return true;

            Debug.WriteLine("couldnt add", gameObject);
            return false;
        }

        public List<GameObject> queryRange(Rectangle range)
        {
            List<GameObject> result = new List<GameObject>();

            if (!boundary.Intersects(range)) return result;

            foreach (GameObject gameObject in points)
            {
                if(range.Contains(gameObject.getWorldPosition()))
                {
                    result.Add(gameObject);
                }
            }
            if (leftTopTree == null) return result;

            result.AddRange(leftTopTree.queryRange(range));
            result.AddRange(rightTopTree.queryRange(range));
            result.AddRange(leftBottomTree.queryRange(range));
            result.AddRange(rightBottomTree.queryRange(range));

            return result;
        }

        public void subdivide()
        {
            int newWidth = boundary.Width / 2;
            int newHeight = boundary.Height / 2;
            leftTopTree = new QuadTree(new Rectangle(boundary.Center.X - newWidth, boundary.Center.Y - newHeight, newWidth, newHeight));
            rightTopTree = new QuadTree(new Rectangle(boundary.Center.X, boundary.Center.Y - newHeight, newWidth, newHeight));
            leftBottomTree = new QuadTree(new Rectangle(boundary.Center.X - newWidth, boundary.Center.Y, newWidth, newHeight));
            rightBottomTree = new QuadTree(new Rectangle(boundary.Center.X, boundary.Center.Y, newWidth, newHeight));
        }
    }
}
