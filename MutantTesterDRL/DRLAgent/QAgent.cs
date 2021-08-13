using ConvnetSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace DeepQLearning.DRLAgent
{
    [Serializable]
    public struct Intersect
    {
        public double ua;
        public double ub;
        public Vec up;
        public int type;
        public bool intersect;
    };

    // A mutation operation, which consists of a combination of mutation operators and occurence count
    public struct Operation
    {
        public Operation(string combination, int occurence)
        {
            Combination = combination;
            Occurence = occurence;
        }

        public string Combination { get; set; }
        public int Occurence { get; set; }

        public override string ToString() => $"({Combination}, {Occurence})";
    }

    // A 2D vector utility
    [Serializable]
    public class Vec
    {
        public double x, y;

        public Vec (double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        // utilities
        public double dist_from(Vec v) { return Math.Sqrt(Math.Pow(this.x - v.x, 2) + Math.Pow(this.y - v.y, 2)); }
        public double length() { return Math.Sqrt(Math.Pow(this.x, 2) + Math.Pow(this.y, 2)); }

        // new vector returning operations
        public Vec add(Vec v) { return new Vec(this.x + v.x, this.y + v.y); }
        public Vec sub(Vec v) { return new Vec(this.x - v.x, this.y - v.y); }
        public Vec rotate(double a)
        {  // CLOCKWISE
            return new Vec(this.x * Math.Cos(a) + this.y * Math.Sin(a),
                           -this.x * Math.Sin(a) + this.y * Math.Cos(a));
        }
      
        // in place operations
        public void scale(double s) { this.x *= s; this.y *= s; }
        public void normalize() { var d = this.length(); this.scale(1.0 / d); }
    }

    // Wall is made up of two points
    [Serializable]
    public class Wall
    {
        public Vec p1, p2;

        public Wall(Vec p1, Vec p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }
    }

    // Eye sensor has a maximum range and senses walls
    [Serializable]
    public class Eye
    {
        public double angle;
        public double max_range;
        public double sensed_proximity;
        public int sensed_type;

        public Eye(double angle)
        {
            this.angle = angle; // angle of the eye relative to the agent
            this.max_range = 85; // maximum proximity range
            this.sensed_proximity = 85; // proximity of what the eye is seeing. will be set in world.tick()
            this.sensed_type = -1; // what type of object does the eye see?
        }
    }

    // item is circle thing on the floor that agent can interact with (see or eat, etc)
    [Serializable]
    public class Item
    {
        public Vec p;
        public int type;
        public double rad;
        public int age;
        public bool cleanup_;

        public Item(double x, double y, int type)
        {
            this.p = new Vec(x, y); // position
            this.type = type;
            this.rad = 10; // default radius
            this.age = 0;
            this.cleanup_ = false;
        }
    }

    // A single agent
    [Serializable]
    public class Agent
    {
        public List<Eye> eyes;
        //public List<double[]> actions;
        public double angle, oangle, reward_bonus, digestion_signal;
        public double rad, rot1, rot2, prevactionix;
        public Vec p, op;
        public int actionix;
        public DeepQLearn brain;
        public string instanceNumber;

        // added for concurrency and metrics
        //public DeepQLearnSharedSingleton brainShared;
        //public DeepQLearnShared brainShared;
        public int processed_item_reward_count = 0, processed_item_punishment_count = 0, totalMutants = 0, killedMutants=0;
        public double reward = 0, score = 0, maxFactor = 0, minFactor = 0;
        public string category;
        public string mutation_operator;
        public string solution;
        public string source;
        bool random, obstruct, infinite;

        //const int num_mutation_operators = 8;
        //public Operation[] mutation_bit_operations = new Operation[num_mutation_operators];
        List<Operation> mutation_bit_operations = new List<Operation>();


        public Agent(DeepQLearn brain, string category, int numOperators, string solution, string source, double maxFactor, double minFactor, string instanceNumber, bool random, bool obstruct, bool infinite)
        {
            this.brain = brain;
            this.instanceNumber = instanceNumber;
            this.category = category;
            this.solution = solution;
            this.source = source;
            this.maxFactor = maxFactor;
            this.minFactor = minFactor;
            this.random = random;
            this.obstruct = obstruct;
            this.infinite = infinite;

            // positional information
            this.p = new Vec(50, 50);
            this.op = this.p; // old position
            this.angle = 0; // direction facing

            // possible actions
            //this.actions = new List<double[]>();
            ////this.actions.Add(new double[] { 1, 1 }); // straight
            //this.actions.Add(new double[] { 0.8, 1 }); // soft left or addToMul
            //this.actions.Add(new double[] { 1, 0.8 }); // soft right or addToSub
            //this.actions.Add(new double[] { 0.5, 0 }); // hard right or addToDiv
            //this.actions.Add(new double[] { 0, 0.5 }); // hard left or addToRem
            //this.actions.Add(new double[] { 0.8, 1 }); // soft left or addToMul
            //this.actions.Add(new double[] { 1, 0.8 }); // soft right or addToSub
            //this.actions.Add(new double[] { 0.5, 0 }); // hard right or addToDiv
            //this.actions.Add(new double[] { 0, 0.5 }); // hard left or addToRem
            //this.actions.Add(new double[] { 0.8, 1 }); // soft left or addToMul
            //this.actions.Add(new double[] { 1, 0.8 }); // soft right or addToSub
            //this.actions.Add(new double[] { 0.5, 0 }); // hard right or addToDiv
            //this.actions.Add(new double[] { 0, 0.5 }); // hard left or addToRem
            //this.actions.Add(new double[] { 0.8, 1 }); // soft left or addToMul
            //this.actions.Add(new double[] { 1, 0.8 }); // soft right or addToSub
            //this.actions.Add(new double[] { 0.5, 0 }); // hard right or addToDiv
            //this.actions.Add(new double[] { 0, 0.5 }); // hard left or addToRem

            int count = 0;
            int[] arr = new int[numOperators];
            generateAllBinaryStrings(mutation_bit_operations, numOperators, arr, 0);
            //for (int i = 0; i <= 10; i = i + 2)
            //{
            //    //0001,0010,0100,1000,
            //    //1,2,4,8,16,32,64,128

            //    Console.WriteLine(i);
            //}
            //int value = 8;
            //string binary = Convert.ToString(value, 2);


            // properties
            this.rad = 10;
            this.eyes = new List<Eye>();
            //for (var k = 0; k < 9; k++) { this.eyes.Add(new Eye((k - 3) * 0.25)); }
            this.eyes.Add(new Eye(1));

            this.reward_bonus = 0.0;
            this.digestion_signal = 0.0;

            // outputs on world
            this.rot1 = 0.0; // rotation speed of 1st wheel
            this.rot2 = 0.0; // rotation speed of 2nd wheel

            this.prevactionix = -1;
        }




        // Function to string the output 
        static string stringTheArray(int[] arr, int n)
        {
            string result = "";
            for (int i = 0; i < n; i++)
            {
                result += arr[i].ToString();
            }
            return result;
        }

        // Function to generate all binary strings of n bits
        static void generateAllBinaryStrings(List<Operation> mbo, int n, int[] arr, int i)
        {
            if (i == n)
            {
                Operation op = new Operation();
                op.Combination = stringTheArray(arr, n);
                op.Occurence = 0;
                mbo.Add(op);
                return;
            }

            // First assign "0" at ith position 
            // and try for all other permutations 
            // for remaining positions 
            arr[i] = 0;
            generateAllBinaryStrings(mbo, n, arr, i + 1);

            // And then assign "1" at ith position 
            // and try for all other permutations 
            // for remaining positions 
            arr[i] = 1;
            generateAllBinaryStrings(mbo, n, arr, i + 1);
        }

        public void forward()
        {

            // in forward pass the agent simply behaves in the environment
            // create input to brain
            var num_eyes = this.eyes.Count;
            var input_array = new double[num_eyes * 3];
            for (var i = 0; i < num_eyes; i++)
            {
                var e = this.eyes[i];
                input_array[i * 3] = 1.0;
                input_array[i * 3 + 1] = 1.0;
                input_array[i * 3 + 2] = 1.0;
                if (e.sensed_type != -1)
                {
                    // sensed_type is 0 for wall, 1 for survived and 2 for killed.
                    // 1-of-k encoding into the input array, normalize to [0,1]
                    input_array[i * 3 + e.sensed_type] = e.sensed_proximity / e.max_range;
                }
            }

            Volume input = new Volume(num_eyes, 3, 1);
            input.w = input_array;

            // get action from brain
            var actionix = this.brain.forward(input);
            //var action = this.actions[actionix];
            this.actionix = actionix; //back this up

            // demultiplex into behavior variables
            //this.rot1 = action[0] * 1;
            //this.rot2 = action[1] * 1;

            //this.rot1 = 0;
            //this.rot2 = 0;
        }

        public void backward()
        {
            //// in backward pass agent learns.
            //// compute reward 
            //var proximity_reward = 0.0;
            //var num_eyes = this.eyes.Count;
            //for (var i = 0; i < num_eyes; i++)
            //{
            //    var e = this.eyes[i];
            //    // agents dont like to see walls, especially up close
            //    proximity_reward += e.sensed_type == 0 ? e.sensed_proximity / e.max_range : 1.0;
            //}
            //proximity_reward = proximity_reward / num_eyes;
            //proximity_reward = Math.Min(1.0, proximity_reward * 2);

            //// agents like to go straight forward
            //var forward_reward = 0.0;
            ////if (this.actionix == 0 && proximity_reward > 0.75)
            ////    forward_reward = 0.1 * proximity_reward;
            //// TODO: agents dont like to spin in circles
            //if (this.actionix == 0)
            //    forward_reward = 0.1 * proximity_reward;

            //// agents like to eat good things
            //var digestion_reward = this.digestion_signal;
            //this.digestion_signal = 0.0;

            //var reward = proximity_reward + forward_reward + digestion_reward;

            int combo;
            // when infinite, always use the all mutations combination (i.e. 1's)
            if (infinite)
            {
                int allCombo = mutation_bit_operations.Count - 1;
                combo = allCombo;
                mutation_operator = mutation_bit_operations[allCombo].Combination;
            }
            // when random, choose mutation combination randomly, not using reinforcement learning
            else if (random)
            {
                Util util = new Util();
                // MAS - 6/1/2021
                //int rndCombo = util.randi(0, mutation_bit_operations.Count - 1);
                //https://docs.microsoft.com/en-us/dotnet/api/system.random.next?view=net-5.0
                //A 32-bit signed integer that is greater than or equal to 0 and less than MaxValue.
                int rndCombo = util.randi(0, mutation_bit_operations.Count);
                combo = rndCombo;
                mutation_operator = mutation_bit_operations[rndCombo].Combination;
            }
            // else determine mutation combination based on reinforcement learning
            else
            {
                combo = this.actionix;
                mutation_operator = mutation_bit_operations[combo].Combination;
            }

            // increment the occurence count
            Operation t = mutation_bit_operations[combo];
            t.Occurence++;
            mutation_bit_operations[combo] = t;

            // create process to determine reward
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "..\\..\\..\\MutantTestCmdLine\\bin\\Release\\mutanttesting.exe";
            //info.FileName = "C:\\data\\MyPhD\\doctoral-research\\winter2021\\code-MT\\Mutation-Testing\\Source\\slnMutantTesting\\MutantTestCmdLine\\bin\\Debug\\mutanttesting.exe";
            //info.Arguments = "-solution=C:\\data\\MyPhD\\doctoral-research\\winter2021\\code-MT\\source\\FindMax\\FindMax.sln -source=FindMax.dll -operators=" + operators + " > C:\\data\\MyPhD\\doctoral-research\\winter2021\\code-MT\\Mutation-Testing\\Source\\slnMutantTesting\\MutantTestCmdLine\\bin\\Debug\\mutanttesting.txt";
            //info.Arguments = "-solution=C:\\data\\MyPhD\\operating-systems\\code\\ConsoleAppNUnit\\ConsoleApp1.sln -source=ClassLibrary1.dll -operators=" + mutation_operator + " > C:\\data\\MyPhD\\doctoral-research\\winter2021\\code-MT\\Mutation-Testing\\Source\\slnMutantTesting\\MutantTestCmdLine\\bin\\Debug\\mutanttesting.txt";
            //info.Arguments = "-solution=C:\\data\\MyPhD\\operating-systems\\code\\ConsoleAppNUnit\\ConsoleApp1.sln -source=ClassLibrary1.dll -operators=" + mutation_operator + " -agentNumber=" + this.instanceNumber.ToString();
            //info.Arguments = "-solution=C:\\data\\MyPhD\\doctoral-research\\winter2021\\code-MT\\source\\BasicMath2\\TestExample.sln -source=ClassLibrary1.dll -category=" + this.category + " -operators=" + mutation_operator + " -agentNumber=" + this.instanceNumber.ToString();
            info.Arguments = "-solution=" + this.solution + " -source=" + this.source + " -category=" + this.category + " -operators=" + mutation_operator + " -agentNumber=" + this.instanceNumber.ToString();
            info.UseShellExecute = false;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
            var p = Process.Start(info);
            p.WaitForExit(300000); // timeout after 5 minutes (i.e. 300,000 milliseconds)

            // output from mutation testing
            string o = p.StandardOutput.ReadToEnd();

            // parse output
            int start = 0, end = 0;

            // parse mutation score from output
            start = o.IndexOf("Score:") + 7;
            end = o.IndexOf("\r\n",start);
            if (start != -1) Double.TryParse(o.Substring(start, end - start), out this.score);

            // parse killed mutants from output
            start = o.IndexOf("Killed Mutants:") + 16;
            end = o.IndexOf("\r\n", start);
            if (start != -1) Int32.TryParse(o.Substring(start, end - start), out this.killedMutants);

            // parse total mutants from output
            start = o.IndexOf("Total Mutants:") + 15;
            end = o.IndexOf("\r\n", start);
            if (start != -1) Int32.TryParse(o.Substring(start, end - start), out this.totalMutants);


            //// define reward as the 1 - mutation score, with excluding no mutations (ie. 0000 or all 0's)
            //// which looks to find the most mutation operators that result in live mutations (ie. invalid test cases)
            ////this.reward = mutation_operator == "0000" ? 0 : 1 - this.score;
            ////this.reward = mutation_operator.Trim('0')=="" ? 0 : 1 - this.score;
            //double score_reward, multiple_reward = 0.0;
            //score_reward = 1 - this.score;
            //multiple_reward = 0.01 * (mutation_operator.Split('1').Length - 1);
            //this.reward = mutation_operator.Trim('0')=="" ? 0.05 : score_reward + multiple_reward;

            // define reward = min_reward or (score_reward + max_reward)
            // score_reward is 1 - mutation score
            // max_reward is # operations x max_factor when score_reward != 0
            // which looks to maximize mutation operators that result in live mutations (ie. invalid test cases)
            // only choosing no mutations (ie. 0000 or all 0's) using min_reward when all mutations are killed.
            double score_reward, max_reward, min_reward = 0.0;
            score_reward = 1 - this.score;
            max_reward = score_reward == 0.0 ? 0.0 : maxFactor * (mutation_operator.Split('1').Length - 1);
            min_reward = (1 / (double)mutation_operator.Length) * minFactor;
            this.reward = mutation_operator.Trim('0') == "" ? min_reward : score_reward + max_reward;

            // pass to brain for learning
            this.brain.backward(reward);
        }
        
        public string visSelf()
        {
            string output;
            output =
                "Current Mutation Category: " + this.category + Environment.NewLine +
                "Current Mutation Operator: " + this.mutation_operator + Environment.NewLine +
                "Current Total Mutants Count: " + this.totalMutants + Environment.NewLine +
                "Current Killed Mutants Count: " + this.killedMutants + Environment.NewLine +
                "Current Live Mutants Count: " + (this.totalMutants - this.killedMutants).ToString() + Environment.NewLine +
                "Current Mutation Score: " + this.score + Environment.NewLine +
                "Current Reward: " + this.reward + Environment.NewLine + Environment.NewLine +
                "Actions:" + Environment.NewLine + "(combination,count) " + Environment.NewLine +
                string.Join(", ", mutation_bit_operations) + Environment.NewLine;

            return output;
        }
    }

    // World object contains many agents and walls and food and stuff
    [Serializable]
    public class World
    {
        Util util;

        int W, H;
        public int clock;
        public int num_items;
        public string category;

        public List<Wall> walls;
        public List<Item> items;
        public List<Agent> agents;

        List<Intersect> collpoints;

        public World(DeepQLearn brain, int canvas_Width, int canvas_Height, string category, int num_items, string solution, string source, double maxFactor, double minFactor, bool random, bool obstruct, bool infinite, string instanceNumber)
        {
            this.agents = new List<Agent>();
            this.W = canvas_Width;
            this.H = canvas_Height;
            this.num_items = num_items;
            this.category = category;

            this.util = new Util();
            this.clock = 0;

            //// set up walls in the world
            //this.walls = new List<Wall>();
            //var pad = 10;
            ////outer walls
            //util_add_box(this.walls, pad, pad, this.W - pad * 2, this.H - pad * 2);
            //// inner walls
            //if (obstruct)
            //{
            //    util_add_box(this.walls, 100, 100, 200, 300);
            //    this.walls.RemoveAt(walls.Count - 1);
            //    util_add_box(this.walls, 400, 100, 200, 300);
            //    this.walls.RemoveAt(walls.Count - 1);
            //}

            // set up food/fail and poison/pass test cases
            //this.items = new List<Item>();
            //for (var k = 0; k < num_items; k++)
            //{
            //    double x = 0, y = 0;
            //    int t = 0;
            //    if (random)
            //    {
            //        // define random based objects
            //        x = util.randf(20, this.W - 20);
            //        y = util.randf(20, this.H - 20);
            //        t = util.randi(1, 3); // food/fail or poison/pass (1 and 2)
            //    }
            //    else
            //    {
            //        // define policy based objects
            //        x = (this.W / 35) * (k + 1) + ((k % 2 == 0) ? 50 : -50);
            //        y = (this.H / 35) * (k + 1);
            //        t = (k % 2 == 0) ? 1 : 2; // food/fail or poison/pass (1 and 2)
            //    }

            //    // add objects to environment
            //    var it = new Item(x, y, t);
            //    this.items.Add(it);
            //}

            // set up food and poison
            this.agents = new List<Agent>();
            this.agents.Add(new Agent(brain, category, num_items, solution, source, maxFactor, minFactor, instanceNumber, random, obstruct, infinite));
        }

        //private void util_add_box(List<Wall> lst, double x, double y, double w, double h)
        //{
        //    lst.Add(new Wall(new Vec(x, y), new Vec(x + w, y)));
        //    lst.Add(new Wall(new Vec(x + w, y), new Vec(x + w, y + h)));
        //    lst.Add(new Wall(new Vec(x + w, y + h), new Vec(x, y + h)));
        //    lst.Add(new Wall(new Vec(x, y + h), new Vec(x, y)));
        //}

        // helper function to get closest colliding walls/items
        //public Intersect stuff_collide_(Vec p1, Vec p2, bool check_walls, bool check_items)
        //{
        //    Intersect minres = new Intersect() { intersect = false };

        //    // collide with walls
        //    if (check_walls)
        //    {
        //        for (int i = 0, n = this.walls.Count; i < n; i++)
        //        {
        //            var wall = this.walls[i];
        //            var res = line_intersect(p1, p2, wall.p1, wall.p2);
        //            if (res.intersect)
        //            {
        //                res.type = 0; // 0 is wall
        //                if (!minres.intersect)
        //                {
        //                    minres = res;
        //                }
        //                else
        //                {   // check if its closer
        //                    if (res.ua < minres.ua)
        //                    {
        //                        // if yes replace it
        //                        minres = res;
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    // collide with items
        //    if (check_items)
        //    {
        //        for (int i = 0, n = this.items.Count; i < n; i++)
        //        {
        //            var it = this.items[i];
        //            var res = line_point_intersect(p1, p2, it.p, it.rad);
        //            if (res.intersect)
        //            {
        //                res.type = it.type; // store type of item
        //                if (!minres.intersect) { minres = res; }
        //                else
        //                {   // check if its closer
        //                    if (res.ua < minres.ua)
        //                    {
        //                        // if yes replace it
        //                        minres = res;
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return minres;
        //}

        // line intersection helper function: does line segment (p1,p2) intersect segment (p3,p4) ?
        //public Intersect line_intersect(Vec p1, Vec p2, Vec p3, Vec p4)
        //{
        //    Intersect result = new Intersect() { intersect= false };

        //    var denom = (p4.y - p3.y) * (p2.x - p1.x) - (p4.x - p3.x) * (p2.y - p1.y);
        //    if (denom == 0.0) { result.intersect = false; } // parallel lines

        //    var ua = ((p4.x - p3.x) * (p1.y - p3.y) - (p4.y - p3.y) * (p1.x - p3.x)) / denom;
        //    var ub = ((p2.x - p1.x) * (p1.y - p3.y) - (p2.y - p1.y) * (p1.x - p3.x)) / denom;
        //    if (ua > 0.0 && ua < 1.0 && ub > 0.0 && ub < 1.0)
        //    {
        //        var up = new Vec(p1.x + ua * (p2.x - p1.x), p1.y + ua * (p2.y - p1.y));
        //        return new Intersect { ua = ua, ub = ub, up = up, intersect = true }; // up is intersection point
        //    }
        //    return result;
        //}
        
        //public Intersect  line_point_intersect(Vec A, Vec B, Vec C, double rad) {

        //    Intersect result = new Intersect { intersect = false };

        //    var v = new Vec(B.y-A.y,-(B.x-A.x)); // perpendicular vector
        //    var d = Math.Abs((B.x-A.x)*(A.y-C.y)-(A.x-C.x)*(B.y-A.y));
        //    d = d / v.length();
        //    if(d > rad) { return result; }
      
        //    v.normalize();
        //    v.scale(d);
        //    double ua = 0.0;
        //    var up = C.add(v);
        //    if(Math.Abs(B.x-A.x)>Math.Abs(B.y-A.y)) {
        //        ua = (up.x - A.x) / (B.x - A.x);
        //    } else {
        //        ua = (up.y - A.y) / (B.y - A.y);
        //    }
        //    if(ua>0.0 && ua<1.0) {
        //        result = new Intersect { ua = ua, up = up, intersect = true };
        //    }
        //    return result;
        //}

        private Boolean AreSimilar(double a, double b, double tolerance)
        {
            // Values are within specified tolerance of each other....
            return Math.Abs(a - b) < tolerance;
        }

        public int tick(bool infinite, bool random, double rval, double pval)
        {
            // tick the environment
            this.clock++;

            // fix input to all agents based on environment process eyes
            //this.collpoints = new List<Intersect>();
            //for (int i = 0, n = this.agents.Count; i < n; i++)
            //{
            //    var a = this.agents[i];

            //    for (int ei = 0, ne = a.eyes.Count; ei < ne; ei++)
            //    {
            //        var e = a.eyes[ei];
            //        // we have a line from p to p->eyep
            //        var eyep = new Vec(a.p.x + e.max_range * Math.Sin(a.angle + e.angle), a.p.y + e.max_range * Math.Cos(a.angle + e.angle));
            //        var res = this.stuff_collide_(a.p, eyep, true, true);

            //        if (res.intersect)
            //        {
            //            // eye collided with wall
            //            e.sensed_proximity = res.up.dist_from(a.p);
            //            e.sensed_type = res.type;
            //        }
            //        else
            //        {
            //            e.sensed_proximity = e.max_range;
            //            e.sensed_type = -1;
            //        }
            //    }
            //}

            // let the agents behave in the world based on their input
            for (int i = 0, n = this.agents.Count; i < n; i++)
            {
                this.agents[i].forward();
            }

            //// apply outputs of agents on environment
            //for (int i = 0, n = this.agents.Count; i < n; i++)
            //{
            //    var a = this.agents[i];
            //    a.op = a.p; // back up old position
            //    a.oangle = a.angle; // and angle

            //    // steer the agent according to outputs of wheel velocities
            //    var v = new Vec(0, a.rad / 2.0);
            //    v = v.rotate(a.angle + Math.PI / 2);
            //    var w1p = a.p.add(v); // positions of wheel 1 and 2
            //    var w2p = a.p.sub(v);
            //    var vv = a.p.sub(w2p);
            //    vv = vv.rotate(-a.rot1);
            //    var vv2 = a.p.sub(w1p);
            //    vv2 = vv2.rotate(a.rot2);
            //    var np = w2p.add(vv);
            //    np.scale(0.5);
            //    var np2 = w1p.add(vv2);
            //    np2.scale(0.5);
            //    a.p = np.add(np2);

            //    a.angle -= a.rot1;
            //    if (a.angle < 0) a.angle += 2 * Math.PI;
            //    a.angle += a.rot2;
            //    if (a.angle > 2 * Math.PI) a.angle -= 2 * Math.PI;

            //    // agent is trying to move from p to op. Check walls
            //    var res = this.stuff_collide_(a.op, a.p, true, false);
            //    if (res.intersect)
            //    {
            //        // wall collision! reset position
            //        a.p = a.op;
            //    }

            //    // handle boundary conditions
            //    if (a.p.x < 0) a.p.x = 0;
            //    if (a.p.x > this.W) a.p.x = this.W;
            //    if (a.p.y < 0) a.p.y = 0;
            //    if (a.p.y > this.H) a.p.y = this.H;
            //}

            //// tick all items
            //var update_items = false;
            //var reward_count = 0;
            //for (int i = 0, n = this.items.Count; i < n; i++)
            //{
            //    var it = this.items[i];
            //    it.age += 1;
            //    if (it.type == 1) reward_count++;

            //    // see if some agent gets lunch
            //    for (int j = 0, m = this.agents.Count; j < m; j++)
            //    {
            //        var a = this.agents[j];
            //        var d = a.p.dist_from(it.p);
            //        if (d < it.rad + a.rad)
            //        {
            //            // wait lets just make sure that this isn't through a wall
            //            var rescheck = this.stuff_collide_(a.p, it.p, true, false);
            //            if (!rescheck.intersect)
            //            {
            //                // ding! nom nom nom
            //                switch (it.type)
            //                {
            //                    case 1:
            //                        a.processed_item_reward_count++;
            //                        //a.digestion_signal += 5.0; // mmm delicious apple/failed test (+reward)
            //                        a.digestion_signal += rval; // mmm delicious apple/failed test (+reward)
            //                        break;
            //                    case 2:
            //                        a.processed_item_punishment_count++;
            //                        //a.digestion_signal += -6.0; // ewww poison/passed test (-punishment)
            //                        a.digestion_signal += pval; // ewww poison/passed test (-punishment)
            //                        break;
            //                    default:
            //                        break;
            //                }
            //                it.cleanup_ = true;
            //                update_items = true;
            //                break; // break out of loop, item was consumed
            //            }
            //        }
            //    }

            //    // random aging process
            //    if (random && it.age > 5000 && this.clock % 100 == 0 && util.randf(0, 1) < 0.1)
            //    {
            //        it.cleanup_ = true; // replace this one, has been around too long
            //        update_items = true;
            //    }
            //}

            //// update process
            //if (update_items)
            //{
            //    var nt = new List<Item>();
            //    for (int i = 0, n = this.items.Count; i < n; i++)
            //    {
            //        var it = this.items[i];
            //        if (!it.cleanup_) nt.Add(it);
            //    }
            //    this.items = nt; // swap
            //}

            //// policy regeneration process
            //if (infinite && !random && this.items.Count < num_items && this.clock % 10 == 0 && util.randf(0, 1) < 0.25)
            //{
            //    this.items = new List<Item>();
            //    for (var k = 0; k < num_items; k++)
            //    {
            //        // define policy based objects
            //        var x = (this.W / 35) * (k + 1) + ((k % 2 == 0) ? 50 : -50);
            //        var y = (this.H / 35) * (k + 1);
            //        var t = (k % 2 == 0) ? 1 : 2; // food/fail or poison/pass (1 and 2)

            //        // add objects to environment
            //        var it = new Item(x, y, t);
            //        this.items.Add(it);
            //    }
            //}

            //// random regeneration process
            //if (infinite && random && this.items.Count < num_items && this.clock % 10 == 0 && util.randf(0, 1) < 0.25)
            //{
            //    var newitx = util.randf(20, this.W - 20);
            //    var newity = util.randf(20, this.H - 20);
            //    var newitt = util.randi(1, 3); // food or poison (1 and 2)
            //    var newit = new Item(newitx, newity, newitt);
            //    this.items.Add(newit);
            //}

            // agents are given the opportunity to learn based on feedback of their action on environment
            for (int i = 0, n = this.agents.Count; i < n; i++)
            {
                this.agents[i].backward();
            }

            //// endless loop or still rewards remaining
            //if (infinite || reward_count > 0)
            //    return 0;
            //else
            //    // stop when all rewards have been found
            //    return 1;

            // endless loop
            return 0;
        }
    }

    [Serializable]
    public class QAgent
    {
        public int simspeed = 1;
        public int experiencesize = 0;
        public bool random, infinite;
        public string instanceNumber;
        public string category;
        public World w;

        [NonSerialized]
        Pen greenPen = new Pen(Color.LightGreen, 2);

        [NonSerialized]
        Pen redPen = new Pen(Color.Red, 2);

        [NonSerialized]
        Pen greenPen2 = new Pen(Color.LightGreen, 1);

        [NonSerialized]
        Pen redPen2 = new Pen(Color.Red, 1);

        [NonSerialized]
        Pen bluePen = new Pen(Color.Blue, 2);

        [NonSerialized]
        Pen blackPen = new Pen(Color.Black);
        
        public QAgent(DeepQLearn brain, int canvas_W, int canvas_H, string category, int num_items, string solution, string source, double maxFactor, double minFactor, bool random, bool obstruct, bool infinite, string instanceNumber)
        {
            this.random = random;
            this.infinite = infinite;
            this.instanceNumber = instanceNumber;
            this.category = category;
            this.w = new World(brain, canvas_W, canvas_H, category, num_items, solution, source, maxFactor, minFactor, random, obstruct, infinite, instanceNumber);
        }

        public void Reinitialize()
        {
            greenPen = new Pen(Color.LightGreen, 2);
            redPen = new Pen(Color.Red, 2);
            greenPen2 = new Pen(Color.LightGreen, 1);
            redPen2 = new Pen(Color.Red, 1);
            bluePen = new Pen(Color.Blue, 2);
            blackPen = new Pen(Color.Black);

            this.simspeed = 1;
            this.w.agents[0].brain.learning = false;
            this.w.agents[0].brain.epsilon_test_time = 0.01;

            this.w.agents[0].op.x = 500;
            this.w.agents[0].op.y = 500;
        }

        public int tick(double rval, double pval)
        {
            return w.tick(infinite, random, rval, pval);
        }

        // get Agent's current tick count
        public int getTickCount()
        {
            return w.clock;
        }

        // get Agent's experience count
        public int getExperienceCount()
        {
            return w.agents[0].brain.instanceExperienceCount();
        }

        // get Agent's processed item count
        public double getProcessedItemCount()
        {
            return w.agents[0].processed_item_reward_count + w.agents[0].processed_item_punishment_count;
        }

        // get Agent's total mutants count
        public double getTotalMutantsCount()
        {
            return w.agents[0].totalMutants;
        }

        // get Agent's killed mutants count
        public double getKilledMutantsCount()
        {
            return w.agents[0].killedMutants;
        }

        // get Agent's live mutants count
        public double getLiveMutantsCount()
        {
            return w.agents[0].totalMutants - w.agents[0].killedMutants;
        }

        // get Agent's current average reward
        public double getAvgReward()
        {
            return w.agents[0].brain.average_reward_window.get_average();
        }

        // get Agent's current average loss
        public double getAvgLoss()
        {
            return w.agents[0].brain.average_loss_window.get_average();
        }

        // Draw everything and return stats
        public string draw_world(Graphics g)
        {
            var agents = w.agents;

            //// draw walls in environment
            //for (int i = 0, n = w.walls.Count; i < n; i++)
            //{
            //    var q = w.walls[i];
            //    drawLine(g, q.p1, q.p2, blackPen);
            //}

            //// draw agents
            //for (int i = 0, n = agents.Count; i < n; i++)
            //{
            //    // draw agent's body
            //    var a = agents[i];
            //    drawArc(g, a.op, (int)a.rad, 0, (float)(Math.PI * 2), blackPen);

            //    // draw agent's sight
            //    for (int ei = 0, ne = a.eyes.Count; ei < ne; ei++)
            //    {
            //        var e = a.eyes[ei];
            //        var sr = e.sensed_proximity;
            //        Pen pen;

            //        if (e.sensed_type == 1) pen = redPen2;          // apples
            //        else if (e.sensed_type == 2) pen = greenPen2;   // poison
            //        else pen = blackPen;                            // wall

            //        //var new_x = a.op.x + sr * Math.Sin(radToDegree((float)a.oangle) + radToDegree((float)e.angle));
            //        //var new_y = a.op.y + sr * Math.Cos(radToDegree((float)a.oangle) + radToDegree((float)e.angle));

            //        var new_x = a.op.x + sr * Math.Sin(a.oangle + e.angle);
            //        var new_y = a.op.y + sr * Math.Cos(a.oangle + e.angle);
            //        Vec b = new Vec(new_x, new_y);

            //        drawLine(g, a.op, b, pen);
            //    }
            //}

            //// draw items
            //for (int i = 0, n = w.items.Count; i < n; i++)
            //{
            //    Pen pen = blackPen;
            //    var it = w.items[i];
            //    if (it.type == 1) pen = redPen; 
            //    if (it.type == 2) pen = greenPen;

            //    drawArc(g, it.p, (int)it.rad, 0, (float)(Math.PI * 2), pen);
            //}

            //t.Text = w.agents[0].visSelf();

            return w.agents[0].brain.visSelf() + w.agents[0].visSelf();
            //return w.agents[0].brain.visSelf();
        }

        public void gofastest()
        {
            simspeed = 10;
        }

        public void goveryfast()
        {
            simspeed = 3;
        }

        public void gofast()
        {
            simspeed = 2;
        }

        public void gonormal()
        {
            simspeed = 1;
        }

        public void goslow()
        {
            simspeed = 0;
        }

        public void startlearn()
        {
            this.w.agents[0].brain.learning = true;
        }

        public void stoplearn()
        {
            this.w.agents[0].brain.learning = false;            
        }

        private void drawCircle(Graphics g, Vec center, int radius, Pen pen)
        {
            var rect = new Rectangle((int)center.x - radius, (int)center.y - radius, radius * 2, radius * 2);
            g.DrawEllipse(pen, rect);
        }

        private void drawArc(Graphics g, Vec center, int radius, float startAngle, float sweepAngle, Pen pen)
        {
            var rect = new Rectangle((int)center.x - radius, (int)center.y - radius, radius * 2, radius * 2);
            g.DrawArc(pen, rect, radToDegree(startAngle), radToDegree(sweepAngle));
        }

        private void drawLine(Graphics g, Vec a, Vec b, Pen pen)
        {
            Point[] points =
            {
                new Point((int)a.x, (int)a.y),
                new Point((int)b.x, (int)b.y)
            };

            g.DrawLines(pen, points);
        }

        private float radToDegree(float rad)
        {
            return (float)(rad * 180 / Math.PI);
        }

    }
}
