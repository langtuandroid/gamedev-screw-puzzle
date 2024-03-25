using UnityEngine;

namespace Game.Scripts
{
    public class Board : MonoBehaviour
    {
        public static Board instance;

        private void Awake()
        {
            instance = this;
        }
    }
}
