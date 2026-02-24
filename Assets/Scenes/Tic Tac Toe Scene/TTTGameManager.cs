using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class TTTGameManager : MonoBehaviour
{
    public Button[] buttons;
    public Sprite xSprite;
    public Sprite oSprite;
    public Sprite noneSprite;
    public TMP_Text logText;

    private Image[] buttonImages;

    private enum Player { None, X, O }

    private Player[] board = new Player[9];

    private Queue<int> xMoves = new Queue<int>();
    private Queue<int> oMoves = new Queue<int>();

    private bool gameOver = false;

    // 🎨 Turn Colors
    private Color32 xTurnColor = new Color32(167, 123, 243, 255); // #A77BF3
    private Color32 oTurnColor = new Color32(255, 128, 192, 255); // #FF80C0

    void Start()
    {
        buttonImages = new Image[buttons.Length];

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;

            board[i] = Player.None;

            buttonImages[i] = buttons[i].GetComponentInChildren<Image>();
            buttonImages[i].sprite = noneSprite;
            buttonImages[i].color = Color.white;

            buttons[i].interactable = true;
            buttons[i].onClick.AddListener(() => OnPlayerClick(index));
        }

        UpdateTurnUI("X Turn");
    }

    void OnPlayerClick(int index)
    {
        if (gameOver) return;
        if (board[index] != Player.None) return;

        MakeMove(index, Player.X);

        if (!gameOver)
            Invoke(nameof(AIMove), 0.4f);
    }

    void AIMove()
    {
        if (gameOver) return;

        List<int> emptyIndexes = new List<int>();

        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] == Player.None)
                emptyIndexes.Add(i);
        }

        if (emptyIndexes.Count == 0)
            return;

        int randomIndex = emptyIndexes[Random.Range(0, emptyIndexes.Count)];
        MakeMove(randomIndex, Player.O);
    }

    void MakeMove(int index, Player player)
    {
        board[index] = player;

        if (player == Player.X)
        {
            buttonImages[index].sprite = xSprite;
            xMoves.Enqueue(index);

            if (xMoves.Count > 3)
            {
                int oldIndex = xMoves.Dequeue();
                ClearCell(oldIndex);
            }
        }
        else
        {
            buttonImages[index].sprite = oSprite;
            oMoves.Enqueue(index);

            if (oMoves.Count > 3)
            {
                int oldIndex = oMoves.Dequeue();
                ClearCell(oldIndex);
            }
        }

        if (CheckWinner())
        {
            UpdateTurnUI(player + " Wins!");
            gameOver = true;
            return;
        }

        if (player == Player.X)
            UpdateTurnUI("O Turn");
        else
            UpdateTurnUI("X Turn");
    }

    void ClearCell(int index)
    {
        board[index] = Player.None;
        buttonImages[index].sprite = noneSprite;
    }

    bool CheckWinner()
    {
        int[,] winPatterns = new int[,]
        {
            {0,1,2},{3,4,5},{6,7,8},
            {0,3,6},{1,4,7},{2,5,8},
            {0,4,8},{2,4,6}
        };

        for (int i = 0; i < 8; i++)
        {
            int a = winPatterns[i,0];
            int b = winPatterns[i,1];
            int c = winPatterns[i,2];

            if (board[a] != Player.None &&
                board[a] == board[b] &&
                board[b] == board[c])
            {
                return true;
            }
        }

        return false;
    }

    void UpdateTurnUI(string message)
    {
        logText.text = message;

        if (message == "X Turn")
            logText.color = xTurnColor;
        else if (message == "O Turn")
            logText.color = oTurnColor;
        else if (message == "X Wins!")
            logText.color = xTurnColor;
        else if (message == "O Wins!")
            logText.color = oTurnColor;
        else
            logText.color = Color.white;
    }

    public void ResetGame()
    {
        gameOver = false;

        xMoves.Clear();
        oMoves.Clear();

        for (int i = 0; i < board.Length; i++)
        {
            board[i] = Player.None;
            buttonImages[i].sprite = noneSprite;
            buttonImages[i].color = Color.white;
            buttons[i].interactable = true;
        }

        UpdateTurnUI("X Turn");
    }

    public void Back()
    {
        SceneManager.LoadScene("Main");
    }
}