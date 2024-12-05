using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RNGTests : MonoBehaviour
{
    [SerializeField]
    int rangeMin = 0;
    [SerializeField]
    int rangeMax = 100;
    [SerializeField]
    int iterations = 100000;

    // Start is called before the first frame update
    void Start()
    {
        RNG.SetSeed(2983);
        int[] results = new int[iterations];

        for (int i = 0; i < iterations; i++) 
            results[i] = RNG.Rand(rangeMin, rangeMax);

        RNG.SetSeed(2983);

        int[] results2 = new int[iterations];

        for (int i = 0; i < iterations; i++)
            results2[i] = RNG.Rand(rangeMin, rangeMax);

        bool equalResults = results.SequenceEqual(results2);

        Debug.Log("Algorithm is deterministic: " + equalResults);

        Dictionary<int, int> dict = new Dictionary<int, int>();

        for (int i = rangeMin; i < rangeMax; ++i)
            dict.Add(i, 0);

        for (int i = 0; i < results.Length; ++i)
        {
            dict[results[i]]++;
        }

        KeyValuePair<int, int> pairMin = new KeyValuePair<int, int>(0, results.Length);
        KeyValuePair<int, int> pairMax = new KeyValuePair<int, int>(0, 0);

        for (int i = rangeMin; i < rangeMax; ++i)
        {
            if (dict[i] < pairMin.Value)
                pairMin = new KeyValuePair<int, int>(i, dict[i]);
            else if (dict[i] > pairMax.Value)
                pairMax = new KeyValuePair<int, int>(i, dict[i]);
        }
        
        double avg = dict.Values.Average();

        Debug.Log(pairMin + ", " + pairMax + "\n" + avg + ", [" + dict.Keys.Min() + ", " + dict.Keys.Max() + "]");
    }

}
