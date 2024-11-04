using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public float movementSpeed;
    public float angularSpeed;

    public float angularModifier;

    public Vector2 movementInput => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    public Vector2 movementInputChecker;
    public Rigidbody rb;

    public float movementAccelUp;
    public float movementAccelDown;

    float desiredSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        float rotationRate = angularSpeed * movementInput.x * Time.deltaTime;
        rotationRate *= movementInput.y != 0 ? 1 : angularModifier;
        transform.Rotate(Vector3.up, rotationRate);

        movementInputChecker = movementInput;

        desiredSpeed = Mathf.MoveTowards(desiredSpeed, movementInput.y * movementSpeed, (movementInput.y != 0 ? movementAccelUp : movementAccelDown) * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Vector3 velocity = rb.velocity;
        //velocity = Vector3.MoveTowards(velocity, transform.forward * movementInput.y * movementSpeed, movementAccel * Time.fixedDeltaTime);
        velocity = transform.forward * desiredSpeed;
        rb.velocity = velocity;
    }

    private void OnDrawGizmos()
    {
        if (rb == null)
            return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + rb.velocity);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * movementInput.y * movementSpeed);
    }
}
