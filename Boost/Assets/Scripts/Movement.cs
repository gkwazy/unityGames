using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float turnThrust = 75f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainThrustParticle;
    [SerializeField] ParticleSystem leftThrustParticle;
    [SerializeField] ParticleSystem rightThrustParticle;

    AudioSource audioSource;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }


    void ProcessThrust()
    {
       if (Input.GetKey(KeyCode.Space))
        {
            StartThrust();
        }

        else
        {
            StopThrust();
        }
    }

    void ProcessRotation()
        {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
        
        }
            
        else if (Input.GetKey(KeyCode.A))
            {
                rotateLeft();
            }

            else if (Input.GetKey(KeyCode.D))
            {
                rotateRight();

            }
            else
            {
                StopRotate();
            }
        }
    

    private void StartThrust()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainThrustParticle.isPlaying)
        {
            mainThrustParticle.Play();
        }
    }

    private void StopThrust()
    {
        audioSource.Stop();
        mainThrustParticle.Stop();
    }

    private void rotateLeft()
    {
        turnRocket(turnThrust);
        if (!rightThrustParticle.isPlaying)
        {
            rightThrustParticle.Play();
        }
    }

    private void rotateRight()
    {
        turnRocket(-turnThrust);
        if (!leftThrustParticle.isPlaying)
        {
            leftThrustParticle.Play();
        }
    }

    private void StopRotate()
    {
        rightThrustParticle.Stop();
        leftThrustParticle.Stop();
    }

    void turnRocket(float rotationPerFrame)
    {
        rb.freezeRotation = true; // this is to freeze so player can rotate. 
        transform.Rotate(Vector3.forward * rotationPerFrame * Time.deltaTime);
        rb.freezeRotation = false; // lets phys system take back over
    }
}

