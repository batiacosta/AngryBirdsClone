
using System.Collections;
using DefaultNamespace;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Pivot : MonoBehaviour
{
    [SerializeField] private Bird currentBird;
    [SerializeField] private float factor;
    [SerializeField] private InGameDataSO inGameDataSo;
    [SerializeField] private Transform placeToSpawn;

    private Rigidbody2D _birdRigidBody2D;
    private Trajectory _trajectory;
    private Camera _mainCamera;
    private bool _isDragging;
    private Vector2 _touchPosition;
    private Vector3 _worldFingerPosition;
    Vector3 _initialPosition;
    Vector3 _direction;
    private CharacterSO _currentBirSo;
    
    private void Start()
    {
        _mainCamera = Camera.main;
        inGameDataSo.OnCurrentBirdSOChanged += SetNewBirdFromEvent;
        if (inGameDataSo.CurrentCharacterSo != null)
        {
            SetNewCurrentBird(inGameDataSo.CurrentCharacterSo);
        }

        _initialPosition = transform.position;
        _trajectory = GetComponent<Trajectory>();
    }

    private void OnDestroy()
    {
        inGameDataSo.OnCurrentBirdSOChanged -= SetNewBirdFromEvent;
    }

    private void SetNewBirdFromEvent(object sender, CharacterSO e)
    {
        SetNewCurrentBird(e);
    }

    private void SetNewCurrentBird(CharacterSO birdSo)
    {
        _isDragging = false;
        if (placeToSpawn.childCount != 0)
        {
            foreach (Transform child in placeToSpawn.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        var birdPrefab = Instantiate(birdSo.prefab, placeToSpawn, true);
        birdPrefab.SetPositionAndRotation(placeToSpawn.transform.position, Quaternion.identity );
        birdPrefab.transform.localScale *= 0.05f;
        currentBird = birdPrefab.GetComponent<Bird>();
        _currentBirSo = birdSo;
        _birdRigidBody2D = currentBird.GetComponent<Rigidbody2D>();
        
    }


    private void Update() 
    {
        if(currentBird == null) return;
        if (!Touchscreen.current.primaryTouch.press.isPressed)
        {
            if (_isDragging)
            {
                ShootBird();
            }

            _isDragging = false;
            return;
        }
        if (currentBird.State != Bird.BirdState.Released)
        {
            SetBirdPositionWhilePressed();
        }
    }

    private void SetBirdPositionWhilePressed()
    {
        
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            _isDragging = true;
            _touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            _worldFingerPosition = _mainCamera.ScreenToWorldPoint(_touchPosition);
            currentBird.State = Bird.BirdState.Pressed;
            _birdRigidBody2D.position = _worldFingerPosition;
            _trajectory.UpdateDotsPosition(transform.position, ((_initialPosition- _worldFingerPosition)*factor));
        }
    }

    private void ShootBird()
    {
        currentBird.State = Bird.BirdState.Shooting;

        Vector3 finalPosition = _birdRigidBody2D.position;
        
        _direction = _initialPosition - finalPosition;
        _birdRigidBody2D.velocity = new Vector2(_direction.x, _direction.y) * factor;
        ReleaseBird();
    }
    private void ReleaseBird()
    {
        _trajectory.Hide();
        currentBird.State = Bird.BirdState.Released;
        currentBird = null;
        StartCoroutine(DelayNewBird());
    }

    private IEnumerator DelayNewBird()
    {
        yield return new WaitForSeconds(2.5f);
        inGameDataSo.SetBirdQuantityInGame(_currentBirSo,false);
    }
}
