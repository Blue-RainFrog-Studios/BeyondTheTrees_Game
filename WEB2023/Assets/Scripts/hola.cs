using System;
using System.Collections.Generic;
using UnityEngine;
using BehaviourAPI.Core;
using BehaviourAPI.Core.Actions;
using BehaviourAPI.Core.Perceptions;
using BehaviourAPI.UnityToolkit;
using BehaviourAPI.StateMachines;

public class hola : BehaviourRunner
{
	[SerializeField] private Transform WalkToPlayer_action_target;
	
	
	protected override BehaviourGraph CreateGraph()
	{
		FSM newbehaviourgraph = new FSM();
		
		ChaseAction WalkToPlayer_action = new ChaseAction();
		WalkToPlayer_action.speed = 0f;
		WalkToPlayer_action.target = WalkToPlayer_action_target;
		WalkToPlayer_action.maxDistance = 0f;
		WalkToPlayer_action.maxTime = 0f;
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
		PunchRange_perception.MaxDistance = 3f;
		StateTransition PunchRange = newbehaviourgraph.CreateTransition(WalkToPlayer, Punch, PunchRange_perception);
		
		StateTransition StopP = newbehaviourgraph.CreateTransition(Punch, WalkToPlayer, statusFlags: StatusFlags.Finished);
		
		return newbehaviourgraph;
	}
}
