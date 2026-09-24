using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.PlayerLoop;

public class ThingsMgr : MonoBehaviour
{
    public GlobalsMgr g;
    public ToolsMgr toolsMgr;
    public HistoryMgr historyMgr;
    public GameObject prefabThing;
    public List<ThingMgr>things = new();

    public ThingsSO soUI;

    public SimpleUIController ui;
    Vector3 posTarget = Vector3.zero;
    Vector3 eulTarget = Vector3.zero;
    Pose poseTarget;
    ThingMgr head;

    void Start()
    {
        CreateThings();
        InvokeRepeating(nameof(UpdateThing), 1, g.intervalThing);
        InvokeRepeating(nameof(UpdateLength), g.intervalLength, g.intervalLength);
    }

    void UpdateLength()
    {
        soUI.Health = historyMgr.GetLength();
        // Debug.Log("Test: " + soUI.Health);
        ui.SetLabel(soUI.Health.ToString());
    }

    void Update()
    {
        posTarget = g.smoothTarget * poseTarget.position + (1 - g.smoothTarget) * posTarget;
        // eulTarget = g.smoothTarget * poseTarget.rotation.eulerAngles + (1 - g.smoothTarget) * eulTarget;
        head.transform.SetPositionAndRotation(posTarget, Quaternion.Euler(eulTarget));
        head.transform.position = posTarget;
        head.transform.LookAt(poseTarget.position);
    }

    void UpdateThing()
    {
        // head = things[0];
        poseTarget = toolsMgr.GetRandomPoseNear(head);
        if (historyMgr.lineSegs.Count == 0)
        {
            historyMgr.AddLineSeg(posTarget);
        } else
        {
            float dist = Vector3.Distance(historyMgr.lineSegs.Last().transform.position, posTarget);
            if (dist >= g.distMin)
            {
                historyMgr.AddLineSeg(posTarget);
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
        head = things[0];
    }
}
