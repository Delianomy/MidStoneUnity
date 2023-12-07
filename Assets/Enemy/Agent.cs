using UnityEngine;
using System.Collections;
using UnityEngine.XR;
using System;

public class Agent : MonoBehaviour
{
    public float maxSpeed;
    public float maxAccel;
    public float maxRotation;
    public float maxAngularAccel;
    public float orientation;
    public float rotation;
    public Vector3 velocity;
    public Steering goodPath;
    private Rigidbody rb;
    // Handles the movements


    void Start()
    {
        velocity = Vector3.zero;
        goodPath = new Steering();
        // Instantiate the Object for movement

    }

    public virtual void Update()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = velocity;
        Vector3 displacement = velocity * Time.deltaTime;
        orientation += rotation * Time.deltaTime;

        if (orientation < 0.0f)
            orientation += 360.0f;
        else if (orientation > 360.0f)
            orientation -= 360.0f;

        transform.LookAt(transform.position + velocity);

        orientation = transform.rotation.eulerAngles.y;

        //transform.Translate(displacement, Space.World);
        transform.rotation = new Quaternion();
        transform.Rotate(Vector3.up, orientation);
        ;
    }



    public virtual void LateUpdate()
    {
        // Update the movement using steering for linear and angular movement
        // Use steering to update the rotation
        // Use steering to update the velocity

        velocity += goodPath.linear * Time.deltaTime;
        rotation += goodPath.angular * Time.deltaTime;

        if (velocity.magnitude > maxSpeed)
        {
            velocity.Normalize();
            velocity *= maxSpeed;
        }

        // Reset the steering for the next update
        goodPath = new Steering();
    }

    public void SetSteering(Steering steering)
    {
        // Assign the steering reference
        goodPath = steering;

    }
}