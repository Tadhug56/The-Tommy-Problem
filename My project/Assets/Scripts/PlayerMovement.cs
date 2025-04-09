using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float speed = 5.0f;
    private float moveRange = 4.5f;

    private float input;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        input = Input.GetAxis("Horizontal");

        Vector3 newPosition = transform.position + new Vector3(0f, 0f, input * speed * Time.deltaTime);

        newPosition.z = Mathf.Clamp(newPosition.z, startPosition.z - moveRange, startPosition.z + moveRange);

        transform.position = newPosition;
    }
}
