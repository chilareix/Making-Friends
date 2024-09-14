using UnityEngine;

public class ChilareixUtilities
{
	///<summary>
	///Takes in a GameObject array and sorts it by numbers inside first set of parentheses.
	/// ***Make sure numbers are different and do not exceed the last index of the array***
	/// 
	///Example:
	///Name = abc(123)efg(456)
	///The index will be 123 for the GameObject
	///</summary>
	public GameObject[] SortGameObjectsByName(GameObject[] array)
	{
		GameObject[] toReturn = new GameObject[array.Length];
		foreach (GameObject obj in array)
		{
			string objName = obj.name;
			Debug.Log(objName);
			int objIdx = int.Parse(objName.Substring(objName.IndexOf("(") + 1, objName.IndexOf(")") - objName.IndexOf("(") - 1));
			toReturn[objIdx] = obj;
		}
		return toReturn;
	}
}
