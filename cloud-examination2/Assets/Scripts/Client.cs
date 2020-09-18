using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    private static readonly string URL = "ws://localhost:8080/";
    [SerializeField] private Text text = null;
    [SerializeField] private InputField inputField = null;
    private WebSocket websocket = null;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        if (text == null)
        {
            Debug.LogError("Textをセットしてください。");
        }

        if (inputField == null)
        {
            Debug.LogError("InputFieldをセットしてください。");
        }
#endif
        Connect();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        if (websocket == null)
        {
            return;
        }

        if (websocket.ReadyState == WebSocketState.Open || websocket.ReadyState == WebSocketState.Connecting)
        {
            websocket.Close();
        }
        websocket = null;
    }

    public void Connect()
    {
        if (websocket != null)
        {
#if UNITY_EDITOR
            Debug.LogError("不正です。");
#endif
        }

        websocket = new WebSocket(URL);
        websocket.OnOpen += (sender, args) =>
        {
#if UNITY_EDITOR
            Debug.Log("open");
#endif
        };
        websocket.OnMessage += (sender, args) =>
        {
#if UNITY_EDITOR
            Debug.Log("message");
#endif
            text.text = args.Data;
        };
        websocket.OnClose += (sender, args) =>
        {
#if UNITY_EDITOR
            Debug.Log("close");
#endif
        };
        websocket.OnError += (sender, args) =>
        {
#if UNITY_EDITOR
            Debug.Log("error");
# endif
        };
        websocket.ConnectAsync();
    }

    public void Send()
    {
#if UNITY_EDITOR
        if (websocket == null)
        {
            Debug.LogError("不正です。");
            return;
        }
#endif
        websocket.Send(inputField.text);
    }
}
