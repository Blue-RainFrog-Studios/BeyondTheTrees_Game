using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Model.NormalItemSO;

namespace Assets.Scripts.Model
{
    [System.Serializable]
    [CreateAssetMenu]
    public class NormalItemSO : ItemSO, INormalItem
    {
        public interface INormalItem
        {

        }
        
    }
}