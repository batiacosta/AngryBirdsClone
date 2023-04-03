using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "BirdSOLibrarySO", menuName = "BirdSOLibrarySO", order = 0)]
    public class BirdSOLibrarySO : ScriptableObject
    {
        public List<BirdData> BirdsData;
        [Serializable]
        public class BirdData
        {
            public BirdSO BirdSO;
            public int Quantity;    //  This value could be set by fetching from user's data, coins, etc.
        }
    }
}