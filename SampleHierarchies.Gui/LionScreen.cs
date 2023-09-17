using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Mammals main screen.
/// </summary>
public sealed class LionScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Data service.
    /// </summary>
    private IDataService _dataService;
    private readonly SettingsService _settingsService;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    public LionScreen(IDataService dataService, SettingsService settingsService)
    {
        _dataService = dataService;
        _settingsService = settingsService;
    }

    #endregion Properties And Ctor

    #region Public Methods

    /// <inheritdoc/>
    public override void Show()
    {
        while (true)
        {
            _settingsService.GetConsoleColor(Screens.LionScreen);
            Console.WriteLine();
            Console.WriteLine("Your available choices are:");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. List all Lion");
            Console.WriteLine("2. Create a new Lion");
            Console.WriteLine("3. Delete existing Lion");
            Console.WriteLine("4. Modify existing Lion");
            Console.Write("Please enter your choice: ");

            string? choiceAsString = Console.ReadLine();

            // Validate choice
            try
            {
                if (choiceAsString is null)
                {
                    throw new ArgumentNullException(nameof(choiceAsString));
                }

                LionScreenChoices choice = (LionScreenChoices)Int32.Parse(choiceAsString);
                switch (choice)
                {
                    case LionScreenChoices.List:
                        ListLion();
                        break;

                    case LionScreenChoices.Create:
                        AddLion(); break;

                    case LionScreenChoices.Delete:
                        DeleteLion();
                        break;

                    case LionScreenChoices.Modify:
                        EditLionMain();
                        break;

                    case LionScreenChoices.Exit:
                        Console.WriteLine("Going back to parent menu.");
                        return;
                }
            }
            catch
            {
                Console.WriteLine("Invalid choice. Try again.");
            }
        }
    }

    #endregion // Public Methods

    #region Private Methods

    /// <summary>
    /// List all Lions.
    /// </summary>
    private void ListLion()
    {
        Console.WriteLine();
        if (_dataService?.Animals?.Mammals?.Lion is not null &&
            _dataService.Animals.Mammals.Lion.Count > 0)
        {
            Console.WriteLine("Here's a list of Lion:");
            int i = 1;
            foreach (Lion lion in _dataService.Animals.Mammals.Lion)
            {
                Console.Write($"Lion number {i}, ");
                lion.Display();
                i++;
            }
        }
        else
        {
            Console.WriteLine("The list of Lion is empty.");
        }
    }

    /// <summary>
    /// Add a Lion.
    /// </summary>
    private void AddLion()
    {
        try
        {
            Lion lion = AddEditLion();
            _dataService?.Animals?.Mammals?.Lion?.Add(lion);
            Console.WriteLine($"Lion with name: {lion.Name} has been added to a list of Lions");
        }
        catch
        {
            Console.WriteLine("Invalid input.");
        }
    }

    /// <summary>
    /// Deletes a Lion.
    /// </summary>
    private void DeleteLion()
    {
        try
        {
            Console.Write("What is the name of the Lion you want to delete? ");
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Lion? lion = (Lion?)(_dataService?.Animals?.Mammals?.Lion
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (lion is not null)
            {
                _dataService?.Animals?.Mammals?.Lion?.Remove(lion);
                Console.WriteLine($"Lion with name: {lion.Name} has been deleted from a list of Lions");
            }
            else
            {
                Console.WriteLine("Lion not found.");
            }
        }
        catch
        {
            Console.WriteLine("Invalid input.");
        }
    }

    /// <summary>
    /// Edits an existing Lion after choice made.
    /// </summary>
    private void EditLionMain()
    {
        try
        {
            Console.Write("What is the name of the Lion you want to edit? ");
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Lion? lion = (Lion?)(_dataService?.Animals?.Mammals?.Lion
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (lion is not null)
            {
                Lion LionEdited = AddEditLion();
                lion.Copy(LionEdited);
                Console.Write("Lion after edit:");
                lion.Display();
            }
            else
            {
                Console.WriteLine("Lion not found.");
            }
        }
        catch
        {
            Console.WriteLine("Invalid input. Try again.");
        }
    }

    /// <summary>
    /// Adds/edit specific Lion.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    private Lion AddEditLion()
    {
        Console.Write("What name of the Lion? ");
        string? name = Console.ReadLine();
        Console.Write("What is the Lion's age? ");
        string? ageAsString = Console.ReadLine();
        Console.Write("Does top predator? (yes/no) ");
        string? predatorAsString = Console.ReadLine();
        Console.Write("Is pack hunter? (yes/no) ");
        string? packHunterAsString = Console.ReadLine();
        Console.Write("What mane does it have?");
        string? mane = Console.ReadLine();
        Console.Write("Is it roaring? (yes/no) ");
        string? roaringAsString = Console.ReadLine();
        Console.Write("Does it protect the territory? (yes/no) ");
        string? territoryDefenseAsString = Console.ReadLine();


        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        if (ageAsString is null)
        {
            throw new ArgumentNullException(nameof(ageAsString));
        }
        if (predatorAsString is null)
        {
            throw new ArgumentNullException(nameof(predatorAsString));
        }
        if (packHunterAsString is null)
        {
            throw new ArgumentNullException(nameof(packHunterAsString));
        }
        if (mane is null)
        {
            throw new ArgumentNullException(nameof(mane));
        }
        if (roaringAsString is null)
        {
            throw new ArgumentNullException(nameof(roaringAsString));
        }
        if (territoryDefenseAsString is null)
        {
            throw new ArgumentNullException(nameof(territoryDefenseAsString));
        }
        int age = Int32.Parse(ageAsString);
        bool predator = predatorAsString.ToLower() == "yes";
        bool packHunter = packHunterAsString.ToLower() == "yes";
        bool roaring = roaringAsString.ToLower() == "yes";
        bool territoryDefense = territoryDefenseAsString.ToLower() == "yes";
        Lion lion = new Lion(name, age, predator, packHunter, mane, roaring, territoryDefense);

        return lion;
    }

    #endregion // Private Methods
}
