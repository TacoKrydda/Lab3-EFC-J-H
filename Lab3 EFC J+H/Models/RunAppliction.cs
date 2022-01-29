using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_EFC_J_H.Models
{
    public class RunAppliction
    {
        #region Menu
        ChangePlaylist change = new ChangePlaylist();
        SearchMethods search = new SearchMethods();

        public int textMenu()
        {
            Console.WriteLine("1. New playlist\n2. Add track to playlist\n3. Remove playlist\n" +
                "4. Remove track from playlist\n5. Search artists\n6. Search for playlist\n7. close program");
            bool isValid = int.TryParse(Console.ReadLine(), out int userInput);
            if (isValid)
            {
                return userInput;
            }
            else
            {
                return -1;
            }
        }
        public void menu()
        {
            var loop = true;
            while (loop)
            {
                Console.Clear();
                var userInput = textMenu();
                switch (userInput)
                {
                    case -1:
                        Console.WriteLine("Invalid data");
                        break;
                    case 1:
                        change.RunAddNewPlaylist();
                        break;
                    case 2:
                        change.RunAddTrackToPlaylist();
                        break;
                    case 3:
                        change.RunRemovePlaylist();
                        break;
                    case 4:
                        change.RunRemoveTrackFromPlaylist();
                        break;
                    case 5:
                        search.RunSearchArtists();
                        break;
                    case 6:
                        search.RunSearchPlaylist();
                        break;
                    case 7:
                        Environment.Exit(0);
                        break;
                    case 8:
                        break;
                    default:
                        break;
                }
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
        }
        public void returnToMenu()
        {
            menu();
        }
        #endregion
    }
}
