using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }

            return m_instance;
        }
    }

    private static UIManager m_instance; // 싱글톤이 할당될 변수

    public GameObject gameoverUI; // 게임 오버시 활성화할 UI 

    // 게임 오버 UI 활성화
    public void SetActiveGameoverUI(bool active) {
        gameoverUI.SetActive(active);
    }

    // 게임 재시작
    public void GameRestart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}