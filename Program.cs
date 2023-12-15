using System.IO.Pipes;
using System.Xml;


class Program
{

    static void Main()
    {
        double t = 0.0; 
        double dt = 0.02;
        double tEnd = 5.0;

        SimPendulum pend; 
        pend = new SimPendulum();

        pend.Angle = 1.1;

        while(t<tEnd)
        {
            pend.StepRK2(t,dt);

            t += dt;

            Console.WriteLine(t + "," + pend.Angle);
        }
    }
}
