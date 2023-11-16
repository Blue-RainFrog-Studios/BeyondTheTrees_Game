using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourAPI.Core;
using BehaviourAPI.Core.Actions;
using BehaviourAPI.StateMachines;
using BehaviourAPI.Core.Perceptions;

namespace BehaviourAPI.UnityToolkit.Demos
{
    public class SScript : BehaviourRunner
    {
        ActionsSquirrel _ActionsSquirrel;
        float maxDistance = 10.0f;

        protected override void Init()
        {
            _ActionsSquirrel = GetComponent<ActionsSquirrel>();
            base.Init();
        }

        protected override BehaviourGraph CreateGraph()
        {
            FSM Squirrelfsm = new FSM();

            //actions
            /**/
            PatrolAction PatrolAction = new PatrolAction(maxDistance); 
            /**/
            FunctionalAction WalkToAcornAction = new FunctionalAction(_ActionsSquirrel.StartMethodAcorn, _ActionsSquirrel.UpdateMethodAcorn);
            /**/
            FunctionalAction WalkToPlayerAction = new FunctionalAction(_ActionsSquirrel.StartMethodWalkToPlayer, _ActionsSquirrel.UpdateMethodWalkToPlayer);

            //states
            /**/
            State Patrol = Squirrelfsm.CreateState(PatrolAction); 
            /**/
            State WalkToAcorn = Squirrelfsm.CreateState(WalkToAcornAction);
            /**/
            State WalkToPlayer = Squirrelfsm.CreateState(WalkToPlayerAction);

            //Perceptions
            /**/
            ConditionPerception isInAcornRange = new ConditionPerception((/*Parametros*/) =>/*expresion*/_ActionsSquirrel.CheckInAcornRange());
            /**/
            ConditionPerception acornExists = new ConditionPerception((/*Parametros*/) =>/*expresion*/!_ActionsSquirrel.CheckAcornExists());

            //Transitions
            /**/
            StateTransition WalkingToAcorn_to_Eat = Squirrelfsm.CreateTransition(Patrol, WalkToAcorn, statusFlags: StatusFlags.Finished/*cuando el player entre o cuando aparezca el acorn*/);

            StateTransition Patrol_after_eat = Squirrelfsm.CreateTransition(WalkToAcorn, Patrol, statusFlags: StatusFlags.Finished);
            /**/
            StateTransition WalkingToPlayer_to_Attack = Squirrelfsm.CreateTransition(WalkToAcorn, WalkToPlayer, statusFlags: StatusFlags.Finished); 

            StateTransition Patrol_after_kill = Squirrelfsm.CreateTransition(WalkToAcorn, WalkToPlayer, statusFlags: StatusFlags.Finished); 

            /**/
            return Squirrelfsm;

        }

    }
}