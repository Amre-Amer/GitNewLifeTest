using System;
using System.Collections.Generic;
using UnityEngine;

public class ThingsMgr : MonoBehaviour
{
    public int numThings = 5;
    public GameObject prefabThing;
    List<ThingMgr>things = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateThings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Pose GetStartPose()
    {
        Vector3 pos = UnityEngine.Random.insideUnitSphere;
        Quaternion rot = Quaternion.Euler(pos * 360);
        Pose pose = new Pose(pos, rot);
        return pose;
    }
    ThingMgr CreateThing()
    {
        ThingMgr thing = Instantiate(prefabThing, transform).GetComponent<ThingMgr>();
        Pose pose = GetStartPose();
        thing.transform.SetPositionAndRotation(pose.position, pose.rotation);
        return thing;
    }

    void CreateThings()
    {
        for(int n = 0; n < numThings; n++)
        {
            ThingMgr thing = CreateThing();
            things.Add(thing);
        }
    }
}
