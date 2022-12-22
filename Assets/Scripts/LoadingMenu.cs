using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class LoadingMenu : MonoBehaviour
{
	public static LoadingMenu instance;
	public GameObject loadingScreen;

	public TextMeshProUGUI loadingText;
	public TextMeshProUGUI percentText;
	public Slider slider;

	void Start()
	{
		instance = this;
	}

	bool loading = false;
	public void LoadScenes(string mainScene)
	{
		loading = true;
		loadingScreen.SetActive(true);
		StartCoroutine(UpdateLoadingText());
		StartCoroutine(LoadScenesProgress(mainScene));
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
	IEnumerator LoadScenesProgress(string mainScene)
	{
		AsyncOperation asyncOperation1 = SceneManager.LoadSceneAsync(mainScene, LoadSceneMode.Single);

		while (asyncOperation1.progress < 1f)
		{
			float loadValue = asyncOperation1.progress;
			slider.value = loadValue;
			percentText.text = (loadValue * 100).ToString("000.0") + "%";
			yield return null;
		}
		StopLoading();
	}
}
