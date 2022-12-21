namespace MediatorDesignPattern.WithMediator.WithPerson;



public interface FacebookGroupMediator
{
    void SendMessage(string msg, User user);
    void SendMessageForPerson(string msg, User user);
    void RegisterUser(User user);
}




public abstract class User
{
    protected FacebookGroupMediator mediator;
    protected string name;


    public User(FacebookGroupMediator mediator, string name)
    {
        this.mediator = mediator;
        this.name = name;
    }

    public abstract void Send(string message);
    public abstract void SendPerson(string message, User user);
    public abstract void Receive(string message);
}



public class ConcreteUser : User
{
    public ConcreteUser(FacebookGroupMediator mediator, string name)
        : base(mediator, name) { }



    public override void Receive(string message)
    {
        Console.WriteLine(name + ": Received Message: " + message);
    }

    public override void Send(string message)
    {
        Console.WriteLine(name + ": Sending Message: " + message + "\n");
        mediator.SendMessage(message, this);
    }

    public override void SendPerson(string message, User user)
    {
        Console.WriteLine(name + ": Sending Message: " + message + "\n");
        mediator.SendMessageForPerson(message, user);
    }
}



public class ConcreteFacebookGroupMediator : FacebookGroupMediator
{
    private List<User> usersList = new List<User>();

    public void RegisterUser(User user)
    {
        usersList.Add(user);
    }

    public void SendMessage(string message, User user)
    {
        foreach (var u in usersList)
        {
            // message should not be received by the user sending it
            if (u != user)
            {
                u.Receive(message);
            }
        }
    }

    public void SendMessageForPerson(string message, User user)
    {
        foreach (var u in usersList)
        {
            // message should not be received by the user sending it
            if (u == user)
            {
                u.Receive(message);
            }
        }
    }
}






class Program
{
    static void Main2()
    {
        FacebookGroupMediator facebookMediator = new ConcreteFacebookGroupMediator();

        User Pam = new ConcreteUser(facebookMediator, "Pam    ");
        User Sam = new ConcreteUser(facebookMediator, "Sam    ");
        User Ram = new ConcreteUser(facebookMediator, "Ram    ");
        User Dave = new ConcreteUser(facebookMediator, "Dave   ");
        User John = new ConcreteUser(facebookMediator, "John   ");
        User Smith = new ConcreteUser(facebookMediator, "Smith  ");
        User Rajesh = new ConcreteUser(facebookMediator, "Rajesh ");
        User Anurag = new ConcreteUser(facebookMediator, "Anurag ");

        facebookMediator.RegisterUser(Ram);
        facebookMediator.RegisterUser(Dave);
        facebookMediator.RegisterUser(Smith);
        facebookMediator.RegisterUser(Rajesh);
        facebookMediator.RegisterUser(Sam);
        facebookMediator.RegisterUser(Pam);
        facebookMediator.RegisterUser(Anurag);
        facebookMediator.RegisterUser(John);

        Dave.Send("The situation in Africa is a bomb");

        Console.WriteLine();

        Rajesh.Send("It is the same in Azerbaijan");

        Console.WriteLine();

        Dave.SendPerson("Hello", Pam);
        Console.Read();

    }
}