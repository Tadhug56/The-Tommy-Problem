using UnityEngine;

// MoveAlongWithGround class that handles moving Instantiated objects along the ground to simulate motion
public class MoveAlongWithGround : MonoBehaviour
{
    public float speed = GroundTile.speed; // Tie speed to whatever speed the ground is moving out

    // Every frame move the object along the ground
    void Update()
    {
        speed = GroundTile.speed; // Update with changes to ground speed

        // Move strictly along x axis
        transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);

        // If hit off the map or passed behind the player destroy the object
        if(transform.position.x > 15f || transform.position.x < -50f)
        {
            Destroy(gameObject); // Destroy
        }
    }
}