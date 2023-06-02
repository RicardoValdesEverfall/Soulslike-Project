using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RVT
{
    [CreateAssetMenu]
    public class ProjectilesSO : ScriptableObject
    {
        public List<ObjectData> ProjectilesData;
    }

    [Serializable]
    public class ObjectData
    {
        [field: SerializeField]
        public string Name { get; private set; }
        [field: SerializeField]
        public int ID { get; private set; }
        [field: SerializeField]
        public int Damage { get; private set; }
        [field: SerializeField]
        public float DespawnTime { get; private set; }
        [field: SerializeField]
        public GameObject VFXImpact { get; private set; }
    }
}