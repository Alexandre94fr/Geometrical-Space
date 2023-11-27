using UnityEngine;

public class ShowConditionAttribute : PropertyAttribute
{
    private readonly string _condition;
    private readonly bool _disable;

    public string Condition => _condition;

    public bool Disable => _disable;

    public ShowConditionAttribute(string condition, bool disable = false)
    {
        _condition = condition;
        _disable = disable;
    }
}