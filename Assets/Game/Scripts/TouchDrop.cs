using DG.Tweening;
using UnityEngine;

namespace Game.Scripts
{
    public class TouchDrop : MonoBehaviour
    {
        public static TouchDrop instance;

        [SerializeField] public bool start;
    
        private RaycastHit _hit;
        [Header("Scripts")]
        public GameManager gameManager;

        public float boltremoveheight;
    
        private Vector3 offset;
        
        private AudioManager _audioManager;
        private void Awake()
        {
            instance = this;
            boltremoveheight = 2f;
        }

        void Start()
        {
            gameManager=GameManager.instance;
            _audioManager = AudioManager.instance;
            
            if (gameManager.GameMode == GameManager.Modes.Null)
            {
                start = true;
            }
        }

    
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!UIManager.instance.Win && start)
                {
                    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out _hit))
                    {
                        if (_hit.collider.gameObject.CompareTag("BOLT"))
                        {
                            if (gameManager.gamestate == GameManager.State.Done)
                            {
                                var selectObject = _hit.collider.gameObject;
                                BoltShifting(selectObject);
                            }
                        
                        }

                        if (_hit.collider.gameObject.CompareTag("Fill"))
                        {
                            if (gameManager.gamestate == GameManager.State.Done)
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


        public void Filling(GameObject fillingreferance)
        {
            if (gameManager.dupPlug!=null)
            {
                if (UIManager.instance.Fill)
                {
                    UIManager.instance.Fill = false;
                } 
                gameManager.Vibration();
                var parent = gameManager.dupPlug.transform.parent;
                var position1 = fillingreferance.transform.position;
            
                gameManager.dupcolider.transform.localPosition = parent.transform.localPosition;
                gameManager.dupcolider.GetComponent<CircleCollider2D>().enabled = true;
                parent.GetComponent<CircleCollider2D>().enabled = false;
                
                parent.DOMoveX(position1.x, 0.25f);
                parent.DOMoveY(position1.y, 0.25f).SetEase(Ease.Linear).OnComplete(() =>
                {
                
                    _audioManager.Play("Fill");
                    Instantiate(gameManager.fillPartical, new Vector3(position1.x,position1.y,position1.z - 1.5f),
                        new Quaternion(0f,0f,0f,0f));
                
                    parent.transform.DOLocalMoveZ(
                            parent.transform.localPosition.z + boltremoveheight, 0.3f)
                        .SetEase(Ease.Linear).OnComplete(() =>
                        {
                            gameManager.dupcolider.GetComponent<CircleCollider2D>().enabled = false;
                            parent.GetComponent<CircleCollider2D>().enabled = true;
                            gameManager.gamestate = GameManager.State.Done;
                        });
                });
                gameManager.dupPlug = null;
            }
        }

        private void BoltShifting(GameObject boltReference)
        {
            if (gameManager.dupPlug != null)
            {
                gameManager.Vibration();
                _audioManager.Play("Bolt");
                if (UIManager.instance.Pin)
                {
                    UIManager.instance.Pin = false;
                    UIManager.instance.Fill = true;
                }
                if (gameManager.dupPlug == boltReference)
                {
            
                    gameManager.gamestate = GameManager.State.Select;
                    gameManager.dupPlug = null;
                    var parent = boltReference.transform.parent;
                    parent.DOLocalMoveZ(parent.localPosition.z + boltremoveheight, 0.3f).SetEase(Ease.Linear).OnComplete(
                        () => gameManager.gamestate = GameManager.State.Done);
                    
                }
                else if (gameManager.dupPlug != boltReference)
                {
                    gameManager.gamestate = GameManager.State.Select;
                    var parent = gameManager.dupPlug.transform.parent;
                    parent.DOLocalMoveZ(parent.localPosition.z + boltremoveheight, 0.3f).SetEase(Ease.Linear);
                    gameManager.dupPlug = null;
                    gameManager.dupPlug=boltReference;
                    var parent1 = boltReference.transform.parent;

                    parent1.DOLocalMoveZ(parent1.localPosition.z - boltremoveheight, 0.3f).SetEase(Ease.Linear).OnComplete(
                        () =>
                        {
                            gameManager.gamestate = GameManager.State.Done;
                        });
                }
            }
        
            else if (gameManager.dupPlug == null)
            {
                gameManager.gamestate = GameManager.State.Select;
                if (UIManager.instance.Pin)
                {
                    UIManager.instance.Pin = false;
                    UIManager.instance.Fill = true;
                }
                gameManager.Vibration();
                _audioManager.Play("Bolt");
                var parent = boltReference.transform.parent;
                parent.DOLocalMoveZ(parent.localPosition.z - boltremoveheight, 0.3f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    gameManager.gamestate = GameManager.State.Done;
                });
                gameManager.dupPlug = boltReference;
            }
        }
    }
}