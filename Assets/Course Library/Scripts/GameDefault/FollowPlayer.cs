using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3 (0, 12, -25);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Make the camera follow the player's position plus the offset behind it
        transform.position = player.transform.position + player.transform.TransformDirection(offset);

        // Make the camera look forward from the player's rear
        transform.rotation = Quaternion.LookRotation(player.transform.forward, Vector3.up);
    }
}
