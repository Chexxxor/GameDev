using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
	public Canvas MainCanvas;

	public void StartGame()
	{
		Application.LoadLevel(1);
	}

	public void Quit()
	{
		Application.Quit();
	}
}