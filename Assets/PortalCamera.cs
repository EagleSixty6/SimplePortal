using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Screen projectionScreen;

    private void LateUpdate()
    {
        Vector3 scr_right = projectionScreen.right;
        Vector3 scr_up = projectionScreen.up;
        Vector3 scr_normal = projectionScreen.normal;
        Matrix4x4 screenKS = projectionScreen.coordinateSystem;
        Vector3 headPos = transform.position;

        // from eye to projection screen corners
        Vector3 headToLowerLeft = projectionScreen.lowerLeft - headPos;
        Vector3 headToLowerRight = projectionScreen.lowerRight - headPos;
        Vector3 headToUpperLeft = projectionScreen.upperLeft - headPos;
        Vector3 headToUpperRight = projectionScreen.upperRight - headPos;

        //distance from eye to projection screen plane
        float headScreenDistance =  - Vector3.Dot(headToLowerLeft, scr_normal);
        Camera.main.nearClipPlane = headScreenDistance;
        
        float nearPlane = Camera.main.nearClipPlane;
        float farPlane = Camera.main.farClipPlane;

        // float compression = nearPlane / headScreenDistance;
        //float l = Vector3.Dot(scr_right, headToLowerLeft) * compression;
        //float r = Vector3.Dot(scr_right, headToLowerRight) * compression;
        //float b = Vector3.Dot(scr_up, headToLowerLeft) * compression;
        //float t = Vector3.Dot(scr_up, headToUpperLeft) * compression;

        float l = Vector3.Dot(scr_right, headToLowerLeft);
        float r = Vector3.Dot(scr_right, headToLowerRight);
        float b = Vector3.Dot(scr_up, headToLowerLeft);
        float t = Vector3.Dot(scr_up, headToUpperLeft);

        Matrix4x4 projectionMatrix = Matrix4x4.Frustum(l, r, b, t, nearPlane, farPlane);

        // translation to eye position
        Matrix4x4 inverseHeadTranslate = Matrix4x4.Translate(-headPos);
        Matrix4x4 screenRotation = Matrix4x4.Rotate(Quaternion.Inverse(transform.rotation) * projectionScreen.transform.rotation);
        Camera.main.worldToCameraMatrix = screenKS * screenRotation * inverseHeadTranslate;

        Camera.main.projectionMatrix = projectionMatrix;
    }




}
