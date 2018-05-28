using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {
    public static CharacterManager instance;  // just used to know where the character position is

    private void Awake()
    {
        instance = this;
    }

}
