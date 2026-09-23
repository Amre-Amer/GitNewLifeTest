using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ThingsMgr : MonoBehaviour
{
    public GlobalsMgr g;
    public ToolsMgr toolsMgr;
    public HistoryMgr historyMgr;
    public GameObject prefabThing;
    public List<ThingMgr>things = new();

    void Start()
    {
        CreateThings();
        InvokeRepeating(nameof(UpdateThing), 1, g.interval);
    }

    void UpdateThing()
    {
        ThingMgr thing = things[0];
        Pose pose = toolsMgr.GetRandomPoseNear(thing);
        thing.transform.SetPositionAndRotation(pose.position, pose.rotation);
        if (historyMgr.lineSegs.Count == 0)
        {
            historyMgr.AddLineSeg(pose.position);
        } else
        {
            float dist = Vector3.Distance(historyMgr.lineSegs.Last().transform.position, pose.position);
            if (dist >= g.distMin)
            {
                historyMgr.AddLineSeg(pose.position);
            }
        }
    }

    ThingMgr CreateThing()
    {
        ThingMgr thing = Instantiate(prefabThing, transform).GetComponent<ThingMgr>();
        return thing;
    }

    void CreateThings()
    {
        for(int n = 0; n < g.numThings; n++)
        {
            ThingMgr thing = CreateThing();
            thing.InitThing(this, n);
            things.Add(thing);
        }
    }
}
