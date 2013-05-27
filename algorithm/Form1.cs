using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace algorithm
{
    public partial class Form1 : Form
    {

        int shiftslots = 13;
        int scheduleDays = 14;
        int female = 6;
        int male = 7;
        int day = 0;
        string[] weekName = new string[] { "DO", "VRY", "SA", "SO", "MA", "DI", "WO" };

        int[] duties = new int[] { 0 , 1, 2, 3, 4, 5, 6, 7, 8};
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //CALCULATION
            int totalemp = female + male;
            string[,] shifted = new string[shiftslots, scheduleDays];

            double TotalFemaleAverage = scheduleDays / female;
            double TotalMaleAverage = scheduleDays / male;
            double TotalUnisexAverage = scheduleDays / totalemp;

            double FemaleWeekAverage = 7 / female;
            double MaleWeekAverage = 7 / male;
            double UnisexWeekAverage = 1;
            int weeks = scheduleDays / 7;
            int[] week = new int[scheduleDays];

            PopulateWeekNames(week);
            


            //Populate off time
            PopululateOffTime(shifted);
            



            //SCHEDULE
            //loop through week
            for (int weekday = 0; weekday < scheduleDays; weekday++)
            {
                //If weekday
                if (week[weekday] == 0 || week[weekday] == 4 || week[weekday] == 5 || week[weekday] == 6)
                {
                    D2Population(weekday, shifted,ref TotalFemaleAverage,ref FemaleWeekAverage);
                    //ResetTotalAverage(totalemp, ref TotalFemaleAverage, ref TotalMaleAverage, ref TotalUnisexAverage, ref FemaleWeekAverage, ref MaleWeekAverage, ref UnisexWeekAverage);
                    N1Population(weekday, shifted, ref TotalUnisexAverage, ref UnisexWeekAverage, totalemp);
                    //N2
                    //D5
                    //D4
                    //D3
                    //D1
                }
                //WEEKEND POPULATION
                else
                {
                    D2Population(weekday, shifted,ref TotalFemaleAverage,ref FemaleWeekAverage);
                    //ResetTotalAverage(totalemp, ref TotalFemaleAverage, ref TotalMaleAverage, ref TotalUnisexAverage, ref FemaleWeekAverage, ref MaleWeekAverage, ref UnisexWeekAverage);
                    N1Population(weekday, shifted, ref TotalUnisexAverage, ref UnisexWeekAverage, totalemp);
                    //N2
                    //D5
                    //D6
                    //D4
                    //D3
                    //D1
                }
            }








            lbldays.Text = day.ToString();
            Gridview(shifted, totalemp);
        }




        //METHODS

        //SHIFTING
        public void PopululateOffTime(string[,] shifted)
        {

            for (int r = 0; r < shifted.GetLength(0); r++)
            {
                for (int c = 0; c < shifted.GetLength(1); c++)
                {
                    shifted[r, c] = "000000000000000";
                }
            }

        }
        public void D2Population(int weekday, string[,] shifted,ref double TotalFemaleAverage, ref double FemaleWeekAverage)
        {
            //D2
            for (int employee = 0; employee < 13; employee++)
            {
                //Check previous day
                if (weekday > 0)
                {
                    //check if female == true
                    if (CheckFemale(employee))     //Method
                    {                           
                        //check D2 exists
                        if (CheckD2Found(employee, shifted, weekday) == false)    //Method
                        {
                            //check if prev shift is not a nightshift
                            if (CheckPreviousNotNightshift(shifted, employee, weekday))    //Method
                            {
                                //Check empAverage
                                if (CountTotalD2(shifted, employee) < TotalFemaleAverage)
                                {
                                    //Check weekAverage
                                    if (CountTotalD2(shifted, employee) < FemaleWeekAverage) //Method
                                    {
                                        shifted[employee, weekday] = "2";
                                    }
                                }
                            }
                        }
                    }
                }

                //NO PREVIOUS DAYS //FIRST DAY EVER POPULATION //DUPE
                else
                {
                    //check if female == true
                    if (CheckFemale(employee))
                    {
                        //check D2 exists
                        if (CheckD2Found(employee, shifted, weekday) == false)
                        {
                                shifted[employee, weekday] = "2";
                        }
                    }
                }
            }

            //next day in week
            day++;

            //Increase/balance week average per week
            if (day == female || day == female * 2 || day == female * 3 || day == female * 4 || day == female * 5 || day == female * 6)
            {
                FemaleWeekAverage += 1;
            }

            //Increase TotalAverage for unbalanced shifts
            double daysPerFemale = (scheduleDays / female);

            if (day == Convert.ToInt32(Math.Truncate(daysPerFemale) * female))
            {
                TotalFemaleAverage += 1;
            }

            //END
        }
        public void N1Population(int weekday, string[,] shifted, ref double TotalUnisexAverage, ref double UnisexWeekAverage, int totalemp)
        {
            //N1
            for (int employee = 0; employee < 13; employee++)
            {
                //Check previous day
                if (weekday > 0)
                {
                    //check N1 exists
                    if (CheckN1Found(employee, shifted, weekday) == false)    //Method
                    {
                        ////check if next shift is not a dayshift
                        if (CheckNextNotDayshift(shifted, employee, weekday))    //Method
                        {
                            //Check empAverage
                            if (CountTotalN1(shifted, employee) < TotalUnisexAverage)
                            {
                                //Check weekAverage
                                if (CountTotalN1(shifted, employee) < UnisexWeekAverage) //Method
                                {
                                    shifted[employee, weekday] = "7";
                                }
                            }
                        }
                    }
                }

                //NO PREVIOUS DAYS //FIRST DAY EVER POPULATION //DUPE
                else
                {
                    //check N1 exists
                    if (CheckN1Found(employee, shifted, weekday) == false)    //Method
                    {
                        //check if next shift is not a dayshift
                        if (CheckNextNotDayshift(shifted, employee, weekday))    //Method
                        {
                            shifted[employee, weekday] = "7";
                        }
                    }
                } 
            }

            //next day in week
            //day++;

            //Increase/balance week average per week
            if (day == totalemp || day == totalemp * 2 || day == totalemp * 3 || day == totalemp * 4 || day == totalemp * 5 || day == totalemp * 6)
            {
                UnisexWeekAverage += 1;
            }

            //Increase TotalAverage for unbalanced shifts
            double daysPerEmp = (scheduleDays / totalemp);

            if (day == Convert.ToInt32(Math.Truncate(daysPerEmp) * totalemp ))
            {
                TotalUnisexAverage += 1;
            }

            //END
        }



        //CHECKS
        //D2 ?????
        public bool CheckFemale(int employee)
        {
            return employee >= male;
        }
        public bool CheckD2Found(int employee, string[,] shifted, int weekday)
        {
            bool d2Found = false;
            for (int shiftSlot = 0; shiftSlot < employee; shiftSlot++)
            {
                if (shifted[shiftSlot, weekday] == "2")
                {
                    d2Found = true;
                }

            }

            return d2Found;
        }
        public bool CheckPreviousNotNightshift(string[,] shifted,int employee, int weekday)
        {

            return int.Parse(shifted[employee, weekday - 1]) < 7;       
        
        }
        public bool CheckPreviousNotD2(string[,] shifted,int employee, int weekday)
        {
            return int.Parse(shifted[employee, weekday - 1]) != 2;       
        }
        public int CountTotalD2(string[,] shifted, int employee)
        {
            int D2Total = 0;

            for (int i = 0; i < scheduleDays; i++)
            {
                if (shifted[employee, i] == "2")
                {
                    D2Total++;
                }
            }

            return D2Total;
        }
        //N1 ?????
        public bool CheckN1Found(int employee, string[,] shifted, int weekday)
        {
            bool n1Found = false;
            for (int shiftSlot = 0; shiftSlot < employee; shiftSlot++)
            {
                if (shifted[shiftSlot, weekday] == "7")
                {
                    n1Found = true;
                }

            }

            return n1Found;
        
        }
        public bool CheckNextNotDayshift(string[,] shifted, int employee, int weekday)
        {
            if (weekday + 1 < scheduleDays)
            {
                if (int.Parse(shifted[employee, weekday + 1]) == 7 || int.Parse(shifted[employee, weekday + 1]) == 8 || int.Parse(shifted[employee, weekday + 1]) == 000000000000000)
                {
                    return true;
                }

                return false  ; 
            }
            else
            {
                return false;
            }
        }
        public int CountTotalN1(string[,] shifted, int employee)
        {
            int N1Total = 0;

            for (int i = 0; i < scheduleDays; i++)
            {
                if (shifted[employee, i] == "7")
                {
                    N1Total++;
                }
            }

            return N1Total;        
        }


        //RESETS
        public void ResetTotalAverage(int totalemp, ref double TotalFemaleAverage,ref double TotalMaleAverage,ref double TotalUnisexAverage,ref double FemaleWeekAverage,ref double MaleWeekAverage ,ref double UnisexWeekAverage )
        {
             TotalFemaleAverage = scheduleDays / female;
             TotalMaleAverage = scheduleDays / male;
             TotalUnisexAverage = scheduleDays / totalemp;

             FemaleWeekAverage = 7 / female;
             MaleWeekAverage = 7 / male;
             UnisexWeekAverage = 7 / totalemp;
        
        }

        //GRIDVIEW
        public void PopulateWeekNames(int[] week)
        {
            for (int i = 0; i < scheduleDays; i++)
            {
                switch (i)
                {
                    case 0:
                    case 7:
                    case 14:
                    case 21:
                        week[i] = 0;
                        break;
                    case 1:
                    case 8:
                    case 15:
                    case 22:
                        week[i] = 1;
                        break;
                    case 2:
                    case 9:
                    case 16:
                    case 23:
                        week[i] = 2;
                        break;
                    case 3:
                    case 10:
                    case 17:
                    case 24:
                        week[i] = 3;
                        break;
                    case 4:
                    case 11:
                    case 18:
                    case 25:
                        week[i] = 4;
                        break;
                    case 5:
                    case 12:
                    case 19:
                    case 26:
                        week[i] = 5;
                        break;
                    case 6:
                    case 13:
                    case 20:
                    case 27:
                        week[i] = 6;
                        break;
                }

            }


        }         
        public void Gridview(string[,] shifted,int totalemp)
        {


            //ARRAY TO GRIDVIEW
            var rowCount = shifted.GetLength(0);
            var rowLength = shifted.GetLength(1);
            dataGridView1.ColumnCount = rowLength;

            for (int rowIndex = 0; rowIndex < rowCount; ++rowIndex)
            {
                var row = new DataGridViewRow();
                for (int columnIndex = 0; columnIndex < rowLength; ++columnIndex)
                {
                    row.Cells.Add(new DataGridViewTextBoxCell()
                    {
                        Value = shifted[rowIndex, columnIndex]
                    });
                }
                dataGridView1.Rows.Add(row);
            }

            //COL HEADER TEXT
            int index = 0;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.HeaderText = weekName[index];
                index++;
                if (index > 6)
                {
                    index = 0;
                }
            }

            //ROW HEADER TEXT
            index = 0;
            for (int i = 0; i <= male; i++)
            {
                if (index < male)
                {
                    dataGridView1.Rows[i].HeaderCell.Value = "Male " + ++index;
                }
            }

            index = 0;
            for (int i = male; i <= totalemp; i++)
            {
                if (index < totalemp)
                {
                    dataGridView1.Rows[i].HeaderCell.Value = "Female " + ++index;
                }

            }
        
        
        
        }
    }

}
