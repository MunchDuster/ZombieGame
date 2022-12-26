using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;
using TMPro;

public class PauseMenu : MonoBehaviour
{
	public string mainMenuScene;

	public AudioMixer fxMixer;
	public AudioMixer musicMixer;
	public AudioMixer ambienceMixer;
	public AudioMixer zombieMixer;
	public AudioMixer weaponMixer;

	public PlayerMovement playerMovement;

	public GameObject pauseScreen;
	public UnityEvent<bool> OnPause;


	public void Quit()
	{
		Application.Quit();
	}

	public void BackToMenu()
	{
		LoadingMenu.instance.LoadScene(mainMenuScene);
	}
	public void Restart()
	{
		LoadingMenu.instance.ReloadScene();
	}

	public void SetMusicVolume(float volumeInput)
	{
		musicMixer.SetFloat("Volume(dB)", GetVolumeDB(volumeInput));
	}
	public void SetFXVolume(float volumeInput)
	{
		fxMixer.SetFloat("Volume(dB)", GetVolumeDB(volumeInput));
	}
	public void SetZombieVolume(float volumeInput)
	{
		zombieMixer.SetFloat("Volume(dB)", GetVolumeDB(volumeInput));
	}
	public void SetWeaponVolume(float volumeInput)
	{
		weaponMixer.SetFloat("Volume(dB)", GetVolumeDB(volumeInput));
	}
	public void SetAmbienceVolume(float volumeInput)
	{
		ambienceMixer.SetFloat("Volume(dB)", GetVolumeDB(volumeInput));
	}

	public void SetSensitivity(float sensitivityInput)
	{
		playerMovement.mouseSensitivity = 0.1f + sensitivityInput * 2;
	}
	public void SetBrightness(float brightnessInput)
	{
		MapPostProcessing.instance.SetBrightness(brightnessInput);
	}
	public void SetMotionBlur(float motionBlurInput)
	{
		MapPostProcessing.instance.SetMotionBlur(motionBlurInput);
	}

	float GetVolumeDB(float volumeInput)
	{
		float x = Mathf.Clamp(0.01f, volumeInput * 5, 5f);
		return Mathf.Log(x, 1.1f);
	}

	public GameObject fpsTextParent;
	public TextMeshProUGUI fpsText;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) TogglePaused();
		if (Input.GetKeyDown(KeyCode.BackQuote)) ToggleDisplayFPS();

		if (isDisplayingFPS)
		{
			if (timeSinceLastUpdatedFPS > minTimeUpdateFPS)
			{
				fpsText.text = "FPS: " + Mathf.RoundToInt(1f / Time.deltaTime);
				timeSinceLastUpdatedFPS = 0;
			}
			else
			{
				timeSinceLastUpdatedFPS += Time.deltaTime;
			}
		}
	}

	bool isPaused = false;
	void TogglePaused()
	{
		isPaused = !isPaused;
		pauseScreen.SetActive(isPaused);
		Time.timeScale = isPaused ? 0f : 1f;
		MapPostProcessing.instance.SetBlur(isPaused);
		OnPause.Invoke(!isPaused);
	}

	float minTimeUpdateFPS = 0.2f;
	float timeSinceLastUpdatedFPS = 0;
	bool isDisplayingFPS = false;
	void ToggleDisplayFPS()
	{
		isDisplayingFPS = !isDisplayingFPS;

		fpsTextParent.SetActive(isDisplayingFPS);

	}
}
