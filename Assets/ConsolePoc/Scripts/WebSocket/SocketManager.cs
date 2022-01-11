using NativeWebSocket;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Nagih
{
    public class SocketManager : MonoBehaviour
    {
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
        }

        private async void Connect()
        {
            Manager.GetInstance().FrontLoading.SetActive(true);
            websocket = new WebSocket("url");
            websocket.OnOpen += OnOpen;
            websocket.OnError += OnError;
            websocket.OnClose += OnClose;
            websocket.OnMessage += OnMessage;

            await websocket.Connect();
        }

        private void OnOpen()
        {
            
        }

        private void OnClose(WebSocketCloseCode e)
        {

        }

        private void OnError(object e)
        {

        }

        private void OnMessage(byte[] bytes)
        {
            string message = Encoding.UTF8.GetString(bytes);
            Debug.Log("Message = " + message);
            JObject messageJson = JObject.Parse(message);
            WebSocketData data = messageJson.ToObject<WebSocketData>();
            if (data == null) return;

            if (data.type == (int)SocketType.InputUser)
            {
                OnInput(messageJson);
            }
            else if(data.type == (int)SocketType.ConnectedUser)
            {
                OnConnected(messageJson);
            }
            else if(data.type == (int)SocketType.DisconnectUser)
            {
                OnDisconnect(messageJson);
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
