using System;
using UnityEngine;

namespace Game.Scripts
{
    [Serializable]
    public class SpawnContentByShortcut
    {
        [SerializeField] private Sprite sprite;
        [SerializeField][FMODUnity.EventRef] private string spawnSound;
        [SerializeField] private PlayerPrefKey playerPrefKey;
        [SerializeField] private DefaultKeyStroke defaultKeyStroke;
        [SerializeField] private float cooldown;
        [SerializeField] private GameObject prefab;
        [SerializeField] private int rewardCost = 1;
        private Texture2D cursor;

        public Texture2D Cursor
        {
            get
            {
                if (!cursor)
                    cursor = sprite.texture;
                return cursor;
            }
        }

        public string SpawnSound => spawnSound;
        public PlayerPrefKey PlayerPrefKey => playerPrefKey;
        public DefaultKeyStroke DefaultKeyStroke => defaultKeyStroke;
        public float CoolDown => cooldown;
        public GameObject Prefab => prefab;

        public int RewardCost => rewardCost;
        public float timePassedDoNotTouch;
    }
}