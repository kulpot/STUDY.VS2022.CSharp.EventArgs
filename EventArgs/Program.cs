using System;

//ref link:https://www.youtube.com/watch?v=NJLOnRzTPFo&list=PLAE7FECFFFCBE1A54&index=18
// EventHandler delegate type with the sender argument.

public enum CowState
{
    Awake,
    Sleeping,
    Dead,
}

class CowTippedEventArgs : EventArgs // Inherit from EventArgs
{
    public CowState CurrentCowState { get; private set; }
    public CowTippedEventArgs(CowState currentState) 
    { 
        CurrentCowState = currentState; 
    }
}

class Cow
{
    public string Name { get; set; }
    public event EventHandler<CowTippedEventArgs> Moo;
    public void BeTippedOver()
    {
        // Logic..
        if (Moo != null)
            //Moo(this, EventArgs.Empty);
            //Moo(this, new CowTippedEventArgs(CowState.Awake));
            Moo(this, new CowTippedEventArgs(CowState.Dead));
    }
}

class MainClass
{
    static void Main()
    {
        Cow c1 = new Cow { Name = "Betsy" };
        c1.Moo += giggle;
        Cow c2 = new Cow { Name = "Georgy" };
        c2.Moo += giggle;
        Cow victim = new Random().Next() % 2 == 0 ? c1 : c2; // make random boolean (Random().Next() & 2 == 0, if its true ref c1 otherwise ref c2
        victim.BeTippedOver();
    }
    //static void giggle(object sender, EventArgs e) // variants covariants
    static void giggle(object sender, CowTippedEventArgs e)
    {
        Cow c = sender as Cow;
        Console.WriteLine("Giggle giggle... We made " +
            c.Name + " moo!");
        switch (e.CurrentCowState)
        {
            case CowState.Awake:
                Console.WriteLine("Run!");
                break;
            case CowState.Sleeping:
                Console.WriteLine("Tickle it");
                break;
            case CowState.Dead:
                Console.WriteLine("Butcher it");
                break;
        }
    }
}