using UnityEngine;

public class TTLSceneManager : MonoBehaviour
{
    public static TTLSceneManager Instance;

    public GameObject[] balloons;
    public Transform canvas;

    public int score = 0;
    public int lives = 5;

    public float baseSpeed = 300f;
    public float speedPerPoint = 2f;

    public float spawnInterval = 1.5f;
    float spawnTimer;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnBalloon();
            spawnTimer = 0f;
        }

        
    }

    public void SpawnBalloon()
    {
        int index = Random.Range(0, balloons.Length);

        GameObject balloon = Instantiate(balloons[index], canvas);

        RectTransform rect = balloon.GetComponent<RectTransform>();

        float randomX = Random.Range(-470f, 470f);

        rect.anchoredPosition = new Vector2(randomX, -1100f);
    }

    public void AddScore(int points)
    {
        score += points;
    }

    public void LoseLife()
    {
        lives--;

        if (lives <= 0)
        {
            Debug.Log("Game Over");
            Time.timeScale = 0;
        }
    }

    public float GetSpeed()
    {
        return baseSpeed + score * speedPerPoint;
    }
}
