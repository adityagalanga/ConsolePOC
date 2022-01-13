using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Nagih
{
    public class Character : MonoBehaviour
    {
        public GameObject CharacterObject;
        public Rigidbody CharacterRigidBody;
        public Renderer PlayerRenderer;
        public Text IDText;
        public float Speed = 0.2f;

        public void AddIDText(string text)
        {
            IDText.text = text;
        }
        public void ResetColor()
        {
            PlayerRenderer.material.color = Color.white;
        }

        public void MoveCharacter(Vector3 translate)
        {
            if (translate != Vector3.zero)
            {
                CharacterRigidBody.MovePosition(CharacterObject.transform.position + translate * Speed);
            }
        }

        public void ChangeColorCharacter(Color color)
        {
            PlayerRenderer.material.color = color;
        }

    }
}