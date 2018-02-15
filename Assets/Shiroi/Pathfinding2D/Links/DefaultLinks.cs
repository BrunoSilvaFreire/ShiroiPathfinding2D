using System;
using UnityEngine;

namespace Shiroi.Pathfinding2D.Links {
    [Serializable]
    public class LinearLink : Link {
        [SerializeField]
        private int destination;

        public LinearLink(int destination) {
            this.destination = destination;
        }

        public override int GetDestination() {
            return destination;
        }
    }
}