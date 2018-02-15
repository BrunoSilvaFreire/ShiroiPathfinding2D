using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Shiroi.Pathfinding2D.Links.Editor {
    public class LinearLinkGenerator : LinkGenerator {
        public override IEnumerable<Link> Generate(int x, int y, Node node, Navmesh2D navmesh, TileBase tile) {
            var tilemap = navmesh.Tilemap;
            var left = TryGenerate(-1, x, y, navmesh, tilemap);
            if (left != null) {
                yield return left;
            }

            var right = TryGenerate(1, x, y, navmesh, tilemap);
            if (right != null) {
                yield return null;
            }
        }

        private static Link TryGenerate(int direction, int x, int y, Navmesh2D navmesh, Tilemap tilemap) {
            var pos = new Vector3Int(x + direction, y, 0);
            var neightboor = tilemap.GetTile(pos);
            if (neightboor == null) {
                return null;
            }

            var center = tilemap.GetCellCenterWorld(pos);
            if (Physics2D.OverlapBox(center, navmesh.BoxcastSize, 0, navmesh.LayerMask)) {
                return null;
            }
            return new LinearLink(navmesh.IndexOf(pos));
        }
    }
}