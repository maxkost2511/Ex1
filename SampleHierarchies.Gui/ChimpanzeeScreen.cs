using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Mammals main screen.
/// </summary>
public sealed class ChimpanzeeScreen : Screen
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
    public ChimpanzeeScreen(IDataService dataService, SettingsService settingsService)
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
            _settingsService.GetConsoleColor(Screens.ChimpanzeeScreen);
            Console.WriteLine();
            Console.WriteLine("Your available choices are:");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. List all Chimpanzee");
            Console.WriteLine("2. Create a new Chimpanzee");
            Console.WriteLine("3. Delete existing Chimpanzee");
            Console.WriteLine("4. Modify existing Chimpanzee");
            Console.Write("Please enter your choice: ");

            string? choiceAsString = Console.ReadLine();

            // Validate choice
            try
            {
                if (choiceAsString is null)
                {
                    throw new ArgumentNullException(nameof(choiceAsString));
                }

                ChimpanzeeScreenChoices choice = (ChimpanzeeScreenChoices)Int32.Parse(choiceAsString);
                switch (choice)
                {
                    case ChimpanzeeScreenChoices.List:
                        ListChimpanzee();
                        break;

                    case ChimpanzeeScreenChoices.Create:
                        AddChimpanzee(); break;

                    case ChimpanzeeScreenChoices.Delete: 
                        DeleteChimpanzee();
                        break;

                    case ChimpanzeeScreenChoices.Modify:
                        EditChimpanzeeMain();
                        break;

                    case ChimpanzeeScreenChoices.Exit:
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
    /// List all chimpanzee.
    /// </summary>
    private void ListChimpanzee()
    {
        Console.WriteLine();
        if (_dataService?.Animals?.Mammals?.Chimpanzee is not null &&
            _dataService.Animals.Mammals.Chimpanzee.Count > 0)
        {
            Console.WriteLine("Here's a list of chimpanzee:");
            int i = 1;
            foreach (Chimpanzee chimpanzee in _dataService.Animals.Mammals.Chimpanzee)
            {
                Console.Write($"Chimpanzee number {i}, ");
                chimpanzee.Display();
                i++;
            }
        }
        else
        {
            Console.WriteLine("The list of chimpanzee is empty.");
        }
    }

    /// <summary>
    /// Add a Chimpanzee.
    /// </summary>
    private void AddChimpanzee()
    {
        try
        {
            Chimpanzee сhimpanzee = AddEditChimpanzee();
            _dataService?.Animals?.Mammals?.Chimpanzee?.Add(сhimpanzee);
            Console.WriteLine($"Chimpanzee with name: {сhimpanzee.Name} has been added to a list of chimpanzees");
        }
        catch
        {
            Console.WriteLine("Invalid input.");
        }
    }

    /// <summary>
    /// Deletes a Chimpanzee.
    /// </summary>
    private void DeleteChimpanzee()
    {
        try
        {
            Console.Write("What is the name of the Chimpanzee you want to delete? ");
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Chimpanzee? сhimpanzee = (Chimpanzee?)(_dataService?.Animals?.Mammals?.Chimpanzee
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (сhimpanzee is not null)
            {
                _dataService?.Animals?.Mammals?.Chimpanzee?.Remove(сhimpanzee);
                Console.WriteLine($"Chimpanzee with name: {сhimpanzee.Name} has been deleted from a list of Chimpanzees");
            }
            else
            {
                Console.WriteLine("Chimpanzee not found.");
            }
        }
        catch
        {
            Console.WriteLine("Invalid input.");
        }
    }

    /// <summary>
    /// Edits an existing Chimpanzee after choice made.
    /// </summary>
    private void EditChimpanzeeMain()
    {
        try
        {
            Console.Write("What is the name of the chimpanzee you want to edit? ");
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Chimpanzee? chimpanzee = (Chimpanzee?)(_dataService?.Animals?.Mammals?.Chimpanzee
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (chimpanzee is not null)
            {
                Chimpanzee ChimpanzeeEdited = AddEditChimpanzee();
                chimpanzee.Copy(ChimpanzeeEdited);
                Console.Write("Chimpanzee after edit:");
                chimpanzee.Display();
            }
            else
            {
                Console.WriteLine("Chimpanzee not found.");
            }
        }
        catch
        {
            Console.WriteLine("Invalid input. Try again.");
        }
    }

    /// <summary>
    /// Adds/edit specific Chimpanzee.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    private Chimpanzee AddEditChimpanzee()
    {
        Console.Write("What name of the chimpanzee? ");
        string? name = Console.ReadLine();
        Console.Write("What is the chimpanzee's age? ");
        string? ageAsString = Console.ReadLine();
        Console.Write("Are the thumbs opposable in Chimpanzees? (yes/no) ");
        string? thumbsAsString = Console.ReadLine();
        Console.Write("What is the chimpanzee's social behavior? ");
        string? behavior = Console.ReadLine();
        Console.Write("Does it use tools? (yes/no) ");
        string? toolsAsString = Console.ReadLine();
        Console.Write("What is the chimpanzee's inteligence?(int) ");
        string? inteligenceAsString = Console.ReadLine();
        Console.Write("What is the chimpanzee's diet? ");
        string? diet = Console.ReadLine();


        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        if (ageAsString is null)
        {
            throw new ArgumentNullException(nameof(ageAsString));
        }
        if (thumbsAsString is null)
        {
            throw new ArgumentNullException(nameof(thumbsAsString));
        }
        if (behavior is null)
        {
            throw new ArgumentNullException(nameof(behavior));
        }
        if (toolsAsString is null)
        {
            throw new ArgumentNullException(nameof(toolsAsString));
        }
        if (inteligenceAsString is null)
        {
            throw new ArgumentNullException(nameof(inteligenceAsString));
        }
        if (diet is null)
        {
            throw new ArgumentNullException(nameof(diet));
        }
        int age = Int32.Parse(ageAsString);
        bool thumbs = thumbsAsString.ToLower() == "yes";
        bool tools = thumbsAsString.ToLower() == "yes";
        int inteligence = Int32.Parse(inteligenceAsString);
        Chimpanzee chimpanzee = new Chimpanzee(name, age, thumbs, behavior, tools, inteligence, diet);

        return chimpanzee;
    }

    #endregion // Private Methods
}
