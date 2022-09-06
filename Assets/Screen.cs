using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Screen : MonoBehaviour
{
    public float width;
    public float height;

    [HideInInspector]
    public Vector3 lowerLeft;

    [HideInInspector]
    public Vector3 lowerRight;

    [HideInInspector]
    public Vector3 upperLeft;

    [HideInInspector]
    public Vector3 upperRight;

    [HideInInspector]
    public Matrix4x4 coordinateSystem;

    [HideInInspector]
    public Vector3 up;

    [HideInInspector]
    public Vector3 right;

    [HideInInspector]
    public Vector3 normal;


    private void Awake()
    {
        float halfWidth = width / 2f;
        float halfHeight = height / 2f;

        lowerLeft = transform.TransformPoint(new Vector3(-halfWidth, -halfHeight));
        lowerRight = transform.TransformPoint(new Vector3(halfWidth, -halfHeight));
        upperLeft = transform.TransformPoint(new Vector3(-halfWidth, halfHeight));
        upperRight = transform.TransformPoint(new Vector3(halfWidth, halfHeight));
        
        // assuming the screen is not moving, we can do this just once at start up
        up = (upperLeft - lowerLeft).normalized;
        right = (lowerRight - lowerLeft).normalized;
        normal = - Vector3.Cross(right, up);

        coordinateSystem = Matrix4x4.zero;
        coordinateSystem[0, 0] = right.x;
        coordinateSystem[0, 1] = right.y;
        coordinateSystem[0, 2] = right.z;

        coordinateSystem[1, 0] = up.x;
        coordinateSystem[1, 1] = up.y;
        coordinateSystem[1, 2] = up.z;

        coordinateSystem[2, 0] = normal.x;
        coordinateSystem[2, 1] = normal.y;
        coordinateSystem[2, 2] = normal.z;

        coordinateSystem[3, 3] = 1.0f;
    }

    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(lowerLeft, lowerRight);
        Gizmos.DrawLine(lowerLeft, upperLeft);
        Gizmos.DrawLine(upperRight, lowerRight);
        Gizmos.DrawLine(upperLeft, upperRight);
    }
}
