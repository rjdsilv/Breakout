using UnityEngine;

namespace Breakout.Assets.Scripts.Controller
{
    /**
     * Class      : BrickController
     * Description: This class will have all the necessary programming for controlling the bricks on the screen.
     * Author     : Rodrigo Januario da Silva
     * Version    : 1.0
     */
    public class BrickController : MonoBehaviour
    {
        // Constant declaration.
        public const string BRICK_TAG = "Brick";

        // Public variables declaration.
        public int pointsWorth = 0;
    }
}