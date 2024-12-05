using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Vector3Int Dimensions = new Vector3Int(0,0,0);
    public float[,,] MapPoints = new float[0,0,0];

    public bool IsVisible = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDrawGizmos()
    {
        if(!IsVisible || Dimensions == Vector3Int.zero )
            return;
        
        for (int x = 0; x < Dimensions.x; x++)
        {
            for (int y = 0; y < Dimensions.y; y++)
            {
                for (int z = 0; z < Dimensions.z; z++)
                {
                    Gizmos.color = new Color(MapPoints[x, y, z],MapPoints[x, y, z],MapPoints[x, y, z]);
                    Gizmos.DrawSphere(new Vector3(x,y,z),0.2f);
                }
            }
        }
    }
}
