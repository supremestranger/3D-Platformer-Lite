using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerComponent
{
    public Transform playerTransform;
    public Rigidbody playerRB;
    public CapsuleCollider playerCollider;
    public Vector3 playerVelocity;
    public float playerJumpForce;
    public float playerSpeed;
    public int coins;
}
