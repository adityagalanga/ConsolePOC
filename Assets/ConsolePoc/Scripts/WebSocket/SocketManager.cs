using NativeWebSocket;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
namespace Nagih
{
    public class SocketManager : MonoBehaviour
    {
        [SerializeField] private Text SocketStateDebug;

        private WebSocket websocket;
        public delegate void SocketData(JObject data);
        public event SocketData OnConnected;
        public event SocketData OnDisconnect;
        public event SocketData OnInput;

        private void Update()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            if (websocket != null)
            {
                websocket.DispatchMessageQueue();
            }
#endif

            if(websocket != null)
            {
                SocketStateDebug.text = websocket.State.ToString();
            }
        }

        private void Start()
        {
            Connect();
        }

        private async void Connect()
        {
            websocket = new WebSocket("wss://nconsole.onigiri.fira.id/game_display");
            websocket.OnOpen += OnOpen;
            websocket.OnError += OnError;
            websocket.OnClose += OnClose;
            websocket.OnMessage += OnMessage;

            await websocket.Connect();
        }

        private void OnOpen()
        {
            Debug.Log("Socket Open");
        }

        private void OnClose(WebSocketCloseCode e)
        {
            Debug.Log("Socket Close with Close Code = "+ e.ToString());
        }

        private void OnError(object e)
        {
            Debug.Log("Socket Error with Error Code = " + e.ToString());
        }

        private void OnMessage(byte[] bytes)
        {
            string message = Encoding.UTF8.GetString(bytes);
            Debug.Log("Message = " + message);

            JObject messageJson = JObject.Parse(message);
            JToken type = messageJson["type"];
            if (type == null)
            {
                Debug.Log("Parse Message Error");
                return;
            }

            WebSocketData SocketData = messageJson.ToObject<WebSocketData>();

            if (SocketData == null) return;

            if (SocketData.type == (int)SocketType.InputUser)
            {
                OnInput(SocketData.data);
            }
            else if(SocketData.type == (int)SocketType.ConnectedUser)
            {
                OnConnected(SocketData.data);
            }
            else if(SocketData.type == (int)SocketType.DisconnectUser)
            {
                OnDisconnect(SocketData.data);
            }
        }

        public void SendMessage(SocketType type, IRequestData data)
        {
            Send(type, data);
        }

        private async void Send(SocketType type, IRequestData data)
        {
            byte[] request = Manager.GetInstance().Request.CreateSocketRequest(type, data);
            await websocket.Send(request);
        }
    }


    public enum SocketType
    {
        ConnectedUser = 1001,
        DisconnectUser = 1002,
        InputUser = 1003,
        ChangeColorController = 2001
    }
}
