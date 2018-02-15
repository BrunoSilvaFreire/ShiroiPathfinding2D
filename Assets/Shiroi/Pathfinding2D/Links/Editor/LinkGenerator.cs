using System.Collections.Generic;
using UnityEngine.Tilemaps;

namespace Shiroi.Pathfinding2D.Links.Editor {
    public abstract class LinkGenerator {
        public abstract IEnumerable<Link> Generate(int x, int y, Node node, TileBase tile, Tilemap tilemap);
    }
}