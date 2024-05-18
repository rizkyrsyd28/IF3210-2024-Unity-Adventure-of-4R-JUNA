using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class KepalaFireState : KepalaKrocoBaseState
{
    public override void EnterState(KepalaMovementManager manager)
    {
        manager.animator.SetBool("IsFire", true);
        if (manager.weapon.IsCanFire()) manager.weapon.Fire();
    }

    public override void UpdateState(KepalaMovementManager manager)
    {
        if (manager.enemyNav.velocity.magnitude > 0) ExitState(manager, manager.walk);
        else if (Vector3.Distance(manager.enemy.position, manager.player.position) < 5) ExitState(manager, manager.idle);

        // Debug.Log("Angular Speed : " +  manager.enemy.velocity.magnitude);
    }

    public void ExitState(KepalaMovementManager manager, KepalaKrocoBaseState state) 
    {
        manager.animator.SetBool("IsFire", false);
        manager.SwitchState(state);
    }
}