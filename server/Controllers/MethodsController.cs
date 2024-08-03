using Microsoft.AspNetCore.Mvc;
using server.Utilities;

[Route("methods")]
[ApiController]
[Consumes("application/json")]
public class MethodsController : ControllerBase
{
    private readonly ILogger<MethodsController> _logger;

    public MethodsController(ILogger<MethodsController> logger)
    {
        _logger = logger;
    }


    [HttpPost]
    [Route("bisection")]
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
                Result result = new Result(x);
                return Ok(result);
            }
            _x = x;

            iteration++;
        }

        return NotFound("Divergent function");




    }
    [HttpPost]
    [Route("newton")]
    public IActionResult Newton(CalcRequest calcRequest)
    {
        var (findee, n, a, b, maxIteration) = calcRequest;

        double f(double x)
        {
            return Math.Pow(x, n) - findee;
        }

        double f_derivate(double x)
        {
            return n * Math.Pow(x, n - 1);
        }
        double x = a, _x = 0;
        for (int i = 0; i < maxIteration; i++)
        {
            x = x - f(x) / f_derivate(x);
            if (Math.Abs(x - _x) < 0.000001)
            {
                Result result = new Result(x);
                return Ok(result);
            }

            _x = x;

        }

        return NotFound("Divergent function");
    }

    [HttpPost]
    [Route("secant")]
    public IActionResult Secant(CalcRequest calcRequest)
    {
        var (findee, n, a, b, maxIteration) = calcRequest;

        double f(double x)
        {
            return Math.Pow(x, n) - findee;
        }

        double _f(double x, double prevX)
        {
            return (f(x) - f(prevX)) / (x - prevX);
        }


        double front = b, back = a, temp;
        for (int i = 0; i < maxIteration; i++)
        {
            temp = front;
            front = front - f(front) / _f(front, back);
            back = temp;

            if (Math.Abs(front - back) < 0.000001)
            {
                Result result = new Result(front);
                return Ok(result);
            }
        }

        return NotFound("Divergent function");
    }
}
