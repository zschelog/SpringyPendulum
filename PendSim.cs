//============================================================================
// PendSim.cs : Defines derived class for simulating a simple pendulum.
//============================================================================
using System;

public class PendSim : Simulator
{
    double L;      // pendulum length

    public PendSim() : base(2)
    {
        L = 0.9;

        x[0] = 1.0;   // default pendulum angle
        x[1] = 0.0;   // default rotation rate

        SetRHSFunc(RHSFuncPendulum);
    }

    //----------------------------------------------------
    // RHSFuncPendulum
    //----------------------------------------------------
    private void RHSFuncPendulum(double[] xx,
        double t, double[] ff)
    {
        ff[0] = xx[1];
        ff[1] = -g/L * Math.Sin(xx[0]);
    }

    //--------------------------------------------------------------------
    // Getters
    //--------------------------------------------------------------------
    public double Angle
    {
        get{
            return(x[0]);
        }

        set{
            x[0] = value;
        }
    }
}