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
		AsyncOperation unloadCurrentSceneOperation = SceneManager.UnloadSceneAsync(currentScene);
		AsyncOperation loadNewSceneOperation = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);

		float totalProgress = 0;
		while (totalProgress < 1f)
		{
			totalProgress = (unloadCurrentSceneOperation.progress + loadNewSceneOperation.progress) / 2f;
			slider.value = totalProgress;
			percentText.text = "Unloading current scene: " + (totalProgress * 100).ToString("000.0") + "%";
			yield return null;
		}

		yield return null;
		Scene scene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(newScene);
		UnityEngine.SceneManagement.SceneManager.SetActiveScene(scene);

		currentScene = newScene;
		StopLoading();
	}
}
