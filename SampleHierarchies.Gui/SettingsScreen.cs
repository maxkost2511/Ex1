using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using System;
using System.IO;
using System.Text.Json;

namespace SampleHierarchies.Gui
{
    /// <summary>
    /// Represents the settings screen of the application.
    /// </summary>
    public sealed class SettingsScreen : Screen
    {
        #region Fields

        private readonly ISettingsService _settingsService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsScreen"/> class.
        /// </summary>
        /// <param name="settingsService">The settings service to use.</param>
        public SettingsScreen(ISettingsService settingsService)
        {
            _settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
        }

        #endregion

        #region Public Methods

        /// <inheritdoc/>
        public override void Show()
        {
            while (true)
            {
                _settingsService.GetConsoleColor(Screens.SettingsScreen);

                Console.WriteLine("Available displays:");
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. Main Screen");
                Console.WriteLine("2. Animal Screen");
                Console.WriteLine("3. Mammal Screen");
                Console.WriteLine("4. Dog Screen");
                Console.WriteLine("5. Chimpanzee Screen");
                Console.WriteLine("6. Lion Screen");
                Console.WriteLine("7. Afalina Screen");
                Console.WriteLine("8. Settings Screen");
                Console.Write("Please enter your choice: ");

                string? choiceAsString = Console.ReadLine();

                // Validate user choice
                try
                {
                    if (choiceAsString is null) throw new ArgumentNullException();

                    int choice = int.Parse(choiceAsString);

                    if (choice >= 1 && choice <= 8)
                    {
                        GetConsoleColor((Screens)choice);
                    }
                    else if (choice == 0)
                    {
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Try again.");
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid choice. Try again.");
                }
            }
        }

        #endregion

        #region Private Methods

        /// <inheritdoc/>
        private void GetConsoleColor(Screens screen)
        {
            try
            {
                Console.Write("Enter a new color for the display: ");
                string? colorAsString = Console.ReadLine();

                if (colorAsString is null)
                {
                    throw new ArgumentNullException();
                }

                if (Enum.TryParse(colorAsString, out ConsoleColor newScreenColor))
                {
                    _settingsService.UpdateConsoleColor(screen, newScreenColor);
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again.");
                }
            }
            catch
            {
                Console.WriteLine("Invalid input. Try again.");
            }
        }

        #endregion
    }
}
