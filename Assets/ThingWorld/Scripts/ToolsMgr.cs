using UnityEngine;

public class ToolsMgr : MonoBehaviour
{
    public void UpdateColor(ThingMgr thing)
    {
        thing.GetComponentInChildren<Renderer>().material.color = Random.ColorHSV();        
    }
}
