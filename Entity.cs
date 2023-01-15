using System;
using System.Collections.Generic;
using static System.Math;

namespace Outraging
{
    //home-made exceptions
    public partial class BelowZeroException : ApplicationException
    {
        public BelowZeroException(string message) : base(message)
        {

        }
    }
    public partial class LowProductionException : ApplicationException
    {
        public LowProductionException(string message) : base(message)
        {

        }
    }
    public partial class WideExceedException : ApplicationException
    {
        public WideExceedException(string message) : base(message)
        {

        }
    }
    public partial class WrongChoseException : ApplicationException
    {
        public WrongChoseException(string message) : base(message)
        {

        }
    }

    public partial class Entity
    {
        private Dictionary<string, int> map;
        private double[] armys;
        private bool side;
        private static int[] armys_cost = new int[13] { 0, 24, 22, 50, 300, 400, 250, 360, 70, 120, 120, 144, 380 };
        private static int[] armys_manpower = new int[13] { 0, 0, 0, 1000, 1200, 500, 500, 500, 1000, 500, 500, 500, 500 };
        private static int[] armys_warwidth = new int[13] { 0, 0, 0, 2, 2, 3, 1, 2, 2, 3, 1, 2, 2 };
        private static int[] armys_organize = new int[13] { 0, 0, 0, 60, 60, 0, 0, 0, 70, 0, 0, 0, 10 };
        private static double[][] preset =
        {
		    new double[13] {0,0,0,0,0,0,0,0,0,0,0,0,0},
            new double[13] {0,98,98,0,8,0,2,1,0,0,0,0,10},
            new double[13] {0,98,98,15,0,0,0,0,15,5,2,0,0},
            new double[13] {0,98,98,10,0,5,2,0,5,5,0,0,0},
            new double[13] {0,98,98,12,0,0,2,0,0,4,0,0,5}
	    };
	    public Entity(int arg)
        {
            map = new Dictionary<string, int>();
            armys = new double[13];
            if (arg == 0)
            {
                side = true;
                map.Add("production", 9000);
                map.Add("manpower", 0);
                map.Add("warwidth", 0);
                map.Add("organize", 0);
            }
            else
            {
                side = false;
                for(int i=1;i<=12;i++)
                    armys[i] = preset[1][i];
                map.Add("manpower", mancalc());
                map.Add("organize", orgcalc());
            }
        }

        public Entity(Entity backup,int arg)
        {
            map=new Dictionary<string, int>();
            armys=new double[13];
            for (int i = 1; i <= 12; i++)
                armys[i] = backup.getArmy(i);
            side = backup.getSide() == 0 ? true : false;
            if(arg == 0)
            {
                map.Add("production", backup.getAttribute("production"));
                map.Add("manpower", backup.getAttribute("manpower"));
                map.Add("warwidth", backup.getAttribute("warwidth"));
                map.Add("organize", backup.getAttribute("organize"));
            }
            else
            {
                map.Add("manpower", backup.getAttribute("manpower"));
                map.Add("organize", backup.getAttribute("organize"));
            }
        }

        public Entity(Entity backup)
        {
            map = new Dictionary<string, int>();
            armys = new double[13];
            for (int i = 1; i <= 12; i++)
                armys[i] = backup.getArmy(i);
            side = backup.getSide() == 0 ? true : false;
            map.Add("manpower", backup.getAttribute("manpower"));
            map.Add("organize", backup.getAttribute("organize"));
        }

        //get&set
        public void swapSide()
        {
            side = side == false ? true:false;
        }
        public int getSide()
        {
            return side ? 0 : 1;
        }
        public void setEnemy(int arg)
        {
            for (int i = 1; i <= 12; i++)
                armys[i] = preset[arg][i];
            map["manpower"] = mancalc();
            map["organize"] = orgcalc();
        }
        public void setArmy(int arg, double num)
        {
            double temp = armys[arg]; armys[arg] = num;
            if (prdcalc() < 0)
            {
                armys[arg] = temp;
                throw new LowProductionException("You cannot exceed your production limit");
            }
            if (num < 0)
            {
                armys[arg] = temp;
                throw new BelowZeroException("The number of this unit cannot be under zero");
            }
            if (wwtcalc() > 42)
            {
                armys[arg] = temp;
                throw new WideExceedException("The war width cannot be over 42");
            }
            map["production"] = prdcalc();
            map["manpower"] = mancalc();
            map["organize"] = orgcalc();
            map["warwidth"] = wwtcalc();
        }
        public double getArmy(string arg)
        {
            if(arg == "inf"||arg == "a") return armys[3];
            else if(arg == "hel" || arg == "b") return armys[4];
            else if (arg == "mec" || arg == "c") return armys[5];
            else if (arg == "can" || arg == "d") return armys[6];
            else if (arg == "spa" || arg == "e") return armys[7];
            else if (arg == "aa" || arg == "f") return armys[8];
            else if (arg == "aam" || arg == "g") return armys[9];
            else if (arg == "at" || arg == "h") return armys[10];
            else if (arg == "sat" || arg == "i") return armys[11];
            else if (arg == "pan" || arg == "j") return armys[12];
            else if (arg == "fig" || arg == "k") return armys[1];
            else if (arg == "atc" || arg == "l") return armys[2];
            else throw new WrongChoseException("what the fuck man?");
        }

        public void setArmy(string arg,double value)
        {
            if (arg == "inf" || arg == "a") armys[3] = value;
            else if (arg == "hel" || arg == "b") armys[4] = value;
            else if (arg == "mec" || arg == "c") armys[5] = value;
            else if (arg == "can" || arg == "d") armys[6] = value;
            else if (arg == "spa" || arg == "e") armys[7] = value;
            else if (arg == "aa" || arg == "f") armys[8] = value;
            else if (arg == "aam" || arg == "g") armys[9] = value;
            else if (arg == "at" || arg == "h") armys[10] = value;
            else if (arg == "sat" || arg == "i") armys[11] = value;
            else if (arg == "pan" || arg == "j") armys[12] = value;
            else if (arg == "fig" || arg == "k") armys[1] = value;
            else if (arg == "atc" || arg == "l") armys[2] = value;
            else throw new WrongChoseException("what the fuck man?");
        }

        public double getArmy(int arg)
        {
            return armys[arg];
        }
        public int getAttribute(string str)
        {
            return map[str];
        }

        public void setAttribute(string str,int value)
        {
            map[str] = value>=0?value:0;
        }

        //calc
        private int prdcalc()
        {
            double num = 0;
            for (int i = 1; i <= 12; i++)
                num += armys[i] * armys_cost[i];
            return (int)Math.Round(9000-num);
        }
        private int mancalc()
        {
            double num = 0;
            for (int i = 1; i <= 12; i++)
                num += armys[i] * armys_manpower[i];
            return (int)Math.Round(num);
        }
        private int wwtcalc()
        {
            double num = 0;
            for (int i = 1; i <= 12; i++)
                num += armys[i] * armys_warwidth[i];
            return (int)Math.Round(num);
        }
        private int orgcalc()
        {
            double num = 0, tot = 0;
            for (int i = 3; i <= 12; i++)
            {
                num += armys[i] * armys_organize[i];
                tot += armys[i];
            }
            if (tot == 0)
                return 0;
            return (int)Math.Round(num / tot);

        }
    }

}