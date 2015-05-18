using UnityEngine;
using System.Collections;

public class StartMenuManager : MonoBehaviour
{

    private int firstTime;

    void Awake()
    {
        firstTime = PlayerPrefs.GetInt("firstTime");
    }
    void Start()
    {
        firstTime = PlayerPrefs.GetInt("firstTime");
    }
    public void LoadLevel(int level)
    {
        if (level == 42 && firstTime == 0)
        {
            PlayerPrefs.SetInt("firstTime", 1);
            firstTime = PlayerPrefs.GetInt("firstTime");
            Application.LoadLevel(46);
        }
        else
        {
            Application.LoadLevel(level);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteAll();
            firstTime = PlayerPrefs.GetInt("firstTime");
        }
    }
}
