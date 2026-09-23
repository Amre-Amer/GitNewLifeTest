using UnityEngine;
using System.Collections;

public class ThingMgr : MonoBehaviour
{
    public ThingsMgr mgr;

    public int nTarget;
    public float distMove = .1f;
    float distNear = 1f;

    public void InitThing(ThingsMgr mgr0, int n)
    {
        name = "thing " + n;
        mgr = mgr0;
        StartPose();
        nTarget = n - 1;
        mgr.toolsMgr.UpdateColor(this);
        distMove *= 1 - nTarget * .1f;
    }

    void Update()
    {
        UpdatePose();
    }

    public void UpdatePose()
    {
        if (nTarget < 0) return;
        Vector3 pos = mgr.things[nTarget].transform.position;
        transform.LookAt(pos);     
        float dist = Vector3.Distance(transform.position, pos);
        if (dist > distNear)
        {
            transform.Translate(0, 0, distMove);   
        }
    }

    public void StartPose()
    {
        Pose pos = mgr.toolsMgr.GetRandomPose();
        transform.SetPositionAndRotation(pos.position, pos.rotation);
    }
}
