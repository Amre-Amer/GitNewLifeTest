using UnityEngine;

public class ToolsMgr : MonoBehaviour
{
    float radius = 5;

    public void UpdateColor(ThingMgr thing)
    {
        thing.GetComponentInChildren<Renderer>().material.color = Random.ColorHSV();        
    }

    public Pose GetRandomPose()
    {
        Vector3 pos = Random.insideUnitSphere * radius;
        Quaternion rot = Quaternion.Euler(pos * 360);
        Pose pose = new Pose(pos, rot);
        return pose;
    }
}
