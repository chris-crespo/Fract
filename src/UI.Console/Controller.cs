using App;
using App.Models;

namespace UI.Console;

using Op = Func<Fraction, Fraction, Fraction>;

public class Controller 
{
    private Vista _view;
    private Calculator _calc;

    private Dictionary<string, Action> _useCases;
    public Controller(Vista view, Calculator calc)
    {
        _view = view;
        _calc = calc;
        _useCases = new Dictionary<string, Action>() {
            { "Sumar",       performOp(_calc.Add) },
            { "Restar",      performOp(_calc.Sub) },
            { "Multiplicar", performOp(_calc.Mul) },
            { "Dividir",     performOp(_calc.Div) },
            { "Mostrar operaciones realizadas", showOperations }
        };
    }

    public void Run() 
    {
        _view.LimpiarPantalla();

        while (true)
        {
            try 
            {
                var menu = _useCases.Keys.ToList<String>();
                var option = _view.TryObtenerElementoDeLista(
                    "Menu", menu, "Selecciona una opción");
                
                invokeUseCase(option);
                _view.Mostrar("");

                _view.MostrarYReturn(
                    "Pulsa <Return> para continuar", 
                    ConsoleColor.DarkGray);
                _view.LimpiarPantalla();
            }
            catch
            {
                return;
            }
        }
    }

    private void invokeUseCase(string option)
    {
        _useCases
            .FirstOrDefault(k => k.Key == option)
            .Value
            .Invoke();
    }        

    private Fraction getFractionFromUserInput(ValueTuple<Fraction?, int> tuple)
    {
        var (fraction, index) = tuple;
        if (fraction is Fraction f)
            return f;

        return _calc.RetrieveOperationResult(index);
    }

    private Action performOp(Op op) => () =>
    {
        var count = _calc.OperationsCount();
        var f1 = getFractionFromUserInput(
            _view.TryGetUserInput("Introduce la primera fracción", count));
        var f2 = getFractionFromUserInput(
            _view.TryGetUserInput("Introduce la segunda fracción", count));

        if (!f1.IsProper() && !f2.IsProper())
            _view.Mostrar(
                "Al menos una de las fracciones debe ser propia", 
                ConsoleColor.DarkRed);
        else 
            try
            {
                _view.Mostrar($"Resultado: {op(f1, f2)}");
            }
            catch
            {
                _view.Mostrar("No se pudo realizar la operacion", ConsoleColor.DarkRed);
            }
    };

    private void showOperations()
    {
        var count = _calc.OperationsCount();
        if (count == 0) 
        {
            _view.Mostrar("No se ha realizado ninguna operación");
            return;
        }

        _view.Mostrar($"Se han realizado {count} operaciones");

        var n = _view.TryObtenerValorEnRangoInt(
                0,
                count,
                "Introduce el número de operaciones a mostrar");
        var operations = _calc.RetrieveLastOperations(n);
        operations.ForEach(operation => {
            var (x, symb, y, res) = operation;
            _view.Mostrar($"{x} {symb} {y} = {res}");
        });
    }
}
