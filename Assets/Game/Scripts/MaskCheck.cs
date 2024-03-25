using UnityEngine;

namespace Game.Scripts
{
    public class MaskCheck : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("MASk"))
            {
                gameObject.tag = "Fill";
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("MASk"))
            {
                gameObject.tag = "Untagged";
            }
        }
    }
}
