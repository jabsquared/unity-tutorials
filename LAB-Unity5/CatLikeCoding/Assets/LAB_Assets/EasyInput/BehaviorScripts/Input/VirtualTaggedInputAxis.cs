using UnityEngine;
using System.Collections;
using UnitySampleAssets.CrossPlatformInput;

/// Axis Convention: [tag]+"Horizontal" or "Vertical
public class VirtualTaggedInputAxis {

	private string tag;
	private string horizontalString, verticalString;
	
	public VirtualTaggedInputAxis (string tag){
		this.tag = tag;
		this.horizontalString = tag + "Horizontal";
		this.verticalString = tag + "Vertical";
	}
	
	public override string ToString (){
		return string.Format ("[Virtual {0}: h = {1}, v = {2}]", tag, h, v);
	}
	
	public string GetTag () {
		return this.tag;
	}
	
	public float h {
		get {
			return CrossPlatformInputManager.GetAxis (horizontalString);
		}
	}
	
	public float v {
		get {
			return  CrossPlatformInputManager.GetAxis (verticalString);
		}
	}
}
