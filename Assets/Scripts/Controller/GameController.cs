using System.Collections;
using UnityEngine;

namespace Breakout.Assets.Scripts.Controller
{
    public class GameController : MonoBehaviour
    {
        public const string GAME_CONTROLLER_NAME = "GameController";

        // Attribute declaration
        private int score = 0;
        private GUIText scoreText;
        private BallController ballControler;
        private PaddleController paddleController;

        // Control attributes declaration.
        private bool throwBall = false;
        
        // Use this for initialization
        void Start()
        {
            scoreText = GetComponent<GUIText>();
            ballControler = GameObject.Find(BallController.BALL_NAME).GetComponent<BallController>();
            paddleController = GameObject.Find(PaddleController.PADDLE_NAME).GetComponent<PaddleController>();
            scoreText.text = score.ToString().PadLeft(3, '0');
            scoreText.transform.SetPositionAndRotation(new Vector3(0.8f, 0.9f, 1.0f), new Quaternion());

            //WaitAndThrow();
            ballControler.Throw(paddleController.GetPositionX());
        }

        // FixedUpdate is like update but for using wigh physics.
        void FixedUpdate()
        {
            if (ballControler.GetPositionY() <= paddleController.GetPositionY())
            {
                ballControler.ResetPosition(paddleController.GetPositionX());
                StartCoroutine(WaitResetAfterScore());
            }

            if (throwBall)
            {
                ballControler.Throw(paddleController.GetPositionX());
                throwBall = false;
            }
        }
        
        /**
         * Method     : WaitResetAfterScore
         * Return     : An asynchronous delayed time to be waited after a score happens.
         * Description: This method will wait for the specied number of seconds before freeing the ball reset.
         */
        IEnumerator WaitResetAfterScore()
        {
            // Let's wait some milliseconds before throwing the ball again.
            throwBall = false;
            yield return new WaitForSeconds(1.0f);
            throwBall = true;
        }

        void WaitAndThrow()
        {
        }

        public void IncreaseAndShowScore(int numberOfPoints)
        {
            score += numberOfPoints;
            scoreText.text = score.ToString().PadLeft(3, '0');
        }
    }
}