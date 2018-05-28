using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
using System;

[ExecuteInEditMode]
public abstract class AdNetwork : MonoBehaviour
{
	bool oldUseValue;
	public bool enabledOnThisPlatform;

	[HideInInspector]
	abstract public string defineSymbolString{ get; set; }


	#if UNITY_EDITOR

	BuildTargetScriptingDefineSymbols buildTargetDefineSymbols;

	void OnEnable ()
	{
		BuildTarget targetPlatform = EditorUserBuildSettings.activeBuildTarget;
		buildTargetDefineSymbols = new BuildTargetScriptingDefineSymbols (targetPlatform);

		if (buildTargetDefineSymbols.DefineSymbols.Contains (defineSymbolString)) {
			enabledOnThisPlatform = true;
		} else {
			enabledOnThisPlatform = false;
		}
	}


	void Update ()
	{	
		if (oldUseValue != enabledOnThisPlatform) {
			if (oldUseValue == true) {
				Debug.Log (defineSymbolString + " DISABLED");
				oldUseValue = false;
			} else {
				Debug.Log (defineSymbolString + " ENABLED");
				oldUseValue = true;
			}

			SetDefineSymbols ();
		}		

	}

	void SetDefineSymbols ()
	{			
		if (enabledOnThisPlatform) {
			buildTargetDefineSymbols.AppendDefineSymbol (defineSymbolString);
		} else {			
			buildTargetDefineSymbols.RemoveDefineSymbol (defineSymbolString);
		}
	}



	#endif



}







