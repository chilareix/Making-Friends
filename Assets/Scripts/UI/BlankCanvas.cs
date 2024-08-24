using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlankCanvas : MonoBehaviour
{
	public Image Background;

    // Start is called before the first frame update
    void Start()
    {
		Background.transform.localScale = new Vector3(Screen.currentResolution.width, Screen.currentResolution.height, 0);
	}

    // Update is called once per frame
    void Update()
    {
	}
}
