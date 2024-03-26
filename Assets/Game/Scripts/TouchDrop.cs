using DG.Tweening;
using UnityEngine;

namespace Game.Scripts
{
    public class TouchDrop : MonoBehaviour
    {
        public static TouchDrop Instance;
        private bool _start;
        private RaycastHit _hit;
        private GameManager _gameManager;
        private AudioManager _audioManager;
        private UIManager _uiManager;
        private float _boltRemoveHeight;
        private void Awake()
        {
            Instance = this;
            _boltRemoveHeight = 2f;
        }

        void Start()
        {
            _gameManager = GameManager.instance;
            _audioManager = AudioManager.instance;
            _uiManager = UIManager.instance;
            
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
                _gameManager.Vibrate();
                var parent = _gameManager.DupPlug.transform.parent;
                var position1 = fillingReference.transform.position;
            
                _gameManager.dupcolider.transform.localPosition = parent.transform.localPosition;
                _gameManager.dupcolider.GetComponent<CircleCollider2D>().enabled = true;
                parent.GetComponent<CircleCollider2D>().enabled = false;
                
                parent.DOMoveX(position1.x, 0.25f);
                parent.DOMoveY(position1.y, 0.25f).SetEase(Ease.Linear).OnComplete(() =>
                {
                
                    _audioManager.Play("Fill");
                    Instantiate(_gameManager.fillPartical, new Vector3(position1.x,position1.y,position1.z - 1.5f),
                        new Quaternion(0f,0f,0f,0f));
                
                    parent.transform.DOLocalMoveZ(
                            parent.transform.localPosition.z + _boltRemoveHeight, 0.3f)
                        .SetEase(Ease.Linear).OnComplete(() =>
                        {
                            _gameManager.dupcolider.GetComponent<CircleCollider2D>().enabled = false;
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
                _gameManager.Vibrate();
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
                _gameManager.Vibrate();
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