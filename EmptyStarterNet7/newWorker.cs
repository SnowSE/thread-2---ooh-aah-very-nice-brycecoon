namespace ApplePicker
{
    public class newWorker
    {

        //counter for picking the apples and loading them on the truck
        public void pickApples(Orchard orchard)
        {

            List<apple> apples = new();
            for (int i = 0; i <= (Program.applesToPick / Program.Threads); i++)
            {
                apples.Add(new apple());

                //if we hit capacity then send the truck away and start a new one
                if (apples.Count == Program.loadCapacity)
                {
                    lock (orchard.applesPicked)
                    {
                        foreach (apple t1 in apples)
                        {
                            orchard.applesPicked.Push(t1);
                        }
                    }
                    apples.Clear();
                }
            }
        }
    }
}