namespace StringMath
{
    public interface IStringEquationFactory
    {
        IStringEquation CreateStringEquation(string stringEquation, params string[] parameterNames);
    }
}