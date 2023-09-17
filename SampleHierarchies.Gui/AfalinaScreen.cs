using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Mammals main screen.
/// </summary>
public sealed class AfalinaScreen : Screen
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
    public AfalinaScreen(IDataService dataService, SettingsService settingsService)
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
            _settingsService.GetConsoleColor(Screens.AfalinaScreen);
            Console.WriteLine();
            Console.WriteLine("Your available choices are:");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. List all Afalina");
            Console.WriteLine("2. Create a new Afalina");
            Console.WriteLine("3. Delete existing Afalina");
            Console.WriteLine("4. Modify existing Afalina");
            Console.Write("Please enter your choice: ");

            string? choiceAsString = Console.ReadLine();

            // Validate choice
            try
            {
                if (choiceAsString is null)
                {
                    throw new ArgumentNullException(nameof(choiceAsString));
                }

                AfalinaScreenChoices choice = (AfalinaScreenChoices)Int32.Parse(choiceAsString);
                switch (choice)
                {
                    case AfalinaScreenChoices.List:
                        ListAfalina();
                        break;

                    case AfalinaScreenChoices.Create:
                        AddAfalina(); break;

                    case AfalinaScreenChoices.Delete:
                        DeleteAfalina();
                        break;

                    case AfalinaScreenChoices.Modify:
                        EditAfalinaMain();
                        break;

                    case AfalinaScreenChoices.Exit:
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
    /// List all Afalinas.
    /// </summary>
    private void ListAfalina()
    {
        Console.WriteLine();
        if (_dataService?.Animals?.Mammals?.Afalina is not null &&
            _dataService.Animals.Mammals.Afalina.Count > 0)
        {
            Console.WriteLine("Here's a list of Afalina:");
            int i = 1;
            foreach (Afalina afalina in _dataService.Animals.Mammals.Afalina)
            {
                Console.Write($"Afalina number {i}, ");
                afalina.Display();
                i++;
            }
        }
        else
        {
            Console.WriteLine("The list of Afalina is empty.");
        }
    }

    /// <summary>
    /// Add a Afalina.
    /// </summary>
    private void AddAfalina()
    {
        try
        {
            Afalina afalina = AddEditAfalina();
            _dataService?.Animals?.Mammals?.Afalina?.Add(afalina);
            Console.WriteLine($"Afalina with name: {afalina.Name} has been added to a list of Afalina");
        }
        catch
        {
            Console.WriteLine("Invalid input.");
        }
    }

    /// <summary>
    /// Deletes a Afalina.
    /// </summary>
    private void DeleteAfalina()
    {
        try
        {
            Console.Write("What is the name of the Afalina you want to delete? ");
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Afalina? afalina = (Afalina?)(_dataService?.Animals?.Mammals?.Afalina
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (afalina is not null)
            {
                _dataService?.Animals?.Mammals?.Afalina?.Remove(afalina);
                Console.WriteLine($"Afalina with name: {afalina.Name}  has been added to a list of Afalina");
            }
            else
            {
                Console.WriteLine("Afalina not found.");
            }
        }
        catch
        {
            Console.WriteLine("Invalid input.");
        }
    }

    /// <summary>
    /// Edits an existing Afalina after choice made.
    /// </summary>
    private void EditAfalinaMain()
    {
        try
        {
            Console.Write("What is the name of the Afalina you want to edit? ");
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Afalina? afalina = (Afalina?)(_dataService?.Animals?.Mammals?.Afalina
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (afalina is not null)
            {
                Afalina AfalinaEdited = AddEditAfalina();
                afalina.Copy(AfalinaEdited);
                Console.Write("Afalina after edit:");
                afalina.Display();
            }
            else
            {
                Console.WriteLine("Afalina not found.");
            }
        }
        catch
        {
            Console.WriteLine("Invalid input. Try again.");
        }
    }

    /// <summary>
    /// Adds/edit specific Afalina.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    private Afalina AddEditAfalina()
    {
        Console.Write("What name of the Afalina? ");
        string? name = Console.ReadLine();
        Console.Write("What is the Afalina's age? ");
        string? ageAsString = Console.ReadLine();
        Console.Write("Does it possess echolocation? (yes/no) ");
        string? echolocationAsString = Console.ReadLine();
        Console.Write("What is the social behavior of this one? ");
        string? socialBehavior = Console.ReadLine();
        Console.Write("Does it have a playful behavior? (yes/no) ");
        string? playfulBehaviorAsString = Console.ReadLine();
        Console.Write("What brain size?(int) ");
        string? brainSizeAsString = Console.ReadLine();
        Console.Write("Does it know how to swim at high speeds? (yes/no) ");
        string? highSpeedSwimmingAsString = Console.ReadLine();


        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        if (ageAsString is null)
        {
            throw new ArgumentNullException(nameof(ageAsString));
        }
        if (echolocationAsString is null)
        {
            throw new ArgumentNullException(nameof(echolocationAsString));
        }
        if (socialBehavior is null)
        {
            throw new ArgumentNullException(nameof(socialBehavior));
        }
        if (playfulBehaviorAsString is null)
        {
            throw new ArgumentNullException(nameof(playfulBehaviorAsString));
        }
        if (brainSizeAsString is null)
        {
            throw new ArgumentNullException(nameof(brainSizeAsString));
        }
        if (highSpeedSwimmingAsString is null)
        {
            throw new ArgumentNullException(nameof(highSpeedSwimmingAsString));
        }
        int age = Int32.Parse(ageAsString);
        bool echolocation = echolocationAsString.ToLower() == "yes";
        bool playfulBehavior = playfulBehaviorAsString.ToLower() == "yes";
        int brainSize = Int32.Parse(brainSizeAsString);
        bool highSpeedSwimming = highSpeedSwimmingAsString.ToLower() == "yes";
        Afalina afalina = new Afalina(name, age, echolocation, socialBehavior, playfulBehavior, brainSize, highSpeedSwimming);

        return afalina;

    }

    #endregion // Private Methods
}
