using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SumValue : MonoBehaviour
{
    [SerializeField]
    public int sumVal;

    [SerializeField]
    private TMP_Text sumValue; 

    public void Start()
    {
        SetRandomSum();
    }
    public void SetRandomSum()
    {
        var Books = GameObject.FindGameObjectsWithTag("book");
        var sumValues = new List<int>();
        var sumObjects = new List<GameObject>();
        int counter;
        if (SceneManager.GetActiveScene().buildIndex == 4)
            counter = 3;
        else
            counter = 2;
        for (int i = 0; i < counter; i++)
        {
            int index = Random.Range(0, Books.Count());
            while (Books[index].GetComponent<MovingBooks>().isCalculated)
            {
                index = Random.Range(0, Books.Count());
            }
            sumObjects.Add(Books[index]);
            Debug.Log(Books[index].GetComponent<MovingBooks>().bookNum + " for sum");
            sumValues.Add(Books[index].GetComponent<MovingBooks>().bookNum);
            Books[index].GetComponent<MovingBooks>().isCalculated = true;
        }
        sumVal = sumValues.Sum();
        sumValue.text = sumVal.ToString();
    }
}
