using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOnClickManager : MonoBehaviour
{
    public void OnClickPause()
    {
        Time.timeScale = 0;
    }

    public void OnClickUnpause()
    {
        Time.timeScale = 1;
    }    

    public void OnClickMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LobbyScene");
    }
}
