using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Scripts.UI
{
    public class LevelMenu : MonoBehaviour
    {
        [Inject] private LevelManager _levelManager;
        [SerializeField] private List<LevelButton> _levelButtons;

        private void Start()
        {
            for (var i = 0; i < _levelButtons.Count; i++)
            {
                int levelIndex = i + 1;
                if (levelIndex > _levelManager.MaxLevel)
                {
                    break;
                }
                _levelButtons[i].Assign(levelIndex);
            }
        }
    }
}
