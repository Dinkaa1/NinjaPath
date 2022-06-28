using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class MovingBooks : MonoBehaviour
{
    [SerializeField]
    public int bookNum;
    public TMP_Text bookNumText;

    Vector3 offset;
    Rigidbody rigbody;
    Vector3 startPosition;
    public bool isCalculated = false;
    Vector3 stageDimensions;
    Vector3 stageSize;
    Vector3 objectSize;
    private void Start()
    {
        startPosition = gameObject.transform.position;
        if (SceneManager.GetActiveScene().buildIndex == 4)
            bookNum = Random.Range(-9, 10);
        else
            bookNum = Random.Range(0, 10);
        bookNumText.text = bookNum.ToString();
        rigbody = gameObject.transform.GetComponent<Rigidbody>();
        stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectSize = GetComponent<Collider>().bounds.size;
        stageSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, Camera.main.transform.position.z));
    }

    void OnMouseDown()
    {
        GameObject.FindGameObjectWithTag("note").GetComponentsInChildren<TextMeshPro>().ToList().ForEach(t => t.GetComponent<MeshRenderer>().forceRenderingOff = true);
        GameObject.FindGameObjectWithTag("note").transform.GetChild(0).GetComponent<Canvas>().enabled = false;
        GameObject.FindGameObjectWithTag("note").transform.GetChild(1).GetComponent<Canvas>().enabled = false;
        offset = transform.position - MouseWorldPosition();
        transform.GetComponent<Collider>().enabled = false;
        CheckSlots();
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "book")
        {
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            gameObject.transform.position = startPosition;
        }
    }
    void OnMouseDrag()
    {
        rigbody.velocity = (MouseWorldPosition() - transform.position) * 15;
    }

    void OnMouseUp()
    {
        var rayOrigin = Camera.main.transform.position;
        var rayDirection = MouseWorldPosition() - Camera.main.transform.position;
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo))
        {
            if (hitInfo.transform.CompareTag("sumSlot") || hitInfo.transform.CompareTag("multiplySlot"))
            {
                hitInfo.collider.GetComponent<MeshRenderer>().forceRenderingOff = true;
                hitInfo.collider.GetComponent<BookSlot>().ChosenBook = gameObject;
                transform.position = hitInfo.transform.position;
            }
        }
        rigbody.velocity = Vector3.zero;
        transform.GetComponent<Collider>().enabled = true;
        if (transform.position.y >= -(stageDimensions.y + objectSize.y))
            transform.position = startPosition;
        else if (transform.position.y <= -(stageSize.y / 2 + stageDimensions.y + objectSize.y))
            transform.position = startPosition;
        if (transform.position.x <= -Mathf.Sqrt(Mathf.Abs(stageDimensions.x)) + objectSize.x)
            transform.position = startPosition;
        else if (transform.position.x >= (-Mathf.Sqrt(Mathf.Abs(stageDimensions.x)) + objectSize.x) * -1)
            transform.position = startPosition;
    }
    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    void CheckSlots()
    {
        var slots = GameObject.FindGameObjectsWithTag("sumSlot").ToList();
        slots.AddRange(GameObject.FindGameObjectsWithTag("multiplySlot"));
        var chosenSlot = slots.FirstOrDefault(s => s.GetComponent<BookSlot>().ChosenBook == gameObject);
        if (chosenSlot != null)
        {
            chosenSlot.GetComponent<MeshRenderer>().forceRenderingOff = false;
            chosenSlot.GetComponent<BookSlot>().ChosenBook = null;
        }
    }
}
