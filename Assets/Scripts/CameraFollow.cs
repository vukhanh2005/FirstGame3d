using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;
    public Vector3 lookAtOffset = new Vector3(0, 1.5f, 0); // Nhìn vào thân trên

    [Header("Rotation")]
    public float rotationSpeed = 4f;
    public float pitchMin = -20f;
    public float pitchMax = 60f;

    [Header("Zoom")]
    public float zoomSpeed = 5f;
    public float zoomMin = -2f;
    public float zoomMax = -15f;

    [Header("Smooth")]
    public float smoothTime = 0.05f;

    [Header("Collision")]
    public float collisionRadius = 0.3f;
    public LayerMask collisionMask;

    public float yaw = 0f;
    public float pitch = 20f;
    public float targetZoom = -8f;
    public float currentZoom = -8f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 desiredPos;

    void Start()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                target = player.transform;
        }

        currentZoom = targetZoom;
    }

    void Update()
    {
        HandleInput();
    }

    void LateUpdate()
    {
        UpdateCameraPosition();
    }

    void HandleInput()
    {
        if (Mouse.current == null) return;

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        yaw += mouseDelta.x * rotationSpeed * Time.deltaTime;
        pitch -= mouseDelta.y * rotationSpeed * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        float scroll = Mouse.current.scroll.ReadValue().y;
        targetZoom += scroll * zoomSpeed * Time.deltaTime;
        targetZoom = Mathf.Clamp(targetZoom, zoomMax, zoomMin);
        currentZoom = Mathf.Lerp(currentZoom, targetZoom, Time.deltaTime * 10f);
    }
    Vector3 offset;
    Vector3 castOrigin;
    void UpdateCameraPosition()
    {
        if (target == null) return;

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        offset = rotation * new Vector3(0, 0, currentZoom);
        castOrigin = target.position + lookAtOffset;
        desiredPos = castOrigin + offset;

        // Check collision
        RaycastHit hit;
        if (Physics.SphereCast(castOrigin, collisionRadius, offset.normalized, out hit, offset.magnitude, collisionMask))
        {
            desiredPos = castOrigin + offset.normalized * (hit.distance - 0.1f);
        }

        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, smoothTime);
        transform.LookAt(castOrigin);
    }
    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        // Các tham số bạn đang dùng
        Vector3 origin = castOrigin; // điểm bắt đầu
        Vector3 direction = offset.normalized;
        float radius = collisionRadius;
        float distance = offset.magnitude;

        // Màu vẽ
        Gizmos.color = Color.green;

        // Vẽ đường đi
        Gizmos.DrawLine(origin, origin + direction * distance);

        // Vẽ quả cầu ở đầu (vị trí bắt đầu)
        Gizmos.DrawWireSphere(origin, radius);

        // Vẽ quả cầu ở cuối (nếu không va chạm)
        Gizmos.DrawWireSphere(origin + direction * distance, radius);
    }

}
