using Microsoft.Extensions.DependencyInjection;

public enum Operator
{
    Add,
    Substract,
    Multiple,
    Divide
}

// Interface định nghĩa các thuật toán
public interface IStrategy
{
    int Calculate(int a, int b, Operator op);
}

public interface IMathOperator
{
    Operator Operator { get; }
    int Calculate(int a, int b);
}


// Các thuật toán cụ thể sẽ implement interface IStrategy
public class AddOperator : IMathOperator
{
    public Operator Operator => Operator.Add;

    public int Calculate(int a, int b) => a + b;
}


public class DivideOperator : IMathOperator
{
    public Operator Operator => Operator.Divide;

    public int Calculate(int a, int b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException();
        }
        return a / b;
    }
}
public class SubtractOperator : IMathOperator
{
    public Operator Operator => Operator.Substract;

    public int Calculate(int a, int b) => a - b;
}

public class MultipleOperator : IMathOperator
{
    public Operator Operator => Operator.Multiple;

    public int Calculate(int a, int b) => a * b;
}

// Service sử dụng một thuật toán thông qua interface IStrategy
public class MathStrategy : IStrategy
{
    private readonly IEnumerable<IMathOperator> _operators;

    public MathStrategy(IEnumerable<IMathOperator> operators) => _operators = operators;

    public int Calculate(int a, int b, Operator op)
        => _operators.FirstOrDefault(x => x.Operator == op)?.Calculate(a, b) ?? throw new ArgumentNullException(nameof(op));
}


class Program
{
    static void Main()
    {
        // Khởi tạo DI container
        var serviceProvider = new ServiceCollection()
            .AddScoped<IStrategy, MathStrategy>()
            .AddScoped<IMathOperator, AddOperator>()
            .AddScoped<IMathOperator, SubtractOperator>()
            .AddScoped<IMathOperator, MultipleOperator>()
            .AddScoped<IMathOperator, DivideOperator>()
            .BuildServiceProvider();

        // Lấy một đối tượng Service từ DI container
        var service = serviceProvider.GetRequiredService<IStrategy>();

        // Thực hiện thuật toán thông qua Service
        var value = service.Calculate(1, 2, Operator.Add);
        Console.WriteLine(value);
    }
}
