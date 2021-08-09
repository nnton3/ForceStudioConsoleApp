using System;

public class Answer
{
    public string mainText { get; protected set; }

    public Answer(string text)
    {
        mainText = text;
    }

    public virtual void InvokeAnswerAction() { Console.WriteLine("answer invoked"); }
}

public class InventoryItemAsnwer : Answer
{
    public readonly string itemName;

    public InventoryItemAsnwer(string text, string itemName) : base(text)
    {
        mainText = $"{text} {itemName}";
        this.itemName = itemName;
    }

    public override void InvokeAnswerAction()
    {
        Program.AddItemAction.Invoke(itemName);
    }
}

public class CollectCoinsAnswer : Answer
{
    public int numberOfCoins { get; private set; } = 0;

    public CollectCoinsAnswer(string text, int numberOfCoins) : base(text)
    {
        if (numberOfCoins < 1)
        {
            Console.WriteLine($"Ошибка исходных данных, монет не может быть меньше 1");
            return;
        }

        mainText = $"{text} {numberOfCoins} монет(ы)";
        this.numberOfCoins = numberOfCoins;
    }

    public override void InvokeAnswerAction()
    {
        Program.CollectCoinsAction.Invoke(numberOfCoins);
    }
}

public class MoveUpOnTreeAnswer : Answer
{
    public MoveUpOnTreeAnswer(string text) : base(text)
    {
        mainText = text;
    }

    public override void InvokeAnswerAction() =>
        Program.MoveUpOnTreeAction.Invoke();
}

public class MoveDownOnTreeAnswer : Answer
{
    private int childIndex;

    public MoveDownOnTreeAnswer(string text, int childIndex) : base(text)
    {
        mainText = text;
        this.childIndex = childIndex;
    }

    public override void InvokeAnswerAction() =>
        Program.MoveDownOnTreeAction.Invoke(childIndex);
}
