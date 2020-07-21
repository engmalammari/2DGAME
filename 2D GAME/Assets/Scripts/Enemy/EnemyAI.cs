using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _shot;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private EnemyStateMachine _currentState;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private Transform _firstPoint;
    [SerializeField] private Transform _secondPoint;

    private int _health;
    private int _maxHealth = 100;
    private float _attackRange = 7f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _player = FindObjectOfType<PlayerStats>().gameObject;
       // _shot = FindObjectOfType<EnemyAttack>().gameObject;
        _currentState = new Idle(this.gameObject, _player, _animator, _shot);

        _health = _maxHealth;

    }

    public void ConfirmDelay()
    {
        StartCoroutine(MakeDelayBetweenShots());
    }

    private IEnumerator MakeDelayBetweenShots()
    {
        yield return new WaitForSeconds(2f);
        _animator.SetTrigger("isIdle");
    }

    private void Update()
    {
        Vector3 distance = transform.position - _player.transform.position;
        
        if(distance.magnitude <= _attackRange)
        {
            _animator.SetTrigger("isAttack");
           
        }


        FlipSprite();

    }
    public void TakeDamage(int damage)
    {
        Debug.Log("Taking Damage");
        _health -= damage;
        if (_health <= 0)
        {
            Debug.Log("die");
            _animator.SetTrigger("isDead");
            StartCoroutine(DieWithDelay());
        }
    }
    private IEnumerator DieWithDelay()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    private void FlipSprite()
    {
        if (transform.position.x >= _player.transform.position.x) _spriteRenderer.flipX = false;
        else if (transform.position.x <= _player.transform.position.x) _spriteRenderer.flipX = true;
    }
}
