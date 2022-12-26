using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class LoadingMenu : MonoBehaviour
{

	string currentScene;
	public static LoadingMenu instance;
	public GameObject loadingScreen;

	public TextMeshProUGUI loadingText;
	public TextMeshProUGUI percentText;
	public Slider slider;

	void Start()
	{
		instance = this;
		currentScene = SceneManager.GetActiveScene().name;
	}

	bool loading = false;
	public void LoadScene(string mainScene)
	{
		Time.timeScale = 1;

		loading = true;
		loadingScreen.SetActive(true);
		StartCoroutine(UpdateLoadingText());
		StartCoroutine(LoadScenesProgress(mainScene));
	}

	public void ReloadScene()
	{
		LoadScene(currentScene);
	}

	public void StopLoading()
	{
		loadingScreen.SetActive(false);
		loading = false;
	}

	IEnumerator UpdateLoadingText()
	{
		string[] dots = new string[] { "", ".", "..", "..." };
		int dotsIndex = 0;

		while (loading)
		{
			loadingText.text = "Loading" + dots[dotsIndex];
			dotsIndex = dotsIndex < dots.Length - 1 ? ++dotsIndex : 0;
			yield return new WaitForSeconds(0.7f);
		}
	}
	IEnumerator LoadScenesProgress(string newScene)
	{
		AsyncOperation asyncOperation2 = SceneManager.UnloadSceneAsync(currentScene);
		while (asyncOperation2.progress < 1f)
		{
			float loadValue = asyncOperation2.progress;
			slider.value = loadValue;
			percentText.text = "Unloading current scene: " + (loadValue * 100).ToString("000.0") + "%";
			yield return null;
		}

		AsyncOperation asyncOperation1 = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);

		while (asyncOperation1.progress < 1f)
		{
			float loadValue = asyncOperation1.progress;
			slider.value = loadValue;
			percentText.text = "Loading next scene: " + (loadValue * 100).ToString("000.0") + "%";
			yield return null;
		}

		currentScene = newScene;
		StopLoading();
	}
}
