using UnityEngine;

namespace Assets.Script_Tools
{
    public static class ShorterVectors
    {
        public static Vector2 v2(float x, float y) => new Vector2(x, y);
        public static Vector2 v3(float x, float y, float z) => new Vector3(x, y, z);
        public static Vector2 v3(Vector2 v2, float z) => new Vector3(v2.x, v2.y, z);
    }
}