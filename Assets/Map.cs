using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Map : MonoBehaviour
{
    private float[,,] EmptyMap = new float[0, 0, 0];
    public Vector3Int Dimensions = new Vector3Int(0,0,0);
    public float[,,] MapPoints = new float[0,0,0];
    private MeshFilter Mf;
    [SerializeField][Range(0f, 1f)] float Threshold = 1;

    private List<Vector3> Vertices = new List<Vector3>();
    private List<int> triangle = new List<int>();
    
    public bool IsVisible = false;
    // Start is called before the first frame update
    void Start()
    {
        Mf = GetComponent<MeshFilter>();
        StartCoroutine(UpdateMap());
    }

    IEnumerator UpdateMap()
    {
        while (true)
        {
            MarchCube();
            SetMesh();
            yield return new WaitForSeconds(1);
        }
    }

    void MarchCube()
    {
        triangle.Clear();
        Vertices.Clear();

        for (int x = 0; x < Dimensions.x - 1; x++)
        {
            for (int y = 0; y < Dimensions.y - 1; y++)
            {
                for (int z = 0; z < Dimensions.z - 1; z++)
                {
                    float[] Corners = new float[8];
                    for (int i = 0; i < 8; i++)
                    {
                        Vector3Int corner = new Vector3Int(x, y, z) + MarchingTable.Corners[i];
                        Corners[i] = MapPoints[corner.x, corner.y, corner.z];
                    }
                    MarchCube(new Vector3(x,y,z),GetConfigIndex(Corners));
                }
            }
        }
    }

    void MarchCube(Vector3 pos, int configID)
    {
        
    }

    private int GetConfigIndex(float[] corners)
    {
        int index = 0;
        
        for (int i = 0; i < 8; i++)
        {
            if (corners[i] > 0.5f)
            {
                index |= 1 << i;
            }
        }

        return index;
    }

    void SetMesh()
    {
        Mesh mesh = new Mesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDrawGizmos()
    {
        bool b =
            MapPoints.Rank == EmptyMap.Rank &&
            Enumerable.Range(0,MapPoints.Rank).All(dimension => MapPoints.GetLength(dimension) == EmptyMap.GetLength(dimension)) &&
            MapPoints.Cast<double>().SequenceEqual(EmptyMap.Cast<double>());
        if (b || !IsVisible || Dimensions == Vector3Int.zero) // Ensuring that the map has valid map data
        {
            IsVisible = false;
            return;
        }
        
        for (int x = 0; x < Dimensions.x; x++)
        {
            for (int y = 0; y < Dimensions.y; y++)
            {
                for (int z = 0; z < Dimensions.z; z++)
                    if(MapPoints[x,y,z]<=Threshold)
                    {
                        Gizmos.color = new Color(MapPoints[x, y, z],MapPoints[x, y, z],MapPoints[x, y, z]);
                        Gizmos.DrawSphere(new Vector3(x,y,z),0.2f);
                    }
            }
        }
    }
}
