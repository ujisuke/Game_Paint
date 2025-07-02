using System;
using Assets.Scripts.CommonObject.Model;
using Assets.Scripts.Familiar.Base.Controller;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [Serializable]
    public class SignData
    {
        [SerializeField][TextArea(5,5)] private string signShape;
        public string SignShape => signShape;
        [SerializeField] private GameObject familiar;

        public void Summon(Position position)
        {
            GameObject newfamiliar = GameObject.Instantiate(familiar);
            newfamiliar.GetComponent<FamiliarController>().OnSummon(position);
        }
    }
}
