using UnityEngine;

namespace Breakout.Assets.Scripts.Controller
{
    /**
     * Class      : BallController
     * Description: This class will have the ball control programed in order to make fly arround the screen with
     *              properly calculated velocity.
     * Author     : Rodrigo Januario da Silva
     * Version    : 1.0
     */
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

        // The ball is currently colliding with an object in the scene.
        void OnCollisionEnter2D(Collision2D collision)
        {
            GameObject collisionObject = collision.gameObject;

            // Collided with a brick
            if (collisionObject.tag == BrickController.BRICK_TAG)
            {
                hitCount++;
                gameController.IncreaseScoreAndBlocksDestroyed(collisionObject.GetComponent<BrickController>().pointsWorth);
                Destroy(collision.gameObject);
            }
            // Collided with the paddle.
            else if (collisionObject.tag == PaddleController.PADDLE_NAME)
            {
                ballRigidBody.velocity = new Vector2(CalculateVelocityX(collision), 1.0f) * speed;
            }
        }

        /**
         * Method     : CalculateVelocityX
         * Param      : paddle - The paddle the ball is colliding with.
         * Return     : The calculated velocity on the X axis based on the position that the paddle was hit.
         * Description: This method will calculate the ball velocity in X axis based on the position it hit the paddle.
         */
        float CalculateVelocityX(Collision2D paddle)
        {
            return (ballRigidBody.position.x - paddle.transform.position.x) / paddle.collider.bounds.size.x;
        }

        /**
         * Method     : ResetPosition
         * Param      : positionX - The X position on the screen the ball should be thrown.
         * Description: This method will reset the ball position on the screen based on the X position given.
         */
        public void ResetPosition(float positionX)
        {
            ballRigidBody.velocity = new Vector2(0, 0);
            transform.SetPositionAndRotation(new Vector3(positionX, throwPositionY, 0), new Quaternion());
        }

        /**
         * Method     : Throw
         * Param      : positionX - The X position on the screen the ball should be thrown.
         * Description: This method will throw the ball on the screen based on the X position given.
         */
        public void Throw(float positionX)
        {
            ballRigidBody.velocity = new Vector2(0, -1.0f) * speed;
        }

        /**
         * Method     : GetPositionY
         * Return     : The ball's position on Y axis.
         * Description: This method get the ball's position on Y axis ans return it.
         */
        public float GetPositionY()
        {
            return transform.position.y;
        }
    }
}