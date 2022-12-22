using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapSelection : MonoBehaviour
{
	[System.Serializable]
	public struct Map
	{
		public Texture texture;
		public string name;
		public string sceneName;
	}
	public RawImage image;
	public TextMeshProUGUI text;

	public Map[] maps;


	int index = 0;

	void Start()
	{
		UpdateMap();
	}

	public void Next()
	{
		index++;
		if (index >= maps.Length) index = 0;
		UpdateMap();
	}

	void UpdateMap()
	{
		Menu.instance.loadMap = maps[index].sceneName;
		image.texture = maps[index].texture;
		text.text = maps[index].name;
	}
}
