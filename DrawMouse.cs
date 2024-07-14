using System.Collections.Generic;
using UnityEngine;

public class DrawMouse : MonoBehaviour
{
    private LineRenderer line;
    private Vector3 previousPosition;
    private DollarOneRecognizer recognizer;
    private List<DollarOneRecognizer.Point> drawnPoints;

    [SerializeField]
    private float minDistance = 0.1f;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 1;
        previousPosition = transform.position;
        recognizer = new DollarOneRecognizer();
        drawnPoints = new List<DollarOneRecognizer.Point>();

        // Add predefined shapes
        recognizer.AddTemplate("circle", new List<DollarOneRecognizer.Point>
    {
        // Add more points for a detailed circle
        new DollarOneRecognizer.Point(0, 1),
        new DollarOneRecognizer.Point(0.5f, 0.866f),
        new DollarOneRecognizer.Point(0.866f, 0.5f),
        new DollarOneRecognizer.Point(1, 0),
        new DollarOneRecognizer.Point(0.866f, -0.5f),
        new DollarOneRecognizer.Point(0.5f, -0.866f),
        new DollarOneRecognizer.Point(0, -1),
        new DollarOneRecognizer.Point(-0.5f, -0.866f),
        new DollarOneRecognizer.Point(-0.866f, -0.5f),
        new DollarOneRecognizer.Point(-1, 0),
        new DollarOneRecognizer.Point(-0.866f, 0.5f),
        new DollarOneRecognizer.Point(-0.5f, 0.866f),
        new DollarOneRecognizer.Point(0, 1)
    });

        recognizer.AddTemplate("triangle", new List<DollarOneRecognizer.Point>
    {
        new DollarOneRecognizer.Point(0, 0),
        new DollarOneRecognizer.Point(1, 2),
        new DollarOneRecognizer.Point(2, 0),
        new DollarOneRecognizer.Point(0, 0)
    });

        recognizer.AddTemplate("line", new List<DollarOneRecognizer.Point>
    {
        new DollarOneRecognizer.Point(0, 0),
        new DollarOneRecognizer.Point(6, 0)
    });

        recognizer.AddTemplate("square", new List<DollarOneRecognizer.Point>
    {
        new DollarOneRecognizer.Point(0, 0),
        new DollarOneRecognizer.Point(1, 0),
        new DollarOneRecognizer.Point(1, 1),
        new DollarOneRecognizer.Point(0, 1),
        new DollarOneRecognizer.Point(0, 0)
    });

        recognizer.AddTemplate("v-shape", new List<DollarOneRecognizer.Point>
    {
        new DollarOneRecognizer.Point(0, 0),
        new DollarOneRecognizer.Point(0.5f, 1),
        new DollarOneRecognizer.Point(1, 0)
    });
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            drawnPoints.Clear();
            line.positionCount = 1;
            previousPosition = transform.position;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = 0f;
            if (Vector3.Distance(currentPosition, previousPosition) > minDistance)
            {
                if (previousPosition == transform.position)
                {
                    line.SetPosition(0, currentPosition);
                }
                else
                {
                    line.positionCount++;
                    line.SetPosition(line.positionCount - 1, currentPosition);
                }
                previousPosition = currentPosition;

                drawnPoints.Add(new DollarOneRecognizer.Point(currentPosition.x, currentPosition.y));
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            RecognizeShape();
        }
    }

    private void RecognizeShape()
    {
        string recognizedShape = recognizer.Recognize(drawnPoints);
        Debug.Log("Recognized Shape: " + recognizedShape);

        // Perform actions based on recognized shape
        switch (recognizedShape)
        {
            case "circle":
                // Perform circle action
                Debug.Log("Circle action performed");
                break;
            case "triangle":
                // Perform triangle action
                Debug.Log("Triangle action performed");
                break;
            case "line":
                // Perform line action
                Debug.Log("Line action performed");
                break;
            case "square":
                // Perform line action
                Debug.Log("Square action performed");
                break;
            case "v-shape":
                // Perform line action
                Debug.Log("v-shape action performed");
                break;
            default:
                Debug.Log("No recognized action");
                break;
        }
    }
}
