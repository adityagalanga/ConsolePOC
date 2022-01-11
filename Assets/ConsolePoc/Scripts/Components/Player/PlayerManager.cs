using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Nagih
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private SocketManager Socket;
        [SerializeField] private CharacterInputBasePooler CharacterInput;

        Dictionary<string, CharacterInputListener> PlayerList = new Dictionary<string, CharacterInputListener>();

        private void Start()
        {
            Socket.OnInput += OnInput;
            Socket.OnConnected += OnConnected;
            Socket.OnDisconnect += OnDisconnect;
        }

        private void OnConnected(JObject data)
        {
            OnUserConnectedReturnData user = data.ToObject<OnUserConnectedReturnData>();
            if(user != null)
            {
                CharacterInputListener obj = CharacterInput.GetObject();
                obj.ClearPosition();

                PlayerList.Add(user.Id, obj);
                obj.gameObject.SetActive(true);
            }
        }

        private void OnDisconnect(JObject data)
        {
            OnUserDisconnectReturnData user = data.ToObject<OnUserDisconnectReturnData>();
            if (user != null)
            {
                if (PlayerList.Any(x => x.Key == user.Id))
                {
                    PlayerList.Remove(user.Id);
                }
            }
        }

        private void OnInput(JObject data)
        {
            OnInputReturnData user = data.ToObject<OnInputReturnData>();
            if (user != null)
            {
                if(PlayerList.Any(x=>x.Key == user.Id))
                {
                    CharacterInputListener selected = PlayerList[user.Id];
                    
                    if((int)user.Input <= 4)
                    {
                        //DPAD Controller
                        selected.OnMove(user.Input, user.Conditon);
                    }
                    else
                    {
                        if(user.Input == InputController.ChangeColor)
                        {
                            string color = selected.OnChangeColor();
                            Socket.SendMessage(SocketType.ChangeColorController, new ChangeColorRequestData(user.Id, color));
                        }
                    }

                }
            }
        }
    }
}