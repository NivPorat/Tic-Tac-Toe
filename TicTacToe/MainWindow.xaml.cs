using System.Windows;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region private Members
        /// <summary>
        /// holds the current results of cells in the active game
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// true if it's player1 turn, false if it's player2 turn
        /// </summary>
        private bool mPlayerOneTurn;

        /// <summary>
        /// true if game has ended
        /// </summary>
        private bool mGameEned;

        #endregion


        #region constructor
        /// <summary>
        /// defult constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }
        #endregion

        /// <summary>
        /// starts a new game and clears all value to default settings
        /// </summary>
        private void NewGame()
        {
            //create a new blank array of freecells
            mResults = new MarkType[9];
            for (int i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            //player1 is the current player
            mPlayerOneTurn = true;
            //iterate every button on the grid using lambda expression
            //empty string, white bg, blue fg
            Container.Children.Cast<Button>().ToList().ForEach(Button =>
            {
                Button.Content = string.Empty;
                Button.Background = Brushes.White;
                Button.Foreground = Brushes.Blue;
            });
            //make sure the game hasnt finished
            mGameEned = false;

        }

        /// <summary>
        /// handles a button click event
        /// </summary>
        /// <param name="sender">the button that was clicked</param>
        /// <param name="e">the events of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {   //start a new game on the click after game is finished
            if(mGameEned)
            {
                NewGame();
                return;
            }
            //cast the sender to a button
            var button = (Button)sender;

            //find button position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            //because its the num of column in a 3X3 matrix
            var index = column + (row * 3);

            //current state of the index
            if (mResults[index] != MarkType.Free)
                return;//dont do anything if cell has a value in it

            //set cell value based on turn player updates the array
            mResults[index] = mPlayerOneTurn ? MarkType.Ex : MarkType.Circle;
            //if player one its X, else it's O
            //updates the visual content of the cel
            button.Content = mPlayerOneTurn ? "X" : "O";

            //changes the color shape of other player 
            if (!mPlayerOneTurn)
                button.Foreground = Brushes.Red;

            //makes the plahyer variable true/false using bitwise operations
            mPlayerOneTurn ^= true;

            //checks if theres a winner in the game
            CheckForAWinner();
        }

        private void CheckForAWinner()
        {
            #region horizontal wins
            //check for horizontal wins
            //
            //row 0
            // 
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                //end game
                mGameEned = true;
                //highlight winning cells
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.LightGreen;
            }
            //
            //row 1
            // 
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                //end game
                mGameEned = true;
                //highlight winning cells
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.LightGreen;
            }
            //
            //row 2
            // 
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                //end game
                mGameEned = true;
                //highlight winning cells
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.LightGreen;
            }
            //end check for horizontal wins//
            #endregion
            #region vertical wins
            //
            //check for vertical wins
            //
            //column 0
            // 
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                //end game
                mGameEned = true;
                //highlight winning cells
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.LightGreen;
            }
            //
            //column 1
            // 
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                //end game
                mGameEned = true;
                //highlight winning cells
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.LightGreen;
            }
            //
            //column 2
            // 
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {   //end game
                mGameEned = true;
                //highlight winning cells
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.LightGreen;
            }
            //
            //end of vertical checks
            #endregion
            #region diagonal wins
            //check for cross wins
            //
            //
            // \ side 0,4,8
            // 
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                //end game
                mGameEned = true;
                //highlight winning cells
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.LightGreen;
            }
            //
            // / side 2, 4, 6
            // 
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                //end game
                mGameEned = true;
                //highlight winning cells
                Button0_2.Background = Button1_1.Background = Button2_0.Background = Brushes.LightGreen;
            }
            #endregion
            #region no winnwers
            //two side lose
            if (!mResults.Any(f => f ==MarkType.Free))
            {
                //game ended
                mGameEned = true;
                Container.Children.Cast<Button>().ToList().ForEach(Button =>
                {
                    Button.Background = Brushes.Orange;
 
                });
                //turn every cell orange. 2 sides lost
                #endregion
            }
        }
    }
}
