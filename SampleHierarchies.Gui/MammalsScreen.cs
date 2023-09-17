using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Mammals main screen.
/// </summary>
public sealed class MammalsScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Animals screen.
    /// </summary>
    private readonly DogsScreen _dogsScreen;
    private readonly ChimpanzeeScreen _chimpanzeeScreen;
    private readonly LionScreen _lionScreen;
    private readonly AfalinaScreen _afalinaScreen;
    private readonly SettingsService _settingsService;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    /// <param name="dogsScreen">Dogs screen</param>
    public MammalsScreen(DogsScreen dogsScreen, ChimpanzeeScreen chimpanzeeScreen, LionScreen lionScreen, AfalinaScreen afalinaScreen, SettingsService settingsService)
    {
        _settingsService = settingsService;
        _dogsScreen = dogsScreen;
        _chimpanzeeScreen = chimpanzeeScreen;
        _lionScreen = lionScreen;
        _afalinaScreen = afalinaScreen;
    }

    #endregion Properties And Ctor

    #region Public Methods

    /// <inheritdoc/>
    public override void Show()
    {
        while (true)
        {
            _settingsService.GetConsoleColor(Screens.MammalScreen);
            Console.WriteLine();
            Console.WriteLine("Your available choices are:");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Dogs");
            Console.WriteLine("2. Chimpanzees");
            Console.WriteLine("3. Lions");
            Console.WriteLine("4. Afalinas");
            Console.Write("Please enter your choice: ");

            string? choiceAsString = Console.ReadLine();

            // Validate choice
            try
            {
                if (choiceAsString is null)
                {
                    throw new ArgumentNullException(nameof(choiceAsString));
                }

                MammalsScreenChoices choice = (MammalsScreenChoices)Int32.Parse(choiceAsString);
                switch (choice)
                {
                    case MammalsScreenChoices.Dogs:
                        _dogsScreen.Show();
                        break;
                    case MammalsScreenChoices.Chimpanzee:
                        _chimpanzeeScreen.Show(); break;
                    case MammalsScreenChoices.Lion:
                        _lionScreen.Show(); break;
                    case MammalsScreenChoices.Afalina:
                        _afalinaScreen.Show(); break;
                    case MammalsScreenChoices.Exit:
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
}
