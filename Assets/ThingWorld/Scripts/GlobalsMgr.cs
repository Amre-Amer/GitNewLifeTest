using UnityEngine;

public class GlobalsMgr : MonoBehaviour
{
    [HideInInspector] public int numThings;
    [HideInInspector] public float interval;
    [HideInInspector] public float radiusGlobal;
    [HideInInspector] public float radiusNear;
    [HideInInspector] public int maxLineSegs;
    [HideInInspector] public float widthLineSeg;
    [HideInInspector] public float distMin;
    [HideInInspector] public float distMove;
    [HideInInspector] public float distNear;

    void Awake()
    {
        distMove = .1f;
        distNear = 1f;
        maxLineSegs = 100;
        distMin = .001f;
        widthLineSeg = .02f;
        radiusGlobal = 5;
        radiusNear = 1;
        numThings = 5;
        interval = .1f;
    }
}
