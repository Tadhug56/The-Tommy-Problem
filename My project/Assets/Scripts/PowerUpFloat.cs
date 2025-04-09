using UnityEngine;

public class PowerUpFloat : MonoBehaviour
{
    public float bobSpeed = 2f;
    public float bobHeight = 0.5f;
    public float rotateSpeed = 50f;
    public float moveSpeed = 5f;
    public Vector3 moveDirection = Vector3.right; // Movement axis (e.g., X)

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Movement along X (or any direction), unaffected by rotation
        Vector3 moveOffset = moveDirection.normalized * moveSpeed * Time.deltaTime;
        transform.position += moveOffset;

        // Bobbing in local Y axis, preserving move offset
        float bobOffset = Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, startPos.y + bobOffset, transform.position.z);

        // Rotate around world Y axis
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.World);
    }
}
