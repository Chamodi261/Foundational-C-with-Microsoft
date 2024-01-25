using System;

class Program
{
    static void Main()
    {
        // Number of exam assignments
        int examAssignments = 5;

        // Array of student names
        string[] studentNames = new string[] { "Sophia", "Andrew", "Emma", "Logan" };

        // Arrays to store individual student scores
        int[] sophiaScores = new int[] { 90, 86, 87, 98, 100, 94, 90 };
        int[] andrewScores = new int[] { 92, 89, 81, 96, 90, 89 };
        int[] emmaScores = new int[] { 90, 85, 87, 98, 68, 89, 89, 89 };
        int[] loganScores = new int[] { 90, 95, 87, 88, 96, 96 };

        // Array to store current student scores
        int[] studentScores = new int[10];

        // Variable to store the current student's letter grade
        string currentStudentLetterGrade = "";

        // Display the header row for scores/grades
        Console.Clear();
        Console.WriteLine("Student\t\tExam Score\tOverall Grade\tExtra Credit\n");

        /*
        The outer foreach loop is used to:
        - iterate through student names
        - assign a student's grades to the studentScores array
        - calculate exam and extra credit sums (inner foreach loop)
        - calculate numeric and letter grade
        - write the score report information
        */
        foreach (string name in studentNames)
        {
            string currentStudent = name;

            // Assign the appropriate scores based on the current student's name
            switch (currentStudent)
            {
                case "Sophia":
                    studentScores = sophiaScores;
                    break;

                case "Andrew":
                    studentScores = andrewScores;
                    break;

                case "Emma":
                    studentScores = emmaScores;
                    break;

                case "Logan":
                    studentScores = loganScores;
                    break;
            }

            int gradedAssignments = 0;
            int gradedExtraCreditAssignments = 0;

            int sumExamScores = 0;
            int sumExtraCreditScores = 0;

            decimal currentStudentGrade = 0;
            decimal currentStudentExamScore = 0;
            decimal currentStudentExtraCreditScore = 0;

            /* 
            The inner foreach loop: 
            - sums the exam and extra credit scores
            - counts the extra credit assignments
            */
            foreach (int score in studentScores)
            {
                gradedAssignments += 1;

                if (gradedAssignments <= examAssignments)
                {
                    // Sum the exam scores
                    sumExamScores += score;
                }
                else
                {
                    // Count and sum the extra credit assignments
                    gradedExtraCreditAssignments += 1;
                    sumExtraCreditScores += score;
                }
            }

            // Calculate the current student's exam and extra credit scores
            currentStudentExamScore = (decimal)sumExamScores / examAssignments;
            currentStudentExtraCreditScore = (decimal)sumExtraCreditScores / gradedExtraCreditAssignments;

            // Calculate the overall grade for the current student
            currentStudentGrade = (decimal)(sumExamScores + (sumExtraCreditScores / 10)) / examAssignments;

            // Determine the letter grade based on the overall grade
            currentStudentLetterGrade = DetermineLetterGrade(currentStudentGrade);

            // Display the score report information
            Console.WriteLine($"{currentStudent}\t\t{currentStudentExamScore}\t\t{currentStudentGrade}\t{currentStudentLetterGrade}\t{currentStudentExtraCreditScore} ({(((decimal)sumExtraCreditScores / 10) / examAssignments)} pts)");
        }

        // Required for running in VS Code (keeps the Output windows open to view results)
        Console.WriteLine("\n\rPress the Enter key to continue");
        Console.ReadLine();
    }

    // Function to determine the letter grade based on the numeric grade
    static string DetermineLetterGrade(decimal numericGrade)
    {
        if (numericGrade >= 97)
            return "A+";

        else if (numericGrade >= 93)
            return "A";

        else if (numericGrade >= 90)
            return "A-";

        else if (numericGrade >= 87)
            return "B+";

        else if (numericGrade >= 83)
            return "B";

        else if (numericGrade >= 80)
            return "B-";

        else if (numericGrade >= 77)
            return "C+";

        else if (numericGrade >= 73)
            return "C";

        else if (numericGrade >= 70)
            return "C-";

        else if (numericGrade >= 67)
            return "D+";

        else if (numericGrade >= 63)
            return "D";

        else if (numericGrade >= 60)
            return "D-";

        else
            return "F";
    }
}
