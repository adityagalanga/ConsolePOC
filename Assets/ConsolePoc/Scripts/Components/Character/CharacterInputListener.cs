using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using System;

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
            Color color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            base.ChangeColorCharacter(color);
            string HexColor = ColorUtility.ToHtmlStringRGB(color);
            return HexColor;
        }

        public void OnMove(InputController data, InputAction move)
        {
            switch (data)
            {
                case InputController.Forward:
                    {
                        Direction.z = move == InputAction.Down ? +1 : 0;
                        break;
                    }
                case InputController.Backward:
                    {
                        Direction.z = move == InputAction.Down ? -1 : 0;
                        break;
                    }
                case InputController.Left:
                    {
                        Direction.x = move == InputAction.Down ? -1 : 0;
                        break;
                    }
                case InputController.Right:
                    {
                        Direction.x = move == InputAction.Down ? 1 : 0;
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

            base.MoveCharacter(Direction);
        }

        private void EditorKey()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnChangeColor();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                OnMove(InputController.Left, InputAction.Down);
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                OnMove(InputController.Left, InputAction.Up);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                OnMove(InputController.Right, InputAction.Down);
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                OnMove(InputController.Right, InputAction.Up);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                OnMove(InputController.Forward, InputAction.Down);
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                OnMove(InputController.Forward, InputAction.Up);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                OnMove(InputController.Backward, InputAction.Down);
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                OnMove(InputController.Backward, InputAction.Up);
            }
        }
    }
}