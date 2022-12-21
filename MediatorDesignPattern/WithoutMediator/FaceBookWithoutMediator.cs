namespace MediatorDesignPattern.WithoutMediator;



public abstract class User
{
    protected string name;

    public User(string name)
    {
        this.name = name;
    }

    public abstract void Receive(string message);
    public abstract void Send(string message, FacebookGroup user);
}



public class ConcreteUser : User
{
    public ConcreteUser(string name) : base(name) { }

    public override void Receive(string message)
    {
        Console.WriteLine(name + ": Received Message: " + message);
    }


    public override void Send(string message, FacebookGroup group )
    {
        Console.WriteLine(name + ": Sending Message: " + message + "\n");
        group.SendMessage(message, this);
    }
}



public class FacebookGroup 
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

}




class Program
{
    static void Main()
    {
        FacebookGroup facebookGroup = new FacebookGroup();

        User Pam = new ConcreteUser("Pam    ");
        User Sam = new ConcreteUser("Sam    ");
        User Ram = new ConcreteUser("Ram    ");
        User Dave = new ConcreteUser("Dave   ");
        User John = new ConcreteUser("John   ");
        User Smith = new ConcreteUser("Smith  ");
        User Rajesh = new ConcreteUser( "Rajesh ");
        User Anurag = new ConcreteUser( "Anurag ");

        facebookGroup.RegisterUser(Ram);
        facebookGroup.RegisterUser(Pam);
        facebookGroup.RegisterUser(Sam);
        facebookGroup.RegisterUser(Dave);
        facebookGroup.RegisterUser(John);
        facebookGroup.RegisterUser(Smith);
        facebookGroup.RegisterUser(Rajesh);
        facebookGroup.RegisterUser(Anurag);


        Dave.Send("HI", facebookGroup);
        Console.WriteLine();

        Rajesh.Send("Hello", facebookGroup);
        Console.Read();

    }
}
