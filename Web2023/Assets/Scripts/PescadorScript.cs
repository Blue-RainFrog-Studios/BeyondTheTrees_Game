using System;
using System.Collections.Generic;
using UnityEngine;
using BehaviourAPI.Core;
using BehaviourAPI.Core.Actions;
using BehaviourAPI.Core.Perceptions;
using BehaviourAPI.UnityToolkit;
using BehaviourAPI.BehaviourTrees;

public class PescadorScript : BehaviourRunner
{
	
	
	protected override BehaviourGraph CreateGraph()
	{
		BehaviourTree BehaviourTree = new BehaviourTree();
		
		FunctionalAction LeafNode_action = new FunctionalAction();
		LeafNode_action.onStarted = StartThrow;
		LeafNode_action.onUpdated = RodThrownStatus;
		LeafNode LeafNode = BehaviourTree.CreateLeafNode(LeafNode_action);
		
		FunctionalAction LeafNode_1_action = new FunctionalAction();
		LeafNode_1_action.onStarted = StartCatch;
		LeafNode_1_action.onUpdated = RodPickedStatus;
		LeafNode LeafNode_1 = BehaviourTree.CreateLeafNode(LeafNode_1_action);
		
		UnityTimerDecorator UnityTimerDecorator = BehaviourTree.CreateDecorator<UnityTimerDecorator>(LeafNode_1);
		UnityTimerDecorator.TotalTime = 3f;
		
		FunctionalAction LeafNode_2_action = new FunctionalAction();
		LeafNode_2_action.onStarted = StoreCaptureInBasket;
		LeafNode_2_action.onUpdated = CompleteOnSuccess;
		LeafNode LeafNode_2 = BehaviourTree.CreateLeafNode(LeafNode_2_action);
		
		ConditionNode ConditionNode = BehaviourTree.CreateDecorator<ConditionNode>(LeafNode_2);
		
		FunctionalAction LeafNode_3_action = new FunctionalAction();
		LeafNode_3_action.onStarted = DropCaptureInWater;
		LeafNode_3_action.onUpdated = CompleteOnSuccess;
		LeafNode LeafNode_3 = BehaviourTree.CreateLeafNode(LeafNode_3_action);
		
		SelectorNode SelectorNode = BehaviourTree.CreateComposite<SelectorNode>(false, ConditionNode, LeafNode_3);
		SelectorNode.IsRandomized = false;
		
		SequencerNode SequencerNode = BehaviourTree.CreateComposite<SequencerNode>(false, LeafNode, UnityTimerDecorator, SelectorNode);
		SequencerNode.IsRandomized = false;
		
		LoopNode LoopNode = BehaviourTree.CreateDecorator<LoopNode>(SequencerNode);
		LoopNode.Iterations = -1;
		
		return BehaviourTree;
	}
	
	private void StartThrow()
	{
		throw new System.NotImplementedException();
	}
	
	private Status RodThrownStatus()
	{
		throw new System.NotImplementedException();
	}
	
	private void StartCatch()
	{
		throw new System.NotImplementedException();
	}
	
	private Status RodPickedStatus()
	{
		throw new System.NotImplementedException();
	}
	
	private void StoreCaptureInBasket()
	{
		throw new System.NotImplementedException();
	}
	
	private Status CompleteOnSuccess()
	{
		throw new System.NotImplementedException();
	}
	
	private void DropCaptureInWater()
	{
		throw new System.NotImplementedException();
	}
}
