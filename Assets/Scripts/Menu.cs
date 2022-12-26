using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public string loadMap;
	public string loadingScene;

	public static Menu instance;

	void Awake()
	{
		instance = this;
		if (LoadingMenu.instance == null) SceneManager.LoadScene(loadingScene, LoadSceneMode.Additive);
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void LoadScene(bool includeAdditive = false)
	{
		Time.timeScale = 1;
		LoadingMenu.instance.LoadScene(loadMap);
	}
}
