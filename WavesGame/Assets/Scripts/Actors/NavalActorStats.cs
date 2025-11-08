using System;

namespace Actors
{
    [Serializable]
    public struct NavalActorStats
    {
        public int strength; //Offensive
        public int speed; //Movement and Number of Movements per Turn
        public int stability; //Constitution against waves
        public int scrutiny; //Awareness
        public int sturdiness; //Constitution against enemies
        public int spirit; //Morale - number of actions per turn

        public NavalActorStats(int strength, int speed, int stability, int scrutiny, int sturdiness, int spirit)
        {
            this.strength = strength;
            this.speed = speed;
            this.stability = stability;
            this.scrutiny = scrutiny;
            this.sturdiness = sturdiness;
            this.spirit = spirit;
        }
    }
}