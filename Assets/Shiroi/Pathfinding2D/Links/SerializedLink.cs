using System;
using Shiroi.Serialization;
using UnityEngine;

namespace Shiroi.Pathfinding2D.Links {
    [Serializable]
    public sealed class SerializedLink {
        [SerializeField]
        private string linkType;

        [SerializeField]
        private SerializedObject obj;

        public SerializedLink(string linkType, SerializedObject obj) {
            this.linkType = linkType;
            this.obj = obj;
        }

        public static SerializedLink From(Link link) {
            var name = link.GetType().Name;
            var obj = SerializedObject.From(link);
            return new SerializedLink(name, obj);
        }

        public string LinkType {
            get {
                return linkType;
            }
        }

        public SerializedObject Obj {
            get {
                return obj;
            }
        }

        public Link Deserialize() {
            var type = Type.GetType(linkType);
            if (type == null) {
                return null;
            }
            var link = Activator.CreateInstance(type);
            obj.DeserializeOnto(link);
            return (Link) link;
        }
    }
}