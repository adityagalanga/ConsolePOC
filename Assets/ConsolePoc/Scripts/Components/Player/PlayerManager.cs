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
                if (!PlayerList.ContainsKey(user.id))
                {
                    CharacterInputListener obj = CharacterInput.GetObject();
                    obj.ClearPosition();
                    obj.AddIDText(user.name);
                    obj.ResetColor();

                    PlayerList.Add(user.id, obj);
                    obj.gameObject.SetActive(true);
                }
            }
        }

        private void OnDisconnect(JObject data)
        {
            OnUserDisconnectReturnData user = data.ToObject<OnUserDisconnectReturnData>();
            if (user != null)
            {
                if (PlayerList.Any(x => x.Key == user.id))
                {
                    CharacterInput.ReturnObject(PlayerList[user.id]);
                    PlayerList.Remove(user.id);
                }
            }
        }

        private void OnInput(JObject data)
        {
            OnInputReturnData user = data.ToObject<OnInputReturnData>();
            if (user != null)
            {
                if(PlayerList.Any(x=>x.Key == user.id))
                {
                    CharacterInputListener selected = PlayerList[user.id];
                    
                    if((int)user.input < 4)
                    {
                        //DPAD Controller
                        selected.OnMove(user.input, user.condition);
                    }
                    else
                    {
                        if(user.input == InputController.ChangeColor)
                        {
                            string color = selected.OnChangeColor();
                            Socket.SendMessage(SocketType.ChangeColorController, new ChangeColorRequestData(user.id, color));
                        }
                    }

                }
            }
        }
    }
}