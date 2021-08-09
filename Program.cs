using System;
using System.Linq;
using System.Collections.Generic;

public enum ExploreMode { Room, Table}

class Program
{
    public static Action MoveUpOnTreeAction;
    public static Action<int> MoveDownOnTreeAction;
    public static Action<string> AddItemAction;
    public static Action<int> CollectCoinsAction;

    static void Main(string[] args)
    {
        PlayerInventory inventory = new PlayerInventory();
        GameConfig config = InitializeGameData();

        TreeNode currentNode = config.GetRootNode();

        AddItemAction += item =>
        {
            if (string.IsNullOrEmpty(item)) return;

            var target = currentNode.answers
                .Where(answer => answer is InventoryItemAsnwer)
                .OfType<InventoryItemAsnwer>()
                .First(answer => answer.itemName == item);

            if (target == null) return;
            
            currentNode.answers.Remove(target);

            inventory.AddItem(item);
            inventory.SeeInInventory();
        };

        CollectCoinsAction += numberOfCoins =>
        {
            if (numberOfCoins < 1) return;

            var target = currentNode.answers
                .Where(answer => answer is CollectCoinsAnswer)
                .OfType<CollectCoinsAnswer>()
                .First(answer => answer.numberOfCoins == numberOfCoins);

            if (target == null) return;

            currentNode.answers.Remove(target);

            inventory.AddCoins(numberOfCoins);
            inventory.SeeInInventory();
        };

        MoveDownOnTreeAction += childIndex =>
        {
            if (currentNode == null)
            {
                Console.WriteLine("Не назначен текущий нод");
                return;
            }
            if (currentNode.childrens == null)
            {
                Console.WriteLine("У текущего нода нет потомков");
                return;
            }
            if (childIndex >= currentNode.childrens.Count)
            {
                Console.WriteLine("Некорректный индекс");
                return;
            }

            currentNode = currentNode.childrens[childIndex];
        };

        MoveUpOnTreeAction += () =>
        {
            if (currentNode.parent == null)
            {
                Console.WriteLine("Вы находитесь на вершине дерева");
                return;
            }
            currentNode = currentNode.parent;
        };

        int answerIndex;
        string input;

        while (true)
        {
            Console.WriteLine(currentNode.TitleText);
            for (int i = 0; i < currentNode.answers.Count; i++)
                Console.WriteLine($"{i} - {currentNode.answers[i].mainText}");

            input = Console.ReadLine();
            if (int.TryParse(input, out answerIndex))
                if (answerIndex < currentNode.answers.Count)
                    currentNode.answers[answerIndex].InvokeAnswerAction();
        }
    }

    private static GameConfig InitializeGameData()
    {
        var room = new TreeNode(
            null,
            new List<TreeNode>(),
            "Вы в комнате\n",
            new List<Answer>()
            {
                new MoveDownOnTreeAnswer("Осмотреть стол\n", 0)
            });

        var table = new TreeNode(
            room,
            new List<TreeNode>(),
            "Вы осматриваете стол\n",
            new List<Answer>()
            {
                new MoveUpOnTreeAnswer("Вернуться к осмотру комнаты\n"),
                new InventoryItemAsnwer("Взять", "амулет\n"),
                new InventoryItemAsnwer("Взять", "книгу\n"),
                new CollectCoinsAnswer("Взять", 33)
            });

        room.AddChild(table);

        GameConfig config = new GameConfig(new List<TreeNode>() { room, table });

        return config;
    }
}
