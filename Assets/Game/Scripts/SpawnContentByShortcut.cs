using System;
using UnityEngine;

namespace Game.Scripts
{
    [Serializable]
    public class SpawnContentByShortcut
    {
        [SerializeField] private Sprite sprite;
        [SerializeField][FMODUnity.EventRef] private string spawnSound;
        [SerializeField] private string shortcut;
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
        public string Shortcut => shortcut;
        public GameObject Prefab => prefab;

        public int RewardCost => rewardCost;
    }
}