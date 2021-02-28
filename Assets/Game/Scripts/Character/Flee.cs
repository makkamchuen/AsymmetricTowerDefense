
    using System;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Character/Flee"), Serializable]    
    public class Flee: ScriptableObject
    {
        public float minDist;
        public float maxDist;
    }