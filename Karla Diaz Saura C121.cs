namespace Weboo.Examen;

public static class Solution
{
    public static int Solve(int[,] map, int capacity, int origin)
    {
        bool[] visited = new bool[map.GetLength(0)];
        bool foundWay = false;
        int Cost = int.MaxValue;

        System.Console.WriteLine($"Current: {origin}");
        for (int i = 0; i < visited.Length; i++)
        {
            if (i == origin) continue;
            System.Console.WriteLine(i );
            System.Console.WriteLine(origin);
            System.Console.WriteLine(i == origin);
            int potentialCost = tryJourney(map, visited, capacity, capacity, origin, i, origin);
            if (potentialCost > -1)
            {
                foundWay = true;
                if (potentialCost < Cost) Cost = potentialCost;
            }
                System.Console.WriteLine($"Potential cost for ride on {i}: {potentialCost}");
            System.Console.WriteLine($"Origin iteration to {i} ended");
        }

        if (foundWay)
        {
            System.Console.WriteLine(Cost);
            return Cost;
        }
        else System.Console.WriteLine(-1);
        return -1;
    }
    
    static bool canRefill(int[,] map, int fuelLeft, int currentCity, int origin)
    {
        if (fuelLeft < map[currentCity, origin]) return false;
        return true;
    }

    static int tryJourney(int[,] map, bool[] visited, int capacity, int fuelLeft, int currentCity, int destinationCity, int origin, int min = 0)
    {
        System.Console.Write("    Visited so far:");
        for (int i = 0; i < visited.Length; i++)
        {
            if(visited[i]) System.Console.Write(i + ", ");
        } 
        System.Console.WriteLine();
            
        int aux = currentCity;
        min += map[currentCity, destinationCity];
        System.Console.WriteLine($"    Partial cost for moving to {currentCity} : {min}");
        int cost = min;
        fuelLeft -= map[currentCity, destinationCity];
            
        currentCity = destinationCity;
        
        if(currentCity != origin) visited[currentCity] = true;
        else fuelLeft = capacity;

        System.Console.WriteLine($"Moved to city {currentCity}");
        System.Console.WriteLine($"    FUel left: {fuelLeft}");

        if (!canRefill(map, fuelLeft, currentCity, origin))
        {
            System.Console.WriteLine("Can't refill, wrong way");
            visited[currentCity] = false;
            return -1;
        } 
        
        for (int i = 0; i < visited.Length; i++) // i es new destination
        {
            if (currentCity == origin && i == origin) continue;
            
            if (!visited[i])
            {
                int x = tryJourney(map, visited, capacity, fuelLeft, currentCity, i, origin, cost);
                if (x > -1)
                {
                    cost = x;
                }
            }
        }

        
        visited[currentCity] = false;
        
        System.Console.WriteLine($"Revisando en {currentCity}");
        System.Console.WriteLine($"Partial result with this journey: {cost}");
        System.Console.WriteLine($"Returning: {cost}");
        
        return cost;
    }
}