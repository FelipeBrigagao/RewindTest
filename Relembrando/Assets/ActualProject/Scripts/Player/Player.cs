using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    float horizontalInput;
    float verticalInput;
   
    Rigidbody rb;

    Movement mov;

    public Transform pickUpPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

    }


    private void FixedUpdate()
    {
        mov = new Movement(rb, horizontalInput, verticalInput);
        mov.Execute();

        if (ReplayManager.Instance.record)
        {
            ReplayManager.Instance.AddMovement(mov);
        }


    }

}
