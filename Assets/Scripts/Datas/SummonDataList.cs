using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Assets.Scripts.StageTiles.Model;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [CreateAssetMenu(fileName = "SummonDataList", menuName = "ScriptableObjects/SummonDataList")]
    public class SummonDataList : ScriptableObject
    {
        [SerializeField] private List<SignData> signDataListGeneral;
        [SerializeField] private List<SignData> signDataListUnique;
        [SerializeField] private float summonByEnemyDelaySeconds;
        private Dictionary<string, SignData> signDictUseSignShape;
        private Dictionary<string, (List<Vector2> posIntList, int width, int height)> signDictUseFamiliarName;
        private static SummonDataList instance;
        public static SummonDataList Instance => instance;

        public void SetInstance()
        {
            instance = this;
        }

        private void InitializeDictUseSignShape()
        {
            signDictUseSignShape = new Dictionary<string, SignData>();

            for (int i = 0; i < signDataListGeneral.Count; i++)
                signDictUseSignShape = AddToDictUseSignShape(signDataListGeneral[i], signDictUseSignShape);
            for (int i = 0; i < signDataListUnique.Count; i++)
                signDictUseSignShape = AddToDictUseSignShape(signDataListUnique[i], signDictUseSignShape);
        }

        private static Dictionary<string, SignData> AddToDictUseSignShape(SignData signData, Dictionary<string, SignData> signDictionary)
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
                updatedSignDictionary = AddCharsToDictUseSignShape(rotatedChars, signData, updatedSignDictionary);
                char[,] flippedChars = GenerateFlippedChars(rotatedChars);
                updatedSignDictionary = AddCharsToDictUseSignShape(flippedChars, signData, updatedSignDictionary);
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

        private static Dictionary<string, SignData> AddCharsToDictUseSignShape(char[,] chars, SignData signData, Dictionary<string, SignData> signDictionary)
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

        public void Summon(List<Vector2Int> posInts, ColorName colorNameInput, bool isByEnemy)
        {
            string signShape = ConvertToString(posInts);
            if (signDictUseSignShape == null)
                InitializeDictUseSignShape();
            try
            {
                float averageX = (posInts.Min(p => p.x) + posInts.Max(p => p.x)) / 2f;
                float averageY = (posInts.Min(p => p.y) + posInts.Max(p => p.y)) / 2f;
                Vector2 centerPos = new(averageX + 0.5f, averageY + 0.5f);
                signDictUseSignShape[signShape].Summon(centerPos, colorNameInput, isByEnemy);
            }
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
                if (y != 0)
                    result += "\n";
            }
            return result;
        }

        public async UniTask SummonByEnemy(string familiarName, Vector2 pos, CancellationToken token)
        {
            if (signDictUseFamiliarName == null)
                InitializeDictUseFamiliarName();

            (List<Vector2> posList, int width, int height) = signDictUseFamiliarName[familiarName];
            Vector2 posMin = pos - new Vector2(width / 2f, height / 2f);

            Vector2 offset = new(0f, 0f);
            if (posMin.x < 0)
                offset.x = -posMin.x;
            else if (posMin.x > StageData.Instance.Width - width)
                offset.x = -(posMin.x + width - StageData.Instance.Width);
            if (posMin.y < 0)
                offset.y = -posMin.y;
            else if (posMin.y > StageData.Instance.Height - height)
                offset.y = -(posMin.y + height - StageData.Instance.Height);

            for (int i = 0; i < posList.Count; i++)
            {
                StageTilesModel.Instance.PaintTile(posList[i] + posMin + offset, ColorName.enemyColor, true);
                await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: token);
            }
            await UniTask.Delay(TimeSpan.FromSeconds(summonByEnemyDelaySeconds), cancellationToken: token);
            StageTilesModel.Instance.CompletePaint(true);
        }

        private void InitializeDictUseFamiliarName()
        {
            signDictUseFamiliarName = new();

            for (int i = 0; i < signDataListGeneral.Count; i++)
                signDictUseFamiliarName = AddToDictUseFamiliarName(signDataListGeneral[i], signDictUseFamiliarName);
            for (int i = 0; i < signDataListUnique.Count; i++)
                signDictUseFamiliarName = AddToDictUseFamiliarName(signDataListUnique[i], signDictUseFamiliarName);
        }

        private static Dictionary<string, (List<Vector2> posList, int width, int height)> AddToDictUseFamiliarName(SignData signData, Dictionary<string, (List<Vector2> posList, int width, int height)> dictUseFamiliarName)
        {
            Dictionary<string, (List<Vector2> posList, int width, int height)> updatedSignDictionary = new(dictUseFamiliarName);

            string[] originalStrings = signData.SignShape.Split('\n');

            int height = originalStrings.Length;
            int width = originalStrings[0].Length;
            List<Vector2> paintPosList = new();
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    if (originalStrings[y][x] == '^')
                        paintPosList.Add(new Vector2(x, y));

            updatedSignDictionary[signData.FamiliarName] = (paintPosList, width, height);

            return updatedSignDictionary;
        }

        public void SummonAtRandom(Vector2 pos, ColorName colorNameInput, bool isByEnemy)
        {
            int r = UnityEngine.Random.Range(0, signDataListGeneral.Count);
            signDataListGeneral[r].Summon(pos, colorNameInput, isByEnemy);
        }
    }
}
