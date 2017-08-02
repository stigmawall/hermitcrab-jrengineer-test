using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    public string SceneName;
    public Text ScoreLabel;
	// Use this for initialization
	void Start () {
        if (PlayerPrefs.GetInt("Score") > 0)
        {
            ScoreLabel.text = PlayerPrefs.GetInt("Score").ToString();
        }
        else {
            ScoreLabel.text = "No score saved";
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void LoadGame() {
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }

    public void ExitGame() {
        Application.Quit();
    }
}
