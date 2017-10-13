using UnityEngine;
using Breakout.Assets.Scripts.Manager;

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
        // Attribute declaration
        private Rigidbody2D paddleRigidBody;

        // Public variables declaration to be on Inspector
        public float speed = 0.0f;
        public BoundaryManager boundaryManager = new BoundaryManager();

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
    }
}