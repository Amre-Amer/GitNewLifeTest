using UnityEngine;

public class GlobalsMgr : MonoBehaviour
{
    [HideInInspector] public int numThings;
    [HideInInspector] public float intervalThing;
    [HideInInspector] public float intervalLength;
    [HideInInspector] public float radiusGlobal;
    [HideInInspector] public float radiusNear;
    [HideInInspector] public int maxLineSegs;
    [HideInInspector] public float widthLineSeg;
    [HideInInspector] public float distMin;
    [HideInInspector] public float distMove;
    [HideInInspector] public float distNear;
    [HideInInspector] public float smoothCam;
    [HideInInspector] public float smoothTarget;
    [HideInInspector] public Vector3 offset;

    void Awake()
    {
        distMove = .1f;
        distNear = 1f;
        maxLineSegs = 200;
        distMin = .001f;
        widthLineSeg = .02f;
        radiusGlobal = 5;
        radiusNear = 3;
        numThings = 5;
        intervalLength = .1f;
        intervalThing = 1;
        smoothCam = .01f;
        smoothTarget = .01f;
        offset = Vector3.one * 4;
    }
}
