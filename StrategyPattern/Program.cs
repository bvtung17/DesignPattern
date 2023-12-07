// Interface định nghĩa các thuật toán
public interface IStrategy
{
    void Execute();
}

// Các thuật toán cụ thể sẽ implement interface IStrategy
public class ConcreteStrategyA : IStrategy
{
    public void Execute()
    {
        Console.WriteLine("Thực hiện thuật toán A");
    }
}

public class ConcreteStrategyB : IStrategy
{
    public void Execute()
    {
        Console.WriteLine("Thực hiện thuật toán B");
    }
}

// Context sử dụng một thuật toán thông qua interface IStrategy
public class Context
{
    private IStrategy strategy;

    // Thiết lập thuật toán ban đầu thông qua constructor hoặc phương thức setter
    public Context(IStrategy strategy)
    {
        this.strategy = strategy;
    }

    // Cho phép client thay đổi thuật toán tại runtime
    public void SetStrategy(IStrategy strategy)
    {
        this.strategy = strategy;
    }

    // Thực hiện thuật toán hiện tại
    public void ExecuteStrategy()
    {
        strategy.Execute();
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Tạo context với một thuật toán ban đầu
        Context context = new Context(new ConcreteStrategyA());

        // Thực hiện thuật toán A
        context.ExecuteStrategy();

        // Thay đổi thuật toán thành B và thực hiện thuật toán B
        context.SetStrategy(new ConcreteStrategyB());
        context.ExecuteStrategy();
    }
}
