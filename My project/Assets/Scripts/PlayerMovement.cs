using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float speed = 5.0f;
    private float moveRange = 4.5f;

    private float input;
    private Vector3 startPosition;
    private Player playerScript;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        playerScript = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerScript.playable)
        {
            input = Input.GetAxis("Horizontal");

            Vector3 newPosition = transform.position + new Vector3(0f, 0f, input * speed * Time.deltaTime);

            newPosition.z = Mathf.Clamp(newPosition.z, startPosition.z - moveRange, startPosition.z + moveRange);

            transform.position = newPosition;
        }
    }
}
