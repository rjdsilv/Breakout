using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breakout.Assets.Scripts.Controller
{
    public class BallController : MonoBehaviour
    {
        // Attribute declaration
        private Rigidbody2D ballRigidBody;
        private Vector2 initialVelocity;

        // Public variables declaration.
        public float speed;

        // Use this for initialization
        void Start()
        {
            ballRigidBody = GetComponent<Rigidbody2D>();
            initialVelocity = new Vector2(0, -1.0f) * speed;
            ballRigidBody.velocity = initialVelocity;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            //ContactPoint2D contactPoint2D = collision.contacts[0];
            //Vector2 ballPosition2D = new Vector2(transform.position.x, transform.position.y);
            //Vector2 ballDirection2D = (contactPoint2D.point - ballPosition2D).normalized;
            //RaycastHit2D hitRay = Physics2D.Raycast(ballPosition2D, ballDirection2D);
            //Vector2 ballReflectionDirection2D = Vector2.Reflect(ballDirection2D, hitRay.normal);
            //ballRigidBody.velocity = ballReflectionDirection2D * speed;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            ballRigidBody.velocity = ballRigidBody.velocity.normalized * speed;
        }
    }
}