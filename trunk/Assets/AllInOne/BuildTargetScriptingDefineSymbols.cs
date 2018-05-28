#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BuildTargetScriptingDefineSymbols
{
	
	BuildTargetGroup targetGroup;


	public BuildTargetScriptingDefineSymbols (BuildTarget target)
	{
		this.targetGroup = BuildPipeline.GetBuildTargetGroup(target);
	}


	public string DefineSymbols {
		get {
			return PlayerSettings.GetScriptingDefineSymbolsForGroup (targetGroup);
		}set {
				PlayerSettings.SetScriptingDefineSymbolsForGroup (targetGroup, value);		
		}
	}

	public void AppendDefineSymbol (string defineSymbol)
	{
		try {
			if (!DefineSymbols.Contains (defineSymbol)) {
				string stringToAppend = ";" + defineSymbol;
				DefineSymbols += stringToAppend;
			}
		} catch {
			Debug.Log ("THERE WAS AN ERROR");
		}
	}

	public void RemoveDefineSymbol (string defineSymbol)
	{
		try {
			string newValue = DefineSymbols.Replace (defineSymbol, "");	
			DefineSymbols = newValue;
		} catch {
			Debug.Log ("THERE WAS AN ERROR");
		}
	}
}

#endif

