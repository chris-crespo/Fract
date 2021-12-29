using App.Models;

namespace App;

using Op = Func<Fraction, Fraction, Fraction>;
using OpResult = ValueTuple<Fraction, char, Fraction, Fraction>;

public class Calculator
{
    List<OpResult> _history;

    public Op Add;
    public Op Sub;
    public Op Mul;
    public Op Div;

    public Calculator() 
    { 
        _history = new List<OpResult>();

        Add = arithOp((x, y) => x + y, '+');
        Sub = arithOp((x, y) => x - y, '-');
        Mul = arithOp((x, y) => x * y, '×');
        Div = arithOp((x, y) => x / y, '÷');
    }

    private Op arithOp(Op op, char symb) => (a, b) => 
    {
        var res = op(a, b);
        _history.Insert(0, (a, symb, b, res));
        return res;
    };

    public Fraction RetrieveOperationResult(int n)
    {
        // Si n es mayor que el numero de elementos en _history,
        // retornamos el último.
        var count = _history.Count;
        return _history[Math.Min(count - 1, n - 1)].Item4;
    }

    public List<OpResult> RetrieveLastOperations(int n) 
    {
        // Si n es mayor que el numero de elementos en _history,
        // retornamos todos los elementos.
        var count = _history.Count;
        return _history.GetRange(
            Math.Max(0, count - n - 1), 
            Math.Min(count, n));
    }

    public int OperationsCount() => _history.Count;
}
