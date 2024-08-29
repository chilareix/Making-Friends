using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
	public uint SceneIndex;
	private CapsuleCollider2D PlayerCollider;

    // Start is called before the first frame update
    void Start()
	{
		PlayerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider2D>();

		if (SceneIndex > SceneManager.sceneCountInBuildSettings - 1) {
			SceneIndex = 0;
		}
		string scenePath = SceneUtility.GetScenePathByBuildIndex((int)SceneIndex);
		scenePath = scenePath.Remove(0, scenePath.LastIndexOf('/') + 1);
		scenePath = scenePath.Remove(scenePath.Length - 6, 6);


		GetComponent<TextMesh>().text = "To " + scenePath;
	}

    // Update is called once per frame
    void FixedUpdate()
    {

		if (GetComponent<BoxCollider2D>().IsTouching(PlayerCollider))
		{
			SceneManager.LoadScene((int) SceneIndex);
		}
    }
}
