using UnityEngine;

namespace Breakout.Assets.Scripts.Controller
{
    public class GameController : MonoBehaviour
    {
        public const string GAME_CONTROLLER_NAME = "GameController";

        // Attribute declaration
        private int score = 0;
        private GUIText scoreText;

        void Start()
        {
            scoreText = GetComponent<GUIText>();
            scoreText.text = score.ToString().PadLeft(3, '0');
            scoreText.transform.SetPositionAndRotation(new Vector3(0.8f, 0.9f, 1.0f), new Quaternion());
        }

        public void IncreaseAndShowScore(int numberOfPoints)
        {
            score += numberOfPoints;
            scoreText.text = score.ToString().PadLeft(3, '0');
        }
    }
}