using System.Collections;
using UnityEngine;

namespace Breakout.Assets.Scripts.Controller
{
    /**
     * Class      : GameController
     * Description: This class will make the necessary calculations and control for the game to keep going. It
     *              will also compute the score and show it to the player.
     * Author     : Rodrigo Januario da Silva
     * Version    : 1.0
     */
    public class GameController : MonoBehaviour
    {
        // Public constant declaration.
        public const string GAME_CONTROLLER_NAME = "GameController";

        // Attribute declaration
        private int score = 0;
        private GUIText scoreText;
        private BallController ballControler;
        private PaddleController paddleController;
        private Vector3 scorePosition = new Vector3(0.8f, 0.9f, 1.0f);

        // Control attributes declaration.
        private bool throwBall = false;

        // Public variabales for the inspector.
        public float waitForRethrow = 0.0f;
        
        // Use this for initialization
        void Start()
        {
            // Start up the components.
            scoreText = GetComponent<GUIText>();
            ballControler = GameObject.Find(BallController.BALL_NAME).GetComponent<BallController>();
            paddleController = GameObject.Find(PaddleController.PADDLE_NAME).GetComponent<PaddleController>();
            scoreText.text = score.ToString().PadLeft(3, '0');
            scoreText.transform.SetPositionAndRotation(scorePosition, new Quaternion());

            // Throws the ball at the start
            ballControler.Throw(paddleController.GetPositionX());
        }

        // FixedUpdate is like update but for using wigh physics.
        void FixedUpdate()
        {
            // Lost the ball. Reposition it and wait
            if (BallLost())
            {
                ballControler.ResetPosition(paddleController.GetPositionX());
                StartCoroutine(WaitResetAfterLooseBall());
            }

            // Throw the ball after waiting for some seconds.
            if (throwBall)
            {
                ballControler.Throw(paddleController.GetPositionX());
                throwBall = false;
            }
        }

        /**
         * Method     : WaitResetAfterLooseBall
         * Return     : An asynchronous delayed time to be waited after a score happens.
         * Description: This method will wait for the specied number of seconds before throwing the ball again.
         */
        IEnumerator WaitResetAfterLooseBall()
        {
            // Let's wait some milliseconds before throwing the ball again.
            throwBall = false;
            yield return new WaitForSeconds(1.0f);
            throwBall = true;
        }

        /**
         * Method     : BallLost
         * Return     : <b>True</b> if the ball has been lost by the paddle. <b>False</b> otherwise
         * Description: This method will wait for the specied number of seconds before throwing the ball again.
         */
        bool BallLost()
        {
            return ballControler.GetPositionY() <= paddleController.GetPositionY();
        }

        /**
         * Method     : IncreaseAndShowScore
         * Param      : pointsWorth - The number of points earned when a brick is destroyed.
         * Description: This method will increase the score by the number of points received and show it.
         */
        public void IncreaseAndShowScore(int pointsWorth)
        {
            score += pointsWorth;
            scoreText.text = score.ToString().PadLeft(3, '0');
        }
    }
}