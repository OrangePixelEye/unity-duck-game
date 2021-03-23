using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : IEnemyState
{
    private Enemy enemy;
    private float throwTimer;
    private float throwCooldown = 1;
    private bool canThrow = true;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        
    }

    public void Execute()
    {
        
        ThrowBallE();
        if (enemy.InMeleeRange)
        {
            enemy.ChangeState(new MeleeState());
        }else if(enemy.Target != null)
        {
            enemy.Move();
        }
        else
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {
       
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }
    private void ThrowBallE()
    {
        
        throwTimer += Time.deltaTime;
        if (canThrow)
        {
            enemy.MyAnimator.SetTrigger("throw");
            canThrow = false;
        }
        if (throwTimer >= throwCooldown)
        {
            canThrow = true;
            throwTimer = 0;
        }
    }
}
