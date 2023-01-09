using System.Security.Cryptography;
using System.Text;

namespace BlazorServerChat.Code;

public static class Gravatar
{
    static Dictionary<string, string> cache = new Dictionary<string, string>();

    public static string ImgSrc(string prezdivka)
    {
        string hash;
        prezdivka = prezdivka.ToLower();
        if (cache.ContainsKey(prezdivka))
            hash = cache[prezdivka];
        else
            using (var md5 = MD5.Create())
            {
                var prezdivkaByty = Encoding.ASCII.GetBytes(prezdivka);
                var hashByty = md5.ComputeHash(prezdivkaByty);
                hash = Convert.ToHexString(hashByty).ToLower();
                cache[prezdivka] = hash;
            }
        return $"https://www.gravatar.com/avatar/{hash}?d=robohash&s=50";
    }
}
