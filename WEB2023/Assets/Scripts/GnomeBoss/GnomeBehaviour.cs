using System;
using System.Collections.Generic;
using UnityEngine;
using BehaviourAPI.Core;
using BehaviourAPI.Core.Actions;
using BehaviourAPI.Core.Perceptions;
using BehaviourAPI.UnityToolkit;
using BehaviourAPI.StateMachines;

public class GnomeBehaviour : BehaviourRunner
{
	
	
	protected override BehaviourGraph CreateGraph()
	{
		FSM newbehaviourgraph = new FSM();
		
		//States
		State WalkToPlayer = newbehaviourgraph.CreateState();
		State Tired = newbehaviourgraph.CreateState();
		State WalkAttack = newbehaviourgraph.CreateState();
		State Punch = newbehaviourgraph.CreateState();
		State GnomeMode = newbehaviourgraph.CreateState();
		//Transitions
		StateTransition StopT = newbehaviourgraph.CreateTransition(Tired, WalkToPlayer);		
		StateTransition WARange = newbehaviourgraph.CreateTransition(WalkToPlayer, WalkAttack);		
		StateTransition StopWA = newbehaviourgraph.CreateTransition(WalkAttack, Tired);	
		StateTransition HalfHP = newbehaviourgraph.CreateTransition(WalkToPlayer, GnomeMode);		
		StateTransition StopGM = newbehaviourgraph.CreateTransition(GnomeMode, WalkToPlayer);		
		StateTransition PunchRange = newbehaviourgraph.CreateTransition(WalkToPlayer, Punch);		
		StateTransition StopP = newbehaviourgraph.CreateTransition(Punch, WalkToPlayer);
		
		return newbehaviourgraph;
	}
}
