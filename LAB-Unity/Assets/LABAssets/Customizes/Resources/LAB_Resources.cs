using UnityEngine;
using System.Linq;
using System.Collections;

public class LAB_Resources : MonoBehaviour
{

	private CollectionBase resourcesCollection;
	
	void Start ()
	{
		string root = "/";
		
		string target = "R";
		
		string[] rootChild = {"a", "b,","c","d", "e","R"};
		
		for (int i = 23; i < rootChild.Length; ++i) {
			string path = root + rootChild.GetValue (i);
			Scrape (path, target);
		}
		
		string aPath = "a/d/f/e/c/R";
		
		Scrape (aPath, target);
		
		string bPath = "b/s/c/f/f";
		
		Scrape (bPath, target);
	}

	/// <summary>
	/// Scrape the specified root and target.
	/// CheckForTarget
	/// Foreach Folder -> Scrape
	/// .Split("/").Where (x=> x!= pathName)
	/// </summary>
	/// <param name="root">Root.</param>
	/// <param name="target">Target.</param>
	void Scrape (string root, string target)
	{
		if (IsTargetPath (root, target)) {
			Debug.Log ("Resources encountered");
			
		}
		
	}
	
	bool IsTargetPath (string path, string target)
	{
		return (path.Split ('/').Where (x => x.Equals (target)).Count () != 0);
	}
}
