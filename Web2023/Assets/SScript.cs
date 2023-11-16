using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourAPI.Core;
using BehaviourAPI.Core.Actions;
using BehaviourAPI.StateMachines;
using BehaviourAPI.Core.Perceptions;
using System;

namespace BehaviourAPI.UnityToolkit.Demos
{
    public class SScript : BehaviourRunner
    {
        ActionsSquirrel _ActionsSquirrel;

        public PushPerception stop;

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
            FunctionalAction SquirrelWalkAcorn = new(_ActionsSquirrel.StartWalkAcorn, _ActionsSquirrel.UpdateWalkAcorn);
            /**/
            FunctionalAction SquirrelWalkPlayer = new(_ActionsSquirrel.StartWalkPlayer, _ActionsSquirrel.UpdateWalkPlayer);            
            /**/
            FunctionalAction SquirrelEatAcorn = new(_ActionsSquirrel.StartEatAcorn, _ActionsSquirrel.UpdateEatAcorn); 
            

            //states
            /**/
            State WalkAcorn = Squirrelfsm.CreateState(SquirrelWalkAcorn);
            /**/
            State WalkPlayer = Squirrelfsm.CreateState(SquirrelWalkPlayer);     
            /**/
            State EatAcorn = Squirrelfsm.CreateState(SquirrelEatAcorn);
            /**/


            //Perceptions
            /**/
            ConditionPerception CheckAcornConsumed = new ConditionPerception((/*Parametros*/) => _ActionsSquirrel.CheckAcornExists());
            /**/
            ConditionPerception CheckEatEnded = new ConditionPerception((/*Parametros*/) => _ActionsSquirrel.CheckEnded());


            //Transitions
            /**/
            StateTransition WalkingAcorn_to_EatingAcorn = Squirrelfsm.CreateTransition(WalkAcorn, EatAcorn, CheckAcornConsumed);
            /**/
            StateTransition WalkingAcorn_to_WalkingPlayer = Squirrelfsm.CreateTransition(WalkAcorn, WalkPlayer, CheckAcornConsumed/*cuando el player entre o cuando aparezca el acorn*/);
            /**/
            StateTransition EatingAcorn_to_WalkingPlayer = Squirrelfsm.CreateTransition(EatAcorn, WalkPlayer, CheckEatEnded);
            /**/

            //Push Perceptions
            /**/
            stop = new(WalkingAcorn_to_WalkingPlayer, EatingAcorn_to_WalkingPlayer);
            

            Squirrelfsm.SetEntryState(WalkAcorn);

            return Squirrelfsm;

        }
    }
}