using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPUSimulator
{
    //0 1 2 3 4 5 6 7 8
    //" a b c d e f g h
    /*
     * 
     * non 0000 	|non 0000 |
     * mov 0001 	|a	 0001 |
     * add 0010 	|b   0010 |
     * sub 0011 	|c   0011 |
     * mul 0100 	|d   0100 |
     * div 0101		|e   0101 |
     * cop 0110		|f   0110 |
     * swi 0111		|g   0111 |
     * del 1111		|h   1000 |
     * 
     * mov a b 0001
     * mov a 5 0010
     * del a   0011
     * 
     * 1111 1111      1111 1111    1111 1111 1111 1111 
     * ^method        ^r1  ^r1  
     */
    public partial class Form1 : Form
    {
        String outAsBinary = "";
        String outAsBinaryNum = "";
        Boolean check = true;
        int[] reg = new int[10];
        public Form1()
        {
            InitializeComponent();
        }


        public void CheckIn(String input)
        {
            String[] s = input.Split(' ');

		//Mov A 3
		//Add A 3
		//Mov B A
		//Mul B -1
		//Del A

            if ( s.Length == 3 )
            {
                String method = s[0].ToLower();
                String Regis1 = s[1].ToLower();
                String Regis2;
                int number = 0;

                if ( int.TryParse( s[2], out number )) //Add A 3
                {
                    outAsBinary = "0010";
                    MethodForInt( method, Regis1, number );
                    NumToBi(number);
                }
                else // Mov A B
                {
                    outAsBinary = "0001";
                    Regis2 = s[2].ToLower();
                    MethodForReg( method, Regis1, Regis2 );
                    NumToBi(0);
                }
            }
            else if ( s.Length == 2 ) // Del A
            {
                String method = s[0].ToLower();
                String Regis1 = s[1].ToLower();

                if( method.Equals( "del" )){
                    outAsBinary = "0011";
                    MethodForFunc( method, Regis1 );
                }
            }
        }

        public void outputAsBinary(String s)
        {
            outAsBinary +=  " " + s;
        }
        public int RegisToBi(String s)
        {
            if (s.Equals("a"))
            {
                return 1;
            }
            else if (s.Equals("b"))
            {
                return 2;
            }
            else if (s.Equals("c"))
            {
                return 3;
            }
            else if (s.Equals("d"))
            {
                return 4;
            }
            else if (s.Equals("e"))
            {
                return 5;
            }
            else if (s.Equals("f"))
            {
                return 6;
            }
            else if (s.Equals("g"))
            {
                return 7;
            }
            else if (s.Equals("h"))
            {
                return 8;
            }
            else
            {
                check = false;
                return 0;
            }

        }
        public String RegisToBiShow(String s)
        {
            if (s.Equals("a"))
            {
                return "0001";
            }
            else if (s.Equals("b"))
            {
                return "0010";
            }
            else if (s.Equals("c"))
            {
                return "0011";
            }
            else if (s.Equals("d"))
            {
                return "0100";
            }
            else if (s.Equals("e"))
            {
                return "0101";
            }
            else if (s.Equals("f"))
            {
                return "0110";
            }
            else if (s.Equals("g"))
            {
                return "0111";
            }
            else if (s.Equals("h"))
            {
                return "1000";
            }
            else
            {
                check = false;
                return "0000";
            }
        }
        public void MethodForInt( String method, String Regis1, int number )
        {   // Add A 2
            int NumOfReg = RegisToBi(Regis1);

            if (method.Equals("mov"))
            {
                outputAsBinary("0001");
                move(NumOfReg, number);
                outputAsBinary(RegisToBiShow(Regis1));
                outputAsBinary("0000");
            }
            else if (method.Equals("add"))
            {
                outputAsBinary("0010");
                add(NumOfReg, number);
                outputAsBinary(RegisToBiShow(Regis1));
                outputAsBinary("0000");
            }
            else if (method.Equals("sub"))
            {
                outputAsBinary("0011");
                sub(NumOfReg, number);
                outputAsBinary(RegisToBiShow(Regis1));
                outputAsBinary("0000");
            }
            else if (method.Equals("mul"))
            {
                outputAsBinary("0100");
                mul(NumOfReg, number);
                outputAsBinary(RegisToBiShow(Regis1));
                outputAsBinary("0000");
            }
            else if (method.Equals("div"))
            {
                outputAsBinary("0101");
                div(NumOfReg, number);
                outputAsBinary(RegisToBiShow(Regis1));
                outputAsBinary("0000");
            }
            else
            {
                check = false;
            }
        }
        public void MethodForReg(String method, String Regis1, String Regis2)
        { // mov b a
            int NumRegis1 = RegisToBi(Regis1);
            int NumRegis2 = RegisToBi(Regis2);

            if (method.Equals("mov"))
            {
                outputAsBinary("0001");
                moveReg(NumRegis1, NumRegis2);
                outputAsBinary(RegisToBiShow(Regis1));
                outputAsBinary(RegisToBiShow(Regis2));
            }
            else if (method.Equals("add"))
            {
                outputAsBinary("0010");
                addReg(NumRegis1, NumRegis2);
                outputAsBinary(RegisToBiShow(Regis1));
                outputAsBinary(RegisToBiShow(Regis2));
            }
            else if (method.Equals("sub"))
            {
                outputAsBinary("0011");
                subReg(NumRegis1, NumRegis2);
                outputAsBinary(RegisToBiShow(Regis1));
                outputAsBinary(RegisToBiShow(Regis2));
            }
            else if (method.Equals("mul"))
            {
                outputAsBinary("0100");
                mulReg(NumRegis1, NumRegis2);
                outputAsBinary(RegisToBiShow(Regis1));
                outputAsBinary(RegisToBiShow(Regis2));
            }
            else if (method.Equals("div"))
            {
                outputAsBinary("0101");
                divReg(NumRegis1, NumRegis2);
                outputAsBinary(RegisToBiShow(Regis1));
                outputAsBinary(RegisToBiShow(Regis2));
            }
            else if (method.Equals("cop"))
            {
                outputAsBinary("0110");
                copyReg(NumRegis1, NumRegis2);
                outputAsBinary(RegisToBiShow(Regis1));
                outputAsBinary(RegisToBiShow(Regis2));
            }
            else if (method.Equals("swi"))
            {
                outputAsBinary("0111");
                swi(NumRegis1, NumRegis2);
                outputAsBinary(RegisToBiShow(Regis1));
                outputAsBinary(RegisToBiShow(Regis2));
            }
            else
            {
                check = false;
            }
        }
        public void MethodForFunc(String method, String Regis1)
        { //del a
            int NumRegis = RegisToBi(Regis1);

            if (method.Equals("del"))
            {
                outAsBinary = "0011";
                outputAsBinary("1111");
                outputAsBinary(RegisToBiShow(Regis1));
                outputAsBinary("0000");
                NumToBi(0);
                del(NumRegis);
            }
            else
            {
                check = false;
            }
        }
        public void NumToBi(int number)
        {
            String temp = number.ToString();
            String n = Convert.ToString(Convert.ToInt32(temp, 10), 2);


            for (int i = n.Length; i < 16; i++)
            {

                n = "0" + n;
            }

            outAsBinaryNum = n;
            
          
            
        }
        public void run()
        {
            CheckIn(InBox1.Text);
            RegUpdate();
            if (check) result();
        }
        public void ResetInt()
        {
            outAsBinary = "";
            outAsBinaryNum = "";
        }
        public void ResetReg()
        {
            for (int i = 0; i < reg.Length; i++)
            {
                reg[i] = 0;
            }
            OutBox1.Text = "";
            RegUpdate();
        }
        public void result()
        {
            String StrOld = OutBox1.Text;
            String StrNew = InBox1.Text + " :\n" + outAsBinary + " " + outAsBinaryNum;
            OutBox1.Text = StrOld + "\n" + StrNew;

        }
        public void RegUpdate()
        {
            Reigs1.Text = reg[1].ToString();
            Reigs2.Text = reg[2].ToString();
            Reigs3.Text = reg[3].ToString();
            Reigs4.Text = reg[4].ToString();
            Reigs5.Text = reg[5].ToString();
            Reigs6.Text = reg[6].ToString();
            Reigs7.Text = reg[7].ToString();
            Reigs8.Text = reg[8].ToString();
        }
        public void move(int r1, int number)
        {
            reg[r1] = number;
        }
        public void moveReg(int r1, int r2)
        {
            int tempReg = reg[r2];
            reg[r1] = tempReg;
            reg[r2] = 0;
        }
        //***************************
        public void add(int r1, int number)
        {
            //add a 5
            reg[r1] = reg[r1] + number;
        }
        public void addReg(int r1, int r2)
        {
            //add b a
            reg[r1] = reg[r1] + reg[r2];
        }
        //***************************
        public void sub(int r1, int number)
        {
            //sub a 5
            reg[r1] = reg[r1] - number;
        }
        public void subReg(int r1, int r2)
        {
            //sub b a
            reg[r1] = reg[r1] - reg[r2];
        }
        //***************************
        public void mul(int r1, int number)
        {
            //mul a 5
            reg[r1] = reg[r1] * number;
        }
        public void mulReg(int r1, int r2)
        {
            //mul b a
            reg[r1] = reg[r1] * reg[r2];
        }
        //***************************
        public void div(int r1, int number)
        {
            //div a 2

            reg[r1] = reg[r1] / number;
        }
        public void divReg(int r1, int r2)
        {
            //div b a

            reg[r1] = reg[r1] / reg[r2];
        }
        //***************************
        public void copyReg(int r1, int r2)
        {
            //cop b a
            int tempReg = reg[r2];
            reg[r1] = tempReg;
        }
        //***************************
        public void swi(int r1, int r2)
        {
            //swi b a
            int tempReg = reg[r2];
            reg[r2] = reg[r1];
            reg[r1] = tempReg;
        }
        //***************************
        public void del(int r1)
        {
            //del a
            reg[r1] = 0;
        }

        private void RunBtn_Click(object sender, EventArgs e)
        {
            run();
            InBox1.Text = "";
            ResetInt();
        }

        private void ClrBtn_Click(object sender, EventArgs e)
        {
            ResetReg();
            ResetInt();
        }
    }
}
