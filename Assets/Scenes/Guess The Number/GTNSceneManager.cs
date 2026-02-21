using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GTNSceneManager : MonoBehaviour
{
    int targetNumber;
    int count = 0;

    public TMP_Text text;

    public TMP_Text selectedNumber;

    void Start()
    {
        targetNumber = Random.Range(10, 100);
        Debug.Log("Target: " + targetNumber);
        text.text = "";
    }

    public void numberClick(int digit)
    {
        string currentText = selectedNumber.text.Trim();

        if (currentText == "00")
        {
            selectedNumber.text = digit.ToString();
        }
        else if (currentText.Length == 1)
        {
            selectedNumber.text += digit.ToString();
        }
    }

    public void guessNumber()
    {
        string currentText = selectedNumber.text.Trim();

        if (currentText.Length != 2)
        {
            Debug.Log("Number should be between 10 and 99");
            text.color = Color.yellow;
            text.text = "Number should be between 10 and 99";
            resetNumber();
            return;
        }

        if (currentText[0] == '0')
        {
            Debug.Log("Number should be between 10 and 99");
            text.color = Color.yellow;
            text.text = "Number should be between 10 and 99";
            resetNumber();
            return;
        }

        int guessedNumber = int.Parse(currentText);
        count += 1;

        if (guessedNumber == targetNumber)
        {
            Debug.Log("Correct Guess!");
            Debug.Log("You got right in " + count + " turns.");
            text.color = Color.green;
            text.text = "Correct Guess! You got right in " + count + " turns.";
            count = 0;
            resetGame();
        }
        else
        {
            if (guessedNumber < targetNumber)
            {
                Debug.Log("Wrong Guess! The number is HIGHER than your guess.");
                text.color = Color.red;
                text.text = "Wrong Guess! The number is HIGHER than your guess.";
            }
            else
            {
                Debug.Log("Wrong Guess! The number is LOWER than your guess.");
                text.color = Color.red;
                text.text = "Wrong Guess! The number is HIGHER than your guess.";
            }

            resetNumber();
        }

    }

    public void resetNumber()
    {
        selectedNumber.text = "00";
    }

    public void resetGame()
    {
        targetNumber = Random.Range(10, 100);
        Debug.Log("New target number is " + targetNumber);
        resetNumber();
    }

    public void backToMain()
    {
        SceneManager.LoadScene("Main");
    }

}
