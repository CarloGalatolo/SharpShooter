using TMPro;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
	[SerializeField] TMP_Text enemiesLeftText;
	[SerializeField] GameObject gameOverContainer;

	int enemiesLeft = 0;
	StarterAssetsInputs starterAssetsInputs;

	const string ENEMIES_LEFT_TEXT = "Enemies left: ";
	const string WIN_TEXT = "You win!";



	//  METHODS  //

	void Start ()
	{
		starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
	}



	public void AdjustEnemiesLeft (int amount)
	{
		enemiesLeft += amount;
		enemiesLeftText.text = ENEMIES_LEFT_TEXT + enemiesLeft;

		if (enemiesLeft <= 0)
		{
			WinGame();
		}
	}



	void WinGame ()
	{
		gameOverContainer.SetActive(true);
		TMP_Text gameOverText = gameOverContainer.GetComponentInChildren<TMP_Text>();
		gameOverText.text = WIN_TEXT;
		starterAssetsInputs.SetCursorState(false);
	}



	public void RestartButton ()
	{
		int currentScene = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(currentScene);
	}



	public void QuitButton ()
	{
		Debug.LogWarning("Application.Quit() dowsn't work inside editor.");
		Application.Quit();
	}
}
