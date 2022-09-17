using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Level_1()
    {
        SceneManager.LoadScene(2);
    }
    public void Level_2()
    {
        SceneManager.LoadScene(3);
    }
    public void Level_3()
    {
        SceneManager.LoadScene(4);
    }
}
