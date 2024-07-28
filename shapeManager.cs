using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeManager : MonoBehaviour
{
    public List<GameObject> shapes; // List of shape prefabs
    public GameObject choicePanel; // UI panel for choices
    public GameObject choiceButtonPrefab; // Prefab for choice buttons

    private GameObject correctShape;
    private GameObject pumpkin;

    void Start()
    {
        AssignRandomShape();
    }

    void AssignRandomShape()
    {
        pumpkin = gameObject; // The game object this script is attached to
        int correctIndex = Random.Range(0, shapes.Count);
        correctShape = shapes[correctIndex];

        // Instantiate the correct shape as a child of the pumpkin
        GameObject instantiatedShape = Instantiate(correctShape, pumpkin.transform.position, Quaternion.identity, pumpkin.transform);

        // Adjust the scale of the instantiated shape to fit inside the pumpkin
        //instantiatedShape.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // Adjust the scale values as needed
        instantiatedShape.transform.localPosition = Vector3.zero; // Ensure it is centered inside the pumpkin

        // Adjust the z-position (or relevant axis) to ensure it's visible inside the pumpkin
        instantiatedShape.transform.localPosition = new Vector3(0, 0, 0.5f); // Adjust based on your scene setup

        // Ensure the shape is rendered in front if using 2D
        if (instantiatedShape.GetComponent<SpriteRenderer>() != null)
        {
            instantiatedShape.GetComponent<SpriteRenderer>().sortingOrder = 1; // Ensure it's in front of the pumpkin
        }

        DisplayChoices();
    }


    void DisplayChoices()
    {
        // Clear previous choices
        foreach (Transform child in choicePanel.transform)
        {
            Destroy(child.gameObject);
        }

        Debug.Log("Previous choices cleared.");

        // Ensure the incorrect shape is different from the correct shape
        int incorrectIndex;
        do
        {
            incorrectIndex = Random.Range(0, shapes.Count);
        } while (incorrectIndex == shapes.IndexOf(correctShape));

        GameObject incorrectShape = shapes[incorrectIndex];
        Debug.Log($"Correct shape: {correctShape.name}, Incorrect shape: {incorrectShape.name}");

        // Create a list of the correct and incorrect shapes
        List<GameObject> choices = new List<GameObject> { correctShape, incorrectShape };

        // Shuffle choices
        for (int i = 0; i < choices.Count; i++)
        {
            GameObject temp = choices[i];
            int randomIndex = Random.Range(i, choices.Count);
            choices[i] = choices[randomIndex];
            choices[randomIndex] = temp;
        }

        Debug.Log("Choices shuffled.");

        // Create buttons
        foreach (GameObject shape in choices)
        {
            GameObject button = Instantiate(choiceButtonPrefab, choicePanel.transform);
            Debug.Log("Button instantiated.");

            // Find the placeholder and set the shape
            Transform shapePlaceholder = button.transform.Find("ShapePlaceholder");

            if (shapePlaceholder != null)
            {
                // Add the shape to the placeholder
                GameObject shapeInstance = Instantiate(shape, shapePlaceholder.position, Quaternion.identity, shapePlaceholder);
                shapeInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // Adjust scale as needed
                shapeInstance.transform.localPosition = Vector3.zero; // Center the shape within the placeholder

                Debug.Log("Shape instantiated inside button: " + shape.name);
            }
            else
            {
                Debug.LogError("ShapePlaceholder not found in the button prefab.");
            }

            // Assign the click listener
            button.GetComponent<Button>().onClick.AddListener(() => OnChoiceSelected(shape));

            Debug.Log("Button click listener added.");
        }

        Debug.Log("Choices displayed.");
    }



    void OnChoiceSelected(GameObject selectedShape)
    {
        if (selectedShape == correctShape)
        {
            Debug.Log("Correct choice!");
            SlingShot();
        }
        else
        {
            Debug.Log("Incorrect choice.");
            PlayerLost();
        }
    }

    void SlingShot()
    {
        Debug.Log("Slingshot activated!");
        // Implement slingshot logic here
    }

    void PlayerLost()
    {
        Debug.Log("Player lost the level.");
        // Display a message that the player lost this level
    }
}
