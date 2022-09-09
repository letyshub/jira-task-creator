using System.Diagnostics.CodeAnalysis;
using System.Text;

public class TextParserService : ITextParserService
{
    public string GetHtmlTitle([NotNull] string body)
    {
        var startIndex = body.IndexOf("<title", StringComparison.OrdinalIgnoreCase);
        var endIndex = body.IndexOf("</title>", StringComparison.OrdinalIgnoreCase);

        if (startIndex == -1 || endIndex == -1)
        {
            throw new Exception("Missing webpage's title");
        }

        var sb = new StringBuilder();
        var wasCloseTagChar = false;

        for (var i = startIndex + 6; i < endIndex; i++)
        {
            if (body[i] == '>' && !wasCloseTagChar)
            {
                wasCloseTagChar = true;
                continue;
            }
            else if (wasCloseTagChar)
            {
                sb.Append(body[i]);
            }
        }

        if (sb.Length == 0)
        {
            throw new Exception("Missing webpage's title");
        }

        return sb.ToString();
    }
}