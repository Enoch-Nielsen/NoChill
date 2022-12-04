using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public Rigidbody2D rb2d;
    [SerializeField] private Transform imageTransform;
    [SerializeField] private PlayerMove playerMove;

    [SerializeField] private float currentRotation, rotationGoal, rotationLerp;

    [SerializeField] private float maxRotationMove, maxRotationDodge;

    // Update is called once per frame
    void Update()
    {
        if (playerMove.isDodge)
        {
            rotationGoal = maxRotationDodge * (rb2d.velocity.x > 0 ? -1.0f : 1.0f);
        }
        else if (playerMove.isMoving)
        {
            rotationGoal = maxRotationMove * (rb2d.velocity.x > 0 ? -1.0f : 1.0f);
        }
        else
        {
            rotationGoal = 0.0f;
        }

        if (Mathf.Abs(rb2d.velocity.y) > Mathf.Abs(rb2d.velocity.x))
        {
            rotationGoal = 0.0f;
        }
        
        currentRotation = Mathf.Lerp(currentRotation, rotationGoal, Time.deltaTime * rotationLerp);

        imageTransform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, currentRotation));
    }
}
