using UnityEngine;
using Zenject;

namespace Game.Scripts.Shop
{
    public class BoardTextureChange : MonoBehaviour
    {
        [Inject] private ItemsSelectedData _itemsSelectedData;
        private void Awake()
        {
            GetComponent<Renderer>().material.mainTexture = _itemsSelectedData.BoardTexture;
        }
    }
}