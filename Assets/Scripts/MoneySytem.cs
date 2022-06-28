using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySytem : MonoBehaviour
{
    public int money;

    public int BeginMoney;


    // Start is called before the first frame update
    void Start()
    {
        money += BeginMoney;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addMoney(int MoneyToAdd)
    {
        money += MoneyToAdd;
    }

    public void substractMoney(int MoneyToSubstract)
    {
        if(money< MoneyToSubstract)
        {

        }
        else
        {
            money -= MoneyToSubstract;
        }
    }
}
