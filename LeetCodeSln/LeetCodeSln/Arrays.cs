using System;

namespace LeetCodeSln;

internal class BuySellStock
{
    public static int MaxProfit(int[] prices)
    {
        // 1d DP.
        if (prices.Length <= 1) return 0;

        int minPrice = prices[0];
        int maxProfit = prices[1] - prices[0];

        for (int i = 2; i < prices.Length; i++)
        {
            int currPrice = prices[i];
            // 1. Is this price cheaper than any other I've seen?
            if (currPrice < minPrice)
            {
                minPrice = currPrice;
            } else
            {
                // If I subtract current price by cheapest is this new maxProfit?
                int netProfit = currPrice - minPrice;
                if (netProfit > maxProfit) maxProfit = netProfit;
            }
        }


        return maxProfit > 0 ? maxProfit : 0;
    }
}
