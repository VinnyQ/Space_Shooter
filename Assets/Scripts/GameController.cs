using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour 
{
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	/*
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	*/
	public Text scoreText;
	public Text gameOverText;
	public GameObject restartButton;

	private bool gameOver;
	private bool restart;
	private int score;

	void Start ()
	{
		Debug.Log ("Game Starting...");
		gameOver = false;
		restart = false;
		//restartText.text = "";
		restartButton.SetActive (false);
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}

	/*
	 * update for restarting game with key press input R
	void Update ()
	{
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}
	*/

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int i = 0; i < hazardCount; i++) {
				GameObject hazard = hazards[Random.Range(0,hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver) {
				// restartText.text = "Press 'R' for Restart";
				restartButton.SetActive (true);
				restart = true;
				break;
			}
		}
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	public void GameOver()
	{
		gameOverText.text = "Game Over, Kiri!";
		gameOver = true;
	}

	public void RestartGame()
	{
		Application.LoadLevel(Application.loadedLevel);
	}
}
