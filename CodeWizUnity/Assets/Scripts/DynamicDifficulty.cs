using System;
using System.Diagnostics;
public class DynamicDifficulty
{
    public static int score;
    public static int currentDifficulty;
    public static void NextDifficulty(int time,int difficulty,int hints_taken)
    {
        /*int hints_taken = 2;*/

        int easy_weight = 0;
        int medium_weight = 1;
        int hard_weight = 2;
        int next_level = 3;
        int next_difficulty = 0;

        int easy = 1000;
        int medium = 5000;
        int hard = 10000;


        
            if (difficulty == easy_weight && time >60)
            {
                next_difficulty = easy_weight;
            currentDifficulty = next_difficulty;
            }
            else if (difficulty == easy_weight && time <= 60)
            {
                score = (easy) - (time * 2) - (hints_taken) * 10;
                next_difficulty = medium_weight;
            currentDifficulty = next_difficulty;
            }
            else if (difficulty == medium_weight && time > 200)
            {
                next_difficulty = easy_weight;
            currentDifficulty = next_difficulty;
            }
            else if (difficulty == medium_weight && time <= 200)
            {
                score = (medium) - (time * 2) - (hints_taken) * 10;
                next_difficulty = hard_weight;
            currentDifficulty = next_difficulty;
            }
            else if (difficulty == hard_weight && time > 500)
            {
                next_difficulty = medium_weight;
            currentDifficulty = next_difficulty;
            }
            else if (difficulty == hard_weight && time <= 500)
            {
                score = (hard) - (time * 2) - (hints_taken) * 10;
                next_difficulty = next_level;
            currentDifficulty = next_difficulty;
            }

        
        /*Console.WriteLine(next_difficulty);
        Console.WriteLine(score);*/
    }
    public static Stopwatch timer=new Stopwatch();
    public static void startTimer()
    {
        timer.Reset();
        timer.Start();
    }
    public static long getTimeElapsed()
    {
        timer.Stop();
        return (long)(timer.ElapsedMilliseconds/1000);
    }

    public static void getinitialN()
    {
        Level level = APIConnections.studentLevel;
        int difficulty = 0;
        if (level.time[2].Length != 0)
            difficulty = 2;
        else if (level.time[1].Length != 0)
            difficulty = 1;
        else
            difficulty = 0;
        currentDifficulty = difficulty;
    }

}