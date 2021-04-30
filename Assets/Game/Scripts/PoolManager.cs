using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts
{
    public class PoolManager : MonoBehaviour
    {
        private static GameObject cachePanel;

        private static readonly Dictionary<string, Queue<GameObject>> PrefabPoolsDictionary =
            new Dictionary<string, Queue<GameObject>>();

        private static readonly Dictionary<GameObject, string> ActivePrefabIdsDictionary =
            new Dictionary<GameObject, string>();

        public void ClearCachePool()
        {
            PrefabPoolsDictionary.Clear();
            ActivePrefabIdsDictionary.Clear();
        }

        public static GameObject Spawn(GameObject prefab, Vector3 position)
        {
            string prefabId = prefab.GetInstanceID().ToString();
            GameObject instance = GetPrefabFromPool(prefabId);
            if (instance == null)
            {
                instance = Instantiate(prefab, position, Quaternion.identity);
                instance.name = prefab.name + Time.time;
            }

            MarkPrefabAsActive(instance, prefabId);
            return instance;
        }

        public static void Despawn(GameObject prefab)
        {
            if (cachePanel == null)
            {
                cachePanel = new GameObject { name = "CachePanel" };
                DontDestroyOnLoad(cachePanel);
            }

            if (prefab == null)
            {
                return;
            }

            prefab.transform.parent = cachePanel.transform;
            prefab.SetActive(false);

            if (!ActivePrefabIdsDictionary.ContainsKey(prefab))
            {
                return;
            }

            string prefabTag = ActivePrefabIdsDictionary[prefab];
            RemovePrefabActiveMark(prefab);

            if (!PrefabPoolsDictionary.ContainsKey(prefabTag))
            {
                PrefabPoolsDictionary[prefabTag] = new Queue<GameObject>();
            }

            PrefabPoolsDictionary[prefabTag].Enqueue(prefab);
        }

        private static GameObject GetPrefabFromPool(string prefabId)
        {
            if (!PrefabPoolsDictionary.ContainsKey(prefabId) || PrefabPoolsDictionary[prefabId].Count <= 0)
            {
                return null;
            }

            GameObject instance = PrefabPoolsDictionary[prefabId].Dequeue();
            instance.SetActive(true);
            return instance;
        }

        private static void MarkPrefabAsActive(GameObject prefab, string prefabId)
        {
            ActivePrefabIdsDictionary.Add(prefab, prefabId);
        }

        private static void RemovePrefabActiveMark(GameObject go)
        {
            if (ActivePrefabIdsDictionary.ContainsKey(go))
            {
                ActivePrefabIdsDictionary.Remove(go);
            }
            else
            {
                Debug.LogError("remove out mark error, gameObject has not been marked");
            }
        }
    }
}