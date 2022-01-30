using System;

[Serializable]
public struct RangedFloat
{
    public float minValue;
    public float maxValue;

    public RangedFloat(float min, float max) { minValue = min; maxValue = max; }

    public float GetRandom()
    {
        return UnityEngine.Random.Range(minValue, maxValue);
    }
    public float GetRange()
    {
        return maxValue - minValue;
    }
}

[Serializable]
public struct RangedInt
{
    public int min;
    public int max;

    public RangedInt(int min, int max)
    {
        this.min = min;
        this.max = max;
    }

    public int GetRandom()
    {
        //Random isn't inclusive in int so we need to add 1 to make it inclusive
        return UnityEngine.Random.Range(min, max + 1);
    }

    public float GetRange()
    {
        return max - min;
    }
}