using UnityEngine;

public class ThingMgr : MonoBehaviour
{
    public ThingsMgr mgr;

    public int nTarget;

    public void InitThing(ThingsMgr mgrNew, int n)
    {
        name = "thing " + n;
        mgr = mgrNew;
        StartPose();
        nTarget = n - 1;
        mgr.toolsMgr.UpdateColor(this);
        mgr.g.distMove *= 1 - nTarget * .1f;
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
        if (dist > mgr.g.distNear)
        {
            transform.Translate(0, 0, mgr.g.distMove);   
        }
    }

    public void StartPose()
    {
        Pose pos = mgr.toolsMgr.GetRandomPose();
        transform.SetPositionAndRotation(pos.position, pos.rotation);
    }
}
