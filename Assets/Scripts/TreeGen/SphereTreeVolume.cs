using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereTreeVolume : TreeVolume
{
    [SerializeField]
    private float radius = 4f;

    override public void InitPoints() 
    {
        points = new Vector3[pointsNum];
        
        for (int i = 0; i < pointsNum; ++i) 
        {
            Vector3 dir = Random.insideUnitSphere.normalized;

            points[i] = dir * RNG.Rand(0f, radius);
        }
    }
}
