using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;
    void Start()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }
    void LateUpdate()
    {
        // Tính vị trí đích muốn camera đến
        Vector3 targetPosition = target.position + offset;

        // Di chuyển camera mượt tới vị trí đích
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
