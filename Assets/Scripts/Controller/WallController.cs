using UnityEngine;

namespace Breakout.Assets.Scripts.Controller
{
    /**
     * Class      : WallController
     * Author     : Rodrigo Januario da Silva
     * Version    : 1.0.0
     * Description: This class will handle the control of the walls just to play collision sounds.
     */
    public class WallController : MonoBehaviour
    {
        // Detects game object collision.
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == "Ball")
            {
                GetComponent<AudioSource>().Play();
            }
        }
    }
}