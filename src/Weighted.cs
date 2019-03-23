using System;
using System.Linq;

namespace AivoTree
{
    public class Weighted
    {
        public static int RandomIndexByWeight(int[] weights, Func<int, int, int> randomBetween)
        {
            var sumOfWeights = weights.Aggregate((i, acc) => i + acc);
            var picked = randomBetween(0, sumOfWeights - 1);
            var indexDistribution = weights.SelectMany((weight, index) => Enumerable.Repeat(index, weight)).ToArray();
            return indexDistribution[picked];
        }
    }
}