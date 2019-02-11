using UnityEngine;

namespace Assets.Script_Tools
{
    public static class VectorReplaceExtensions
    {
        public static Vector3 WithZ(this Vector3 v, float z) => new Vector3(v.x, v.y, z);

        public static void ReplaceZ(this ref Vector3 v, float z)
        {
            v = new Vector3(v.x, v.y, z);
        }
    }
}