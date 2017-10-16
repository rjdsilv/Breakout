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
        private int blocksDestroyed = 0;
        private int currentLevel = 1;
        private int[] blocksPerLevel = { 112 };
        private string gameOverText = "";
        private GUIText scoreAndLivesText;
        private BallController ballControler;
        private PaddleController paddleController;
        private Vector3 scorePosition = new Vector3(0.20f, 0.9f, 1.0f);

        // Control attributes declaration.
        private bool throwBall = false;

        // Public variabales for the inspector.
        public int lives = 0;
        public float waitForRethrow = 0.0f;
        
        // Use this for initialization
        void Start()
        {
            // Start up the components.
            scoreAndLivesText = GetComponent<GUIText>();
            ballControler = GameObject.Find(BallController.BALL_NAME).GetComponent<BallController>();
            paddleController = GameObject.Find(PaddleController.PADDLE_NAME).GetComponent<PaddleController>();
            scoreAndLivesText.text = GetScoreAndLivesText();
            scoreAndLivesText.transform.SetPositionAndRotation(scorePosition, new Quaternion());

            // Throws the ball at the start
            ballControler.Throw(paddleController.GetPositionX());
        }

        // FixedUpdate is like update but for using wigh physics.
        void FixedUpdate()
        {
            if (!IsGameOver())
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

                scoreAndLivesText.text = GetScoreAndLivesText();
            }
            else
            {
                DisplayGameOverText();
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
            if (ballControler.GetPositionY() <= paddleController.GetPositionY())
            {
                lives--;
                return true;
            }

            return false;
        }

        /**
         * Method     : IsGameOver
         * Return     : <b>True</b> if either the left or the right paddle has achieved the win condition. <b>False</b> otherwise
         * Description: This method will check if any of the paddles has achieved the win condition and return accordingly.
         */
        bool IsGameOver()
        {
            bool isGameOver = false;

            if (blocksDestroyed >= blocksPerLevel[currentLevel - 1] || lives <= 0)
            {
                if (blocksDestroyed >= blocksPerLevel[currentLevel - 1])
                {
                    gameOverText = "YOU WON!\n\nSCORE: " + score;
                }
                else if (lives <= 0)
                {
                    gameOverText = "GAME OVER!\n\nSCORE: " + score;
                }

                isGameOver = true;
            }

            return isGameOver;
        }

        /**
         * Method     : DisplayGameOverText
         * Description: Displays to the player the game over text depending on the winnig or the lose in the game.
         */
        void DisplayGameOverText()
        {
            scoreAndLivesText.text = GetScoreAndLivesText() + "\n\n\n\n\n\n\n" + gameOverText;
        }

        /**
         * Method     : GetScoreAndLivesText
         * Return     : The updated score and lives text
         * Description: This method update the text the the number of lives and the score for the player.
         */
        string GetScoreAndLivesText()
        {
            return lives.ToString() + "                             " + score.ToString().PadLeft(3, '0');
        }

        /**
         * Method     : IncreaseScoreAndBlocksDestroyed
         * Param      : pointsWorth - The number of points earned when a brick is destroyed.
         * Description: This method will increase the the number of bricks destroyed by one and the score by the number
         *              of points received and show it.
         */
        public void IncreaseScoreAndBlocksDestroyed(int pointsWorth)
        {
            blocksDestroyed++;
            score += pointsWorth;
        }
    }
}