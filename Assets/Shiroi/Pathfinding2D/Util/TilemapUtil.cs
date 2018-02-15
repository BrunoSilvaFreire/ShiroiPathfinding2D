using System.Reflection;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Shiroi.Pathfinding2D.Util {
    public static class TilemapUtil {
        private const string InstanceFieldName = "s_Instance";
        private const string SetTilemapFieldName = "SetTilemapInstance";

        public static TileBase GetTile(this Tilemap tilemap, int x, int y) {
            return tilemap.GetTile(new Vector3Int(x, y, 0));
        }

        private static ITilemap instance;
        private static readonly MethodInfo info = typeof(ITilemap).GetMethod(SetTilemapFieldName);

        public static ITilemap Instance {
            get {
                return instance ?? (instance = LoadInstance());
            }
        }

        private static ITilemap LoadInstance() {
            var field = typeof(ITilemap).GetField(InstanceFieldName, BindingFlags.Static | BindingFlags.NonPublic);
            if (field == null) {
                return null;
            }

            return (ITilemap) field.GetValue(null);
        }

        private static void SetTilemap(this ITilemap tilemap, Tilemap t) {
            info.Invoke(tilemap, new object[] {t});
        }

        public static ITilemap GetITilemap(this Tilemap tilemap) {
            var i = Instance;
            i.SetTilemap(tilemap);
            return i;
        }
    }
}