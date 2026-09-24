using UnityEngine;
using Unity.Properties; // Required for Unity 6 runtime binding discovery

public class TimerMgr : MonoBehaviour
{
    [CreateProperty] public int currentHealth { get; set; } = 85;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth++;
    }
}
