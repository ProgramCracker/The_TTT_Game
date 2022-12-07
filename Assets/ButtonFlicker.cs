using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFlicker : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _button;
    public int length;

    [SerializeField] Color _orginalColor;
    [SerializeField] Color _lowColor;

    private void Awake()
    {
        _button.color = _orginalColor;
    }

    public void Flicker()
    {
        for (int i = 0; i < length; i++)
        {
            _button.color = _lowColor;
            StartCoroutine(slow());
        }
    }

    IEnumerator slow()
    {
        yield return new WaitForSeconds(2);
        _button.color = _orginalColor;
    }

}
