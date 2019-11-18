using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Algo_Project
{
    public partial class Form1 : Form
    {

        private String s;
        private String pattern;
        private String  pattern1, pattern2, pattern3, pattern4, pattern5;
        private String screenValue;
        int Q = 100007;

        public const int d = 256;

        int[] f = new int[100];

        public Form1()
        {
            InitializeComponent();
        }

        private void Bruteforce_Click(object sender, EventArgs e)
        {
            pattern = enter1.Text.ToUpper(); ;
            RandomGenerator();
            Bruteforce();
            DisplayTime();
        }

        private void rabinkarpbutton_Click(object sender, EventArgs e)
        {
            pattern = enter1.Text.ToUpper(); ;
            RandomGenerator();
            RabinKarp();
            DisplayTime();
        }

        private void boyermoore_Click(object sender, EventArgs e)
        {
            pattern = enter1.Text.ToUpper(); ;
            RandomGenerator();
            BoyerMoore();
            DisplayTime();
        }

        private void knuthmorrispratt_Click(object sender, EventArgs e)
        {
            pattern = enter1.Text.ToUpper(); ;
            RandomGenerator();
            //preKMP();
            KMP(s, pattern);
            DisplayTime();
        }

        private void heuristic_Click(object sender, EventArgs e)
        {
            pattern = enter1.Text.ToUpper();
            RandomGenerator();

            // 50% match
            pattern1 = pattern.Substring(0, (int)(pattern.Length * 0.5));
            screenValue = ("50% Match: ").ToString() + " " + pattern1;
            mydisplay1.Text += screenValue + Environment.NewLine;
            RandomGenerator();
            Heuristic();

            // 60% match from start
            pattern2 = pattern.Substring(0, (int)(pattern.Length * .6));
            screenValue = ("60% Match: ").ToString() + " " + pattern2;
            mydisplay1.Text += screenValue + Environment.NewLine;
            RandomGenerator();
            Heuristic();

            // 70% match from start
            pattern3 = pattern.Substring(0, (int)(pattern.Length * .7));
            screenValue = ("70% Match: ").ToString() + " " + pattern3;
            mydisplay1.Text += screenValue + Environment.NewLine;
            RandomGenerator();
            Heuristic();

            // 80% match from start
            pattern4 = pattern.Substring(0, (int)(pattern.Length * .8));
            screenValue = ("80% Match: ").ToString() + " " + pattern4;
            mydisplay1.Text += screenValue + Environment.NewLine;
            RandomGenerator();
            Heuristic();

            // 90% match from start
            pattern5 = pattern.Substring(0, (int)(pattern.Length * .9));
            screenValue = ("90% Match: ").ToString() + " " + pattern5;
            mydisplay1.Text += screenValue + Environment.NewLine;
            RandomGenerator();
            Heuristic();

            //100%match from start
            screenValue = ("100% Match: ").ToString() + " " + pattern;
            mydisplay1.Text += screenValue + Environment.NewLine;
            RandomGenerator();
            BoyerMoore();

            DisplayTime();
        }
        

        //information for brute force "i" icon
        private void button1_Click(object sender, EventArgs e)
        {
            bruteforceinfo information = new bruteforceinfo();
            information.Show();

        }

        private void rabinkarpinfo_Click(object sender, EventArgs e)
        {
            rabinkarpinfo information = new rabinkarpinfo();
            information.Show();
        }

        private void boyermooreinfo_Click(object sender, EventArgs e)
        {
            boyermoore information = new boyermoore();
            information.Show();
        }

        private void kmpinfo_Click(object sender, EventArgs e)
        {
            knuthmorrispratt information = new knuthmorrispratt();
            information.Show();
        }

        private void heuristicinfo_Click(object sender, EventArgs e)
        {
            heuristic information = new heuristic();
            information.Show();
        }

        public void RandomGenerator()
        {
            Random geneSequence = new Random();  // The random number sequence


            for (int i = 0; i < 1000; i++)
            {
                int x = geneSequence.Next(1, 4);


                switch (x)
                {
                    case 1:
                        s = s + 'C';
                        break;

                    case 2:
                        s = s + 'T';
                        break;

                    case 3:
                        s = s + 'A';
                        break;

                    case 4:
                        s = s + 'G';
                        break;
                }

            }
            screenValue = s;

            randomstring.Text += screenValue + Environment.NewLine;
        }

        public void Bruteforce() // s is string sequence, pattern is what is inputted from user
        {

            //brute force algorithm is executed here 
            int n;
            n = s.Length;

            int m;
            m = pattern.Length;

            int i;
            int j;
            for (i = 0; i <= n - m; i++)
            {
                for (j = 0; j < m; j++)
                {
                    if (pattern[j] != s[i + j])
                        break;
                }
                if (j == m)
                {
                    //i want these results to be sent back to the main screen

                    screenValue = ("Pattern found at:" + i).ToString() + " " + pattern;

                    mydisplay1.Text += screenValue + Environment.NewLine;
                }

            }
        }

        public void RabinKarp() // s is string sequence, pattern is what is inputted from user
        {

            //Rabin karp algorithm is executed here 
            int n;
            n = s.Length;

            int m;
            m = pattern.Length;

            int i;
            int j;

            int p = 0;
            int h = 1;
            int t = 0;

            for (i = 0; i < m - 1; i++)
                h = (h * d) % Q;
            for (i = 0; i < m; i++)
            {
                p = (d * p + pattern[i]) % Q;
                t = (d * t + s[i]) % Q;
            }
            for (i = 0; i <= n - m; i++)
            {
                if (p == t)
                {
                    for (j = 0; j < m; j++)
                    {
                        if (s[i + j] != pattern[j])
                            break;
                    }
                    if (j == m)
                    {
                        screenValue = ("Pattern found at:" + i).ToString() + " " + pattern;

                        mydisplay1.Text += screenValue + Environment.NewLine;
                    }
                }
                if (i < n - m)
                {
                    t = (d * (t - s[i] * h) + s[i + m]) % Q;
                    if (t < 0)
                        t = (t + Q);
                }
            }


        }

        public void BoyerMoore() // s is string sequence, pattern is what is inputted from user
        {
           
            List<int> retVal = new List<int>();

            // boyer moore algorithm is executed here
            int n;
            n = s.Length;

            int m;
            m = pattern.Length;

            int[] badChar = new int[256];

            BadCharHeuristic(pattern, m, ref badChar);

            int i = 0;

            while (i <= (n - m))
            {
                int j = m - 1;

                while (j >= 0 && pattern[j] == s[i + j])
                    --j;

                if (j < 0)
                {
                    retVal.Add(i);
                    i += (i + m < n) ? m - badChar[s[i + m]] : 1;
                }

                else
                {
                    i += Math.Max(1, j - badChar[s[i + j]]);
                }

            }
            

            foreach (var x in retVal)
            {
                screenValue = ("Pattern found at:" + x).ToString() + " " + pattern;
                mydisplay1.Text += screenValue + Environment.NewLine;
            }       

        }

        private static void BadCharHeuristic(string str, int size, ref int[] badChar)
        {
            int i;

            for (i = 0; i < 256; i++)
                badChar[i] = -1;

            for (i = 0; i < size; i++)
                badChar[(int)str[i]] = i;
        }

        private void clear_Click(object sender, EventArgs e)
        {
            mydisplay1.Clear();
            randomstring.Clear();
            timebox.Clear();
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        public void preKMP(string pattern, ref int[] lpsArray)
        { 
            int M = pattern.Length;
            int len = 0;
            lpsArray[0] = 0;
            int i = 1;

            while (i < M)
            {
                if (pattern[i] == pattern[len])
                {
                    len++;
                    lpsArray[i] = len;
                    i++;
                }
                else
                {
                    if (len == 0)
                    {
                        lpsArray[i] = 0;
                        i++;
                    }
                    else
                    {
                        len = lpsArray[len - 1];
                    }
                }
            }
        }


        public List<int> KMP(string s, string pattern) // s is string sequence, pattern is what is inputted from user
        {

            //kmp algorithm is executed here

            int m = pattern.Length;
            int n = s.Length;
            int[] lpsArray = new int[m];
            List<int> matchedIndex = new List<int>();

            if (n < m) return matchedIndex;
            if (n == m && s == pattern) return matchedIndex;
            if (m == 0) return matchedIndex;

            preKMP(pattern, ref lpsArray);

            int i = 0, j = 0;
            while (i < n)
            {
                if (s[i] == pattern[j])
                {
                    i++;
                    j++;
                }

                // match found at i-j
                if (j == m)
                {
                    matchedIndex.Add(i - j);
                 
                    j = lpsArray[j - 1];

                    screenValue = ("Pattern found at:" + i).ToString() + " " + pattern;

                    mydisplay1.Text += screenValue + Environment.NewLine;
                }
                else if (i < n && s[i] != pattern[j])
                {
                    if (j != 0)
                    {
                        j = lpsArray[j - 1];
                    }
                    else
                    {
                        i++;
                    }
                }
            }

            return matchedIndex;

        }

        public void Heuristic() // s is string sequence, pattern is what is inputted from user
        {

            List<int> retVal = new List<int>();
            int n;
            n = s.Length;

            int m;
            m = pattern.Length;

            int[] badChar = new int[256];

            BadCharHeuristic(pattern, m, ref badChar);

            int i = 0;

            while (i <= (n - m))
            {
                int j = m - 1;

                while (j >= 0 && pattern[j] == s[i + j])
                    --j;

                if (j < 0)
                {
                    retVal.Add(i);
                    i += (i + m < n) ? m - badChar[s[i + m]] : 1;
                }
                else
                {
                    i += Math.Max(1, j - badChar[s[i + j]]);
                }

            }


            foreach (var x in retVal)
            {
                screenValue = ("Pattern found at:" + x).ToString() + " " + pattern;
                mydisplay1.Text += screenValue + Environment.NewLine;
            }


        }

        public void DisplayTime()

        {

            //creating a stopwatch instance

            Stopwatch stopWatch = new Stopwatch();

            //starting the stopwatch

            stopWatch.Start();

            //getting ellapsed ticks

            var ellapsedTicks = stopWatch.ElapsedTicks;

            //converting ellapsed ticks into microseconds by multiplying with 1000000 and dividing

            //with the frequency of the stopwatch

            var microSeconds = ellapsedTicks * (1000000L) / Stopwatch.Frequency;

           // Console.WriteLine("Time processed: {0} micro seconds", microSeconds);
            screenValue = ("Time processed: " + microSeconds + " micro seconds" ).ToString();
            timebox.Text += screenValue + Environment.NewLine;
        }

    }
}
