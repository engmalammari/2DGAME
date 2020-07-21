using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public enum STATE { IDLE, PATROL, CHASE, ATTACK, HIT, DIE }
    public enum EVENT { ENTER, UPDATE, EXIT }

    public STATE CurrentState;
    protected EVENT CurrentStage;

    protected GameObject _enemy;
    protected GameObject _player;
    protected GameObject _shot;
    protected Animator _animator;
    protected EnemyStateMachine _nextState;
    protected float _enemySpeed = 5f;
    protected Vector3 _enemyStartPosition;

    private float _visibleDistance = 8f;
    private float _attackRange = 6f;


    public EnemyStateMachine(GameObject enemy, GameObject player, Animator animator, GameObject shot)
    {
        _enemy = enemy;
        _player = player;
        _animator = animator;
        _shot = shot;
        CurrentStage = EVENT.ENTER;

    }

    public virtual void Enter() { CurrentStage = EVENT.UPDATE; }
    public virtual void Update() { CurrentStage = EVENT.UPDATE; }
    public virtual void Exit() { CurrentStage = EVENT.EXIT; }

    public EnemyStateMachine Process()
    {
        if (CurrentStage == EVENT.ENTER) Enter();
        if (CurrentStage == EVENT.UPDATE) Update();
        if (CurrentStage == EVENT.EXIT)
        {
            Exit();
            return _nextState;
        }
        return this;
    }

    public bool CanSeePlayer()
    {
        Vector2 direction = _player.transform.position - _enemy.transform.position;

        if (direction.magnitude <= _visibleDistance) return true;

        return false;
    }

    public bool CanAttackPlayer()
    {
        Vector2 direction = _player.transform.position - _enemy.transform.position;
        if (direction.magnitude <= _attackRange) return true;

        return false;
    }

}

public class Idle : EnemyStateMachine
{
    public Idle(GameObject enemy, GameObject player, Animator animator, GameObject shot) : base(enemy, player, animator, shot)
    {
        CurrentState = STATE.IDLE;

    }

    public override void Enter()
    {
        _animator.SetTrigger("isIdle");
        base.Enter();
    }
    public override void Update()
    {
        if (CanAttackPlayer())
        {
            _nextState = new Attack(_enemy, _player, _animator,_shot);
            CurrentStage = EVENT.EXIT;
        }
    }
    public override void Exit()
    {
        _animator.ResetTrigger("isIdle");
        base.Exit();
    }
}


public class Attack : EnemyStateMachine
{
    public Attack(GameObject enemy, GameObject player, Animator animator, GameObject shot) : base(enemy, player, animator, shot)
    {
        CurrentState = STATE.ATTACK;
    }

    public override void Enter()
    {
        _animator.SetTrigger("isAttack");
        base.Enter();
    }
    public override void Update()
    {
        var player = _player.GetComponent<PlayerStats>();
        if (player.IsDead)
        {
            base.Update();
        }


        if (!CanAttackPlayer())
        {
            _nextState = new Idle(_enemy, _player, _animator, _shot);
            CurrentStage = EVENT.EXIT;
        }
    }
    public override void Exit()
    {
        _animator.ResetTrigger("isAttack");
        base.Exit();
    }
}
