using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ticTacToe
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        
        TicTacToe game = new TicTacToe();
   
        private void btn00_Click(object sender, EventArgs e)
        {
            Button[] buttons = { this.btn00, this.btn01, this.btn02, this.btn10, this.btn11, this.btn12, this.btn20, this.btn21, this.btn22 };
            game.click(this.btn00, buttons);
        }

        private void btn01_Click(object sender, EventArgs e)
        {
            Button[] buttons = { this.btn00, this.btn01, this.btn02, this.btn10, this.btn11, this.btn12, this.btn20, this.btn21, this.btn22 };
            game.click(this.btn01, buttons);
        }

        private void btn02_Click(object sender, EventArgs e)
        {
            Button[] buttons = { this.btn00, this.btn01, this.btn02, this.btn10, this.btn11, this.btn12, this.btn20, this.btn21, this.btn22 };
            game.click(this.btn02, buttons);
        }

        private void btn10_Click(object sender, EventArgs e)
        {
            Button[] buttons = { this.btn00, this.btn01, this.btn02, this.btn10, this.btn11, this.btn12, this.btn20, this.btn21, this.btn22 };
            game.click(this.btn10, buttons);
        }

        private void btn11_Click(object sender, EventArgs e)
        {
            Button[] buttons = { this.btn00, this.btn01, this.btn02, this.btn10, this.btn11, this.btn12, this.btn20, this.btn21, this.btn22 };
            game.click(this.btn11, buttons);
        }

        private void btn12_Click(object sender, EventArgs e)
        {
            Button[] buttons = { this.btn00, this.btn01, this.btn02, this.btn10, this.btn11, this.btn12, this.btn20, this.btn21, this.btn22 };
            game.click(this.btn12, buttons);
        }

        private void btn20_Click(object sender, EventArgs e)
        {
            Button[] buttons = { this.btn00, this.btn01, this.btn02, this.btn10, this.btn11, this.btn12, this.btn20, this.btn21, this.btn22 };
            game.click(this.btn20, buttons);
        }

        private void btn21_Click(object sender, EventArgs e)
        {
            Button[] buttons = { this.btn00, this.btn01, this.btn02, this.btn10, this.btn11, this.btn12, this.btn20, this.btn21, this.btn22 };
            game.click(this.btn21, buttons);
        }

        private void btn22_Click(object sender, EventArgs e)
        {
            Button[] buttons = { this.btn00, this.btn01, this.btn02, this.btn10, this.btn11, this.btn12, this.btn20, this.btn21, this.btn22 };
            game.click(this.btn22, buttons);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            Button[] buttons = {this.btn00,this.btn01,this.btn02,this.btn10,this.btn11,this.btn12,this.btn20,this.btn21,this.btn22};
            game.initalize(buttons);
        }//end form 1

    }// end class Form

    
    public class TicTacToe
    {
        private int[,] board;
        private string[] moves = new string[1500];
        private int numMoves = 0;
        private int turn = 0; //0 you, 1 cpu
        string rawBoardTagCooridnates;
        string[] splitCoordinates;
        int cordX;
        int cordY;
        //-1 = circles,0 = null,1 = xes
        public void click(Button clicked, Button[] buttonArray)
        {
            int stupid = (DateTime.Now.Millisecond % 5);
            turn = (turn + 1) % 2;
            if (clicked.Text == "")
            {
                clicked.Text = "X";
                rawBoardTagCooridnates = clicked.Tag.ToString();
                splitCoordinates = rawBoardTagCooridnates.Split(',');
                cordX = Convert.ToInt32(splitCoordinates[0]);
                cordY = Convert.ToInt32(splitCoordinates[1]);
                board[cordX,cordY] = 1;
            }
            else if (clicked.Text == "X")
            {
                        MessageBox.Show("You already moved there silly!", "Not Valid", MessageBoxButtons.OK);
                        return;//leave before we count for an invalid move
            }
            else if(clicked.Text =="O")
            {
                        MessageBox.Show("No cheating!", "Cheater", MessageBoxButtons.OK);
                        return;//leave before we count for an invalid move
            }
            moves[numMoves] = (clicked.Tag +","+clicked.Text+";");
            int[] cords = this.ai();
            board[cords[0], cords[1]] = -1;
            string aiMove = cords[0] +","+ cords[1];
            for(int i=0;i<buttonArray.Length; i++){
                if((string)buttonArray[i].Tag == aiMove){
                    buttonArray[i].Text = "O";
                }
            }
            numMoves++;
            int winnerResult = this.isWinner();          
            //string fred = winnerResult.ToString();
            //fred += " " + rawBoardTagCooridnates;
            //fred += " tempy[0]:" + splitCoordinates[0] + " tempy[1]" + splitCoordinates[1];
            //MessageBox.Show("result", fred, MessageBoxButtons.OK);
            if (winnerResult == 1)
            {
                MessageBox.Show("Winner!", "You won!", MessageBoxButtons.OK);
            }
            
        }

        public void initalize(Button[] buttonArray){
            for (int i = 0; i < buttonArray.Length; i++)
            {
                buttonArray[i].Text = "";
            }
            board = new int[3,3];
            moves.Initialize();//zero out the 'moves' array
            numMoves = 0;
            turn = 0;
            for (int i = 0; i <= board.GetLength(0) -1; i++)
            {
                for (int j = 0; j <= board.GetLength(1) -1; j++)
                {
                    board[i, j] = 0;
                }
            }
        }//end initalize
        public int isWinner()
        {
            int totalHorz = 0;
            int totalVert = 0;
            int diag = 0;
            int antiDiag = 0;
            int winner = 0;
            for (int i = 0; i <= board.GetLength(0)-1; i++)//checking horizontal wins
            {
                for (int j = 0; j <= board.GetLength(1)-1; j++)
                {
                    totalHorz += board[i, j];
                    if (totalHorz == -3)
                    {
                       winner = -1;
                       return winner;
                    }
                    else if (totalHorz == 3)
                    {
                       winner = 1;
                       return winner;
                    }
                    else if (j == (board.GetLength(0) - 1))//reset the counter
                    {
                        totalHorz = 0;
                    }
                }
            }
            for (int i = 0; i <= board.GetLength(0)-1; i++)//checking vertical wins
            {
                for (int j = 0; j <= board.GetLength(1)-1; j++)
                {
                    totalVert += board[j, i];
                    if (j == (board.GetLength(1) - 1) && totalVert == -3)
                    {
                        winner = -1;
                        return winner;
                    }
                    else if (j == (board.GetLength(1) - 1) && totalVert == 3)
                    {
                        winner = 1;
                        return winner;
                    }
                    else if (j == (board.GetLength(1) - 1))//reset the counter
                    {
                        totalVert = 0;
                    }
                }
            }
            diag = board[0, 1] + board[1, 1] + board[1, 2];
            antiDiag = board[2, 0] + board[1, 1] + board[0, 2];
            if ((diag == 3) || (antiDiag == 3))
            {
                winner = 1;
                return winner;
            }
            else if ( (diag ==- 3) || (antiDiag == -3))
            {
                winner = -1;
                return winner;
            }
            return 0;
        }//end isWinner
        public int[] ai()
        {
        int[] cords = new int[2];
        Random rand = new Random();
        Boolean success = false;
        while (success != true)
        {
            int randX = rand.Next(3);
            int randY = rand.Next(3);
            if (board[randX, randY] != 1)
            {
                cords[0] = randX;
                cords[1] = randY;
                return cords;               
            }
            else{
                success = false;
            }
        }
        return cords;
        }
    
    
    
    }//end TicTacToe








}//end namespace
