using UnityEngine;

namespace Assets.Scripts.GameSystems.InputSystem.Controller
{
    public class CustomInputSystem
    {
        private bool isPushingSelect;
        private bool isPushingBack;
        private static CustomInputSystem instance;
        public static CustomInputSystem Instance => instance ??= new CustomInputSystem();

        private CustomInputSystem()
        {
            isPushingSelect = false;
        }

        public bool DoesSelectKeyUp()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                isPushingSelect = true;
                return false;
            }
            if (isPushingSelect)
            {
                isPushingSelect = false;
                return true;
            }
            return false;
        }

        public bool DoesBackKeyUp()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                isPushingBack = true;
                return false;
            }
            if (isPushingBack)
            {
                isPushingBack = false;
                return true;
            }
            return false;
        }
    }
}
