using UnityEngine;

public class MoveAlongWithGround : MonoBehaviour
{
    public float speed = 5.0f;

    void Update()
    {
        // Move strictly along world X axis
        transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);

        // Destroy if beyond a certain x position
        if (transform.position.x > 15f)
        {
            Destroy(gameObject);
        }
    }
}