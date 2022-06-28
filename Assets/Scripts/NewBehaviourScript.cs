using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;

    public float textSpeed;
    private int index;
    public int sceneNum;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                Nextline();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];

            }
        }
    }

    void StartDialogue()
    {

        index = 0;
        Debug.Log(index);
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void Nextline()
    {
        if (index < lines.Length - 1)
        {
            index++; 
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
            Debug.Log(index);
        }
        else
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene(sceneNum);
        }

    }


}
