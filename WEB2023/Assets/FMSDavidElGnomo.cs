using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourAPI.Core;
using BehaviourAPI.Core.Actions;
using BehaviourAPI.StateMachines;
using BehaviourAPI.Core.Perceptions;

namespace BehaviourAPI.UnityToolkit.Demos
{ 
public class FMSDavidElGnomo : BehaviourRunner
{
    ActionsDavidElGnomo _ActionsDavidElGnomo;
    protected override void Init()
    {
        _ActionsDavidElGnomo = GetComponent<ActionsDavidElGnomo>();
        base.Init();
    }

    protected override BehaviourGraph CreateGraph()
    {
        FSM Gnomefsm = new FSM();

            //actions
            /**/ FunctionalAction WalkToPlayerAction = new FunctionalAction(_ActionsDavidElGnomo.StartMethodWalk, _ActionsDavidElGnomo.UpdateMethodWalk); //estados que hay que meter
            /**/ FunctionalAction PunchAction = new FunctionalAction(_ActionsDavidElGnomo.StartMethodPunch, _ActionsDavidElGnomo.UpdateMethodPunch); //estados que hay que meter
            FunctionalAction WalkAttackAction = new FunctionalAction(_ActionsDavidElGnomo.StartMethodWalkAttack, _ActionsDavidElGnomo.UpdateMethodWalkAttack);

            //states
            /**/ State WalkToPlayer = Gnomefsm.CreateState(WalkToPlayerAction); //el estado WalkToPlayer se crea con la acción WalkToPlayerAction
            /**/ State Punch = Gnomefsm.CreateState(PunchAction);
            State WalkAttack = Gnomefsm.CreateState(WalkAttackAction);

            //Perceptions
            /**/ ConditionPerception isInPunchRange = new ConditionPerception((/*Parametros*/)=>/*expresion*/_ActionsDavidElGnomo.CheckPlayerInPunchRange()); //esto nos va a decir cuando debe cambiar de estado
            /**/ ConditionPerception leavesPunchRange = new ConditionPerception((/*Parametros*/)=>/*expresion*/!_ActionsDavidElGnomo.CheckPlayerInPunchRange()); //esto nos va a decir cuando debe cambiar de estado
            ConditionPerception collisionWithYAxis = new ConditionPerception(() => _ActionsDavidElGnomo.CheckCollisionWithYAxis());
            ConditionPerception noCollisionWithYAxis = new ConditionPerception(() => !_ActionsDavidElGnomo.CheckCollisionWithYAxis());

            //Transitions
            /**/ StateTransition WalkingToPlayer_to_Punch = Gnomefsm.CreateTransition(WalkToPlayer, Punch, isInPunchRange); // cambiara cuando se cumpla la percepcion 
            /**/ StateTransition Punch_to_WalkingToPlayer = Gnomefsm.CreateTransition(Punch, WalkToPlayer, leavesPunchRange); // cambiara cuando se acabe la accion
            StateTransition WalkToPlayer_to_WalkAttack = Gnomefsm.CreateTransition(WalkToPlayer, WalkAttack, collisionWithYAxis);
            StateTransition WalkAttack_to_WalkToPlayer = Gnomefsm.CreateTransition(WalkAttack, WalkToPlayer, noCollisionWithYAxis);
            return Gnomefsm;

    }

}
}