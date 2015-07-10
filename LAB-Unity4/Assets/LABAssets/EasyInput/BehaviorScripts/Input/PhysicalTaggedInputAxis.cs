using UnityEngine;
using System.Collections;
using UnitySampleAssets.CrossPlatformInput;

/// Axis Convention: [tag]+"Horizontal" or "Vertical
public class PhysicalTaggedInputAxis {
	
	private string tag;
	private string horizontalString, verticalString;

	public PhysicalTaggedInputAxis (string tag){
		this.tag = tag;
		this.horizontalString = tag + "Horizontal";
		this.verticalString = tag + "Vertical";
	}

	public override string ToString (){
		return string.Format ("[ Physical {0}: h = {1}, v = {2}]", tag, h, v );
	}
	

	public string GetTag () {
		return this.tag;
	}

	//TODO: Make these a variables inside class

	public float h {
		get {
			return Input.GetAxis (horizontalString);
		}
	}

	public float v {
		get {			
			return Input.GetAxis (verticalString);
		}
	}
}
