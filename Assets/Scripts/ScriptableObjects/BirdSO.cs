using System;
using System.Collections.Generic;
using UnityEngine;


namespace ScriptableObjects
{
    [CreateAssetMenu()]
    public class BirdSO : ScriptableObject
    {
        public int index;
        public Sprite birdImage;
        public Sprite birdUIReference;
        public Transform prefab;
        public String type;
    }
}
