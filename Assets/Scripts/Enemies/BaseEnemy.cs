using System.Collections;
using ScriptableObjects;
using UnityEngine;

namespace Enemies
{
    public class BaseEnemy : MonoBehaviour
    {
        public enum EnemyState
    {
        Idle,
        Hit
    }

    public EnemyState State
    {
        get => _state;
        set
        {
            _state = value;
            SetEnemyState();
        }
    }

    [SerializeField] private SpriteRenderer enemySprite;
    [SerializeField] private CharacterSO enemySo;
    [SerializeField] private float delayForDiying;
    [SerializeField] private InGameDataSO inGameDataSo;
    [SerializeField] private Transform vfx;

    private EnemyState _state = EnemyState.Idle;
    public Rigidbody2D EnemyRigidbody2D { get; private set; } 
    public bool GotHit { get; private set; } 
    private void Start()
    {
        enemySprite.sprite = enemySo.characterImage;
        EnemyRigidbody2D = GetComponent<Rigidbody2D>();
        State = EnemyState.Idle;
        GotHit = false;
        inGameDataSo.IncreaseEnemies();
        vfx.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("NotInteractable"))
        {
            return;
        }
        if (!GotHit)
        {
            State = EnemyState.Hit;
            GotHit = true;
        }
    }
    

    private void SetEnemyState()
    {
        switch (State)
        {
            case EnemyState.Idle:
                EnemyRigidbody2D.isKinematic = true;
                SetIdle();
                break;
            case EnemyState.Hit:
                SetHit();
                break;
            default: break;
        }
    }


    public virtual void SetHit()
    {
        //  Play animations when bird hits something
        //  Play animations when bird hits something
        StartCoroutine(DelayForDestruction());
    }

    private IEnumerator DelayForDestruction()
    {
        yield return new WaitForSeconds(1f);
        inGameDataSo.DecreaseEnemies();
        vfx.gameObject.SetActive(true);
        Destroy(gameObject);
    }
    public virtual void SetIdle()
    {
        //  Play animations for idle
        //  Play animations for idle
    }
    }
}