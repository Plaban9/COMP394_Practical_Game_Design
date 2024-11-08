using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace Randomness
{
    public class NormalDistribution : MonoBehaviour
    {
        public enum NormalDistributionMethod
        {
            Normal01,
            Normal_With_Parameters
        }

        [SerializeField] private int sampleSize = 1000;
        [SerializeField] private float mean = 0;
        [SerializeField] private float stdDev = 1;
        [SerializeField] private NormalDistributionMethod _normalDistributionMethod;
        [SerializeField] private Button _normalButton;
        [SerializeField] private Text _normalText;

        private void Awake()
        {
            _normalButton.onClick.AddListener(ActionToPerform);
        }

        float[] data;

        private void ActionToPerform()
        {
            switch (_normalDistributionMethod)
            {
                case NormalDistributionMethod.Normal01:
                    Normal01(sampleSize);
                    break;
                case NormalDistributionMethod.Normal_With_Parameters:
                    Normal(mean, stdDev, sampleSize);
                    break;
            }
        }
        void Normal01(int Count)
        {
            Normal(0, 1, Count);
        }

        void Normal(float mu, float sigma, int Count)
        {

            float[] data = new float[sampleSize];

            for (int i = 0; i < sampleSize; i += 2)
            {
                // Generate two uniform random numbers
                var u1 = Random.Range(0f, 1f);
                var u2 = Random.Range(0f, 1f);

                // Apply Box-Muller transform
                float z0 = Mathf.Sqrt(-2.0f * Mathf.Log((float)u1)) * Mathf.Cos((float)(2.0f * Mathf.PI * u2));
                float z1 = Mathf.Sqrt(-2.0f * Mathf.Log((float)u1)) * Mathf.Sin((float)(2.0f * Mathf.PI * u2));

                // Scale and shift to match desired mean and standard deviation
                data[i] = z0 * stdDev + mean;
                if (i + 1 < sampleSize)
                {
                    data[i + 1] = z1 * stdDev + mean;
                }
            }

            Debug.Log(data.ToString());
            PrintToTextCSV(data);
        }

        private void PrintToTextCSV(float[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                _normalText.text += data[i] + ", ";
            }
        }
    }
}