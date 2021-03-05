using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolow : MonoBehaviour
{
    Transform player;
    
    [SerializeField]
    Vector3 offset;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player != null)
        {
            transform.position = player.position + offset;
            transform.LookAt(player);

        }
    }
}
