using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public string loadMap;
	public string loadScene;

	public static Menu instance;

	void Awake()
	{
		instance = this;
		SceneManager.LoadScene(loadScene, LoadSceneMode.Additive);
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void LoadScene(bool includeAdditive = false)
	{
		Time.timeScale = 1;
		LoadingMenu.instance.LoadScenes(loadMap);
	}
}
