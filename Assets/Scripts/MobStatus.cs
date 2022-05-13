using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//モブの状態を管理するComponent
[RequireComponent(typeof(Animator))]
public class MobStatus : MonoBehaviour
{
    //状態を表す列挙型
    public enum StateEnum
    {
        Normal,     //通常
        Attack,     //通常攻撃中
        FallAttack, //落下攻撃中
        Syagami,    //しゃがみ状態
        Die         //死亡
    }

    //移動可能かを返す
    public bool IsMovable => _state == StateEnum.Normal;

    //しゃがみ可能かを返す
    public bool IsSyagamiable => _state == StateEnum.Normal;

    //しゃがみ中かを返す
    public bool IsSyagaming => _state == StateEnum.Syagami;

    //攻撃可能かを返す
    public bool IsAttackable => _state == StateEnum.Normal;

    //攻撃中かを返す
    public bool IsAttacking => _state == StateEnum.Attack || _state == StateEnum.FallAttack;


    //TODO：HP管理の追加


    private StateEnum _state;   //現在の状態
    protected Animator _animator;



    // Start is called before the first frame update
    protected virtual void Start()
    {
        _state = StateEnum.Normal;
        _animator = GetComponent<Animator>();
        
    }

    //可能なら通常状態へ遷移する
    public void GoToNormalStateIfPossible()
    {
        if (_state == StateEnum.Die) return;

        _state = StateEnum.Normal;
        _animator.SetBool("syagami", false);
    }

    //可能ならしゃがみ状態へ遷移する
    public void GoToSyagamiStateIfPossible()
    {
        if (!IsSyagamiable) return;

        _state = StateEnum.Syagami;
        _animator.SetBool("syagami", true);
    }

    //可能なら通常攻撃状態へ遷移する
    public void GoToAttackStateIfPossible()
    {
        if (!IsAttackable) return;

        _state = StateEnum.Attack;
        _animator.SetTrigger("attack");
    }

    //可能なら落下攻撃状態へ遷移する
    public void GoToFallAttackStateIfPossible()
    {
        if (!IsAttackable) return;

        _state = StateEnum.FallAttack;
        _animator.SetTrigger("fallattack");
    }

}
