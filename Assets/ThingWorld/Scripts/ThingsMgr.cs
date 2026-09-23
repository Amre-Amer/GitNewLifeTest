using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ThingsMgr : MonoBehaviour
{
    [HideInInspector] public ToolsMgr toolsMgr;
    [HideInInspector] public HistoryMgr historyMgr;
    public int numThings;
    public GameObject prefabThing;
    public List<ThingMgr>things = new();

    void Awake()
    {
        numThings = 5;
    }

    void Start()
    {
        toolsMgr = GetComponent<ToolsMgr>();
        historyMgr = GetComponent<HistoryMgr>();
        CreateThings();
        InvokeRepeating(nameof(UpdateThing), 1, .1f);
    }

    void UpdateThing()
    {
        ThingMgr thing = things[0];
        Pose pose = toolsMgr.GetRandomPoseNear(thing);
        thing.transform.SetPositionAndRotation(pose.position, pose.rotation);
        if (historyMgr.lineSegPoints.Count == 0)
        {
            historyMgr.AddLineSegPoint(pose.position);
        } else
        {
            float dist = Vector3.Distance(historyMgr.lineSegPoints.Last(), pose.position);
            if (dist >= historyMgr.distMin)
            {
                historyMgr.AddLineSegPoint(pose.position);
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
        for(int n = 0; n < numThings; n++)
        {
            ThingMgr thing = CreateThing();
            thing.InitThing(this, n);
            things.Add(thing);
        }
    }
}
