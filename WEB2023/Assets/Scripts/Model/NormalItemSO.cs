using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Model.NormalItemSO;

namespace Assets.Scripts.Model
{
    [CreateAssetMenu]
    public class NormalItemSO : ItemSO, INormalItem//, IDestroyableItem//, IItemAction
    {
        /*[SerializeField]
        private List<ModifierData> modifiersData = new();
        /*public string ActionName => "Usar";

        [field: SerializeField]
        public AudioClip actionSFX {get; private set;}

        /*public bool PerformAction(GameObject character)
        {
            foreach(ModifierData data in modifiersData)
            {
                data.statModifier.AffectCharacter(character, data.value);
            }
            return true;
        
    }

        */
        public interface INormalItem
        {

        }
        
        /*
        public interface IDestroyableItem
        {

        }

        /*public interface IItemAction
        {
            public string ActionName { get; }
            public AudioClip actionSFX { get; }
            bool PerformAction(GameObject character);
        }*/

        /*[Serializable]
        public class ModifierData
        {
            /*public CharacterStatModifierSO statModifier;
            public float value;
        }*/
    }
}