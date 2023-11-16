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
            /**/
            FunctionalAction WalkToPlayerAction = new FunctionalAction(_ActionsDavidElGnomo.StartMethodWalk, _ActionsDavidElGnomo.UpdateMethodWalk); //estados que hay que meter
            /**/
            FunctionalAction PunchAction = new FunctionalAction(_ActionsDavidElGnomo.StartMethodPunch, _ActionsDavidElGnomo.UpdateMethodPunch); //estados que hay que meter

            /**/
            FunctionalAction WalkAttackAction = new FunctionalAction(_ActionsDavidElGnomo.StartMethodWalkAttack, _ActionsDavidElGnomo.UpdateMethodWalkAttack);

            FunctionalAction GnomeModeAction = new FunctionalAction(_ActionsDavidElGnomo.StartMethodGnomeMode, _ActionsDavidElGnomo.UpdateMethodGnomeMode);

            FunctionalAction TiredAction = new FunctionalAction(_ActionsDavidElGnomo.StartMethodTired, _ActionsDavidElGnomo.UpdateMethodTired);

            //states
            /**/
            State WalkToPlayer = Gnomefsm.CreateState(WalkToPlayerAction); //el estado WalkToPlayer se crea con la acción WalkToPlayerAction
            /**/
            State Punch = Gnomefsm.CreateState(PunchAction);

            /**/
            State WalkAttack = Gnomefsm.CreateState(WalkAttackAction);

            State GnomeMode = Gnomefsm.CreateState(GnomeModeAction); //SOLO REPETIR 1 vez

            State Tired = Gnomefsm.CreateState(TiredAction);

            //Perceptions
            /**/
            ConditionPerception isInPunchRange = new ConditionPerception((/*Parametros*/) =>/*expresion*/_ActionsDavidElGnomo.CheckPlayerInPunchRange()); //esto nos va a decir cuando debe cambiar de estado
            /**/
            ConditionPerception leavesPunchRange = new ConditionPerception((/*Parametros*/) =>/*expresion*/!_ActionsDavidElGnomo.CheckPlayerInPunchRange()); //esto nos va a decir cuando debe cambiar de estado

            /**/
            ConditionPerception collisionWithYAxis = new ConditionPerception(() => _ActionsDavidElGnomo.CheckCollisionWithYAxis());
            /**/
            ConditionPerception noCollisionWithYAxis = new ConditionPerception(() => !_ActionsDavidElGnomo.CheckCollisionWithYAxis());

            ConditionPerception HPLow = new ConditionPerception(() => _ActionsDavidElGnomo.CheckHPLow());

            //Transitions
            /**/
            StateTransition WalkingToPlayer_to_Punch = Gnomefsm.CreateTransition(WalkToPlayer, Punch, isInPunchRange); // cambiara cuando se cumpla la percepcion 
            /**/
            StateTransition Punch_to_WalkingToPlayer = Gnomefsm.CreateTransition(Punch, WalkToPlayer, /*leavesPunchRange,*/ statusFlags: StatusFlags.Finished); // cambiara cuando se acabe la accion

            /**/
            StateTransition WalkToPlayer_to_WalkAttack = Gnomefsm.CreateTransition(WalkToPlayer, WalkAttack, collisionWithYAxis);
            /**/ //StateTransition WalkAttack_to_WalkToPlayer = Gnomefsm.CreateTransition(WalkAttack, WalkToPlayer, statusFlags: StatusFlags.Finished);

            StateTransition GnomeMode_to_WalkToPlayer = Gnomefsm.CreateTransition(GnomeMode, WalkToPlayer, statusFlags: StatusFlags.Finished);
            StateTransition WalkToPlayer_to_GnomeMode = Gnomefsm.CreateTransition(WalkToPlayer, GnomeMode, HPLow);
            StateTransition WalkToPlayerAttack_to_GnomeMode = Gnomefsm.CreateTransition(WalkAttack, GnomeMode, HPLow);
            StateTransition Punch_to_GnomeMode = Gnomefsm.CreateTransition(Punch, GnomeMode, HPLow);
            StateTransition Tired_to_GnomeMode = Gnomefsm.CreateTransition(Tired, GnomeMode, HPLow);

            StateTransition WalkingToPlayerAttack_to_Tired = Gnomefsm.CreateTransition(WalkAttack, Tired, statusFlags: StatusFlags.Success);
            StateTransition Tired_to_WalkingToPlayer = Gnomefsm.CreateTransition(Tired, WalkToPlayer, statusFlags: StatusFlags.Finished);

            /**/
            return Gnomefsm;
        }

        private FSM CreateGnomeWithoutHatFSM()
        {
            FSM GnomeWithoutHatFSM = new FSM();
            //actions
            FunctionalAction WalkToPlayerActionNoHat = new FunctionalAction(); //estados que hay que meter
            FunctionalAction PunchActionNoHat = new FunctionalAction(); //estados que hay que meter

            FunctionalAction WalkAttackActionNoHat = new FunctionalAction();

            FunctionalAction GnomeModeActionNoHat = new FunctionalAction();

            FunctionalAction TiredActionNoHat = new FunctionalAction();

            //states
            State WalkToPlayerNoHat = GnomeWithoutHatFSM.CreateState(WalkToPlayerActionNoHat); //el estado WalkToPlayer se crea con la acción WalkToPlayerAction
            State PunchNoHat = GnomeWithoutHatFSM.CreateState(PunchActionNoHat);

            State WalkAttackNoHat = GnomeWithoutHatFSM.CreateState(WalkAttackActionNoHat);

            State GnomeModeNoHat = GnomeWithoutHatFSM.CreateState(GnomeModeActionNoHat); //SOLO REPETIR 1 vez

            State TiredNoHat = GnomeWithoutHatFSM.CreateState(TiredActionNoHat);

            //Perceptions
            ConditionPerception isInPunchRangeNoHat = new ConditionPerception(); //esto nos va a decir cuando debe cambiar de estado
            ConditionPerception leavesPunchRangeNoHat = new ConditionPerception(); //esto nos va a decir cuando debe cambiar de estado

            ConditionPerception collisionWithYAxisNoHat = new ConditionPerception();
            ConditionPerception noCollisionWithYAxisNoHat = new ConditionPerception();

            ConditionPerception HPLowNoHat = new ConditionPerception(() => _ActionsDavidElGnomo.CheckHPLow());

            //Transitions
            StateTransition WalkingToPlayerNoHat_to_PunchNoHat = GnomeWithoutHatFSM.CreateTransition(WalkToPlayerNoHat, PunchNoHat, isInPunchRangeNoHat); // cambiara cuando se cumpla la percepcion 
            StateTransition PunchNoHat_to_WalkingToPlayerNoHat = GnomeWithoutHatFSM.CreateTransition(PunchNoHat, WalkToPlayerNoHat, /*leavesPunchRange,*/ statusFlags: StatusFlags.Finished); // cambiara cuando se acabe la accion

            StateTransition WalkToPlayer_to_WalkAttack = GnomeWithoutHatFSM.CreateTransition(WalkToPlayerNoHat, WalkAttackNoHat, collisionWithYAxisNoHat);

            StateTransition GnomeModeNoHat_to_WalkToPlayerNoHat = GnomeWithoutHatFSM.CreateTransition(GnomeModeNoHat, WalkToPlayerNoHat, statusFlags: StatusFlags.Finished);
            StateTransition WalkToPlayerNoHat_to_GnomeModeNoHat = GnomeWithoutHatFSM.CreateTransition(WalkToPlayerNoHat, GnomeModeNoHat, HPLowNoHat);
            StateTransition WalkToPlayerAttackNoHat_to_GnomeModeNoHat = GnomeWithoutHatFSM.CreateTransition(WalkAttackNoHat, GnomeModeNoHat, HPLowNoHat);
            StateTransition PunchNoHat_to_GnomeModeNoHat = GnomeWithoutHatFSM.CreateTransition(PunchNoHat, GnomeModeNoHat, HPLowNoHat);
            StateTransition TiredNoHat_to_GnomeModeNoHat = GnomeWithoutHatFSM.CreateTransition(TiredNoHat, GnomeModeNoHat, HPLowNoHat);

            StateTransition WalkingToPlayerAttackNoHat_to_TiredNoHat = GnomeWithoutHatFSM.CreateTransition(WalkAttackNoHat, TiredNoHat, statusFlags: StatusFlags.Success);
            StateTransition TiredNoHat_to_WalkingToPlayerNoHat = GnomeWithoutHatFSM.CreateTransition(TiredNoHat, WalkToPlayerNoHat, statusFlags: StatusFlags.Finished);

            return GnomeWithoutHatFSM;
        }

    }

}