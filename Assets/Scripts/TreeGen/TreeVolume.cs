using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeVolume : MonoBehaviour
{
    public Vector3[] points;

    protected int pointsNum;

    virtual public void InitPoints()
    {
        points = new Vector3[pointsNum];
    }
}
