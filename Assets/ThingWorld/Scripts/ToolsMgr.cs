using UnityEngine;

public class ToolsMgr : MonoBehaviour
{
    float radiusGlobal;
    float radiusNear;

    void Awake()
    {
        radiusGlobal = 5;
        radiusNear = 1;
    }

    public void UpdateColor(ThingMgr thing)
    {
        thing.GetComponentInChildren<Renderer>().material.color = Random.ColorHSV();        
    }

    public Pose GetRandomPoseNear(ThingMgr thing)
    {
        Vector3 pos = thing.transform.position;
        pos += Random.insideUnitSphere * radiusNear;
        Quaternion rot = Quaternion.Euler(pos * 360);
        Pose pose = new Pose(pos, rot);
        return pose;
    }

    public Pose GetRandomPose()
    {
        Vector3 pos = Random.insideUnitSphere * radiusGlobal;
        Quaternion rot = Quaternion.Euler(pos * 360);
        Pose pose = new Pose(pos, rot);
        return pose;
    }
}
