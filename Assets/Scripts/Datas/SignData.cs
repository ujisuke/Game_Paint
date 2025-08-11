using System;
using Assets.Scripts.Objects.Familiars.Base.Controller;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [Serializable]
    public class SignData
    {
        [SerializeField][TextArea(5,5)] private string signShape;
        [SerializeField] private GameObject familiar;
        public string SignShape => signShape;
        public string FamiliarName => familiar.name;

        public void Summon(Vector2 pos, ColorName colorNameInput, bool isByEnemy)
        {
            GameObject newfamiliar = GameObject.Instantiate(familiar);
            newfamiliar.GetComponent<FamiliarController>().OnSummon(pos, colorNameInput, isByEnemy);
        }
    }
}
