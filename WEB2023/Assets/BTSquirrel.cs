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

        var squirrelPerception = new ConditionPerception(() => _ActionsSquirrel.consumedAcorn);

        //bt
        var walkAcorn = bt.CreateLeafNode("Walk Acorn", new FunctionalAction(_ActionsSquirrel.StartWalkAcorn, _ActionsSquirrel.UpdateWalkAcorn));
        
        var eatAcorn = bt.CreateLeafNode("Eat Acorn", new FunctionalAction(_ActionsSquirrel.StartEatAcorn, _ActionsSquirrel.UpdateEatAcorn));

        var walkPlayer = bt.CreateLeafNode("Walk Player", new FunctionalAction(_ActionsSquirrel.StartWalkPlayer, _ActionsSquirrel.UpdateWalkPlayer));


        var seq = bt.CreateComposite<SequencerNode>("seq", false, walkAcorn, walkPlayer);  //Nodo secuencia

        var sel = bt.CreateComposite<SelectorNode>("sel", false, walkAcorn, eatAcorn, walkPlayer);


        var loop = bt.CreateDecorator<LoopNode>("loop", seq).SetIterations(-1);

        bt.SetRootNode(loop);

        return bt;
    }

    private void Start()
    {
        bt.Start();
        bt.Update();
    }


}
