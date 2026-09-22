using System.Collections.Generic;
using UnityEngine;

public class ThingsMgr : MonoBehaviour
{
    public ToolsMgr toolsMgr;
    public int numThings = 5;
    public GameObject prefabThing;
    List<ThingMgr>things = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        toolsMgr = GetComponent<ToolsMgr>();
        CreateThings();
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
            thing.mgr = this;
            thing.UpdatePose();
            things.Add(thing);
        }
    }
}
