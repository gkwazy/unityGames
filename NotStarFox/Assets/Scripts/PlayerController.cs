using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General info")]
    [Tooltip("speed of user input")]
    [SerializeField] float controlSpeed = 20f;
    [SerializeField] float xRange = 15f;
    [SerializeField] float yRange = 7f;
    [SerializeField] float positonPitchF = -2f;
    [SerializeField] float controlPitchF = -10f;
    [SerializeField] float postionYawF = 2f;
    [SerializeField] float controlRollF = -30f;
    [SerializeField] GameObject[] laserArray;

    float yThrow;
    float xThrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotaions();
        ProcessFiring();
    }

    private void ProcessRotaions()
    {
        float pitchDueToPostion = transform.localPosition.y * positonPitchF;
        float pitchDueToControlThrow = yThrow * controlPitchF;

        float yawDueToPostion = transform.localPosition.x * postionYawF;

        float rollDueToControlThrow = xThrow * controlRollF;

        float pitch = pitchDueToPostion + pitchDueToControlThrow;
        float yaw =  yawDueToPostion;
        float roll = rollDueToControlThrow;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLaserActive(true);
        }
        else
        {
            SetLaserActive(false);
        }
    }

    private void SetLaserActive(bool input)
    {
        foreach (var laser in laserArray)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = input;
        }
    }
}
