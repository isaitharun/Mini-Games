using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Balloon : MonoBehaviour
{
    public BalloonData data;

    public Button tapBtn;
    public Image loonImg;
    public TMP_Text pointsTxt;

    public bool tapped;

    public float speed = 300f;

    RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();

        pointsTxt.gameObject.SetActive(false);

        tapBtn.onClick.AddListener(OnBalloonClicked);
    }

    void Update()
    {
        rect.anchoredPosition += Vector2.up * speed * Time.deltaTime;

        if (rect.anchoredPosition.y > 1100f)
        {
            if(!tapped)
            {
                if (data.points > 0)
                    TTLSceneManager.Instance.LoseLife();
            }
            Destroy(gameObject);
        }
    }

    void OnBalloonClicked()
    {
        loonImg.gameObject.SetActive(false);

        pointsTxt.gameObject.SetActive(true);
        pointsTxt.text = data.points.ToString();

        if (data.points > 0)
        {
            TTLSceneManager.Instance.AddScore(data.points);
        }
        else
        {
            TTLSceneManager.Instance.LoseLife();
        }
    }
}
