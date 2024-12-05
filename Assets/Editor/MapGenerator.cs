using UnityEditor;
using UnityEngine;

public class MapGenerator : EditorWindow
{
    private Vector3Int MapDimensions = new Vector3Int(10,10,10);
    private int MapSeed = 136442497;
    private float MapThreshold = 0.5f;
    private int MapScale = 5;
    private float PointOffset = 5;
    private Map GeneratedMap;
    private Vector2 MapOffset;

    [MenuItem("Component/Tool/Map generator")]
    public static void ShowWindow()
    {
        GetWindow(typeof(MapGenerator));
    }

    private void GenerateMap()
    {
        GeneratedMap.Dimensions = MapDimensions;
        GeneratedMap.MapPoints = new float[MapDimensions.x + 1, MapDimensions.y + 1, MapDimensions.z + 1];
        for (int x = 0; x < MapDimensions.x; x++)
        {
            for (int y = 0; y < MapDimensions.y; y++)
            {
                for (int z = 0; z < MapDimensions.z; z++)
                {
                    float crtHeight = MapDimensions.z * Mathf.PerlinNoise((float)x / MapScale + MapOffset.x,
                                                                          (float)z /  MapScale+MapOffset.y);
                    if (y>crtHeight)
                    {
                        GeneratedMap.MapPoints[x,y,z] = y - crtHeight;
                    }
                    else
                    {
                        GeneratedMap.MapPoints[x,y,z] = crtHeight - y;
                    }
                }
            }
        }
    }

    private void OnGUI()
    {
        MapDimensions = EditorGUILayout.Vector3IntField("Dimensions", MapDimensions);
        MapSeed = EditorGUILayout.IntField("Seed", MapSeed);
        MapThreshold = EditorGUILayout.Slider("Threshold", MapThreshold, .0f, 1f);
        MapScale = EditorGUILayout.IntField("Scale", MapScale);
        PointOffset = EditorGUILayout.FloatField("Point offset", PointOffset);
        GeneratedMap = EditorGUILayout.ObjectField("Map", GeneratedMap, typeof(Map), true) as Map;
        
        if (GUILayout.Button("Generate map"))
        {
            Random.InitState(MapSeed);
            MapOffset = new Vector2(Random.Range(0, 999999f), Random.Range(0, 999999f));
            if (MapDimensions.x <= 0 || MapDimensions.y <= 0 || MapDimensions.z <= 0)
            {
                Debug.Log("<color=red>WARNING: All map dimensions must be higher than 0</color>");
                return;
            }

            if (!GeneratedMap)
            {
                Debug.Log("<color=red>WARNING: Map is not assigned</color>");
                return;
            }
            GenerateMap();
        }
    }
}
