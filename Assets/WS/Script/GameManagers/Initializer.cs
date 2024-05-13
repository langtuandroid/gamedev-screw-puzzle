using UnityEngine;

namespace WS.Script.GameManagers
{
    public class Initializer : MonoBehaviour
    {
        private readonly int FramesPerSecond = 60;
        private void Start()
        {
            Application.targetFrameRate = FramesPerSecond;
        }
    }
}
