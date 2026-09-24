using UnityEngine;

public class CamMgr : MonoBehaviour
{
    public Camera cam;
    public Camera camA;
    public Camera camB;
    public Camera camC;
    public Transform poseA;
    public Transform poseB;
    public float lerpDuration = 3.0f;

    private float elapsedTime = 0f;
    private bool isLerping = false;
    private bool goToPoseB = true;

    private bool ynZoom;
    private Vector3 posZoomFrom;
    private Vector3 posZoomTo;
    private float zoomDist = .25f;
    private float durationZoom = 1;
    CamType camType = CamType.Main;

    private void Start()
    {
        SetCam(CamType.Main);
        SetCam2D();
    }
    
    public void SetCam(CamType camType0)
    {
        camType = camType0;
        cam.enabled = false;
        camA.enabled = false;
        camB.enabled = false;
        camC.enabled = false;
        switch (camType)
        {
            case CamType.Main:
                cam.enabled = true;
                cam.rect = new Rect(0.0f, 0.0f, 1f, 1.0f);
                break;
            case CamType.A:
                camA.enabled = true;
                camA.rect = new Rect(0.0f, 0.0f, 1f, 1.0f);
                break;
            case CamType.B:
                camB.enabled = true;
                camB.rect = new Rect(0.0f, 0.0f, 1f, 1.0f);
                break;
            case CamType.C:
                camC.enabled = true;
                camC.rect = new Rect(0.0f, 0.0f, 1f, 1.0f);
                break;
            case CamType.Split:
                cam.enabled = true;
                cam.rect = new Rect(0.0f, 0.0f, .5f, .5f);
                camA.enabled = true;
                camA.rect = new Rect(0.0f, 0.5f, .5f, .5f);
                camB.enabled = true;
                camB.rect = new Rect(0.5f, 0.5f, .5f, .5f);
                camC.enabled = true;
                camC.rect = new Rect(0.5f, 0.0f, .5f, .5f);
                break;
            case CamType.Reset:
                cam.enabled = true;
                cam.rect = new Rect(0.0f, 0.0f, 1f, 1.0f);
                SetCam2D();
                if (ynZoom) ZoomOut();
                break;
        }
    }

    public void SetCam3D()
    {
        elapsedTime = 0f;
        goToPoseB = true;
        isLerping = true;
    }
    
    public void SetCam2D()
    {
        elapsedTime = 0f;
        goToPoseB = false;
        isLerping = true;
    }

    public void ZoomIn()
    {
        elapsedTime = 0f;
        posZoomFrom = cam.transform.position;
        posZoomTo = cam.transform.TransformPoint(0, 0, zoomDist);
        ynZoom = true;
    }

    public void ZoomOut()
    {
        elapsedTime = 0f;
        posZoomFrom = cam.transform.position;
        posZoomTo = cam.transform.TransformPoint(0, 0, -zoomDist);
        ynZoom = true;
    }

    void Update()
    {
        UpdateRotate();
        UpdateZoom();
    }

    void UpdateZoom()
    {
        if (!ynZoom) return;
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / durationZoom;
        cam.transform.position = Vector3.Lerp(posZoomFrom, posZoomTo, percentageComplete);
        if (percentageComplete >= 1.0f)
        {
            ynZoom = false;
        }
    }
    
    void UpdateRotate()
    {
        
        if (isLerping)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / lerpDuration;
            if (goToPoseB)
            {
                transform.position = Vector3.Lerp(poseA.position, poseB.position, percentageComplete);
                transform.rotation = Quaternion.Lerp(poseA.rotation, poseB.rotation, percentageComplete);
            }
            else
            {
                transform.position = Vector3.Lerp(poseB.position, poseA.position, percentageComplete);
                transform.rotation = Quaternion.Lerp(poseB.rotation, poseA.rotation, percentageComplete);
            }

            if (percentageComplete >= 1.0f)
            {
                isLerping = false;
            }
        }
    }
}

public enum CamType
{
    Main,
    A,
    B,
    C,
    Split,
    Reset
}
