using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopOptionManu : MonoBehaviour
{
    [SerializeField] private GameObject optionMenu;
    private bool isPaused = false; 

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key was pressed");
            if (isPaused)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
    }

    private void pauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        optionMenu.SetActive(true);
    }

    private void resumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        optionMenu.SetActive(false);
    }

    public void resume()
    {
        resumeGame(); 
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
