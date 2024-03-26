using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Game.Scripts
{
    public class TouchDrop : MonoBehaviour
    {
        private bool _start;
        private RaycastHit _hit;
        [Inject] private GameManager _gameManager;
        [Inject] private AudioManager _audioManager;
        [Inject] private UIManager _uiManager;
        private readonly float _boltRemoveHeight = 2f;

        void Start()
        {
            if (_gameManager.GameMode == GameManager.Modes.Null)
            {
                _start = true;
            }
        }

        public void SetStart()
        {
            _start = true;
        }

    
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!_uiManager.Win && _start)
                {
                    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out _hit))
                    {
                        if (_hit.collider.gameObject.CompareTag("BOLT"))
                        {
                            if (_gameManager.GameState == GameManager.State.Done)
                            {
                                var selectObject = _hit.collider.gameObject;
                                BoltShifting(selectObject);
                            }
                        
                        }

                        if (_hit.collider.gameObject.CompareTag("Fill"))
                        {
                            if (_gameManager.GameState == GameManager.State.Done)
                            {
                                var fillingplace = _hit.collider.gameObject;
                                Filling(fillingplace);
                                print(" "+_hit.collider.gameObject.name);
                            }
                        }
                    }
                }
            
            }
        }


        private void Filling(GameObject fillingReference)
        {
            if (_gameManager.DupPlug != null)
            {
                if (_uiManager.Fill)
                {
                    _uiManager.Fill = false;
                } 
                GameManager.Vibrate();
                var parent = _gameManager.DupPlug.transform.parent;
                var position1 = fillingReference.transform.position;
            
                _gameManager.EnableDupCollider(parent.transform.localPosition);
                parent.GetComponent<CircleCollider2D>().enabled = false;
                
                parent.DOMoveX(position1.x, 0.25f);
                parent.DOMoveY(position1.y, 0.25f).SetEase(Ease.Linear).OnComplete(() =>
                {
                
                    _audioManager.Play("Fill");
                    _gameManager.SpawnParticle(new Vector3(position1.x,position1.y,position1.z - 1.5f));
                    
                
                    parent.transform.DOLocalMoveZ(
                            parent.transform.localPosition.z + _boltRemoveHeight, 0.3f)
                        .SetEase(Ease.Linear).OnComplete(() =>
                        {
                            _gameManager.DisableDupCollider();
                            parent.GetComponent<CircleCollider2D>().enabled = true;
                            _gameManager.GameState = GameManager.State.Done;
                        });
                });
                _gameManager.DupPlug = null;
            }
        }

        private void BoltShifting(GameObject boltReference)
        {
            if (_gameManager.DupPlug != null)
            {
                GameManager.Vibrate();
                _audioManager.Play("Bolt");
                if (_uiManager.Pin)
                {
                    _uiManager.Pin = false;
                    _uiManager.Fill = true;
                }
                if (_gameManager.DupPlug == boltReference)
                {
            
                    _gameManager.GameState = GameManager.State.Select;
                    _gameManager.DupPlug = null;
                    var parent = boltReference.transform.parent;
                    parent.DOLocalMoveZ(parent.localPosition.z + _boltRemoveHeight, 0.3f).SetEase(Ease.Linear).OnComplete(
                        () => _gameManager.GameState = GameManager.State.Done);
                    
                }
                else if (_gameManager.DupPlug != boltReference)
                {
                    _gameManager.GameState = GameManager.State.Select;
                    var parent = _gameManager.DupPlug.transform.parent;
                    parent.DOLocalMoveZ(parent.localPosition.z + _boltRemoveHeight, 0.3f).SetEase(Ease.Linear);
                    _gameManager.DupPlug = null;
                    _gameManager.DupPlug=boltReference;
                    var parent1 = boltReference.transform.parent;

                    parent1.DOLocalMoveZ(parent1.localPosition.z - _boltRemoveHeight, 0.3f).SetEase(Ease.Linear).OnComplete(
                        () =>
                        {
                            _gameManager.GameState = GameManager.State.Done;
                        });
                }
            }
        
            else if (_gameManager.DupPlug == null)
            {
                _gameManager.GameState = GameManager.State.Select;
                if (_uiManager.Pin)
                {
                    _uiManager.Pin = false;
                    _uiManager.Fill = true;
                }
                GameManager.Vibrate();
                _audioManager.Play("Bolt");
                var parent = boltReference.transform.parent;
                parent.DOLocalMoveZ(parent.localPosition.z - _boltRemoveHeight, 0.3f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    _gameManager.GameState = GameManager.State.Done;
                });
                _gameManager.DupPlug = boltReference;
            }
        }
    }
}