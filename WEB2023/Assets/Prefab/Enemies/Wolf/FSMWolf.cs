using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourAPI.Core;
using BehaviourAPI.Core.Actions;
using BehaviourAPI.StateMachines;
using BehaviourAPI.Core.Perceptions;
using BehaviourAPI.UnityToolkit;
using BehaviourAPI.StateMachines.StackFSMs;


public class FSMWolf : BehaviourRunner
{

    Actions_Wolf _ActionsWolf;
    public PushPerception itemExist; //mirar distanca a enemigo
    public PushPerception iCrashed; //mirar distanca a enemigo
    public PushPerception iLoseItem; // Comprueba si el item se ha perdido
    protected override void Init()
    {
        _ActionsWolf = GetComponent<Actions_Wolf>();
        base.Init();
    }
    protected override BehaviourGraph CreateGraph()
    {
        //StackFSM WolfFsm = new StackFSM();
        FSM WolfFsm = new FSM();

        #region Actions
        FunctionalAction WatingAction = new FunctionalAction(_ActionsWolf.StartMethodWaiting, _ActionsWolf.UpdateMethodWaiting); //estados que hay que meter

        FunctionalAction WalkToPlayerAction = new FunctionalAction(_ActionsWolf.StartMethodWalk, _ActionsWolf.UpdateMethodWalk); //estados que hay que meter

        FunctionalAction ChargingToPlayerAction = new FunctionalAction(_ActionsWolf.StartMethodCharge, _ActionsWolf.UpdateMethodCharge); //estados que hay que meter

        FunctionalAction BiteToPlayerAction = new FunctionalAction(_ActionsWolf.StartMethodBiteAttack, _ActionsWolf.UpdateMethodBiteAttack);

        FunctionalAction DazedWolf = new FunctionalAction(_ActionsWolf.StartMethodDazed, _ActionsWolf.UpdateMethodDazed);

        FunctionalAction DazeToPlayerAction = new FunctionalAction(_ActionsWolf.StartMethodWalk, _ActionsWolf.UpdateMethodWalk);

        FunctionalAction WalkToItemAction = new FunctionalAction(_ActionsWolf.StartMethodItemChase, _ActionsWolf.UpdateMethodItemChase);

        FunctionalAction DestroyItem = new FunctionalAction(_ActionsWolf.StartMethodDestroyItem, _ActionsWolf.UpdateMethodDestroyItem);


        //------------------ cambio de maquina
        //FunctionalAction TiredAction = new FunctionalAction(_ActionsWolf.StartMethodTired, _ActionsWolf.UpdateMethodTired);
        //SubsystemAction GnomeNoHatSubFSM = new SubsystemAction(GnomeNoHatfsm);
        #endregion

        #region States
        State Wating = WolfFsm.CreateState(WatingAction); //SOLO REPETIR 1 vez

        State WalkToPlayer = WolfFsm.CreateState(WalkToPlayerAction); //el estado WalkToPlayer se crea con la acción WalkToPlayerAction

        State WalkToItem = WolfFsm.CreateState(WalkToItemAction);

        State DestroyingItem = WolfFsm.CreateState(DestroyItem);
        
        State Charging = WolfFsm.CreateState(ChargingToPlayerAction); 

        State Bite = WolfFsm.CreateState(BiteToPlayerAction); 

        State Dazed = WolfFsm.CreateState(DazedWolf); 



        #endregion

        #region Perception
        ConditionPerception isInBiteRange = new ConditionPerception(() => _ActionsWolf.CheckPlayerInBiteRange()); //esto nos va a decir cuando debe cambiar de estado
        /**/
        ConditionPerception leavesBiteRange = new ConditionPerception(() => !_ActionsWolf.CheckPlayerInBiteRange()); //esto nos va a decir cuando debe cambiar de estado

        /**/
        ConditionPerception collisionWithYAxis = new ConditionPerception(() => _ActionsWolf.CheckCollisionWithYAxis());
        /**/
        ConditionPerception noCollisionWithYAxis = new ConditionPerception(() => !_ActionsWolf.CheckCollisionWithYAxis());

        ConditionPerception HPLow = new ConditionPerception(() => _ActionsWolf.CheckHPLow());

        ConditionPerception itemExist = new ConditionPerception(() => _ActionsWolf.CheckItemExist());

        ConditionPerception itemPickedByPlayer = new ConditionPerception(() => !_ActionsWolf.CheckItemExist());

        ConditionPerception itemDestroyed = new ConditionPerception(() => _ActionsWolf.CheckItemExist());


        //
        #endregion

        #region Transitions
        StateTransition Wating_to_WalkToPlayer = WolfFsm.CreateTransition(Wating, WalkToPlayer, statusFlags: StatusFlags.Finished); // cambiara cuando se acabe la accion

        StateTransition WalkingToPlayer_to_Charging = WolfFsm.CreateTransition(WalkToPlayer, Charging, isInBiteRange); // cambiara cuando se cumpla la percepcion 
        

        /**/
        //StateTransition WalkToPlayer_to_WalkAttack = WolfFsm.CreateTransition(WalkToPlayer, WalkAttack, collisionWithYAxis); // old gnome

        StateTransition Charging_to_Bite = WolfFsm.CreateTransition(Charging, Bite, statusFlags: StatusFlags.Finished); // new wolf 

        StateTransition Bite_to_WalkToPlayer = WolfFsm.CreateTransition(Bite, WalkToPlayer, statusFlags: StatusFlags.Finished); // new wolf 

        StateTransition WalkingToPlayer_to_WalkingItem = WolfFsm.CreateTransition(WalkToPlayer, WalkToItem, itemExist);

        StateTransition WalkingItem_to_DestroyingItem = WolfFsm.CreateTransition(WalkToItem, DestroyingItem, statusFlags: StatusFlags.Success);
                                                                                                                   //De momento solo se tiene en cuenta que consigue comer                                 
        StateTransition DestroyingItem_to_WalkingToPlayer = WolfFsm.CreateTransition(DestroyingItem, WalkToPlayer, statusFlags: StatusFlags.Success);


        //push perceptions

        //StateTransition Bite_to_Dazed = WolfFsm.CreateTransition(Bite, Dazed, collisionWithYAxis); // new wolf dazed
        StateTransition Bite_to_Dazed = WolfFsm.CreateTransition(Bite, Dazed, statusFlags: StatusFlags.Success); // new wolf dazed
        //StateTransition WalkingToPlayerAttack_to_Tired = WolfFsm.CreateTransition(WalkAttack, Tired, statusFlags: StatusFlags.Success);
        StateTransition Dazed_to_WalkingToPlayer = WolfFsm.CreateTransition(Dazed, WalkToPlayer, statusFlags: StatusFlags.Finished);


        StateTransition WalkingItem_to_WalkToPlayer = WolfFsm.CreateTransition(WalkToItem, WalkToPlayer, statusFlags: StatusFlags.Finished);
        #endregion

        #region Push Perceptions
        iCrashed = new PushPerception(Bite_to_Dazed);
        iLoseItem = new PushPerception(WalkingItem_to_WalkToPlayer);
        //itemExist = new PushPerception(WalkToPlayer_to_WalkToItem, Waitintg_to_WalkToItem, Bite_to_WalkToItem, Dazed_to_WalkToItem); //para cuando tenga el imtem
        #endregion

        return WolfFsm;
    }

    public void Crash()
    {
        iCrashed.Fire();
    }

    public void LoseItem()
    {
        iLoseItem.Fire();
    }
}
