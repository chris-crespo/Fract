using UI.Console;
using App;

var view = new Vista();
var calc = new Calculator();

var controller = new Controller(view, calc);

controller.Run();
