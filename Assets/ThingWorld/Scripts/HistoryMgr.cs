using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HistoryMgr : MonoBehaviour
{
    public ThingsMgr mgr;
    public GameObject prefabLineSeg;
    [HideInInspector] public List<GameObject>lineSegs = new();
    GameObject parentLineSegs;

    void Awake()
    {
        CreateParentLineSegs();
    }

    void CreateParentLineSegs()
    {
        parentLineSegs = new GameObject("parentLineSegs");
        parentLineSegs.transform.SetParent(transform);
    }

    GameObject CreateLineSeg()
    {
        GameObject lineSeg = Instantiate(prefabLineSeg, parentLineSegs.transform);
        return lineSeg;
    }

    void UpdateLineSeg(GameObject lineSeg, Vector3 posFrom, Vector3 posTo)
    {
        float dist = Vector3.Distance(posFrom, posTo);
        lineSeg.transform.position = posFrom;
        lineSeg.transform.LookAt(posTo);
        lineSeg.transform.localScale = new (mgr.g.widthLineSeg, mgr.g.widthLineSeg, dist);
    }

    public void AddLineSeg(Vector3 pos)
    {
        if (lineSegs.Count == mgr.g.maxLineSegs) {
            ScrollLineSegPoints();
        } else
        {
            GameObject lineSeg = CreateLineSeg(); 
            lineSegs.Add(lineSeg);
        }
        lineSegs.Last().transform.position = pos;
        UpdateLineSeg(lineSegs.Last(), pos, pos);
        if (lineSegs.Count > 1)
        {
            int nLast = lineSegs.Count - 1;
            int nLastLast = nLast - 1;
            GameObject lineSegLastLast = lineSegs[nLastLast];
            Vector3 posLastLast = lineSegLastLast.transform.position;
            UpdateLineSeg(lineSegLastLast, posLastLast, pos);
        }
        parentLineSegs.name = "parentLineSegs " + parentLineSegs.transform.childCount;
    }
 
    void ScrollLineSegPoints()
    {
        for(int n = 0; n < lineSegs.Count - 1; n++)
        {            
            GameObject lineSeg0 = lineSegs[n];
            GameObject lineSeg1 = lineSegs[n + 1]; 
            lineSeg0.transform.SetPositionAndRotation(lineSeg1.transform.position, lineSeg1.transform.rotation);
        }
     }
}
