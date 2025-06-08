namespace HttpClient.RefitProxy;

public class HelperPadding
{
    public static string Pad(int val, int amountToPad)
    {
        return val.ToString("D" + amountToPad);
    }

}