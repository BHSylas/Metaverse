using System.Collections.Generic;

public static class DialogStorage
{
    private static Dictionary<string, List<Dialog>> dict
        = new Dictionary<string, List<Dialog>>();

    public static void Store(Dialog[] dialogs)
    {
        dict.Clear();

        foreach (var d in dialogs)
        {
            if (!dict.ContainsKey(d.place))
                dict[d.place] = new List<Dialog>();

            dict[d.place].Add(d);
        }
    }

    public static List<Dialog> GetByPlace(string place)
    {
        return dict.ContainsKey(place)
            ? dict[place]
            : new List<Dialog>();
    }
}