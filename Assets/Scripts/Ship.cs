using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines Spaceship Behaviour & Implements Screen Wrap
/// </summary>
public class Ship : MonoBehaviour
{
    Rigidbody2D rigidBody;
    float colliderHalfRadius;
    Vector2 thrustDirection = new Vector2(1,0);
    const float ThrustForce = 5;
    const float RotateDegreesPerSecond = 45;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
        colliderHalfRadius = circleCollider.radius / 2;
    }

    // Update is called every frame
    private void Update()
    {
        // Rotate the Ship
        float rotationInput = Input.GetAxis("Rotate");
        // calculate rotation amount and apply rotation
        if (rotationInput != 0) 
        {
            float rotationAngle = transform.eulerAngles.z;
            rotationAngle *= Mathf.Deg2Rad;
            thrustDirection.x = Mathf.Cos(rotationAngle);
            thrustDirection.y = Mathf.Sin(rotationAngle);

            float rotationAmount = RotateDegreesPerSecond * Time.deltaTime;
            if (rotationInput < 0)
            {
                rotationAmount *= -1;
            }
            transform.Rotate(transform.forward, rotationAmount);
        }
    }

    void FixedUpdate()
    {
        // Apply Force to the Spaceship
        float thrustInput = Input.GetAxis("Thrust");
        if(thrustInput != 0)
        {
            rigidBody.AddForce(ThrustForce * transform.right , ForceMode2D.Force);
        }
    }

    // Implement the Screen Wrap
    private void OnBecameInvisible()
    {
        Vector2 shipPosition = transform.position;

        if (shipPosition.x + colliderHalfRadius < ScreenUtils.ScreenLeft)
        {
            shipPosition.x *= -1;
        }
        else if (shipPosition.x - colliderHalfRadius > ScreenUtils.ScreenRight)
        {
            shipPosition.x *= -1;
        }

        if (shipPosition.y + colliderHalfRadius < ScreenUtils.ScreenBottom)
        {
            shipPosition.y *= -1;
        }
        else if (shipPosition.y - colliderHalfRadius > ScreenUtils.ScreenTop)
        {
            shipPosition.y *= -1;
        }
        transform.position = shipPosition;
    }
}
