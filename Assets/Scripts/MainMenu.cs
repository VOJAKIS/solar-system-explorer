using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public Button playButton;
	public Button exitButton;

	// Start is called before the first frame update
	void Start()
	{
		playButton.onClick.AddListener(ChangeSceneToGame);
		exitButton.onClick.AddListener(ExitApplication);
	}

	// Update is called once per frame
	void Update()
	{

	}

	void ChangeSceneToGame()
	{
		SceneManager.LoadScene("Game");
	}

	void ExitApplication()
	{
		Application.Quit();
	}
}
