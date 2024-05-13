using UnityEngine;
using UnityEditor;
using System.Collections;
using WS.Script.GameManagers;

// Cartoon FX  - (c) 2013,2014 Jean Moreno

// CFX Spawn System Editor interface

[CustomEditor(typeof(ObjectPool))]
public class SpawnSystemHelperEditor : Editor
{
	public override void OnInspectorGUI()
	{
		(this.target as ObjectPool).isHide = GUILayout.Toggle((this.target as ObjectPool).isHide, "Hide Preloaded Objects in Hierarchy");
		
		GUI.SetNextControlName("DragDropBox");
		EditorGUILayout.HelpBox("Drag GameObjects you want to preload here!\n\nTIP:\nUse the Inspector Lock at the top right to be able to drag multiple objects at once!", MessageType.None);
		
		for(int i = 0; i < (this.target as ObjectPool)._preloadObjects.Length; i++)
		{
			GUILayout.BeginHorizontal();
			
			(this.target as ObjectPool)._preloadObjects[i] = (GameObject)EditorGUILayout.ObjectField((this.target as ObjectPool)._preloadObjects[i], typeof(GameObject), true);
			EditorGUILayout.LabelField(new GUIContent("times","Number of times to copy the effect\nin the pool, i.e. the max number of\ntimes the object will be used\nsimultaneously"), GUILayout.Width(40));
			int nb = EditorGUILayout.IntField("", (this.target as ObjectPool)._numberOfPreloads[i], GUILayout.Width(50));
			if(nb < 1)
				nb = 1;
			(this.target as ObjectPool)._numberOfPreloads[i] = nb;
			
			if(GUI.changed)
			{
				EditorUtility.SetDirty(target);
			}
			
			if(GUILayout.Button("X", EditorStyles.miniButton, GUILayout.Width(24)))
			{
				Object preloadedObject = (this.target as ObjectPool)._preloadObjects[i];
				string objectName = (preloadedObject == null) ? "" : preloadedObject.name;
				Undo.RegisterUndo(target, string.Format("Remove {0} from Spawn System", objectName));
				
				ArrayUtility.RemoveAt<GameObject>(ref (this.target as ObjectPool)._preloadObjects, i);
				ArrayUtility.RemoveAt<int>(ref (this.target as ObjectPool)._numberOfPreloads, i);
				
				EditorUtility.SetDirty(target);
			}
			
			GUILayout.EndHorizontal();
		}
		
		if(Event.current.type == EventType.DragPerform || Event.current.type == EventType.DragUpdated)
		{
			DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
			
			if(Event.current.type == EventType.DragPerform)
			{
				foreach(Object o in DragAndDrop.objectReferences)
				{
					if(o is GameObject)
					{
						bool already = false;
						foreach(GameObject otherObj in (this.target as ObjectPool)._preloadObjects)
						{
							if(o == otherObj)
							{
								already = true;
								Debug.LogWarning("SpawnSystemHelper: Object has already been added: " + o.name);
								break;
							}
						}
						
						if(!already)
						{
							Undo.RegisterUndo(target, string.Format("Add {0} to Spawn System", o.name));
							
							ArrayUtility.Add<GameObject>(ref (this.target as ObjectPool)._preloadObjects, (GameObject)o);
							ArrayUtility.Add<int>(ref (this.target as ObjectPool)._numberOfPreloads, 1);
							
							EditorUtility.SetDirty(target);
						}
					}
				}
			}
		}
	}
}
