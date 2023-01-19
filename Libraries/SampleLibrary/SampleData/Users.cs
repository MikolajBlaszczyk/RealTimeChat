namespace SampleData;

public class Users
{
    private static string[] randomUsers = new[] {"Alison", "Slater", "Lily",	"Peake", "William",	"Randall", "Wanda",	"Lambert", "Julian", "Sanderson",
        "Stephen",	"Parsons", "Hill",  "Yvonne",	"Parsons", "Jacob",	"Sanderson", "Claire", "MacDonald", "Angela", "Anderson"};
    public static List<string> Populate(int quantity = 15)
    {
        List<string> sampleData = new();
        Random random = new();
        
        for (int i = 0; i < quantity; i++)
        {
            sampleData.Add($"{randomUsers[random.Next(0,randomUsers.Length)]}{random.Next(10000)}");
        }

        return sampleData;
    }
}