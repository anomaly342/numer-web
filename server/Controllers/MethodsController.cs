using Microsoft.AspNetCore.Mvc;
using server.Utilities;

[Route("methods")]
[ApiController]
public class MethodsController : ControllerBase
{
    private readonly ILogger<MethodsController> _logger;

    public MethodsController(ILogger<MethodsController> logger)
    {
        _logger = logger;
    }


    [HttpPost]
    [Route("bisection")]
    [Consumes("application/json")]
    public IActionResult Bisection(CalcRequest calcRequest)
    {
        var (findee, n, a, b, maxIteration) = calcRequest;
        double t = 0, ft = 0, _ft = 0;
        int iteration = 0;




        while (iteration <= maxIteration)
        {
            t = (a + b) / 2;
            ft = Math.Pow(t, n) - findee;
            if (Math.Abs(ft - _ft) < 0.000001)
            {
                break;
            }

            if (ft < 0)
            {
                a = t;
            }
            else
            {
                b = t;
            }

            _ft = ft;
            iteration++;
        }
        Result result = new Result(t);
        return Ok(result);
    }

    [HttpPost]
    [Route("falsi")]
    [Consumes("application/json")]
    public IActionResult Falsi(CalcRequest calcRequest)
    {

        var (findee, n, a, b, maxIteration) = calcRequest;

        double f(double x)
        {
            return Math.Pow(x, n) - findee;
        }

        if (f(a) * f(b) >= 0)
        {

            return BadRequest("Invalid values");
        }

        double _c = 0, c = 0, iteration = 0;

        while (iteration <= maxIteration)
        {
            c = (a * f(b) - b * f(a)) / (f(b) - f(a));

            if (Math.Abs(c - _c) < 0.000001)
            {
                break;
            }

            if (f(a) * f(c) < 0)
            {
                b = c;
            }
            else
            {
                a = c;
            }

            _c = c;
            iteration++;

        }

        Result result = new Result(c);
        return Ok(result);


    }

    [HttpPost]
    [Route("fixed")]
    [Consumes("application/json")]

    public IActionResult FixedPoint(CalcRequest calcRequest)
    {

        var (findee, n, a, b, maxIteration) = calcRequest;

        double g(double x)
        {
            return (Math.Pow(x, n) - findee + (10 * x)) / 10.0;
        }

        int iteration = 0;
        double x = 0, _x = 0;
        while (iteration <= maxIteration)
        {
            x = g(x);
            if (Math.Abs(x - _x) < 0.000001)
            {
                break;
            }
            _x = x;

            iteration++;
        }

        Result result = new Result(x);
        return Ok(result);


    }
}
