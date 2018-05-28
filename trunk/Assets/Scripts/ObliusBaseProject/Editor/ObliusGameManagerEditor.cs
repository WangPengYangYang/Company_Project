using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObliusGameManager))]
[CanEditMultipleObjects]
public class ObliusGameManagerEditor : Editor {

	public override void OnInspectorGUI()
	{
		ObliusGameManager myTarget = (ObliusGameManager)target;
		DrawDefaultInspector ();

		if (GUILayout.Button("Delete player prefs")) {
			PlayerPrefs.DeleteAll ();
		}

	}
}
