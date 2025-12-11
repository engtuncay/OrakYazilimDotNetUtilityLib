using System.Text;

namespace OrakYazilimLib.Util.core
{
  public static class FiCrypto
  {
    public static string ComputeMd5Hash(string input)
    {
      using (var md5 = System.Security.Cryptography.MD5.Create())
      {
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        byte[] hashBytes = md5.ComputeHash(inputBytes);
        StringBuilder sb = new StringBuilder();
        foreach (byte b in hashBytes)
        {
          sb.Append(b.ToString("x2"));
        }
        return sb.ToString();
      }
    }
  }
}