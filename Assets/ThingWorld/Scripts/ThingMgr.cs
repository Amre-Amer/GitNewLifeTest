using UnityEngine;
using System.Collections;

public class ThingMgr : MonoBehaviour
{
    public ThingsMgr mgr;
    float duration = 2.0f; // Time in seconds to complete the transition
    float radiusMin = 2;
    float radiusMax = 3;

    void Start()
    {
        InvokeRepeating(nameof(UpdatePose), 0, duration);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void UpdatePose()
    {
        Pose startPose = new Pose(transform.position, transform.rotation);

        Pose pose = GetRandomPose();
        Pose endPose = new Pose(pose.position, pose.rotation);

        StartCoroutine(LerpPoseRoutine(startPose, endPose, duration));

        mgr.toolsMgr.UpdateColor(this);
    }

    Pose GetRandomPose()
    {
        float x = Random.Range(-radiusMin, radiusMax);
        float y = Random.Range(-radiusMin, radiusMax);
        float z = Random.Range(-radiusMin, radiusMax);
        Vector3 pos = new(x, y, z);
        Quaternion rot = Quaternion.Euler(pos * 360);
        Pose pose = new Pose(pos, rot);
        return pose;
    }

    private IEnumerator LerpPoseRoutine(Pose start, Pose end, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration; // Normalize time between 0 and 1

            // 1. Interpolate Position
            Vector3 currentPosition = Vector3.Lerp(start.position, end.position, t);

            // 2. Interpolate Rotation (Slerp is preferable for smoother angular movement)
            Quaternion currentRotation = Quaternion.Slerp(start.rotation, end.rotation, t);

            // 3. Apply to transform
            transform.SetPositionAndRotation(currentPosition, currentRotation);

            yield return null; // Wait for the next frame
        }

        // Ensure it snaps perfectly to the target at the end
        transform.SetPositionAndRotation(end.position, end.rotation);
    }
    
}
