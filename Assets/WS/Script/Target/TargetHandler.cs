using System.Collections.Generic;
using UnityEngine;
using WS.Script.Other;
using WS.Script.Weapon;
using Zenject;

namespace WS.Script.Target
{
    public class TargetHandler : MonoBehaviour
    {
        [Inject] private DiContainer _diContainer;
        [Inject] private WeaponHandler _weaponManager;
        private List<Target> _trgets = new();
        public Target _currentTarget { get; private set; }
        public int _stage { get; private set; } = 0;
        public int _stageToBoss { get; private set; }

        private void Awake()
        {
            TakeTargets();
        }

        public void Hit(GameObject weapon)
        {
            _currentTarget.KnifeHit(weapon);
        }

        public void Configure()
        {
            _currentTarget = _diContainer.InstantiatePrefab(_trgets[_stage].gameObject, transform.position, Quaternion.identity, null).GetComponent<Target>();
            _currentTarget.gameObject.SetActive(true);

            if (_stage == 0 || _currentTarget._stage == STAGE_TYPE.BossFight)
            {
                if(_stage == 0)
                {
                    _stageToBoss = 1;
             
                    for (int i = _stage; i < _trgets.Count; i++)
                    {
                    
                        if (_trgets[i]._stage == STAGE_TYPE.BossFight)
                            break;
                    }
                }
                else
                {
                    _stageToBoss = 0;
                
                    for (int i = _stage + 1; i < _trgets.Count; i++)
                    {
                        if (_trgets[i]._stage == STAGE_TYPE.BossFight)
                            break;
                    }
                }
            }
            else
                _stageToBoss++;

            _weaponManager.Configure();
        }

        public void StageComplete()
        {
            if (_currentTarget)
                Destroy(_currentTarget.gameObject);
            _stage++;

            Configure();
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
                return;

            TakeTargets();
        }

        private void TakeTargets()
        {
            var stage = transform.GetComponentsInChildren<Target>(true);
            _trgets = new List<Target>(stage);

            for (int i = 0; i < _trgets.Count; i++)
            {
                _trgets[i].gameObject.name = "TargetController " + (i + 1) + (_trgets[i]._stage == STAGE_TYPE.BossFight ? ": BOSS FIGHT" : "");
                _trgets[i].gameObject.SetActive(false);
                if(_trgets[i]._ID == null)
                {
                    Debug.LogError("ERROR! MISSING TARGET BEHAVIOR ID IN: " + _trgets[i].gameObject.name);
                }
            }
        }
    }
}
