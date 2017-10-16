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
        private const float THROW_POSITION_Y = -2f;

        // Attribute declaration
        private GameController gameController;
        private Rigidbody2D ballRigidBody;
        private int hitCount = 0;
        private float startSpeed = 0;

        // Public variables declaration.
        public float speed = 4.0f;
        public float maxSpeed = 10.0f;

        // Use this for initialization
        void Awake()
        {
            startSpeed = speed;
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
                GetComponent<AudioSource>().Play();
                gameController.IncreaseScoreAndBlocksDestroyed(collisionObject.GetComponent<BrickController>().pointsWorth);
                IncreaseSpeed(collision);
                Destroy(collision.gameObject);
            }
            // Collided with the paddle.
            else if (collisionObject.tag == PaddleController.PADDLE_NAME)
            {
                ballRigidBody.velocity = new Vector2(CalculateVelocityX(collision), 1.0f) * speed;
            }
            // Collided with the up wall
            else if (collisionObject.tag == WallController.UP_WALL_NAME)
            {
                gameController.ShrinkPaddle();
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
         * Method     : IncreaseSpeed
         * Param      : brick - The brick the ball is colliding with.
         * Description: Increases the ball speed in determined intervals such as 4 hits, 12 hits and also when hitting
         *              an orange or a red brick. The speed is increased to a maximum defined by the user.
         */
        void IncreaseSpeed(Collision2D brick)
        {
            string brickName = brick.gameObject.name;
            if (hitCount == 12 || hitCount == 4 || CollidedWithOrangeBrick(brickName) || CollidedWithRedBrick(brickName))
            {
                speed = Mathf.Clamp(speed + 1, startSpeed, maxSpeed);
            }
        }

        /**
         * Method     : CollidedWithOrangeBrick
         * Param      : name - The name of the brick with which the ball collided
         * Return     : <b>True</b> if the brick is an orange one. <b>False</b> otherwise
         * Description: This method will check if the brick with which the ball collided is an orange one.
         */
        bool CollidedWithOrangeBrick(string name)
        {
            return name.StartsWith(BrickController.ORANGE_BRICK_PREFIX);
        }

        /**
         * Method     : CollidedWithRedBrick
         * Param      : name - The name of the brick with which the ball collided
         * Return     : <b>True</b> if the brick is a red one. <b>False</b> otherwise
         * Description: This method will check if the brick with which the ball collided is a red one.
         */
        bool CollidedWithRedBrick(string name)
        {
            return name.StartsWith(BrickController.RED_BRICK_PREFIX);
        }

        /**
         * Method     : ResetSpeedAndPosition
         * Param      : positionX - The X position on the screen the ball should be thrown.
         * Description: This method will reset the ball position and speed on the screen based on the X position given.
         */
        public void ResetSpeedHitCountAndPosition(float positionX)
        {
            hitCount = 0;
            speed = startSpeed;
            ballRigidBody.velocity = new Vector2(0, 0);
            transform.SetPositionAndRotation(new Vector3(positionX, THROW_POSITION_Y, 0), new Quaternion());
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