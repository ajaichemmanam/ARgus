using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBehavior : MonoBehaviour
{

    public Text uiText;
    private Vector3 showPosition = new Vector3(0, 210f, 0);
    private Vector3 hidePosition = new Vector3(0, 342f, 0);
    private Vector3 desiredPosition;

    private void Awake()
    {
        desiredPosition = hidePosition;
    }

    private void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, desiredPosition, 8F * Time.deltaTime);
    }

    public void ShowMessage(string message)
    {
        HideMessage();
        desiredPosition = showPosition;
        uiText.text = message;
        if (DelayCoroutine != null)
        {
            StopCoroutine(DelayCoroutine);
        }
        DelayCoroutine = StartCoroutine(DelayHideMessage());
    }

    Coroutine DelayCoroutine;
    IEnumerator DelayHideMessage()
    {
        yield return new WaitForSeconds(2f);
        HideMessage();
        DelayCoroutine = null;
    }

    void HideMessage()
    {
        desiredPosition = hidePosition;
        uiText.text = "";
    }
}
