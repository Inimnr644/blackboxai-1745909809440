using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Controls the game flow combining dialogue and math quiz
public class GameController : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public MathQuizManager mathQuizManager;

    private int state = 0;

    void Start()
    {
        StartStory();
    }

    void StartStory()
    {
        // Introduction with Bima cultural reference
        List<string> introDialogue = new List<string>()
        {
            "Selamat datang di permainan edukasi matematika kelas 7 SMP!",
            "Kita akan belajar matematika sambil mengenal budaya Bima.",
            "Mari kita mulai petualangan ini."
        };
        dialogueManager.StartDialogue(introDialogue, null);
    }

    public void OnDialogueEnd()
    {
        if (state == 0)
        {
            // Start first math question
            state = 1;
            AskMathQuestion();
        }
        else
        {
            // End of game or next steps
            dialogueManager.StartDialogue(new List<string>() { "Terima kasih telah bermain! Sampai jumpa!" });
        }
    }

    void AskMathQuestion()
    {
        string question = "Berapakah hasil dari 7 x 8?";
        string answer = "56";

        mathQuizManager.SetQuestion(question, answer, OnMathAnswerResult);
    }

    void OnMathAnswerResult(bool correct)
    {
        if (correct)
        {
            List<string> correctDialogue = new List<string>()
            {
                "Benar sekali! 7 dikali 8 adalah 56.",
                "Ini adalah contoh perkalian dalam matematika kelas 7."
            };
            dialogueManager.StartDialogue(correctDialogue, OnDialogueEnd);
        }
        else
        {
            // Allow retry, no state change
        }
    }
}
