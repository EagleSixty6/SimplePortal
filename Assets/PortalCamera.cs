using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform portalTransform;

    private Camera portalCamera;
    private Camera playerCamera;
    
    void Awake()
    {
        playerCamera = Camera.main;
        portalCamera = this.GetComponent<Camera>();
        portalCamera.enabled = false;
        
        // to prevent z-fighting cull the TV screen itself
        portalCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("Cull"));
    }

    void Update()
    {
        UpdateClippingPlane();
    }

    private void UpdateClippingPlane()
    {
        Vector3 portalPosCameraRef = playerCamera.worldToCameraMatrix.MultiplyPoint(portalTransform.position);
        Vector3 portalNormalCameraRef = playerCamera.worldToCameraMatrix.MultiplyVector(-portalTransform.up);
        float distToProjection = -Vector3.Dot(portalPosCameraRef, portalNormalCameraRef);
        Vector4 clipPlaneCameraRef = new Vector4(portalNormalCameraRef.x, portalNormalCameraRef.y, portalNormalCameraRef.z, distToProjection);
        portalCamera.projectionMatrix = playerCamera.CalculateObliqueMatrix(clipPlaneCameraRef);
        portalCamera.Render();
    }
}
