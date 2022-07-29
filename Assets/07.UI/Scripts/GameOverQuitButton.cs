using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverQuitButton : MonoBehaviour
{
    public void OnClickQuitButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LobbyScene");
    }
}
