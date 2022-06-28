using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressUI : MonoBehaviour
{
    [SerializeField] private Image UIFillImage;
    [SerializeField] private TextMeshProUGUI UIStartText;
    [SerializeField] private TextMeshProUGUI UIEndText;
    [SerializeField] private Transform PlayerTranform;
    [SerializeField] private Transform EndLineTranform;
    [SerializeField] private Image UIImage;

    private Vector3 EndLinepos;

    private float fullDistance;



    // Start is called before the first frame update
    void Start()
    {
        EndLinepos = EndLineTranform.position;
        fullDistance = GetDistance();
    }

    public void SetLevelTexts(int level)
    {
        UIStartText.text = level.ToString();
        UIEndText.text = (level + 1).ToString();
    }

    private float GetDistance()
    {
        return Vector3.Distance(PlayerTranform.position, EndLinepos);
    }

    private void UpdateProgressFill(float value)
    {
        UIFillImage.fillAmount = value;
    }

    private void Update()
    {
        if (PlayerTranform.position.z <= EndLineTranform.position.z)
        {
            float newDistance = GetDistance();
            float ProgressValue = Mathf.InverseLerp(fullDistance, 0f, newDistance);
            UpdateProgressFill(ProgressValue);
        }
        else
        {
            UIImage.enabled = false;
            UIFillImage.enabled = false;
        }

    }
}
