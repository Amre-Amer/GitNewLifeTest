using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HistoryMgr : MonoBehaviour
{
    ThingsMgr mgr;
    public GameObject prefabLineSeg;
    List<GameObject>lineSegs = new();
    [HideInInspector] public List<Vector3>lineSegPoints = new();
    int maxLineSegs;
    GameObject parentLineSegs;
    float widthLineSeg;
    [HideInInspector] public float distMin;

    void Awake()
    {
        maxLineSegs = 100;
        distMin = .001f;
        widthLineSeg = .02f;
        mgr = GetComponent<ThingsMgr>();
        CreateParentLineSegs();
    }

    void CreateParentLineSegs()
    {
        parentLineSegs = new GameObject("parentLineSegs");
        parentLineSegs.transform.SetParent(transform);
    }

    GameObject CreateLineSeg(Vector3 posFrom, Vector3 posTo)
    {
        float dist = Vector3.Distance(posFrom, posTo);
        GameObject lineSeg = Instantiate(prefabLineSeg, parentLineSegs.transform);
        lineSeg.transform.position = (posFrom + posTo) / 2;
        lineSeg.transform.LookAt(posTo);
        lineSeg.transform.localScale = new (widthLineSeg, widthLineSeg, dist);
        return lineSeg;
    }

    public void AddLineSegPoint(Vector3 pos)
    {
        if (lineSegs.Count > maxLineSegs) {
            Debug.Log("max reached " + maxLineSegs);
            return;
        }
        // first lineSeg is null
        lineSegPoints.Add(pos);
        GameObject lineSeg = null;
        if (lineSegPoints.Count > 1)
        {
            Vector3 posLast = lineSegPoints[lineSegPoints.Count - 2];
            lineSeg = CreateLineSeg(posLast, pos); 
        }
        lineSegs.Add(lineSeg);
        parentLineSegs.name = "parentLineSegs " + parentLineSegs.transform.childCount;
    }
}
