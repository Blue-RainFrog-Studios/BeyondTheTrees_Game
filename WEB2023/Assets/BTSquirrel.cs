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


    ActionsSquirrel _ActionsSquirrel;

    

    protected override void Init()
    {
        _ActionsSquirrel = GetComponent<ActionsSquirrel>();
        base.Init();
    }

    protected override BehaviourGraph CreateGraph()
    {
        BehaviourTree bt = new BehaviourTree();

        //Conditions Node-------------------------------------------------------------------------------------------------------------------------------

        //HayBellota? Condition
        var acornExists_action = new FunctionalAction(() => { return !_ActionsSquirrel.CheckAcornEated() ? Status.Success : Status.Failure; });
        //HayArdillaCome? Condition
        var squirrelEater_action = new FunctionalAction(() => {return _ActionsSquirrel.CheckSquirrelEater() ? Status.Success : Status.Failure; });

        //Leafs Nodes-----------------------------------------------------------------------------------------------------------------------------------

        //Nodo  Ir a por el jugador
        var walkPlayer = bt.CreateLeafNode("Walk Player", new FunctionalAction(_ActionsSquirrel.StartWalkPlayer, _ActionsSquirrel.UpdateWalkPlayer));
        //Nodo HayBellota?
        var acornExists = bt.CreateLeafNode("Acorn?", acornExists_action);
        //Nodo HayArdillaCome?
        var squirrelEater = bt.CreateLeafNode("Eater?", squirrelEater_action);
        //Nodo Proteger a la ardilla que come (las que protegen se ponen en perpendicular a la que come)
        var protect = bt.CreateLeafNode("Protect", new FunctionalAction(_ActionsSquirrel.StartProtecting, _ActionsSquirrel.UpdateProtecting));
        //Nodo Ir a por la bellota
        var walkAcorn = bt.CreateLeafNode("Walk Acorn", new FunctionalAction(_ActionsSquirrel.StartWalkAcorn, _ActionsSquirrel.UpdateWalkAcorn));
        //Nodo Comer la bellota
        var eatAcorn = bt.CreateLeafNode("Eat Acorn", new FunctionalAction(_ActionsSquirrel.StartEatAcorn, _ActionsSquirrel.UpdateEatAcorn));


        //Sequence Node 3 (IrBellota y ComerBellota)
        var seq3 = bt.CreateComposite<SequencerNode>("Sequence 3", false, walkAcorn, eatAcorn);
        //Sequence Node 2 (HayArdillas? y Proteger)
        var seq2 = bt.CreateComposite<SequencerNode>("Sequence 2", false, squirrelEater, protect);
        //Selector Node 2 (Proteger o Comer)
        var sel2 = bt.CreateComposite<SelectorNode>("Selector 2", false, seq2, seq3);
        //Sequence Node 1 (HayBellota? y Proteger/Comer)
        var seq1 = bt.CreateComposite<SequencerNode>("Sequence 1", false, acornExists, sel2);
        //Selector Node 1 (Proteger/Comer o Atacar)
        var sel1 = bt.CreateComposite<SelectorNode>("Selector 1", false, seq1, walkPlayer);
        //Loop node
        var loop = bt.CreateDecorator<LoopNode>("loop", sel1).SetIterations(-1);

        //Ponemos el loop como nodo raiz
        bt.SetRootNode(loop);
        //Devolvemos el BT
        return bt;
    }
    /*protected override BehaviourGraph CreateGraph()
    {
        //Perceptions
        var squirrelPerception = new ConditionPerception(() => !_ActionsSquirrel.CheckAcornEated());

        var rolePerception = new ConditionPerception(() => (_ActionsSquirrel.rol == "Protector") && (!_ActionsSquirrel.CheckAcornEated()));

        var otherSquirrels = new ConditionPerception(() => _ActionsSquirrel.CheckOtherSquirrels());
        
        var formEnded = new ConditionPerception(() => !_ActionsSquirrel.CheckFormationDone());


        //bt
        var walkAcorn = bt.CreateLeafNode("Walk Acorn", new FunctionalAction(_ActionsSquirrel.StartWalkAcorn, _ActionsSquirrel.UpdateWalkAcorn));
        
        var eatAcorn = bt.CreateLeafNode("Eat Acorn", new FunctionalAction(_ActionsSquirrel.StartEatAcorn, _ActionsSquirrel.UpdateEatAcorn));

        var walkPlayer = bt.CreateLeafNode("Walk Player", new FunctionalAction(_ActionsSquirrel.StartWalkPlayer, _ActionsSquirrel.UpdateWalkPlayer));
        
        var form = bt.CreateLeafNode("Form", new FunctionalAction(_ActionsSquirrel.StartForming, _ActionsSquirrel.UpdateForming));

        var protect = bt.CreateLeafNode("Protect", new FunctionalAction(_ActionsSquirrel.StartProtecting, _ActionsSquirrel.UpdateProtecting));


        //Nodes
        var protectRole = bt.CreateDecorator<ConditionNode>("Protect Role", protect).SetPerception(rolePerception);

        var seq4 = bt.CreateComposite<SequencerNode>("seq4", false, protectRole);  //Nodo secuencia

        var seq5 = bt.CreateComposite<SequencerNode>("seq5", false, walkAcorn, eatAcorn);  //Nodo secuencia

        var sel3 = bt.CreateComposite<SelectorNode>("Selector 3", false, seq4, seq5);

        var formationDone = bt.CreateDecorator<ConditionNode>("Formation Done", form).SetPerception(formEnded);

        //var inversor1 = bt.CreateDecorator<InverterNode>("Inversor 1", formationDone);

        var seq3 = bt.CreateComposite<SequencerNode>("seq3", false, formationDone);  //Nodo secuencia

        var otherEnemy = bt.CreateDecorator<ConditionNode>("Other Enemy", seq3).SetPerception(otherSquirrels);

        var seq2 = bt.CreateComposite<SequencerNode>("seq2", false, otherEnemy);  //Nodo secuencia

        var sel2 = bt.CreateComposite<SelectorNode>("Selector 2", false, seq2, sel3);

        var acornExists = bt.CreateDecorator<ConditionNode>("Acorn Exists", sel2).SetPerception(squirrelPerception);

        var seq1 = bt.CreateComposite<SequencerNode>("seq1", false, acornExists);  //Nodo secuencia

        var sel1 = bt.CreateComposite<SelectorNode>("Selector 1", false, seq1, walkPlayer);

        var loop = bt.CreateDecorator<LoopNode>("loop", sel1).SetIterations(-1);  //Nodo loop

        bt.SetRootNode(loop);

        return bt;
    }*/



}
