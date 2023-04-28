using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform target; // The object to follow
    public float smoothing = 5f; // The speed of camera movement

    Vector3 offset; // The distance between the camera and the target

    void Start()
    {
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, 
        targetCamPos, smoothing * Time.deltaTime);
    }
}
