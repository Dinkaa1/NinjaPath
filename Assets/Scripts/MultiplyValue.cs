using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MultiplyValue : MonoBehaviour
{
    public int multiplyVal;
    [SerializeField]
    private TMP_Text multiplyValue;

    public void Start()
    {
        SetRandomMultiply();
    }
    public void SetRandomMultiply()
    {
        var Books = GameObject.FindGameObjectsWithTag("book");
        var sumObjects = Books.ToList().Where(b => b.GetComponent<MovingBooks>().isCalculated);
        Debug.Log(sumObjects.Count());
        var multipleValues = new List<int>();
        foreach (var book in Books.Except(sumObjects))
        {
            multipleValues.Add(book.GetComponent<MovingBooks>().bookNum);
        }

        multiplyVal = multipleValues.Aggregate((x, y) => x * y);
        multiplyValue.text = multiplyVal.ToString();
    }
}
