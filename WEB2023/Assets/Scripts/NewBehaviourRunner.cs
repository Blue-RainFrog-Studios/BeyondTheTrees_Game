using System;
using System.Collections.Generic;
using UnityEngine;
using BehaviourAPI.Core;
using BehaviourAPI.Core.Actions;
using BehaviourAPI.Core.Perceptions;
using BehaviourAPI.UnityToolkit;
using BehaviourAPI.StateMachines;
public class NewBehaviourRunner : BehaviourRunner
{
	[SerializeField] private Transform WalkToPlayer_action_target;
	[SerializeField] private GameObject DavidElGnomo;
	
	protected override BehaviourGraph CreateGraph()
	{
		FSM newbehaviourgraph = new FSM();
		
		ChaseAction WalkToPlayer_action = new ChaseAction();
		WalkToPlayer_action.speed = 1f;
		WalkToPlayer_action.target = WalkToPlayer_action_target;
		WalkToPlayer_action.maxDistance = 100f;
		WalkToPlayer_action.maxTime = 100f;

		FunctionalAction WalkAttack_Action = new FunctionalAction();
		WalkAttack_Action.onUpdated = () => Status.Running; 


        State WalkToPlayer = newbehaviourgraph.CreateState(WalkToPlayer_action);
		
		State Tired = newbehaviourgraph.CreateState();
		
		State WalkAttack = newbehaviourgraph.CreateState();
		
		State Punch = newbehaviourgraph.CreateState();
		
		State GnomeMode = newbehaviourgraph.CreateState();
		
		StateTransition StopT = newbehaviourgraph.CreateTransition(Tired, WalkToPlayer, statusFlags: StatusFlags.Finished);
		
		StateTransition WARange = newbehaviourgraph.CreateTransition(WalkToPlayer, WalkAttack);
		
		StateTransition StopWA = newbehaviourgraph.CreateTransition(WalkAttack, Tired, statusFlags: StatusFlags.Finished);
		
		StateTransition HalfHP = newbehaviourgraph.CreateTransition(WalkToPlayer, GnomeMode);
		
		StateTransition StopGM = newbehaviourgraph.CreateTransition(GnomeMode, WalkToPlayer, statusFlags: StatusFlags.Finished);
		
		DistancePerception PunchRange_perception = new DistancePerception();
		PunchRange_perception.OtherTransform = WalkToPlayer_action_target;
		PunchRange_perception.MaxDistance = 0f;
		StateTransition PunchRange = newbehaviourgraph.CreateTransition(WalkToPlayer, Punch, PunchRange_perception);
		
		StateTransition StopP = newbehaviourgraph.CreateTransition(Punch, WalkToPlayer, statusFlags: StatusFlags.Finished);
		
		return newbehaviourgraph;
	}
}
