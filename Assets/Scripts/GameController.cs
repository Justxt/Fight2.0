using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController gameController;

    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject drawPanel;


    public bool isPlayerWin;
    public bool isEnemyWin;
    public bool isDraw;




    public void goToMenu() {
        SceneManager.LoadScene("Start");
    }

    public void reMatch() {
        SceneManager.LoadScene("Gameplay");
    }

    IEnumerator GameEnded() {
        yield return new WaitForSeconds(1.5f);

        Time.timeScale = 0;
        if (isPlayerWin) {
            winPanel.SetActive(true);
        } else if (isEnemyWin) {
            losePanel.SetActive(true);
        } else {
            drawPanel.SetActive(true);
        }
    }
}
