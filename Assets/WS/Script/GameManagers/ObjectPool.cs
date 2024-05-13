using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace WS.Script.GameManagers
{
	public class ObjectPool : MonoBehaviour
	{
		[Inject] private DiContainer _diContainer;
		 
		[FormerlySerializedAs("objectsToPreload")] public GameObject[] _preloadObjects = Array.Empty<GameObject>();
		[FormerlySerializedAs("objectsToPreloadTimes")] public int[] _numberOfPreloads = Array.Empty<int>();
		public bool isHide { get; set; } = true;
		
		private bool allObjectsLoaded;
		private Dictionary<int,List<GameObject>> instantiatedObjects = new();
		private Dictionary<int,int> poolCursors = new();
	
		private void Start()
		{
			allObjectsLoaded = false;
			for(int i = 0; i < _preloadObjects.Length; i++)
			{
				SaveItem(_preloadObjects[i], _numberOfPreloads[i]);
			}
			allObjectsLoaded = true;
		}
		private void AddToPool(GameObject sourceObject, int number)
		{
			int uniqueId = sourceObject.GetInstanceID();
			
			if(!instantiatedObjects.ContainsKey(uniqueId))
			{
				instantiatedObjects.Add(uniqueId, new List<GameObject>());
				poolCursors.Add(uniqueId, 0);
			}

			GameObject newObj;
			for(int i = 0; i < number; i++)
			{
				newObj =  _diContainer.InstantiatePrefab(sourceObject, new Vector2(0,100),sourceObject.transform.rotation, null);
				newObj.SetActive(false);
				instantiatedObjects[uniqueId].Add(newObj);

				if(isHide)
					newObj.hideFlags = HideFlags.HideInHierarchy;
			}
		}
		public void GetFreeElement(GameObject sourceObj, bool activateObject, Vector2 pos)
		{
			int uniqueId = sourceObj.GetInstanceID();

			if(!poolCursors.ContainsKey(uniqueId))
			{
				Debug.LogError("[CFX_SpawnSystem.GetNextPoolObject()] Object hasn't been preloaded: " + sourceObj.name + " (ID:" + uniqueId + ")");
				return;
			}

			int cursor = poolCursors[uniqueId];
			poolCursors[uniqueId]++;
			if(poolCursors[uniqueId] >= instantiatedObjects[uniqueId].Count)
			{
				poolCursors[uniqueId] = 0;
			}

			GameObject returnObj = instantiatedObjects[uniqueId][cursor];
			returnObj.transform.position = pos;
			if (activateObject)
				if(returnObj)
					returnObj.SetActive(true);
		}


		private void SaveItem(GameObject sourceObj, int poolSize = 1)
		{
			AddToPool(sourceObj, poolSize);
		}
	}
}
