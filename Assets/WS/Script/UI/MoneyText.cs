using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using WS.Script.GameManagers;

namespace WS.Script.UI
{
    public class MoneyText : MonoBehaviour
    {
        [FormerlySerializedAs("coinTxt")] public TMP_Text _coinText;

        private void Update()
        {
            _coinText.text = ValueStorage.CoinsData + "";
        }
    }
}
