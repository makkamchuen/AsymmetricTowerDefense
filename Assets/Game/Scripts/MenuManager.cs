using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class MenuManager: MonoBehaviour
    {
        private void Start()
        {
            Button startButton = GameObject.FindGameObjectWithTag("StartButton").GetComponent<Button>();
            startButton.onClick.AddListener(StartGame);
            Debug.Log(startButton);
        }

        public void StartGame()
        {
            SceneManager.LoadScene("newgame");
        }
    }
}