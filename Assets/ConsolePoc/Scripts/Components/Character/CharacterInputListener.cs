using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

namespace Nagih
{
    public class CharacterInputListener : Character
    {
        private Vector3 Direction = Vector3.zero;

        public void ClearPosition()
        {
            Direction = Vector3.zero;
        }

        public string OnChangeColor()
        {
            Color color = new Color(
                 Random.Range(0f, 1f),
                 Random.Range(0f, 1f),
                 Random.Range(0f, 1f)
             );
            ChangeColorCharacter(color);
            string HexColor = ColorUtility.ToHtmlStringRGB(color);
            return HexColor;
        }

        public void OnMove(InputController data, InputAction move)
        {
            switch (data)
            {
                case InputController.up:
                    {
                        Direction.z = move == InputAction.down ? +1 : 0;
                        break;
                    }
                case InputController.down:
                    {
                        Direction.z = move == InputAction.down ? -1 : 0;
                        break;
                    }
                case InputController.left:
                    {
                        Direction.x = move == InputAction.down ? -1 : 0;
                        break;
                    }
                case InputController.right:
                    {
                        Direction.x = move == InputAction.down ? 1 : 0;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void Update()
        {
            EditorKey();
            MoveCharacter(Direction);
        }

        private void EditorKey()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnChangeColor();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                OnMove(InputController.left, InputAction.down);
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                OnMove(InputController.left, InputAction.up);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                OnMove(InputController.right, InputAction.down);
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                OnMove(InputController.right, InputAction.up);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                OnMove(InputController.up, InputAction.down);
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                OnMove(InputController.up, InputAction.up);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                OnMove(InputController.down, InputAction.down);
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                OnMove(InputController.down, InputAction.up);
            }
        }
    }
}