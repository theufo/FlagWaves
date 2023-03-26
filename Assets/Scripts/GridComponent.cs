using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class GridComponent : MonoBehaviour
    {
        public int XVertices, YVertices;
        private int _xSize, _ySize;
        private Vector3[] _vertices;
        private Mesh _mesh;

        [Header("CPU animation properties Wave")] 
        public float WaveSpeed;
        public float Amplitude;
        
        [Header("CPU animation properties Scroll")] 
        public float Speed;

        private void Awake()
        {
            GenerateMesh();
        }

        public void SetSize(int xSize, int ySize)
        {
            XVertices = xSize;
            YVertices = ySize;
            
            GenerateMesh();
        }

        private void GenerateMesh()
        {
            _xSize = XVertices - 1;
            _ySize = YVertices - 1;
            
            GetComponent<MeshFilter>().mesh = _mesh = new Mesh();
            _mesh.name = "Grid";

            _vertices = new Vector3[XVertices * YVertices];
            var uv = new Vector2[_vertices.Length];
            var tangents = new Vector4[_vertices.Length];
            var tangent = new Vector4(1f, 0f, 0f, -1f);
            for (int i = 0, y = 0; y <= _ySize; y++)
            {
                for (int x = 0; x <= _xSize; x++, i++)
                {
                    _vertices[i] = new Vector3(x, y);
                    uv[i] = new Vector2((float)x / _xSize, (float)y / _ySize);
                    tangents[i] = tangent;
                }
            }

            _mesh.vertices = _vertices;
            _mesh.uv = uv;

            var triangles = new int[_xSize * _ySize * 6];
            for (int y = 0, vi = 0, ti = 0; y < _ySize; y++, vi++)
            {
                for (int x = 0; x < _xSize; x++, ti += 6, vi++)
                {
                    triangles[ti] = vi;
                    triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                    triangles[ti + 4] = triangles[ti + 1] = vi + _xSize + 1;
                    triangles[ti + 5] = vi + _xSize + 2;
                }
            }

            _mesh.triangles = triangles;
            _mesh.tangents = tangents;
            _mesh.RecalculateNormals();
        }

        private void Update()
        {
            var uv = _mesh.uv;
            for (var i = 0; i < _vertices.Length; i++)
            {
                _vertices[i].z += Amplitude * 0.01f * Mathf.Sin(_vertices[i].x - Time.time * WaveSpeed);
            }

            for (int i = 0; i < uv.Length; i++)
            {
                uv[i] += 0.01f * new Vector2(Speed *Time.deltaTime, 0);
            }

            _mesh.vertices = _vertices;
            _mesh.uv = uv;
        }
    }
}