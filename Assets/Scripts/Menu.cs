using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public void Quit()
	{
		Application.Quit();
	}

	public void LoadScene(string name)
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(name);
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.Q)) Quit();
		if (Input.GetKey(KeyCode.R)) LoadScene(SceneManager.GetActiveScene().name);
	}
}
