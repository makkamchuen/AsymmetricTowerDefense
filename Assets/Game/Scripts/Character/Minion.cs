public class Minion : AI
{
    public Mover mover;
    public PatrolPattern PatrolPattern;

    private void Start()
    {
        mover = GetComponent<Mover>();
    }
}
