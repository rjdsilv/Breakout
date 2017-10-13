using UnityEngine;

namespace Breakout.Assets.Scripts.Controller
{
    public class BallController : MonoBehaviour
    {
        // Attribute declaration
        private GameController gameController;
        private Rigidbody2D ballRigidBody;
        private Vector2 initialVelocity;
        private int hitCount = 0;

        // Public variables declaration.
        public float speed;

        // Use this for initialization
        void Start()
        {
            gameController = GameObject.Find(GameController.GAME_CONTROLLER_NAME).GetComponent<GameController>();
            ballRigidBody = GetComponent<Rigidbody2D>();
            initialVelocity = new Vector2(0, -1.0f) * speed;
            ballRigidBody.velocity = initialVelocity;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            GameObject collisionObject = collision.gameObject;
            if (collisionObject.tag == BrickController.BRICK_TAG)
            {
                hitCount++;
                gameController.IncreaseAndShowScore(collisionObject.GetComponent<BrickController>().pointsWorth);
                Destroy(collision.gameObject);
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            ballRigidBody.velocity = ballRigidBody.velocity.normalized * speed;
        }
    }
}