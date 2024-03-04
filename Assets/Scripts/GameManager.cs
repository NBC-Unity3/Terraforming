using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }

    private static GameManager m_instance;

    private int score = 0;
    public bool isGameover { get; private set; }

    private void Awake() {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start() 
    {

    }

    // 게임 오버 처리
    public void EndGame() {
        isGameover = true;
        UIManager.instance.SetActiveGameoverUI(true);
    }
}