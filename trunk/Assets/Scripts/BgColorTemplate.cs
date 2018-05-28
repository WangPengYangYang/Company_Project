using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BgColorTemplate  {

	public Color colorBottom,colorTop;
        public BgColorTemplate(Color col1,Color col2)
    {
        colorBottom = col1;
        colorTop = col2;
    }
}
