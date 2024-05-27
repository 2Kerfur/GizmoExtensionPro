using UnityEditor;
using UnityEngine;

namespace Utils
{
    public static class GizmoPro
    {
        private static Mesh IcosphereMesh;

        private static bool resourcesLoaded = false;
        public static void DrawWireCube(Vector3 center, Vector3 size, Quaternion rotation)
        {
            var old = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(center, rotation, size);
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
            Gizmos.matrix = old;
        }
        public static void DrawWireArrow(Vector3 from, Vector3 to, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f, float arrowHeadSize = 1f) {
			Gizmos.DrawLine(from, to);
			var direction = to - from;
			var right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, arrowHeadSize);
			var left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, arrowHeadSize);
			Gizmos.DrawLine(to, to + right * arrowHeadLength);
			Gizmos.DrawLine(to, to + left * arrowHeadLength);
		}
        public static void DrawWireSphere(Vector3 center, float radius, Quaternion rotation)
        {
            var old = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
            Gizmos.DrawWireSphere(Vector3.zero, radius);
            Gizmos.matrix = old;
        }
        public static void DrawWireCircle(Vector3 center, float radius, Quaternion rotation, int segments = 20)
        {
            DrawWireArc(center, radius, 360, rotation, segments);
        }
        public static void DrawWireArc(Vector3 center, float radius, float angle, Quaternion rotation, int segments = 20)
        {
            var old = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
            Vector3 from = Vector3.forward * radius;
            var step = Mathf.RoundToInt(angle / segments);
            for (int i = 0; i <= angle; i += step)
            {
                var to = new Vector3(radius * Mathf.Sin(i * Mathf.Deg2Rad), 0, radius * Mathf.Cos(i * Mathf.Deg2Rad));
                Gizmos.DrawLine(from, to);
                from = to;
            }
            Gizmos.matrix = old;
        }
        public static void DrawWireIcosphere(Vector3 center, Vector3 size, Quaternion rotation)
        {
            if(!resourcesLoaded) { LoadResources(); }
            if (IcosphereMesh != null)
                Gizmos.DrawWireMesh(IcosphereMesh, center, rotation, size);
        }
        public static void DrawIcosphere(Vector3 center, Vector3 size, Quaternion rotation)
        {
            if (!resourcesLoaded) { LoadResources(); }
            if(IcosphereMesh != null)
                Gizmos.DrawMesh(IcosphereMesh, center, rotation, size);
        }
        public static void DrawWireCapsule(Vector3 center, Quaternion rotation, float radius = 1, float height = 4)
        {
            Matrix4x4 angleMatrix = Matrix4x4.TRS(center, rotation, Handles.matrix.lossyScale);
            using (new Handles.DrawingScope(angleMatrix))
            {
                var pointOffset = (height - (radius * 2)) / 2;
                //draw sideways
                Handles.DrawWireArc(Vector3.up * pointOffset, Vector3.left, Vector3.back, -180, radius);
                Handles.DrawLine(new Vector3(0, pointOffset, -radius), new Vector3(0, -pointOffset, -radius));
                Handles.DrawLine(new Vector3(0, pointOffset, radius), new Vector3(0, -pointOffset, radius));
                Handles.DrawWireArc(Vector3.down * pointOffset, Vector3.left, Vector3.back, 180, radius);
                //draw frontways
                Handles.DrawWireArc(Vector3.up * pointOffset, Vector3.back, Vector3.left, 180, radius);
                Handles.DrawLine(new Vector3(-radius, pointOffset, 0), new Vector3(-radius, -pointOffset, 0));
                Handles.DrawLine(new Vector3(radius, pointOffset, 0), new Vector3(radius, -pointOffset, 0));
                Handles.DrawWireArc(Vector3.down * pointOffset, Vector3.back, Vector3.left, -180, radius);
                //draw center
                Handles.DrawWireDisc(Vector3.up * pointOffset, Vector3.up, radius);
                Handles.DrawWireDisc(Vector3.down * pointOffset, Vector3.up, radius);
            }
        }
        public static void DrawWireCylinder(Vector3 center, float radius, float height, Quaternion rotation = default(Quaternion))
        {
            var old = Gizmos.matrix;
            if (rotation.Equals(default(Quaternion)))
                rotation = Quaternion.identity;
            Gizmos.matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
            var half = height / 2;

            //draw the 4 outer lines
            Gizmos.DrawLine(Vector3.right * radius - Vector3.up * half, Vector3.right * radius + Vector3.up * half);
            Gizmos.DrawLine(-Vector3.right * radius - Vector3.up * half, -Vector3.right * radius + Vector3.up * half);
            Gizmos.DrawLine(Vector3.forward * radius - Vector3.up * half, Vector3.forward * radius + Vector3.up * half);
            Gizmos.DrawLine(-Vector3.forward * radius - Vector3.up * half, -Vector3.forward * radius + Vector3.up * half);

            //draw the 2 cricles with the center of rotation being the center of the cylinder, not the center of the circle itself
            DrawWireArc(center + Vector3.up * half, radius, 360, rotation, 20);
            DrawWireArc(center + Vector3.down * half, radius, 360, rotation, 20);
            Gizmos.matrix = old;
        }
        public static void DrawWireCross(Vector3 position, float size)
        {
            float halfSize = size / 2.0f;
            Gizmos.DrawLine(position + Vector3.left * halfSize, position + Vector3.right * halfSize);
            Gizmos.DrawLine(position + Vector3.up * halfSize, position + Vector3.down * halfSize);
            Gizmos.DrawLine(position + Vector3.back * halfSize, position + Vector3.forward * halfSize);
        }
        public static void DrawWireLine(Vector3 startPos, Vector3 endPos)
        {
            Gizmos.DrawLine(startPos, endPos);
        }

        public static void LoadResources()
        {
            IcosphereMesh = LoadMesh("Assets/Utils/GizmoPro/Models/icosphere.prefab");
            if(IcosphereMesh != null )
            {
                resourcesLoaded = true;
            }
        } 
        private static Mesh LoadMesh(string meshPath)
        {
            GameObject meshObject = AssetDatabase.LoadAssetAtPath<GameObject>(meshPath);
            if(meshObject == null )
            {
                Debug.LogWarning("Failed to load prefab (path): " + meshPath);
                return null;
            }
            MeshFilter meshFilter = meshObject.GetComponent<MeshFilter>();

            if (meshFilter == null)
            {
                Debug.LogWarning("Prefab has no mesh component (path): " + meshPath);
            }
            else
            {
                return meshFilter.sharedMesh;
            }
            return null;
        }
    }
}
