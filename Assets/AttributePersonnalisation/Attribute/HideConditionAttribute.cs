using UnityEngine;

public class HideConditionAttribute : PropertyAttribute
{
    private readonly string _condition;
    private readonly bool _disable;

    public string Condition => _condition;

    public bool Disable => _disable;

    public HideConditionAttribute(string condition, bool disable = false)
    {
        _condition = condition;
        _disable = disable;
    }
}