using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Shiroi.Pathfinding2D {
    public class Navmesh2D : MonoBehaviour {
        public Tilemap Tilemap;

        [SerializeField]
        private Vector2Int min;

        [SerializeField]
        private Vector2Int max;

        public Vector2Int Min {
            get {
                return min;
            }
            set {
                min = value;
            }
        }

        public Vector2Int Max {
            get {
                return max;
            }
            set {
                max = value;
            }
        }

        public int Width {
            get {
                return max.x - min.x + 1;
            }
        }

        public int Height {
            get {
                return max.y - min.y + 1;
            }
        }

        public int Area {
            get {
                return Width * Height;
            }
        }

        public float BoxcastDownsize = 0.1F;

        public Vector2 BoxcastSize {
            get {
                var downscale = (1 - BoxcastDownsize);
                var size = Tilemap.cellSize;
                size.x *= downscale;
                size.y *= downscale;
                return size;
            }
        }

        public LayerMask LayerMask;

        private Node[] nodes;

        private void Reset() {
            nodes = new Node[0];
        }

        public void ReloadNodes() {
            nodes = new Node[Area];
        }

        public Node GetNode(int x, int y) {
            var index = IndexOf(x, y);
            return this[index];
        }

        public Node this[int index] {
            get {
                return IsOutOfBounds(index) ? null : nodes[index];
            }
        }

        public bool IsOutOfBounds(int index) {
            if (nodes == null) {
                return true;
            }

            return index >= nodes.Length || index < 0;
        }

        public int IndexOf(int x, int y) {
            return y * Width + x;
        }

        public int IndexOf(Vector2Int pos) {
            return IndexOf(pos.x, pos.y);
        }

        public int IndexOf(Vector3Int pos) {
            return IndexOf(pos.x, pos.y);
        }
    }
}