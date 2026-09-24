using UnityEngine;

public class CameraMgr : MonoBehaviour
{
    public ThingsMgr mgr;
    ThingMgr target;
    Camera cam;
    Vector3 posCam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        UpdateCam();
    }

    void UpdateCam()
    {
        if (target == null && mgr.things.Count > 0)
        {
            target = mgr.things[0];
        }
        if (target)
        {
            Vector3 posNew = target.transform.position;
            posCam = mgr.g.smoothCam * posNew + (1 - mgr.g.smoothCam) * posCam;
            cam.transform.position = posCam + mgr.g.offset;
            cam.transform.LookAt(posCam);        
        }
    }
}
