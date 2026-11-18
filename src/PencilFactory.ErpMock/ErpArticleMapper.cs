using System;
using System.Collections.Generic;

namespace PencilFactory.ErpMock;

internal static class ErpArticleMapper
{
    private static readonly Dictionary<string, string> Map =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["PEN-GREEN"] = "100001",
            ["PEN-BROWN"] = "100002",
            ["PEN-BLUE"] = "100003",
        };

    public static bool TryMap(string erpArticle, out string materialNumber)
        => Map.TryGetValue(erpArticle?.Trim() ?? "", out materialNumber!);
}
