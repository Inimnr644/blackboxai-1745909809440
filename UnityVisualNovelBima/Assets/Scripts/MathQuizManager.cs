using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Manages math quiz questions and answers
public class MathQuizManager : MonoBehaviour
{
    public Text questionText;
    public InputField answerInput;
    public Button submitButton;
    public Text feedbackText;

    private string currentAnswer;
    private System.Action<bool> onAnswerResult;

    void Start()
    {
        submitButton.onClick.AddListener(CheckAnswer);
        feedbackText.text = "";
    }

    public void SetQuestion(string question, string answer, System.Action<bool> resultCallback)
    {
        questionText.text = question;
        currentAnswer = answer;
        onAnswerResult = resultCallback;
        answerInput.text = "";
        feedbackText.text = "";
        submitButton.interactable = true;
    }

    void CheckAnswer()
    {
        if (answerInput.text.Trim() == currentAnswer)
        {
            feedbackText.text = "Correct! Well done.";
            submitButton.interactable = false;
            onAnswerResult?.Invoke(true);
        }
        else
        {
            feedbackText.text = "Incorrect, please try again.";
            onAnswerResult?.Invoke(false);
        }
    }
}
