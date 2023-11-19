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
        ActionsDavidElGnomoNoHat _ActionsDavidElGnomoNoHat;
        public PushPerception HpLessThanX;
        public PushPerception HpPhaseTwo;
        protected override void Init()
        {
            _ActionsDavidElGnomo = GetComponent<ActionsDavidElGnomo>();
            _ActionsDavidElGnomoNoHat = GetComponent<ActionsDavidElGnomoNoHat>();
            base.Init();
        }

        protected override BehaviourGraph CreateGraph()
        {
            FSM Gnomefsm = new FSM();

            FSM GnomeNoHatfsm = CreateGnomeWithoutHatFSM();

            #region Actions
            FunctionalAction SleepAction = new FunctionalAction(_ActionsDavidElGnomo.StartMethodSleep, _ActionsDavidElGnomo.UpdateMethodSleep); //estados que hay que meter

            FunctionalAction WalkToPlayerAction = new FunctionalAction(_ActionsDavidElGnomo.StartMethodWalk, _ActionsDavidElGnomo.UpdateMethodWalk); //estados que hay que meter
            
            FunctionalAction PunchAction = new FunctionalAction(_ActionsDavidElGnomo.StartMethodPunch, _ActionsDavidElGnomo.UpdateMethodPunch); //estados que hay que meter

            FunctionalAction WalkAttackAction = new FunctionalAction(_ActionsDavidElGnomo.StartMethodWalkAttack, _ActionsDavidElGnomo.UpdateMethodWalkAttack);

            FunctionalAction GnomeModeAction = new FunctionalAction(_ActionsDavidElGnomo.StartMethodGnomeMode, _ActionsDavidElGnomo.UpdateMethodGnomeMode);

            FunctionalAction TiredAction = new FunctionalAction(_ActionsDavidElGnomo.StartMethodTired, _ActionsDavidElGnomo.UpdateMethodTired);

            SubsystemAction GnomeNoHatSubFSM = new SubsystemAction(GnomeNoHatfsm);
            #endregion

            #region States
            State Sleep = Gnomefsm.CreateState(SleepAction); //SOLO REPETIR 1 vez

            State WalkToPlayer = Gnomefsm.CreateState(WalkToPlayerAction); //el estado WalkToPlayer se crea con la acción WalkToPlayerAction
            
            State Punch = Gnomefsm.CreateState(PunchAction);

          
            State WalkAttack = Gnomefsm.CreateState(WalkAttackAction);

            State GnomeMode = Gnomefsm.CreateState(GnomeModeAction); //SOLO REPETIR 1 vez

            State Tired = Gnomefsm.CreateState(TiredAction);

            State NoHat = Gnomefsm.CreateState(GnomeNoHatSubFSM);
            #endregion

            #region Perception
            ConditionPerception isInPunchRange = new ConditionPerception(() =>_ActionsDavidElGnomo.CheckPlayerInPunchRange()); //esto nos va a decir cuando debe cambiar de estado
            /**/
            ConditionPerception leavesPunchRange = new ConditionPerception(() =>!_ActionsDavidElGnomo.CheckPlayerInPunchRange()); //esto nos va a decir cuando debe cambiar de estado

            /**/
            ConditionPerception collisionWithYAxis = new ConditionPerception(() => _ActionsDavidElGnomo.CheckCollisionWithYAxis());
            /**/
            ConditionPerception noCollisionWithYAxis = new ConditionPerception(() => !_ActionsDavidElGnomo.CheckCollisionWithYAxis());

            //ConditionPerception HPLow = new ConditionPerception(() => _ActionsDavidElGnomo.CheckHPLow());

            ConditionPerception HPVeryLow = new ConditionPerception(() => _ActionsDavidElGnomo.CheckHPVeryLow());

            //
            #endregion

            #region Transitions
            StateTransition Sleep_to_WalkToPlayer = Gnomefsm.CreateTransition(Sleep, WalkToPlayer, statusFlags: StatusFlags.Finished); // cambiara cuando se acabe la accion

            StateTransition WalkingToPlayer_to_Punch = Gnomefsm.CreateTransition(WalkToPlayer, Punch, isInPunchRange); // cambiara cuando se cumpla la percepcion 
            /**/
            StateTransition Punch_to_WalkingToPlayer = Gnomefsm.CreateTransition(Punch, WalkToPlayer, /*leavesPunchRange,*/ statusFlags: StatusFlags.Finished); // cambiara cuando se acabe la accion

            /**/
            StateTransition WalkToPlayer_to_WalkAttack = Gnomefsm.CreateTransition(WalkToPlayer, WalkAttack, collisionWithYAxis);
            /**/ //StateTransition WalkAttack_to_WalkToPlayer = Gnomefsm.CreateTransition(WalkAttack, WalkToPlayer, statusFlags: StatusFlags.Finished);

            //push perception de cambio a gnome mode


            StateTransition GnomeMode_to_WalkToPlayer = Gnomefsm.CreateTransition(GnomeMode, WalkToPlayer, statusFlags: StatusFlags.Finished);

            StateTransition WalkToPlayer_to_GnomeMode = Gnomefsm.CreateTransition(WalkToPlayer, GnomeMode, statusFlags: StatusFlags.None);
            StateTransition WalkToPlayerAttack_to_GnomeMode = Gnomefsm.CreateTransition(WalkAttack, GnomeMode, statusFlags: StatusFlags.None);
            StateTransition Punch_to_GnomeMode = Gnomefsm.CreateTransition(Punch, GnomeMode, statusFlags: StatusFlags.None);
            StateTransition Tired_to_GnomeMode = Gnomefsm.CreateTransition(Tired, GnomeMode, statusFlags: StatusFlags.None);


            StateTransition WalkToPlayer_to_NoHat = Gnomefsm.CreateTransition(WalkToPlayer, NoHat, statusFlags: StatusFlags.None);
            StateTransition WalkToPlayerAttack_to_NoHat = Gnomefsm.CreateTransition(WalkAttack, NoHat, statusFlags: StatusFlags.None);
            StateTransition Punch_to_NoHat = Gnomefsm.CreateTransition(Punch, NoHat, statusFlags: StatusFlags.None);
            StateTransition Tired_to_NoHat = Gnomefsm.CreateTransition(Tired, NoHat, statusFlags: StatusFlags.None);




            //push perceptions

            StateTransition WalkingToPlayerAttack_to_Tired = Gnomefsm.CreateTransition(WalkAttack, Tired, statusFlags: StatusFlags.Success);
            StateTransition Tired_to_WalkingToPlayer = Gnomefsm.CreateTransition(Tired, WalkToPlayer, statusFlags: StatusFlags.Finished);
            #endregion

            #region Push Perceptions
            HpLessThanX = new PushPerception(WalkToPlayer_to_GnomeMode, WalkToPlayerAttack_to_GnomeMode, Punch_to_GnomeMode, Tired_to_GnomeMode);
            HpPhaseTwo = new PushPerception(WalkToPlayer_to_NoHat, WalkToPlayerAttack_to_NoHat, Punch_to_NoHat, Tired_to_NoHat);
            #endregion

            return Gnomefsm;
        }

        private FSM CreateGnomeWithoutHatFSM()
        {
            FSM GnomeWithoutHatFSM = new FSM();
            
            #region Actions
            FunctionalAction WalkToPlayerActionNoHat = new FunctionalAction(_ActionsDavidElGnomoNoHat.StartMethodWalkNoHat, _ActionsDavidElGnomoNoHat.UpdateMethodWalkNoHat); 
            FunctionalAction PunchActionNoHat = new FunctionalAction(_ActionsDavidElGnomoNoHat.StartMethodPunchNoHat, _ActionsDavidElGnomoNoHat.UpdateMethodPunchNoHat); 

            FunctionalAction WalkAttackActionNoHat = new FunctionalAction(_ActionsDavidElGnomoNoHat.StartMethodWalkAttackNoHat, _ActionsDavidElGnomoNoHat.UpdateMethodWalkAttackNoHat);

            FunctionalAction GnomeModeActionNoHat = new FunctionalAction(_ActionsDavidElGnomoNoHat.StartMethodGnomeModeNoHat, _ActionsDavidElGnomoNoHat.UpdateMethodGnomeModeNoHat);

            FunctionalAction TiredActionNoHat = new FunctionalAction(_ActionsDavidElGnomoNoHat.StartMethodTiredNoHat, _ActionsDavidElGnomoNoHat.UpdateMethodTiredNoHat);
            #endregion

            #region States
            State GnomeModeNoHat = GnomeWithoutHatFSM.CreateState(GnomeModeActionNoHat); //SOLO REPETIR 1 vez

            State WalkToPlayerNoHat = GnomeWithoutHatFSM.CreateState(WalkToPlayerActionNoHat); //el estado WalkToPlayer se crea con la acción WalkToPlayerAction

            State PunchNoHat = GnomeWithoutHatFSM.CreateState(PunchActionNoHat);

            State WalkAttackNoHat = GnomeWithoutHatFSM.CreateState(WalkAttackActionNoHat);


            State TiredNoHat = GnomeWithoutHatFSM.CreateState(TiredActionNoHat);
            #endregion

            #region Perceptions
            ConditionPerception isInPunchRangeNoHat = new ConditionPerception(() => _ActionsDavidElGnomoNoHat.CheckPlayerInPunchRangeNoHat()); 
            ConditionPerception leavesPunchRangeNoHat = new ConditionPerception(() => !_ActionsDavidElGnomoNoHat.CheckPlayerInPunchRangeNoHat()); 

            ConditionPerception collisionWithYAxisNoHat = new ConditionPerception(() => _ActionsDavidElGnomoNoHat.CheckCollisionWithYAxisNoHat());
            ConditionPerception noCollisionWithYAxisNoHat = new ConditionPerception(() => !_ActionsDavidElGnomoNoHat.CheckCollisionWithYAxisNoHat());

            ConditionPerception HPLowNoHat = new ConditionPerception(() => _ActionsDavidElGnomoNoHat.CheckHPLowNoHat());
            #endregion

            #region Transitions
            StateTransition WalkingToPlayerNoHat_to_PunchNoHat = GnomeWithoutHatFSM.CreateTransition(WalkToPlayerNoHat, PunchNoHat, isInPunchRangeNoHat); 
            StateTransition PunchNoHat_to_WalkingToPlayerNoHat = GnomeWithoutHatFSM.CreateTransition(PunchNoHat, WalkToPlayerNoHat, /*leavesPunchRange,*/ statusFlags: StatusFlags.Finished); // cambiara cuando se acabe la accion

            StateTransition WalkToPlayerNoHat_to_WalkAttackNoHat = GnomeWithoutHatFSM.CreateTransition(WalkToPlayerNoHat, WalkAttackNoHat, collisionWithYAxisNoHat);

            StateTransition GnomeModeNoHat_to_WalkToPlayerNoHat = GnomeWithoutHatFSM.CreateTransition(GnomeModeNoHat, WalkToPlayerNoHat, statusFlags: StatusFlags.Finished);
            StateTransition WalkToPlayerNoHat_to_GnomeModeNoHat = GnomeWithoutHatFSM.CreateTransition(WalkToPlayerNoHat, GnomeModeNoHat, HPLowNoHat);
            StateTransition WalkToPlayerAttackNoHat_to_GnomeModeNoHat = GnomeWithoutHatFSM.CreateTransition(WalkAttackNoHat, GnomeModeNoHat, HPLowNoHat);
            StateTransition PunchNoHat_to_GnomeModeNoHat = GnomeWithoutHatFSM.CreateTransition(PunchNoHat, GnomeModeNoHat, HPLowNoHat);
            StateTransition TiredNoHat_to_GnomeModeNoHat = GnomeWithoutHatFSM.CreateTransition(TiredNoHat, GnomeModeNoHat, HPLowNoHat);

            StateTransition WalkingToPlayerAttackNoHat_to_TiredNoHat = GnomeWithoutHatFSM.CreateTransition(WalkAttackNoHat, TiredNoHat, statusFlags: StatusFlags.Success);
            StateTransition TiredNoHat_to_WalkingToPlayerNoHat = GnomeWithoutHatFSM.CreateTransition(TiredNoHat, WalkToPlayerNoHat, statusFlags: StatusFlags.Finished);
            #endregion

            return GnomeWithoutHatFSM;
        }

        public void ChangeToGnomeMode()
        {
            HpLessThanX.Fire();
        }
        public void ChangeToNoHat()
        {
            Debug.Log("Change to no hat");
            HpPhaseTwo.Fire();
        }

    }

}