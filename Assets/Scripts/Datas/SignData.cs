using System;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [Serializable]
    public class SignData
    {
        [SerializeField][TextArea(5,5)] private string signShape;
        public string SignShape => signShape;
        [SerializeField] private GameObject familiar;

        public void Summon()
        {
            GameObject.Instantiate(familiar);
        }

        public static implicit operator SignData(UnityEngine.Object v)
        {
            throw new NotImplementedException();
        }
    }
}
