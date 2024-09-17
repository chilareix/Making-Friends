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
			int objIdx = int.Parse(BetweenSubstr(objName, objName.IndexOf("("), objName.IndexOf(")")));
			toReturn[objIdx] = obj;
		}
		return toReturn;
	}
	/// <summary>
	/// 
	/// </summary>
	/// <param name="str"></param>
	/// <param name="startIdx"></param>
	/// <param name="endIdx"></param>
	/// <returns>A substring between specified indecies</returns>
	string BetweenSubstr(string str, int startIdx, int endIdx)
	{
		return str.Substring(startIdx + 1, endIdx - startIdx - 1);
	}
}
