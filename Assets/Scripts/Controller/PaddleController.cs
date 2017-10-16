﻿using UnityEngine;

namespace Breakout.Assets.Scripts.Controller
{
    /**
     * Class      : PaddleController
     * Author     : Rodrigo Januario da Silva
     * Version    : 1.0.0
     * Description: This class will handle the control of the paddle for left to right.
     */
    public class PaddleController : MonoBehaviour
    {
        // Constant declaration.
        public const string PADDLE_NAME = "Paddle";

        // Attribute declaration
        private Rigidbody2D paddleRigidBody;

        // Public variables declaration to be on Inspector
        public float speed = 0.0f;

        // Use this for initialization
        void Start()
        {
            paddleRigidBody = GetComponent<Rigidbody2D>();
        }

        // FixedUpdate is like Update but for handling physics.
        void FixedUpdate()
        {
            float horizontal = Input.GetAxis("Horizontal");
            Vector2 horizontalMovement = new Vector2(horizontal, 0);
            paddleRigidBody.velocity = horizontalMovement * speed;
        }

        /**
         * Method     : GetPositionY
         * Return     : The paddle's position on Y axis.
         * Description: This method get the paddle's position on Y axis ans return it.
         */
        public float GetPositionY()
        {
            return transform.position.y;
        }

        /**
         * Method     : GetPositionX
         * Return     : The paddle's position on X axis.
         * Description: This method get the paddle's position on X axis ans return it.
         */
        public float GetPositionX()
        {
            return transform.position.x;
        }
    }
}