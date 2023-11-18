using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourAPI.Core;
using BehaviourAPI.Core.Actions;
using BehaviourAPI.StateMachines;
using BehaviourAPI.Core.Perceptions;
using BehaviourAPI.UnityToolkit;

public class FSMSpookyTree : BehaviourRunner
{
    private ActionsSpookyTree _ActionsSpookyTree;
    protected override void Init()
    {
        _ActionsSpookyTree = GetComponent<ActionsSpookyTree>();
        base.Init();
    }
    protected override BehaviourGraph CreateGraph()
    { 
        FSM TreeFSM = new FSM();

        #region Actions
        FunctionalAction SleepAction = new FunctionalAction(_ActionsSpookyTree.StartMethodSleep, _ActionsSpookyTree.UpdateMethodSleep, _ActionsSpookyTree.StopMethodSleep);
    
        FunctionalAction SharpLeavesP1Action = new FunctionalAction(_ActionsSpookyTree.StartMethodSharpLeavesP1, _ActionsSpookyTree.UpdateMethodSharpLeavesP1);

        FunctionalAction SharpLeavesP2Action = new FunctionalAction(_ActionsSpookyTree.StartMethodSharpLeavesP2, _ActionsSpookyTree.UpdateMethodSharpLeavesP2);

        FunctionalAction GhostSpawnAction = new FunctionalAction(_ActionsSpookyTree.StartMethodGhostSpawn, _ActionsSpookyTree.UpdateMethodGhostSpawn);

        FunctionalAction SharpRootsAction = new FunctionalAction(_ActionsSpookyTree.StartMethodSharpRoots, _ActionsSpookyTree.UpdateMethodSharpRoots);

        FunctionalAction RootsOutAction = new FunctionalAction(_ActionsSpookyTree.StartMethodRootsOut,_ActionsSpookyTree.UpdateMethodRootsOut);
        #endregion

        #region States
        //State Sleep = TreeFSM.CreateState("Sleep", SleepAction);
        State SharpLeavesP1 = TreeFSM.CreateState("SharpLeavesP1", SharpLeavesP1Action);
        State SharpLeavesP2 = TreeFSM.CreateState("SharpLeavesP2", SharpLeavesP2Action);
        State GhostSpawn = TreeFSM.CreateState("GhostSpawn", GhostSpawnAction);
        State SharpRoots = TreeFSM.CreateState("SharpRoots", SharpRootsAction);
        ProbabilisticState RootsOut = TreeFSM.CreateProbabilisticState("RootsOut", RootsOutAction);
        #endregion

        #region Transitions

        //sleep
        //StateTransition Sleep_to_RootsOut = TreeFSM.CreateTransition(Sleep, RootsOut, statusFlags:StatusFlags.Finished);
        //rootOut
        StateTransition RootsOut_to_SharpLeaves1 = TreeFSM.CreateTransition(RootsOut, SharpLeavesP1, statusFlags:StatusFlags.Finished);
        RootsOut.SetProbability(RootsOut_to_SharpLeaves1, 0.3f);
        StateTransition RootsOut_to_SharpLeaves2 = TreeFSM.CreateTransition(RootsOut, SharpLeavesP2, statusFlags:StatusFlags.Finished);
        RootsOut.SetProbability(RootsOut_to_SharpLeaves2, 0.3f);
        StateTransition RootsOut_to_GhostSpawn = TreeFSM.CreateTransition(RootsOut, GhostSpawn, statusFlags:StatusFlags.Finished);
        RootsOut.SetProbability(RootsOut_to_GhostSpawn, 0.2f);
        StateTransition RootsOut_to_SharpRoots = TreeFSM.CreateTransition(RootsOut, SharpRoots, statusFlags:StatusFlags.Finished);
        RootsOut.SetProbability(RootsOut_to_SharpRoots, 0.2f);
        //else to rootOut
        StateTransition SharpLeaves1_to_RootsOut = TreeFSM.CreateTransition(SharpLeavesP1, RootsOut, statusFlags:StatusFlags.Finished);
        StateTransition SharpLeaves2_to_RootsOut = TreeFSM.CreateTransition(SharpLeavesP2, RootsOut, statusFlags:StatusFlags.Finished);
        StateTransition GhostSpawn_to_RootsOut = TreeFSM.CreateTransition(GhostSpawn, RootsOut, statusFlags:StatusFlags.Finished);
        StateTransition SharpRoots_to_RootsOut = TreeFSM.CreateTransition(SharpRoots, RootsOut, statusFlags:StatusFlags.Finished);

        #endregion



        return TreeFSM;
    }
    }
