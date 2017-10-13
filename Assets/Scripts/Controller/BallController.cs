using UnityEngine;

namespace Breakout.Assets.Scripts.Controller
{
    public class BallController : MonoBehaviour
    {
        // Constant declaration.
        public const string BALL_NAME = "Ball";

        // Local constants
        private const float throwPositionY = -2f;

        // Attribute declaration
        private GameController gameController;
        private Rigidbody2D ballRigidBody;
        private int hitCount = 0;

        // Public variables declaration.
        public float speed;

        // Use this for initialization
        void Awake()
        {
            gameController = GameObject.Find(GameController.GAME_CONTROLLER_NAME).GetComponent<GameController>();
            ballRigidBody = GetComponent<Rigidbody2D>();
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
            else if (collisionObject.tag == PaddleController.PADDLE_NAME)
            {
                ballRigidBody.velocity = new Vector2(CalculateVelocityX(collision), 1.0f) * speed;
            }
        }

        float CalculateVelocityX(Collision2D collision)
        {
            return (ballRigidBody.position.x - collision.transform.position.x) / collision.collider.bounds.size.x;
        }

        public void ResetPosition(float positionX)
        {
            ballRigidBody.velocity = new Vector2(0, 0);
            transform.SetPositionAndRotation(new Vector3(positionX, throwPositionY, 0), new Quaternion());
        }

        public void Throw(float positionX)
        {
            ballRigidBody.velocity = new Vector2(0, -1.0f) * speed;
        }

        public float GetPositionY()
        {
            return transform.position.y;
        }
    }
}