using System;
using Assets.Scripts.Objects.Familiars.Base.Controller;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [Serializable]
    public class SignData
    {
        [SerializeField][TextArea(5,5)] private string signShape;
        public string SignShape => signShape;
        [SerializeField] private GameObject familiar;

        public void Summon(Vector2 position, ColorName colorNameInput)
        {
            GameObject newfamiliar = GameObject.Instantiate(familiar);
            newfamiliar.GetComponent<FamiliarController>().OnSummon(position, colorNameInput, false);
        }
    }
}
