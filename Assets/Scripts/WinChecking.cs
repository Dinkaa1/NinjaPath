using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinChecking : MonoBehaviour
{
    private GameObject[] sumSlots;
    private GameObject[] multiplySlots;
    void Start()
    {
        GameObject.FindGameObjectWithTag("buttons").GetComponentsInChildren<Button>()[1].enabled = false;
    }

    void Update()
    {
        sumSlots = GameObject.FindGameObjectsWithTag("sumSlot");
        multiplySlots = GameObject.FindGameObjectsWithTag("multiplySlot");
        if (multiplySlots.All(t => t.GetComponent<BookSlot>().ChosenBook != null) && (sumSlots.All(t => t.GetComponent<BookSlot>().ChosenBook != null)))
        {
            if (CompareSum() && CompareMultiply())
            {
                transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
                GameObject.FindGameObjectWithTag("buttons").GetComponentsInChildren<Button>()[1].enabled = true;
            }
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    bool CompareSum()
    {
        var sumValues = new List<int>();
        foreach(var sumSlot in sumSlots)
        {
            sumValues.Add(sumSlot.GetComponent<BookSlot>().ChosenBook.GetComponent<MovingBooks>().bookNum);
        }
        var sumValue = GameObject.FindGameObjectWithTag("sumValue").GetComponent<SumValue>().sumVal;
        if (sumValues.Sum() == sumValue)
            return true;
        else
            return false;
    }

    bool CompareMultiply()
    {
        var multiplyValues = new List<int>();
        foreach (var multiplySlot in multiplySlots)
        {
            multiplyValues.Add(multiplySlot.GetComponent<BookSlot>().ChosenBook.GetComponent<MovingBooks>().bookNum);
        }
        var multiplyValue = GameObject.FindGameObjectWithTag("multiplyValue").GetComponent<MultiplyValue>().multiplyVal; ;
        if (multiplyValues.Aggregate((x, y) => x * y) == multiplyValue)
            return true;
        return false;
    }
}
