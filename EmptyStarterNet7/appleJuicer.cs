namespace ApplePicker
{
public class appleJuicer {

    public void juiceApples(Orchard orchard) {
        List<apple> apples = new();

            for (int i = 0; i <= orchard.applesPicked.Count/Program.juiceThread; i++)
            {
                apples.Add(new apple());

                if (apples.Count == Program.loadCapacity)
                {
                    lock (orchard.applesJuiced)
                    {
                        foreach (apple t1 in apples)
                        {
                            orchard.applesJuiced.Push(t1);
                        }
                    }
                    apples.Clear();
                }
            }
            System.Console.WriteLine("Done processing " + orchard.applesJuiced + " apples");
    }
}
}