using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourAPI.Core;
using BehaviourAPI.BehaviourTrees;
using BehaviourAPI.UnityToolkit;
using BehaviourAPI.Core.Perceptions;
using BehaviourAPI.Core.Actions;

public class BTSquirrel : BehaviourRunner
{

    private GameObject playerPrefab;

    ActionsSquirrel _ActionsSquirrel;

    BehaviourTree bt = new BehaviourTree();

    protected override void Init()
    {
        playerPrefab = GameObject.FindGameObjectWithTag("Player");
        _ActionsSquirrel = GetComponent<ActionsSquirrel>();
        base.Init();
    }

    protected override BehaviourGraph CreateGraph()
    {
        //Perceptions
        var squirrelPerception = new ConditionPerception(() => !_ActionsSquirrel.consumedAcorn);

        var rolePerception = new ConditionPerception(() => _ActionsSquirrel.rol == "Protector");

        var otherSquirrels = new ConditionPerception(() => true);


        //bt
        var walkAcorn = bt.CreateLeafNode("Walk Acorn", new FunctionalAction(_ActionsSquirrel.StartWalkAcorn, _ActionsSquirrel.UpdateWalkAcorn));
        
        var eatAcorn = bt.CreateLeafNode("Eat Acorn", new FunctionalAction(_ActionsSquirrel.StartEatAcorn, _ActionsSquirrel.UpdateEatAcorn));

        var walkPlayer = bt.CreateLeafNode("Walk Player", new FunctionalAction(_ActionsSquirrel.StartWalkPlayer, _ActionsSquirrel.UpdateWalkPlayer));



        //Nodes
        var otherEnemy = bt.CreateDecorator<ConditionNode>("Other Enemy", walkAcorn).SetPerception(rolePerception);

        var seq2 = bt.CreateComposite<SequencerNode>("seq2", false, otherEnemy);  //Nodo secuencia

        var sel2 = bt.CreateComposite<SelectorNode>("Selector 2", false, seq2);

        var acornExists = bt.CreateDecorator<ConditionNode>("Acorn Exists", sel2).SetPerception(squirrelPerception);

        var seq1 = bt.CreateComposite<SequencerNode>("seq1", false, acornExists);  //Nodo secuencia

        var sel1 = bt.CreateComposite<SelectorNode>("Selector 1", false, seq1, walkPlayer);

        var loop = bt.CreateDecorator<LoopNode>("loop", sel1).SetIterations(-1);  //Nodo loop

        bt.SetRootNode(loop);

        return bt;
    }

    private void Start()
    {
        bt.Start();
        bt.Update();
    }


}
