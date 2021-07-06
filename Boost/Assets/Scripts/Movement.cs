using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float turnThrust = 75f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }


    void ProcessThrust()
    {
       if (Input.GetKey(KeyCode.Space)) {
          rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
       }
    }

    void ProcessRotation()
    {
       if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
       {
       
       }
        
      else if (Input.GetKey(KeyCode.A))
        {
            turnRocket(turnThrust);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            turnRocket(-turnThrust);
        }
    }

    void turnRocket(float rotationPerFrame)
    {
        rb.freezeRotation = true; // this is to freeze so player can rotate. 
        transform.Rotate(Vector3.forward * rotationPerFrame * Time.deltaTime);
        rb.freezeRotation = false; // lets phys system take back over
    }
}

