using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Shiroi.Pathfinding2D.Links.Editor {
    [CustomEditor(typeof(Navmesh2D))]
    public class Navmesh2DEditor : UnityEditor.Editor {
        private Navmesh2D navmesh;

        private void OnEnable() {
            navmesh = (Navmesh2D) target;
        }

        public void Generate(IEnumerable<LinkGenerator> generators) {
            var linkGenerators = generators as LinkGenerator[] ?? generators.ToArray();
            var min = navmesh.Min;
            var max = navmesh.Max;
            var tilemap = navmesh.Tilemap;
            navmesh.ReloadNodes();
            for (var x = min.x; x <= max.x; x++) {
                for (var y = min.y; y < max.y; y++) {
                    var node = navmesh.GetNode(x, y);
                    var tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                    foreach (var generator in linkGenerators) {
                        foreach (var link in generator.Generate(x, y, node, navmesh, tile)) {
                            if (link == null) {
                                continue;
                            }
                        }
                    }
                }
            }
        }
    }
}