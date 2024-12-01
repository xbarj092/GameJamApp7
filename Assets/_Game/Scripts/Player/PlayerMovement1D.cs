using System;
using UnityEngine;

public class PlayerMovement1D : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 5f;
    float input;

    void Update()
    {
        input = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        MoveOnLine(input);

    }

    private void MoveOnLine(float input)
    {
        Vector3 direction = (pointB.position - pointA.position).normalized;

        Vector3 movement = direction * input * moveSpeed * Time.deltaTime;

        Vector3 targetPosition = transform.position + movement;

        Vector3 lineDirection = (pointB.position - pointA.position).normalized;
        Vector3 pointToTarget = targetPosition - pointA.position;
        float projection = Vector3.Dot(pointToTarget, lineDirection);
        targetPosition = pointA.position + lineDirection * projection;
        //if (targetPosition.x >= pointA.position.x - 1 && targetPosition.x <= pointB.position.x + 1){}
        transform.position = new Vector3(targetPosition.x,0,0);
    }
}
