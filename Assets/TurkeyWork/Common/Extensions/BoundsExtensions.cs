using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork {

    public static class BoundsExtensions {

        public static Vector3 TopLeft (this Bounds bounds) {
            return new Vector3 (bounds.min.x, bounds.max.y);
        }

        public static Vector3 TopRight (this Bounds bounds) {
            return new Vector3 (bounds.max.x, bounds.max.y);
        }

        public static Vector3 BottomLeft (this Bounds bounds) {
            return new Vector3 (bounds.min.x, bounds.min.y);
        }

        public static Vector3 BottmRight (this Bounds bounds) {
            return new Vector3 (bounds.max.x, bounds.min.y);
        }
    }

}