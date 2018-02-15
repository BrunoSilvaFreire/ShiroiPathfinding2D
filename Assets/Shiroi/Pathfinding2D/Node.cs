using System;
using System.Collections.Generic;
using Shiroi.Pathfinding2D.Links;
using Shiroi.Serialization;
using UnityEngine;

namespace Shiroi.Pathfinding2D {
    [Serializable]
    public sealed class Node : ISerializationCallbackReceiver {
        private List<Link> links;
        private SerializedLink[] serializedLinks;

        public IEnumerable<Link> Links {
            get {
                return links;
            }
        }

        public void AddLink(Link link) {
            links.Add(link);
        }

        public void AddLinks(IEnumerable<Link> newLinks) {
            this.links.AddRange(newLinks);
        }

        public void ClearLinks() {
            links.Clear();
        }

        public void OnBeforeSerialize() {
            serializedLinks = new SerializedLink[links.Count];
            for (var i = 0; i < links.Count; i++) {
                var link = links[i];
                serializedLinks[i] = SerializedLink.From(link);
            }
        }

        public void OnAfterDeserialize() {
            links = new List<Link>(serializedLinks.Length);
            foreach (var link in serializedLinks) {
                var created = link.Deserialize();
                if (created == null) {
                    continue;
                }

                links.Add(created);
            }
        }
    }
}