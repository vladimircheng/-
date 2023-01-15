using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Outraging
{
    public static class Calc
    {
        public static Random Random = new Random();
        public static double correction(Entity entity,int environment)
        {
            double cor = 0;
            if (environment == 1)
                cor = entity.getArmy("a") * 1 + entity.getArmy("b") * 1 + entity.getArmy("c") * 0.8 + entity.getArmy("d") * 0.7 + entity.getArmy("e") * 0.7 + entity.getArmy("f") * 1 + entity.getArmy("g") * 1 + entity.getArmy("h") * 1 + entity.getArmy("i") * 0.7 + entity.getArmy("j") * 0.7;
            else if (environment == 2)
                cor = entity.getArmy("a") * 1 + entity.getArmy("b") * 0.8 + entity.getArmy("c") * 0.8 + entity.getArmy("d") * 0.8 + entity.getArmy("e") * 0.8 + entity.getArmy("f") * 1 + entity.getArmy("g") * 1 + entity.getArmy("h") * 1 + entity.getArmy("i") * 0.8 + entity.getArmy("j") * 0.8;
            else if (environment == 3)
                cor = entity.getArmy("a") * 1 + entity.getArmy("b") * 1 + entity.getArmy("c") * 0.8 + entity.getArmy("d") * 1 + entity.getArmy("e") * 1 + entity.getArmy("f") * 1 + entity.getArmy("g") * 1 + entity.getArmy("h") * 1 + entity.getArmy("i") * 0.7 + entity.getArmy("j") * 0.7;
            else if (environment == 4)
                cor = entity.getArmy("a") * 1 + entity.getArmy("b") * 1 + entity.getArmy("c") * 0.9 + entity.getArmy("d") * 1 + entity.getArmy("e") * 1 + entity.getArmy("f") * 1 + entity.getArmy("g") * 1 + entity.getArmy("h") * 1 + entity.getArmy("i") * 0.8 + entity.getArmy("j") * 0.8;
            else
                cor = entity.getArmy("a") * 1 + entity.getArmy("b") * 1 + entity.getArmy("c") * 1 + entity.getArmy("d") * 1 + entity.getArmy("e") * 1 + entity.getArmy("f") * 1 + entity.getArmy("g") * 1 + entity.getArmy("h") * 1 + entity.getArmy("i") * 1 + entity.getArmy("j") * 1;
            return cor;
        }
        public static double calculate_attack(Entity entity,int environment)
        {
            Entity temp = new Entity(entity);
            temp.setArmy("a", temp.getArmy("a")*6);
            temp.setArmy("b", temp.getArmy("b")*7);
            temp.setArmy("c", temp.getArmy("c")*10);
            temp.setArmy("d", temp.getArmy("d")*25);
            temp.setArmy("e", temp.getArmy("e")*27);
            temp.setArmy("f", temp.getArmy("f")*3);
            temp.setArmy("g", temp.getArmy("g")*8);
            temp.setArmy("h", temp.getArmy("h")*4);
            temp.setArmy("i", temp.getArmy("i")*22);
            temp.setArmy("j", temp.getArmy("j")*30);
            return correction(temp,environment);
        }
        public static double calculate_hard(Entity entity, int environment)
        {
            Entity temp = new Entity(entity);
            temp.setArmy("a", temp.getArmy("a") * 1);
            temp.setArmy("b", temp.getArmy("b") * 1);
            temp.setArmy("c", temp.getArmy("c") * 10);
            temp.setArmy("d", temp.getArmy("d") * 2);
            temp.setArmy("e", temp.getArmy("e") * 5);
            temp.setArmy("f", temp.getArmy("f") * 7);
            temp.setArmy("g", temp.getArmy("g") * 5);
            temp.setArmy("h", temp.getArmy("h") * 20);
            temp.setArmy("i", temp.getArmy("i") * 20);
            temp.setArmy("j", temp.getArmy("j") * 30);
            return correction(temp, environment);
        }
        public static double calculate_defence(Entity entity, int environment)
        {
            Entity temp = new Entity(entity);
            temp.setArmy("a", temp.getArmy("a") * 23);
            temp.setArmy("b", temp.getArmy("b") * 24);
            temp.setArmy("c", temp.getArmy("c") * 50);
            temp.setArmy("d", temp.getArmy("d") * 10);
            temp.setArmy("e", temp.getArmy("e") * 5);
            temp.setArmy("f", temp.getArmy("f") * 4);
            temp.setArmy("g", temp.getArmy("g") * 2);
            temp.setArmy("h", temp.getArmy("h") * 4);
            temp.setArmy("i", temp.getArmy("i") * 5);
            temp.setArmy("j", temp.getArmy("j") * 19);
            return correction(temp, environment);
        }
        public static double calculate_breach(Entity entity, int environment)
        {
            Entity temp = new Entity(entity);
            temp.setArmy("a", temp.getArmy("a") * 3);
            temp.setArmy("b", temp.getArmy("b") * 4);
            temp.setArmy("c", temp.getArmy("c") * 19);
            temp.setArmy("d", temp.getArmy("d") * 6);
            temp.setArmy("e", temp.getArmy("e") * 20);
            temp.setArmy("f", temp.getArmy("f") * 1);
            temp.setArmy("g", temp.getArmy("g") * 4);
            temp.setArmy("h", temp.getArmy("h") * 0);
            temp.setArmy("i", temp.getArmy("i") * 3);
            temp.setArmy("j", temp.getArmy("j") * 30);
            return correction(temp, environment);
        }
        public static double calculate_antiair(Entity entity, int environment)
        {
            Entity temp = new Entity(entity);
            temp.setArmy("a", temp.getArmy("a") * 0);
            temp.setArmy("b", temp.getArmy("b") * 0);
            temp.setArmy("c", temp.getArmy("c") * 0);
            temp.setArmy("d", temp.getArmy("d") * 0);
            temp.setArmy("e", temp.getArmy("e") * 0);
            temp.setArmy("f", temp.getArmy("f") * 19);
            temp.setArmy("g", temp.getArmy("g") * 32);
            temp.setArmy("h", temp.getArmy("h") * 0);
            temp.setArmy("i", temp.getArmy("i") * 0);
            temp.setArmy("j", temp.getArmy("j") * 0);
            return correction(temp, environment);
        }
        public static int calculate_armor(Entity entity, int environment)
        {
            return (int)((entity.getArmy("c") * 50 + entity.getArmy("e") * 100 + entity.getArmy("i") * 100 + entity.getArmy("j") * 100) / (entity.getArmy("a") + entity.getArmy("b") + entity.getArmy("c") + entity.getArmy("d") + entity.getArmy("e") + entity.getArmy("f") + entity.getArmy("g") + entity.getArmy("h") + entity.getArmy("i") + entity.getArmy("j")));
        }

        //calc for battle
        public static int calculate_organize(double a, double b, double c, double d, double e, double f, double g)
        {
            if ((e * (1 - (c / 100)) + f * (c / 100)) > a)
            {
                return (int)(((e * (1 - (c / 100)) + f * (c / 100)) - a) * (Math.Round(Random.NextDouble() * 4)) * 0.4 * 0.05 + a * (Math.Round(Random.NextDouble() * 2)) * 0.2 * 0.05 + (Math.Round(Random.NextDouble() * 2)) * 0.09 * ((g * 1) / ((b + d) * (1 / 20.0) + 0.1)) + (Math.Round(Random.NextDouble() * 2)) * 0.01 * g);
            }
            else
            {
                return (int)(((e * (1 - (c / 100)) + f * (c / 100))) * (Math.Round(Random.NextDouble() * 2)) * 0.2 * 0.05 + (Math.Round(Random.NextDouble() * 2)) * 0.09 * ((g * 1) / ((b + d) * (1 / 20.0) + 0.1)) + (Math.Round(Random.NextDouble() * 2)) * 0.01 * g);
            }
        }
        public static int calculate_manpower(int m, double a, double b, double c, double d, double e, double f, double g)
        {
            if ((e * (1 - (c / 100)) + f * (c / 100)) > a)
            {
                return (int)((m * 1 - (((e * (1 -(c / 100)) + f * (c / 100)) - a*0.01) * (Math.Round(Random.NextDouble() * 2)) * 8  + (Math.Round(Random.NextDouble()) * 2)) * ((g * 1) / ((b + d) * (1 / 20.0)))*0.1) * 1 + (Math.Round(Random.NextDouble() * 2)) * g*0.01);
            }
            else
            {
                return (int)(m * 1 - (((e * (1 - (c / 100)) + f * (c / 100))) * (Math.Round(Random.NextDouble() *4)) * 4 + (Math.Round(Random.NextDouble() * 2)) * ((g * 1) / ((b + d) * (1 / 20.0) ))*0.1) * 1 + (Math.Round(Random.NextDouble() * 2)) * g*0.01);
            }
        }
    }
}
