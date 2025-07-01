using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [CreateAssetMenu(fileName = "SignDataList", menuName = "ScriptableObjects/SignDataList")]
    public class SignDataList : ScriptableObject
    {
        [SerializeField] private List<SignData> signDataList;
        private Dictionary<string, SignData> signDictionary;

        private void Initialize()
        {
            signDictionary = new Dictionary<string, SignData>();

            for (int i = 0; i < signDataList.Count; i++)
                signDictionary = AddToDictionary(signDataList[i], signDictionary);
        }

        private static Dictionary<string, SignData> AddToDictionary(SignData signData, Dictionary<string, SignData> signDictionary)
        {
            Dictionary<string, SignData> updatedSignDictionary = new(signDictionary);

            string[] originalStrings = signData.SignShape.Split('\n');

            int rows = originalStrings.Length;
            int cols = originalStrings[0].Length;
            char[,] originalChars = new char[rows, cols];
            for (int y = 0; y < rows; y++)
                for (int x = 0; x < cols; x++)
                    originalChars[y, x] = originalStrings[y][x];

            for (int i = 0; i < 4; i++)
            {
                char[,] rotatedChars = GenerateRotatedChars(originalChars);
                updatedSignDictionary = AddCharsToDictionary(rotatedChars, signData, updatedSignDictionary);
                char[,] flippedChars = GenerateFlippedChars(rotatedChars);
                updatedSignDictionary = AddCharsToDictionary(flippedChars, signData, updatedSignDictionary);
                originalChars = rotatedChars;
            }

            return updatedSignDictionary;
        }

        private static char[,] GenerateRotatedChars(char[,] originalChars)
        {
            int rows = originalChars.GetLength(0);
            int cols = originalChars.GetLength(1);
            char[,] rotatedChars = new char[cols, rows];
            for (int y = 0; y < rows; y++)
                for (int x = 0; x < cols; x++)
                    rotatedChars[cols - x - 1, y] = originalChars[y, x];
            return rotatedChars;
        }

        private static char[,] GenerateFlippedChars(char[,] originalChars)
        {
            int rows = originalChars.GetLength(0);
            int cols = originalChars.GetLength(1);
            char[,] flippedChars = new char[rows, cols];
            for (int y = 0; y < rows; y++)
                for (int x = 0; x < cols; x++)
                    flippedChars[y, x] = originalChars[y, cols - x - 1];
            return flippedChars;
        }

        private static Dictionary<string, SignData> AddCharsToDictionary(char[,] chars, SignData signData, Dictionary<string, SignData> signDictionary)
        {
            string result = "";
            for (int y = chars.GetLength(0) - 1; y >= 0; y--)
            {
                for (int x = 0; x < chars.GetLength(1); x++)
                    result += chars[y, x];
                if (y != 0)
                    result += "\n";
            }

            Dictionary<string, SignData> updatedSignDictionary = new(signDictionary);
            if (!updatedSignDictionary.ContainsKey(result))
                updatedSignDictionary.Add(result, signData);
            return updatedSignDictionary;
        }

        public void Summon(List<Vector2Int> posInts)
        {
            string signShape = ConvertToString(posInts);
            if (signDictionary == null)
                Initialize();
            try
                { signDictionary[signShape].Summon(); }
            catch (KeyNotFoundException)
                { Debug.Log("Fail"); }
        }

        private static string ConvertToString(List<Vector2Int> posInts)
        {
            int minX = posInts.Min(p => p.x);
            int maxX = posInts.Max(p => p.x);
            int minY = posInts.Min(p => p.y);
            int maxY = posInts.Max(p => p.y);

            char[,] posChars = new char[maxY - minY + 1, maxX - minX + 1];
            for (int y = 0; y < posChars.GetLength(0); y++)
                for (int x = 0; x < posChars.GetLength(1); x++)
                    posChars[y, x] = '-';
            for (int i = 0; i < posInts.Count; i++)
                posChars[posInts[i].y - minY, posInts[i].x - minX] = '^';

            string result = "";
            for (int y = posChars.GetLength(0) - 1; y >= 0; y--)
            {
                for (int x = 0; x < posChars.GetLength(1); x++)
                    result += posChars[y, x];
                if(y != 0)
                    result += "\n";
            }
            return result;
        }
    }
}
