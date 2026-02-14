using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ControllerGameQuestion : MonoBehaviour
{

    public TextMeshProUGUI textoPregunta;
    public TextMeshProUGUI textoOpcion1;
    public TextMeshProUGUI textoOpcion2;
    public TextMeshProUGUI textoOpcion3;
    public TextMeshProUGUI textoOpcion4;
    List<MultipleQuestion> questions = new List<MultipleQuestion>();
    int currentQuestionIndex = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        ReadCreateListQuestion();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReadCreateListQuestion()
    {

        // Read the entire file text and split into lines
        string texto = File.ReadAllText("Assets/Game/Files/ArchivoPreguntasMV3 - copia.txt");
        var lines = texto.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            // Split each line into parts using '-' as separator
            var parts = line.Split('-');

            // Expect exactly 8 fields per line
            if (parts.Length == 8)
            {
                // Create a MultipleQuestion instance by setting properties (no constructor assumed)
                MultipleQuestion pregunta = new MultipleQuestion
                {
                    Question = parts[0],
                    Option1 = parts[1],
                    Option2 = parts[2],
                    Option3 = parts[3],
                    Option4 = parts[4],
                    Answer = parts[5],
                    Versiculo = parts[6],
                    Difficulty = parts[7]
                };

                // Add the question to the list
                questions.Add(pregunta);

                Debug.Log($"Loaded question: {pregunta.Question}");

            }
            else
            {
                Debug.LogWarning($"Skipping invalid line (expected 8 fields): {line}");
            }
        }

        // After loading all questions, display the first one (if any)
        if (questions.Count > 0)
        {
            currentQuestionIndex = 0;
            DisplayQuestion(currentQuestionIndex);
        }
        else
        {
            Debug.LogWarning("No questions were loaded from the file.");
        }

    }

    // Display the question at the given index (safe)
    public void DisplayQuestion(int index)
    {
        if (questions == null || questions.Count == 0)
            return;

        if (index < 0 || index >= questions.Count)
        {
            Debug.LogWarning($"Question index out of range: {index}");
            return;
        }

        var pregunta = questions[index];

        if (textoPregunta != null) textoPregunta.text = pregunta.Question;
        if (textoOpcion1 != null) textoOpcion1.text = pregunta.Option1;
        if (textoOpcion2 != null) textoOpcion2.text = pregunta.Option2;
        if (textoOpcion3 != null) textoOpcion3.text = pregunta.Option3;
        if (textoOpcion4 != null) textoOpcion4.text = pregunta.Option4;

        Debug.Log($"Showing question {index + 1}/{questions.Count}: {pregunta.Question}");
    }

    // Go to next question (wraps around)
    public void NextQuestion()
    {
        if (questions == null || questions.Count == 0)
            return;

        currentQuestionIndex = (currentQuestionIndex + 1) % questions.Count;
        DisplayQuestion(currentQuestionIndex);
    }

    // Go to previous question (wraps around)
    public void PreviousQuestion()
    {
        if (questions == null || questions.Count == 0)
            return;

        currentQuestionIndex = (currentQuestionIndex - 1 + questions.Count) % questions.Count;
        DisplayQuestion(currentQuestionIndex);
    }
}
